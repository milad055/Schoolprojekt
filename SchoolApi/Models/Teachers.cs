using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.Models
{
    public class Teachers
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int TelefonNumber { get; set; }
        public string? Address { get; set; }
        public string? expertness { get; set; }
    }
}