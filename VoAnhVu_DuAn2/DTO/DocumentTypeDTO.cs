using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.DTO
{
    public class DocumentTypeDTO
    {
        public string DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public GroupPermissionModel GroupPermission { get; set; }
    }
}
