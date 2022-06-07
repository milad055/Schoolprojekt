using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolApi.ViewModels.Loggingview;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Authorize()]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(IConfiguration config, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signManager = signInManager;
            _config = config;
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromQuery] string roleName)
        {

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (!result.Succeeded) return BadRequest(result.Errors);
                return StatusCode(201);
            }

            return BadRequest("Rollen finns redan");

        }

        [HttpPost("register")]
        public async Task<ActionResult<UserViewModel>> RegisterUser(RegisterUserViewModel model)
        {
            // Steg 1.
            // Skapa en ny IdentityUser typ
            var user = new IdentityUser
            {
                Email = model.Email!.ToLower(),
                UserName = model.Email.ToLower()
            };

            // Steg 2. Spara användaren till databasen...
            var result = await _userManager.CreateAsync(user, model.Password);

            // Steg 3. Kontrollera så att inget gick galet!
            if (result.Succeeded)
            {
                // Steg 1. Vi ska skapa en claim som heter Admin och IsAdmin är satt till true...

                if (model.IsAdmin)
                {
                    await _userManager.AddClaimAsync(user, new Claim("Admin", "true"));
                    await _userManager.AddToRoleAsync(user, "Administrators");
                }

                await _userManager.AddClaimAsync(user, new Claim("User", "true"));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id));

                var userData = new UserViewModel
                {
                    UserName = user.UserName,
                    Token = await CreateJwtToken(user)
                };

                return StatusCode(201, userData);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("User registration", error.Description);
                }
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserViewModel>> Login(LoginViewModel model)
        {

            // Steg 1. Kontrollera om användare finns i systemet...
            var user = await _userManager.FindByNameAsync(model.UserName);

            // Steg 2. Om inte kasta ett 401 meddelande
            if (user is null)
                return Unauthorized("Felaktigt användarnamn");

            // Steg 3. Försök att loggga in användaren...
            var result = await _signManager.CheckPasswordSignInAsync(user, model.Password, false);

            // Steg 4. Gick det inte skicka ett 401 meddelande tillbaka...
            if (!result.Succeeded)
                return Unauthorized();

            // Steg 5. Skapa ett user view modell objekt
            var userData = new UserViewModel
            {
                UserName = user.UserName,
                Expires = DateTime.Now.AddDays(7),
                Token = await CreateJwtToken(user)
            };

            return Ok(userData);
        }


        private async Task<string> CreateJwtToken(IdentityUser user)
        {

            // Kommer att hämtas ifrån AppSettings...
            // Ta en lång svår sträng och gör om den till en Byte Array...
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("apiKey"));

            // Hämta användarens claim ifrån identity databasen...
            var userClaims = (await _userManager.GetClaimsAsync(user)).ToList();
            var roles = await _userManager.GetRolesAsync(user);

            userClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));



            // Skapa ett nytt token...
            var jwt = new JwtSecurityToken(
                claims: userClaims,
                // notBefore: Från när skall biljetten/token vara giltig.
                // Vi kan sätta detta till en datum i framtiden om biljetten/token
                // skall skapas men inte vara giltig på en gång...
                notBefore: DateTime.Now,
                // Sätt giltighetstiden på biljetten i detta fallet en vecka.
                // OBSERVERA ATT TIDEN FÖR GILTIGHETEN BÖR LÄGGAS NÅGON ANNANSTANS...
                expires: DateTime.Now.AddDays(7),
                // Skapa en instans av SigningCredential klassen
                // som används för att skapa en hash och signering av biljetten.
                signingCredentials: new SigningCredentials(
                    // Vi använder en SymmetricSecurityKey som tar vår hemlighet
                    // som argument och sedan talar vi om vilken algoritm som skall
                    // användas för att skapa hash värdet.
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                )
            );

            // Vi använder klassen JwtSecurityTokenHandler och dess metod WriteToken för att
            // skapa en sträng av vårt token...
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

    }
}