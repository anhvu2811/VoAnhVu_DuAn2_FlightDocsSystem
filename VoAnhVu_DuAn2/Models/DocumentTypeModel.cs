using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;

namespace VoAnhVu_DuAn2.Models
{
    public class DocumentTypeModel
    {
        public string DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public GroupPermissionEntity GroupPermission { get; set; }
    }
}
