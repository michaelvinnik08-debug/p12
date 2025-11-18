using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moduls
{
    public class Playbacks
    {
        public int id { get; set; }
        public int user { get; set; }
        public int genre { get; set; }
        public int playbacks { get; set; }

        public Playbacks() {}
        public Playbacks (int id,int user,int genre,int playbacks)
        {
            this.id = id;
            this.user = user;
            this.genre = genre;
            this.playbacks = playbacks;
        }
    }
}
