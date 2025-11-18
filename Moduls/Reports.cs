using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moduls
{
    public class Reports
    {
        public int id { get; set; }
        public string report { get; set; }
        public int reported { get; set; }
        public int reporter { get; set; }
        public int solved { get; set; }
        public Reports () { }
        public Reports(int id,string report, int reported, int reporter)
        {
            this.id = id;
            this.report = report;
            this.reported = reported;
            this.reporter = reporter;
        }
    }
}
