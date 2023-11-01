using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.DTO
{
    public class GroupPermissionDTO
    {
        public string GroupPermissionId { get; set; }
        public string GroupPermissionName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Note { get; set; }
        public AccessModel Access { get; set; }
    }
}
