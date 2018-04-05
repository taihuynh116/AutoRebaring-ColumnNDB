using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRebaring.Database.AutoRebaring.Dao
{
    class RebarDesignGeneralDao
    {
        AutoRebaringDbContext db = new AutoRebaringDbContext();
        public RebarDesignGeneralDao()
        {
        }
        public RebarDesignGeneral GetRebarDesignGeneral(int projectId, string mark)
        {
            var res = db.RebarDesignGenerals.Where(x => x.ProjectID == projectId && x.Mark == mark);
            return (res.Count() > 0) ? res.OrderByDescending(x=>x.CreateDate).First() : null;
        }
        public void Update(RebarDesignGeneral rdg)
        {
            var res = db.RebarDesignGenerals.Where(x => x.ProjectID == rdg.ProjectID && x.Mark == rdg.Mark && x.StartLevel == rdg.StartLevel);
            if (res.Count() > 0)
            {
                var data = res.ToList()[0];
                data.StartLevel = rdg.StartLevel;
                data.EndLevel = rdg.EndLevel;
                data.IsLockHead = rdg.IsLockHead;
                data.IsStartRebar = rdg.IsStartRebar;
                data.RebarStartZ1 = rdg.RebarStartZ1;
                data.CreateDate = rdg.CreateDate;
            }
            else
            {
                db.RebarDesignGenerals.Add(rdg);
            }
            db.SaveChanges();
        }
    }
}
