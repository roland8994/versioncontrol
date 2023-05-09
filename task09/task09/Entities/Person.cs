using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task09.Entities
{
    public class Person
    {
        public int BirthYear { get; set; } //születési évet tároljuk, nem a kort, ezért szám és nem dátum
        public Gender Gender { get; set; }
        public byte NbrOfChildren { get; set; }
        public bool IsAlive { get; set; }

        public Person()
        {
            IsAlive = true;
        }
    }
}
