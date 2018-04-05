using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRebaring.Database.AutoRebaring.Dao
{
    public class GeneralParameterInputDao
    {
        AutoRebaringDbContext db = new AutoRebaringDbContext();
        public GeneralParameterInputDao() { }
        public GeneralParameterInput GetGeneralParameterInput(int projectId)
        {
            var res = db.GeneralParameterInputs.Where(x => x.ProjectID == projectId);
            return res.Count() > 0 ? res.First() : null;
        }
        public void Update(GeneralParameterInput gpi)
        {
            var res = db.GeneralParameterInputs.Where(x => x.ProjectID == gpi.ProjectID);
            if (res.Count() > 0)
            {
                var data = res.ToList()[0];
                data.ConcreteCover = gpi.ConcreteCover;
                data.DevelopmentMultiply = gpi.DevelopmentMultiply;
                data.DevelopmentLengthsDistance = gpi.DevelopmentLengthsDistance;
                data.ReinforcementStirrupInclude = gpi.ReinforcementStirrupInclude;
                data.DeltaDevelopmentError = gpi.DeltaDevelopmentError;
                data.NumberDevelopmentError = gpi.NumberDevelopmentError;
                data.DevelopmentErrorInclude = gpi.DevelopmentErrorInclude;
                data.DevelopmentLevelOffsetAllowed = gpi.DevelopmentLevelOffsetAllowed;
                data.DevelopmentLevelOffsetInclude = gpi.DevelopmentLevelOffsetInclude;
                data.ShortenLimit = gpi.ShortenLimit;
                data.AnchorMultiply = gpi.AnchorMultiply;
                data.LockheadMultiply = gpi.LockheadMultiply;
                data.ConcreteTopCover = gpi.ConcreteTopCover;
                data.RatioLH = gpi.RatioLH;
                data.CoverTopSmall = gpi.CoverTopSmall;
            }
            else
            {
                db.GeneralParameterInputs.Add(gpi);
            }
            db.SaveChanges();
        }
    }
}
