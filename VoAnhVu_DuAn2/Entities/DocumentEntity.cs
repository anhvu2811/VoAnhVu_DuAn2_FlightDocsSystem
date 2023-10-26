using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoAnhVu_DuAn2.Entities
{
    [Table("Document")]
    public class DocumentEntity
    {
        [Key]
        public string DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string FileUpLoad { get; set; }
        public string Version { get; set; }
        public DateTime CreateDate { get; set; }
        public string Note { get; set; }
        [ForeignKey("DocumentTypeId")]
        public string DocumentTypeId { get; set; }
        [ForeignKey("FlightId")]
        public string FlightId { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public DocumentTypeEntity DocumentType { get; set; }
        public FlightEntity Flight { get; set; }
        public UserEntity User { get; set; }
    }
}
