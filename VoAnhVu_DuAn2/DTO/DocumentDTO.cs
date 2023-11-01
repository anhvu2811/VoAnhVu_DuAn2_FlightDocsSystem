using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.DTO
{
    public class DocumentDTO
    {
        public string DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string? FileUpLoad { get; set; }
        public double Version { get; set; }
        public DateTime CreateDate { get; set; }
        public string Note { get; set; }
        public DocumentTypeModel DocumentType { get; set; }
        public FlightModel Flight { get; set; }
        public UserModel User { get; set; }
    }
}
