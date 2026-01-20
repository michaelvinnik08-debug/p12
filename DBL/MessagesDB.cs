using Moduls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class MessagesDB :BaseDB<Messages>
    {
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override string GetTableName()
        {
            return "messages";

        }
        protected override async Task<Messages> CreateModelAsync(object[] row)
        {
            Messages m = new Messages();
            m.id= int.Parse(row[0].ToString());
            m.text = row[1].ToString();
            m.Time = row[2] == DBNull.Value
                   ? DateTime.UtcNow
                   : DateTime.Parse(row[2].ToString());

            m.chatid = int.Parse(row[3].ToString());
            m.userid = int.Parse(row[4].ToString());
            return m;
        }
        public async Task<List<Messages>> SelectMessagesByChatId(int id)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>();
            filter.Add("chatid", id);
            return ((List<Messages>)await SelectAllAsync(filter));
        }
        public async Task<int> Delete(int id)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"id" ,id },
            };
            return await base.DeleteAsync(values);
        }
        public async Task<Messages> NewMessage(string text,int chatid,int userid)
        {
            Dictionary<string, object> valuePairs = new Dictionary<string, object>()
            {
                {"text",text },
                {"chatid",chatid },
                {"userid",userid }
            };
            return  (Messages)await base.InsertGetObjAsync(valuePairs);
        }
    }
}
