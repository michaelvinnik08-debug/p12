using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moduls
{
    public class Messages
    {
        public int id { get; set; }
        public  string text { get; set; }
        public DateTime Time { get; set; }
        public int chatid { get; set; }
        public int userid { get; set; }

        public Messages() { }
        public Messages(int id, string text, DateTime Time,int chatid,int userid)
        {
            this.id = id;
            this.text = text;
            this.Time = Time;
            this.chatid = chatid;
            this.userid = userid;
        }
    }

}
