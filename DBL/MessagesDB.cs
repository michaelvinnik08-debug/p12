using Google.Protobuf.Reflection;
using Moduls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DBL
{
    public class MessagesDB : BaseDB<Messages>
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
            m.id = int.Parse(row[0].ToString());
            m.text = row[1].ToString();
            m.Time = row[2] == DBNull.Value
                   ? DateTime.UtcNow
                   : DateTime.Parse(row[2].ToString());

            m.chatid = int.Parse(row[3].ToString());
            m.userid = int.Parse(row[4].ToString());
            return m;
        }
        // public async Task<Messages> SelectMessageById(int Id)
        //  {
        //    Dictionary<string, object> filter = new Dictionary<string, object>()
        //   {
        //      {"id" ,Id },
        //  };
        //  Messages M = ((List<Messages>)await SelectAllAsync(filter))[0];
        //  return M;
        // }

        public async Task<List<Messages>> SelectMessagesByChatId(int id)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>();
            filter.Add("chatid", id);
            return ((List<Messages>)await SelectAllAsync(filter));
        }
        public async Task<int> Delete(int Id)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"id" ,Id },
            };
            return await base.DeleteAsync(values);
        }
        public async Task<Messages> NewMessage(string text, int chatid, int userid)
        {
            Dictionary<string, object> valuePairs = new Dictionary<string, object>()
            {
                {"text",text },
                {"chatid",chatid },
                {"userid",userid }
            };
            return (Messages)await base.InsertGetObjAsync(valuePairs);
        }
        public async Task UpdateMessageText(int messageId, string newText)
        {

            Dictionary<string, object> value = new Dictionary<string, object>()
            {
                {"text",newText }
            };
            Dictionary<string, object> filter = new Dictionary<string, object>()
            {
                {"id",messageId }
            };
            await UpdateAsync(value, filter);
        }
    }

      
    
}
