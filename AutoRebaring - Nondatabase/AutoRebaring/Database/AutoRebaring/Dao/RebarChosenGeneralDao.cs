using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRebaring.Database.AutoRebaring.Dao
{
    public class RebarChosenGeneralDao
    {
        AutoRebaringDbContext db = new AutoRebaringDbContext();
        public RebarChosenGeneralDao() { }
        public RebarChosenGeneral GetRebarChosenGeneral(int projectId)
        {
            var res = db.RebarChosenGenerals.Where(x => x.ProjectID == projectId);
            return res.Count() > 0 ? res.First() : null;
        }
        public void Update(RebarChosenGeneral rcg)
        {
            var res = db.RebarChosenGenerals.Where(x => x.ProjectID == rcg.ProjectID);
            if (res.Count() > 0)
            {
                var data = res.ToList()[0];
                data.Lmax = rcg.Lmax;
                data.Lmin = rcg.Lmin;
                data.Step = rcg.Step;
                data.LImplantMax = rcg.LImplantMax;
                data.FamilyStirrup1 = rcg.FamilyStirrup1;
                data.FamilyStirrup2 = rcg.FamilyStirrup2;
            }
            else
            {
                db.RebarChosenGenerals.Add(rcg);
            }
            db.SaveChanges();
        }
    }
}
