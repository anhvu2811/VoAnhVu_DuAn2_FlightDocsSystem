using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoAnhVu_DuAn2.Entities
{
    [Table("GroupPermission")]
    public class GroupPermissionEntity
    {
        [Key]
        public string GroupPermissionId { get; set; }
        public string GroupPermissionName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Note { get; set; }
        [ForeignKey("AccessId")]
        public string AccessId { get; set; }
        public AccessEntity Access { get; set; }
    }
}
