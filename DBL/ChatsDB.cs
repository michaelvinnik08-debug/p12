using Moduls;
using Org.BouncyCastle.Crypto.Encodings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///לשאול לגבי הטבלה
namespace DBL
{
    public class ChatsDB:BaseDB<Chats>
    {
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override string GetTableName()
        {
            return "chats";
        }
        public async Task<Chats> InsertGetObjAsync(Chats c)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "user1_id", c.user1 },
                { "message",  c.message},
                { "user2_id", c.user2 }
            };
            return (Chats)await base.InsertGetObjAsync(fillValues);
        }
        protected override async Task<Chats> CreateModelAsync(object[] row)
        {
            Chats c = new Chats();
            c.id = int.Parse(row[0].ToString());
            c.user1 = int.Parse(row[1].ToString());
            c.message = row[2].ToString();
            c.last = DateTime.Parse(row[3].ToString());
            c.is_read = int.Parse(row[4].ToString());
            c.user2 = int.Parse(row[5].ToString());
            return c;
        }
        public  async Task<int> UpdateAsync(string message,Chats c)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>();
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("message",message);
            filter.Add("id", c.id.ToString());
            return await base.UpdateAsync( values,filter);
        }
        public async Task<int> DeleteAsync(Chats c)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>();
            filter.Add("id", c.id.ToString());
            return await base.DeleteAsync(filter);

        }
        public async Task<List<Chats>> SelectChats(Tuners T)
        {
            string query = $@"SELECT * FROM project12.chats WHERE {T.Id} =user1_id  Order by last_message_time";
            return ((List<Chats>)await SelectAllAsync(query));
        }
        public async Task<List<Chats>> SelectChat(int id,int id2)
        {
            string query = $@"SELECT * FROM project12.chats WHERE ({id} =user1_id && {id2} = user2_id) || ({id2} =user1_id && {id} =user2_id)  Order by last_message_time ";
            return   ((List<Chats>)await SelectAllAsync(query));
        }
    }
}
