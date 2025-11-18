using Moduls;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class ReportsDB : BaseDB<Reports>
    {
        protected override string GetTableName()
        {
            return "reports";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override async Task<Reports> CreateModelAsync(object[] row)
        {

            Reports r = new Reports();
            r.id = int.Parse(row[0].ToString());
            r.reporter = int.Parse(row[1].ToString());
            r.reported = int.Parse(row[2].ToString());
            r.report = row[3].ToString();

            return r;
        }
        public async Task<Reports> InsertGetObjAsync(Reports R)
        {
            TunersDB s = new TunersDB();
            List<Tuners> t = await s.SelectUserID(R.reported);
            if (t[0].banned == 0)
            {
                Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "reporter", R.reporter },
                { "reported", R.reported },
                { "report", R.report }
            };
                t[0].rep_amount++;
                return (Reports)await base.InsertGetObjAsync(fillValues);
            }
            return null;

        }
        public async Task<List<Reports>> GetAllAsync()
         {
            string query = "SELECT * FROM project12.reports Order by reported_id";
            return ((List<Reports>)await SelectAllAsync());
        }
        
        

    }
}
