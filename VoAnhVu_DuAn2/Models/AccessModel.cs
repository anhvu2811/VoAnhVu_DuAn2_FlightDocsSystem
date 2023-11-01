using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoAnhVu_DuAn2.Models
{
    [Table("Access")]
    public class AccessModel
    {
        [Key]
        public string AccessId { get; set; }
        public string AccessName { get; set; }
    }
}
