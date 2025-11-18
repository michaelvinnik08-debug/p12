using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moduls
{
    public class Matches
    {
        public int id { get; set; }
        public int invited { get; set; }
        public int inviter { get; set; }
        public int accapted { get; set; }
        public int denied { get; set; }
        public DateTime matched { get; set; }
        public Matches() {}
        public Matches(int id, int inviter, int invited, int accapted, int denied,DateTime matched)
        {
            this.id = id;
            this.inviter = inviter;
            this.invited = invited;
            this.accapted = accapted;
            this.denied = denied;
            this.matched = matched;
        }
    }
}
