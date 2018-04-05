using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRebaring.Database.AutoRebaring.Dao
{
    public class ColumnParameterDao
    {
        AutoRebaringDbContext db = new AutoRebaringDbContext();
        public ColumnParameterDao()
        {
        }
        public ColumnParameter GetColumnParameter(int projectId)
        {
            var res = db.ColumnParameters.Where(x => x.ProjectID == projectId);
            return res.Count() > 0 ? res.First() : null;
        }
        public void Update(ColumnParameter colParam)
        {
            var res = db.ColumnParameters.Where(x => x.ProjectID == colParam.ProjectID);
            if (res.Count() > 0)
            {
                var data = res.ToList()[0];
                data.B1_Param = colParam.B1_Param;
                data.B2_Param = colParam.B2_Param;
            }
            else
            {
                db.ColumnParameters.Add(colParam);
            }
            db.SaveChanges();
        }
    }
}
