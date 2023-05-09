using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task09.Entities
{
    public class DeathProbability
    {
        public Gender Gender { get; set; }
        public int Age { get; set; } //születési évet tároljuk, nem a kort, ezért szám és nem dátum
        public double P { get; set; }
    }
}
