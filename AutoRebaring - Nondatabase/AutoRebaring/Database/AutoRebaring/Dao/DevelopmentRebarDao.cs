using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRebaring.Database.AutoRebaring.Dao
{
    public class DevelopmentRebarDao
    {
        AutoRebaringDbContext db = new AutoRebaringDbContext();
        public DevelopmentRebarDao() { }
        public DevelopmentRebar GetDevelopmentRebar(int projectId)
        {
            var res = db.DevelopmentRebars.Where(x => x.ProjectID == projectId);
            return res.Count() > 0 ? res.First() : null;
        }
        public void Update(DevelopmentRebar dr)
        {
            var res = db.DevelopmentRebars.Where(x => x.ProjectID == dr.ProjectID);
            if (res.Count() > 0)
            {
                var data = res.ToList()[0];
                data.BottomOffset = dr.BottomOffset;
                data.TopOffset = dr.TopOffset;
                data.OffsetInclude = dr.OffsetInclude;
                data.BottomOffsetRatio = dr.BottomOffsetRatio;
                data.TopOffsetRatio = dr.TopOffsetRatio;
                data.OffsetRatioInclude = dr.OffsetRatioInclude;
                data.BottomStirrupOffset = dr.BottomStirrupOffset;
                data.TopStirrupOffset = dr.TopStirrupOffset;
                data.StirrupOffsetInclude = dr.StirrupOffsetInclude;
                data.BottomStirrupOffsetRatio = dr.BottomStirrupOffsetRatio;
                data.TopStirrupOffsetRatio = dr.TopStirrupOffsetRatio;
                data.StirrupOffsetRatioInclude = dr.StirrupOffsetRatioInclude;
                data.IsInsideBeam = dr.IsInsideBeam;
                data.IsStirrupInsideBeam = dr.IsStirrupInsideBeam;
            }
            else
            {
                db.DevelopmentRebars.Add(dr);
            }
            db.SaveChanges();
        }
    }
}
