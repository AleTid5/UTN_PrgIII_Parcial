using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial_Tidele.Models
{
    public class Spell
    {
        public int Id { set; get; }
        public String Name { set; get; }
        public String Description { set; get; }
        public Spell SpellDefeat { set; get; }
    }
}
