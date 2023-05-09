using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task09.Entities
{
    public class BirthProbability
    {
        public int Age { get; set; } //születési évet tároljuk, nem a kort, ezért szám és nem dátum
        public byte NbrOfChildren { get; set; }
        public double P { get; set; }
    }
}
