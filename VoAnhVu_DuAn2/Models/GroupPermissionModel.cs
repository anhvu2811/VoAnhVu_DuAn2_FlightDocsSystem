using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;

namespace VoAnhVu_DuAn2.Models
{
    public class GroupPermissionModel
    {
        public string GroupPermissionId { get; set; }
        public string GroupPermissionName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Note { get; set; }
        public AccessEntity Access { get; set; }
    }
}
