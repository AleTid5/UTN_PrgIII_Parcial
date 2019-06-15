using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial_Tidele.Models
{
    public class Magician
    {
        public int Id { set; get; }
        public String Name { set; get; }
        public House House { set; get; }
        public List<Spell> Spells { set; get; }
    }
}
