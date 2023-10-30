using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;

namespace VoAnhVu_DuAn2.Models
{
    public class DocumentModel
    {
        public string DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string? FileUpLoad { get; set; }
        public string Version { get; set; }
        public DateTime CreateDate { get; set; }
        public string Note { get; set; }
        public DocumentTypeEntity DocumentType { get; set; }
        public FlightEntity Flight { get; set; }
        public UserEntity User { get; set; }
    }
}
