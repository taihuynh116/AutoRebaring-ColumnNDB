using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRebaring.Database.AutoRebaring.Dao
{
    class OtherParameterDao
    {
        AutoRebaringDbContext db = new AutoRebaringDbContext();
        public OtherParameterDao()
        {
        }
        public OtherParameter GetOtherParameter(int projectId, string mark)
        {
            var res = db.OtherParameters.Where(x => x.ProjectID == projectId && x.Mark == mark);
            return res.Count() > 0 ? res.First() : null;
        }
        public void Update(OtherParameter op)
        {
            var res = db.OtherParameters.Where(x => x.ProjectID == op.ProjectID && x.Mark == op.Mark);
            if (res.Count() > 0)
            {
                var data = res.ToList()[0];
                data.CheckLevel = op.CheckLevel;
                data.IsReversed = op.IsReversed;
                data.View3dInclude = op.View3dInclude;
                data.View3d = op.View3d;
                data.PartCount = op.PartCount;
            }
            else
            {
                db.OtherParameters.Add(op);
            }
            db.SaveChanges();
        }
    }
}
