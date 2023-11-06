using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoAnhVu_DuAn2.DTO
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Token { get; set; }
    }
}
