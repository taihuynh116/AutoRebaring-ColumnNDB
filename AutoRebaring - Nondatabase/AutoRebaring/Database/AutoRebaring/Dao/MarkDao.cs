using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRebaring.Database.AutoRebaring.Dao
{
    public class MarkDao
    {
        AutoRebaringDbContext db = new AutoRebaringDbContext();
        public MarkDao()
        {
        }
        public List<Mark> GetMarks(int projectId)
        {
            var res = db.Marks.Where(x => x.ProjectID == projectId);
            return (res.Count() != 0) ? res.OrderByDescending(x => x.CreateDate).ToList() : new List<Mark>();
        }
        public void Update(Mark mark)
        {
            var res = db.Marks.Where(x => x.ProjectID == mark.ProjectID && x.Mark1 == mark.Mark1);
            if (res.Count() > 0)
            {
                var data = res.ToList()[0];
                data.Mark1 = mark.Mark1;
                data.CreateDate = mark.CreateDate;
            }
            else
            {
                db.Marks.Add(mark);
            }
            db.SaveChanges();
        }
    }
}
