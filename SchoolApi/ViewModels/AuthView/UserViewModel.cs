using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.ViewModels.Loggingview
{
    public class UserViewModel
    {
    public string? UserName { get; set; }
    public DateTime Expires { get; set; }
    public string? Token { get; set; }
        
    }
}