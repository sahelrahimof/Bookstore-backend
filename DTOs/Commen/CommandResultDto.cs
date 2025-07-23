using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs.Commen
{
    public class CommandResultDto
    {
        public bool  Succesfull { get; set; }
        public string? Message { get; set; }
    }
}