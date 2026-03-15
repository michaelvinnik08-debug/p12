using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moduls;
using Mysqlx;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.X509;
namespace DBL
{
    public class TunersDB : BaseDB<Tuners>
    {
        protected override string GetTableName()
        {
            return "users";
        }
        protected override  string GetPrimaryKeyName()
        {
            return "id";
        }
        public async Task<List<Tuners>> GetAllAsync()
        {
            return ((List<Tuners>)await SelectAllAsync());
        }
        public async Task<Tuners> SelectUserID(int id)
            {
            Dictionary<string, object> filter = new Dictionary<string, object>();
            filter.Add("id", id);
           List<Tuners> T= await SelectAllAsync(filter);
            return  T[0];
        }
        public async Task<Tuners> SelectUserEmail(string email)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>();
            filter.Add("email", email);
            List<Tuners> T = await SelectAllAsync(filter);
            return T[0];
        }
        public async Task<List<Tuners>> SelectUsersByName(string name)
        {

            Dictionary<string, object> filter = new Dictionary<string, object>();
            filter.Add("name", name);
            List<Tuners> T = await SelectAllAsync(filter);
            return  T;
        }
        public async Task<Tuners> Reg(string username,string email, string password)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>();
            filter.Add("email", email);
           List<Tuners> t =await SelectAllAsync(filter);
            if (t.Count == 0)
            {
                Dictionary<string, object> fillValues = new Dictionary<string, object>()
                {
                { "name", username },
                { "email", email },
                { "password", password }
                };
                return (Tuners)await base.InsertGetObjAsync(fillValues);
            }
            else return null;
        }
        protected override async Task<Tuners> CreateModelAsync(object[] rows)
        {
            Tuners t = new Tuners();
            t.Id = int.Parse(rows[0].ToString());
            t.username = rows[1].ToString();
            t.email = rows[2].ToString();
            t.password = rows[3].ToString();
            t.role = int.Parse(rows[4].ToString());
            t.banned = int.Parse(rows[5].ToString());
            t.created = DateTime.Parse(rows[6].ToString());
            t.rep_amount = int.Parse(rows[7].ToString());
            return t;
        }
        public async Task<int> UpdateAsync(Tuners T,string username,string password )
        {
                Dictionary<string, object> filter = new Dictionary<string, object>();
                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("name", username);
                values.Add("password", password);
                filter.Add("id", T.Id.ToString());
            return await base.UpdateAsync( values,filter);
        }
        public async Task<int> Bannedornot(int ban, int Id)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>();
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("banned", ban);
            filter.Add("id", Id);
            return await base.UpdateAsync(filter, values); 
        }
        public async Task<Tuners> Login(string email,string password)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>();
            filter.Add("email", email);
            filter.Add("password", password);
            List<Tuners> t = ((List<Tuners>)await SelectAllAsync(filter));
            if ( t.Count==0)
            {
                return null;
            }
            return t[0];

        }
       



    }
}
