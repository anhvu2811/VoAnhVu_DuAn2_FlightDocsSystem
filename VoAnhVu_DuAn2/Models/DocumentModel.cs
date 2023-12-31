﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoAnhVu_DuAn2.Models
{
    [Table("Document")]
    public class DocumentModel
    {
        [Key]
        public string DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string? FileUpLoad { get; set; }
        public double Version { get; set; }
        public DateTime CreateDate { get; set; }
        public string Note { get; set; }
        [ForeignKey("DocumentTypeId")]
        public string? DocumentTypeId { get; set; }
        [ForeignKey("FlightId")]
        public string? FlightId { get; set; }
        [ForeignKey("UserId")]
        public string? UserId { get; set; }
        public DocumentTypeModel DocumentType { get; set; }
        public FlightModel Flight { get; set; }
        public UserModel User { get; set; }
    }
}
