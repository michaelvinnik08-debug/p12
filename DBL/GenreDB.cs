using Moduls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class GenreDB:BaseDB<Genres>
    {
        protected override string GetTableName()
        {
            return "genres";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override async Task<Genres> CreateModelAsync(object[] row)
        {
            Genres g = new Genres();
            g.id = int.Parse(row[0].ToString());
            g.name = row[1].ToString();
            return g;
        }
        
    }
}
