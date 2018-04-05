using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRebaring.Database.AutoRebaring.Dao
{
    class RebarDesignDao
    {
        AutoRebaringDbContext db = new AutoRebaringDbContext();
        public RebarDesignDao()
        {
        }
        public List<RebarDesign> GetRebarDesigns(int projectId, string mark, string startLevel)
        {
            var res = db.RebarDesigns.Where(x => x.ProjectID == projectId && x.Mark == mark && x.StartLevel == startLevel);
            return res.Count() > 0 ? res.OrderBy(x=> x.Elevation).ToList() : new List<RebarDesign>();
        }
        public void Update(List<RebarDesign> rds)
        {
            long projectId = rds[0].ProjectID;
            string mark = rds[0].Mark;
            string startLevel = rds[0].StartLevel;
            var res = db.RebarDesigns.Where(x => x.ProjectID == projectId && x.Mark == mark && x.StartLevel == startLevel);
            if (res.Count() > 0)
            {
                db.RebarDesigns.RemoveRange(res);
                db.RebarDesigns.AddRange(rds);
            }
            else
            {
                db.RebarDesigns.AddRange(rds);
            }
            db.SaveChanges();
        }
    }
}
