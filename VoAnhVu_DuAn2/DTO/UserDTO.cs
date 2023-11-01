using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.DTO
{
    public class UserDTO
    {
        public string UserId { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public RoleModel Role { get; set; }
    }
}
