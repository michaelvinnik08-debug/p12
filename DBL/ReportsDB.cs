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
        public static class ReportCategory
        {
            public const string FakeOrImpersonation = "Fake or impersonation";
            public const string InappropriateProfileContent = "Inappropriate profile content";
            public const string InappropriateLanguage = "Inappropriate language";
            public const string SpamOrFlooding = "Spam or flooding";
            public const string HarassmentOrThreats = "Harassment or threats";
            public const string Other = "Other";
        }

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
            r.report = row[1].ToString();
            r.reported = int.Parse(row[2].ToString());
            r.reporter = int.Parse(row[3].ToString());
            r.created_at = DateTime.Parse(row[4].ToString());
            return r;
        }

        
        public async Task<TimeSpan?> InsertGetObjAsync(Reports R)
        {
            if (!IsValidCategory(R.report))
                return null;

            TunersDB s = new TunersDB();
            Tuners t = await s.SelectUserID(R.reported);

            if (t == null || t.banned != 0)
                return null;

            // Check cooldown — has this reporter already reported this user in the last 24h?
            Reports existing = await GetRecentReport(R.reporter, R.reported);
            if (existing != null)
            {
                TimeSpan elapsed = DateTime.UtcNow - existing.created_at;
                TimeSpan cooldown = TimeSpan.FromHours(24);
                if (elapsed < cooldown)
                    return cooldown - elapsed; // return remaining wait time
            }

            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "reporter", R.reporter },
                { "reported", R.reported },
                { "report", R.report },
                { "created_at", DateTime.UtcNow }
            };

            t.rep_amount++;
            await base.InsertGetObjAsync(fillValues);
            return null; // null = success
        }

        // Checks if reporter already reported this user within the last 24h
        private async Task<Reports> GetRecentReport(int reporterId, int reportedId)
        {
            string query = $"SELECT * FROM project12.reports WHERE reporter = {reporterId} AND reported = {reportedId} ORDER BY created_at DESC LIMIT 1";
            List<Reports> results = (List<Reports>)await SelectAllAsync(query);
            if (results == null || results.Count == 0) return null;

            Reports last = results[0];
            return (DateTime.UtcNow - last.created_at).TotalHours < 24 ? last : null;
        }
        public async Task<int> DeleteReport(int Id)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>();
            filter.Add("id", Id);
            return await base.DeleteAsync(filter);
        }
        public async Task<List<Reports>> GetAllAsync()
        {
            return (List<Reports>)await SelectAllAsync();
        }


        public List<string> GetReportCategories()
        {
            return new List<string>
            {
                ReportCategory.FakeOrImpersonation,
                ReportCategory.InappropriateProfileContent,
                ReportCategory.InappropriateLanguage,
                ReportCategory.SpamOrFlooding,
                ReportCategory.HarassmentOrThreats,
                ReportCategory.Other
            };
        }

        private bool IsValidCategory(string report)
        {
            return GetReportCategories().Contains(report);
        }
    }
}
