using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Entities
{
    public class Borrow
    {
         public required Book Book { get; set; }
        public required Member Member { get; set; }
        public DateTime Borrowtime { get; set; }
        public DateTime? Returntime { get; set; }
    }
}