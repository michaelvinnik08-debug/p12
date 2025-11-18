using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moduls
{
    public class Chats
    {
        public int id { get; set; }
        public int user1 { get; set; }
        public int user2 { get; set; }
        public DateTime last { get; set; }
        public string message { get; set; }
        public int is_read { get; set; }
        public Chats() { }
        public Chats(int id,int user1,string message, int user2)
        {
            this.id = id;
            this.user1 = user1;
            this.message = message;
            this.user2 = user2;
        }


    }
}
