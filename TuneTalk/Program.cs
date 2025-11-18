
using Moduls;
using DBL;
using System.Diagnostics.Eventing.Reader;
namespace UnitTest
{    

    public class Program
    {
        public static async Task Main(string[] args)
        {
          //  ReportsDB f= new ReportsDB();
          //  List<Reports> report =await f.GetAllAsync();
          //  foreach(var rep in report )
          //  {
          //      Console.WriteLine(rep.report);
           // }
          Tuners t = new Tuners(2,"Jane Smith", "jane.smith@example.com", "securePass1");
           TunersDB r = new TunersDB();
            int a= await r.UpdateAsync(t, "Jane Smith", "securePass1");
          //  if (s == null)
          //  {
          //     Console.WriteLine("EMAIL ALREADY IN USE");
          //  }


        //    else { Console.WriteLine(s.username); }

                        /// Chats s = new Chats();
                        ///  s.id = 13;
                        /// ChatsDB d = new ChatsDB();
                        /// await  d.InsertGetObjAsync(s);
                        /// await d.DeleteAsync(s);
                        //  List<Chats> b = new List<Chats>();
                        // b = await d.SelectChat(1, 2);
                        /// foreach (var chat in b) 
                        /// {
                        ///    Console.WriteLine(chat.message);
                        /// }
        }
    }
}
