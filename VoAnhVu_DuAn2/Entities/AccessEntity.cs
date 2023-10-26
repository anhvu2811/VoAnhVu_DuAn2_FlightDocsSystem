using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoAnhVu_DuAn2.Entities
{
    [Table("Access")]
    public class AccessEntity
    {
        [Key]
        public string AccessId { get; set; }
        public string AccessName { get; set; }
    }
}
