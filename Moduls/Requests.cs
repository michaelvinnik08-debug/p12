using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moduls
{
    public class Requests
    {
        public int id { get; set; }
        public int invited { get; set; }
        public int inviter { get; set; }
        public DateTime matched { get; set; }
        public Requests() {}
        public Requests(int id, int inviter, int invited,DateTime matched)
        {
            this.id = id;
            this.inviter = inviter;
            this.invited = invited;
            this.matched = matched;
        }
    }
}
