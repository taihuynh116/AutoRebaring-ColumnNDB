using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRebaring.Database.AutoRebaring.Dao
{
    public class RebarChosenDao
    {
        AutoRebaringDbContext db = new AutoRebaringDbContext();
        public RebarChosenDao() { }
        public List<RebarChosen> GetRebarChosens(int projectId)
        {
            var res = db.RebarChosens.Where(x => x.ProjectID == projectId).OrderBy(x=> x.LType).OrderBy(x=> x.LStandard);
            return res.Count() > 0 ? res.OrderByDescending(x=> x.LStandard).ToList() : new List<RebarChosen>();
        }
        public void Update(List<RebarChosen> rcs)
        {
            long projectId = rcs[0].ProjectID;
            var res = db.RebarChosens.Where(x => x.ProjectID == projectId);
            if (res.Count() > 0)
            {
                db.RebarChosens.RemoveRange(res);
                db.RebarChosens.AddRange(rcs);
            }
            else
            {
                db.RebarChosens.AddRange(rcs);
            }
            db.SaveChanges();
        }
    }
    public enum RebarChosenType
    {
        Standard, StandardPair, StandardTrip, Implant, ImplantPair
    }
}
