using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moduls
{
    public class Genres
    {
        public int id { get; set; }
        public string name { get; set; }
        public Genres () { }
        public Genres (int id,string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
