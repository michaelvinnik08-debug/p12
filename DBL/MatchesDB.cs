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
    public class MatchesDB :BaseDB<Matches>
    {
        protected override string GetTableName()
        {
            return "matches";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override async Task<Matches> CreateModelAsync(object[] row)
        {
            Matches m = new Matches();
            m.id = int.Parse(row[0].ToString());
            m.inviter = int.Parse(row[1].ToString());
            m.invited=int.Parse(row[2].ToString());
            m.accapted=int.Parse(row[3].ToString());
            m.denied=int.Parse(row[4].ToString());
            m.matched=DateTime.Parse(row[5].ToString());
            return m;
            
        }
        public async Task<List<Matches>> GetMatches(Tuners T)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>();
            filter.Add("id",T.Id );
            List<Matches> m = ((List<Matches>)await SelectAllAsync(filter));
            return m;
        }
        public async Task<Matches> NewMatch(Tuners T,int id)
        {
           // string query = $@"SELECT * FROM project12.matches WHERE ({id} =user1_id && {id2} = user2_id) || ({id2} =user1_id && {id} =user2_id)";
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "inviter_id", T.Id },
                { "invited_id", id },
            };
             return (Matches)await base.InsertGetObjAsync(fillValues);

        }
    }
}
