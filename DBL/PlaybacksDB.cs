using Moduls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class PlaybacksDB :BaseDB<Playbacks>
    {
        protected override string GetTableName()
        {
            return "playbacks";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override async Task<Playbacks> CreateModelAsync(object[] row)
        {
            Playbacks p = new Playbacks ();
            p.id = int.Parse(row[0].ToString());
            p.user = int.Parse(row[1].ToString());
            p.genre = int.Parse(row[2].ToString());
            p.playbacks = int.Parse(row[3].ToString());
            return p;

        }
    }
}
