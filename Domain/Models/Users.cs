using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Users
    {
        public int UsersId { get; set; }
        public string? UsersName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int Otp { get; set; }
        public string? Role { get; set; }
    }
}
