using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moduls
{
    public class Tuners
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int role { get; set; }
        public int banned { get; set; }
        public int rep_amount { get; set; }
        public DateTime created { get; set; }

        public Tuners() { }
        public Tuners(int Id, string username,string email ,string password)
        {
            this.Id = Id;
            this.username = username;
            this.password = password;
            this.email = email;
        }
    }
}
