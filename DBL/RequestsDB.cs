using Moduls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class RequestsDB :BaseDB<Requests>
    {
        protected override string GetTableName()
        {
            return "requests";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override async Task<Requests> CreateModelAsync(object[] row)
        {
            Requests m = new Requests();
            m.id = int.Parse(row[0].ToString());
            m.inviter = int.Parse(row[1].ToString());
            m.invited=int.Parse(row[2].ToString());
            m.accapted=int.Parse(row[3].ToString());
            m.denied=int.Parse(row[4].ToString());
            m.matched=DateTime.Parse(row[5].ToString());
            return m;
            
        }
        public async Task<List<Requests>> GetRequests(Tuners T)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>();
            filter.Add("id",T.Id );
            List<Requests> m = ((List<Requests>)await SelectAllAsync(filter));
            return m;
        }
        public async Task<Requests> NewRequest(int id,int id2)
        {
           
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "inviter_id", id },
                { "invited_id", id2 },
            };
            return (Requests)await base.InsertGetObjAsync(fillValues);
        }
        public async Task<Requests> GetRequestBetween2Users(int id,int id2)
        {
            string query = $@"SELECT * FROM project12.requests WHERE ({id} = inviter_id && {id2} = invited_id) ";
            List<Requests> R = ((List<Requests>)await SelectAllAsync(query));
            if(R.Count==1)
            return R[0];
            return null;

        }
        public async Task Unsend(int id,int id2)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "inviter_id", id },
                { "invited_id", id2 },
            };
            await DeleteAsync(fillValues);
        }

    }
}
