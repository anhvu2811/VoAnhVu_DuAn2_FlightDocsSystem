using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoAnhVu_DuAn2.Models
{
    [Table("DocumentType")]
    public class DocumentTypeModel
    {
        [Key]
        public string DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        [ForeignKey("GroupPermissionId")]
        public string GroupPermissionId { get; set; }
        public GroupPermissionModel GroupPermission { get; set; }
    }
}
