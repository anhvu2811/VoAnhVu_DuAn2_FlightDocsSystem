using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoAnhVu_DuAn2.Entities
{
    [Table("User")]
    public class UserEntity
    {
        [Key]
        public string UserId { get; set; }
        public string? Avatar { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        [ForeignKey("RoleId")]
        public string? RoleId { get; set; }
        public RoleEntity? Role { get; set; }
    }
}
