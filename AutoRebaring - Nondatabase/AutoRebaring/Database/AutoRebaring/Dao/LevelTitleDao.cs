using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRebaring.Database.AutoRebaring.Dao
{
    class LevelTitleDao
    {
        AutoRebaringDbContext db = new AutoRebaringDbContext();
        public LevelTitleDao()
        {
        }
        public List<LevelTitle> GetLevelTitles(int projectId)
        {
            var res = db.LevelTitles.Where(x => x.ProjectID == projectId);
            return res.Count() > 0 ? res.ToList() : new List<LevelTitle>();
        }
        public void Update(List<LevelTitle> lts)
        {
            long projectId = lts[0].ProjectID;
            var res = db.LevelTitles.Where(x => x.ProjectID == projectId);
            if (res.Count() > 0)
            {
                db.LevelTitles.RemoveRange(res);
                db.LevelTitles.AddRange(lts);
            }
            else
            {
                db.LevelTitles.AddRange(lts);
            }
            db.SaveChanges();
        }
    }
}
