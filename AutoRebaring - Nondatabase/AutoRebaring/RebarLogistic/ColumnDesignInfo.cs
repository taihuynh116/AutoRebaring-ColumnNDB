using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRebaring.RebarLogistic
{
    public class ColumnStandardDesignInput
    {
        public int Index { get; set; }
        public Level Level { get; set; }
        public RebarBarType RebarType { get; set; }
        public int N1 { get; set; }
        public int N2 { get; set; }
        public ColumnStandardDesignInput(Level level, RebarBarType rebarType, int n1, int n2)
        {
            Level = level;
            RebarType = rebarType;
            N1 = n1;
            N2 = n2;
        }
        public ColumnStandardDesignInput(string level, string rebarType, int n1, int n2, List<Level> levels, List<RebarBarType> rebarTypes)
        {
            Level = levels.Where(x => x.Name == level).First();
            RebarType = rebarTypes.Where(x => x.Name == rebarType).First();
            N1 = n1;
            N2 = n2;
        }
    }

    public class ColumnStirrupDesignInput
    {
        public int Index { get; set; }
        public Level Level { get; set; }
        public RebarBarType StirrupType1 { get; set; }
        public RebarBarType StirrupType2 { get; set; }
        public double BotTopSpacing1 { get; set; }
        public double BotTopSpacing2 { get; set; }
        public double MiddleSpacing1 { get; set; }
        public double MiddleSpacing2 { get; set; }
        public ColumnStirrupDesignInput(Level level, RebarBarType stirType1, RebarBarType stirType2, double bt1, double bt2, double m1, double m2)
        {
            Level = level;
            StirrupType1 = stirType1;
            StirrupType2 = stirType2;
            BotTopSpacing1 = bt1;
            BotTopSpacing2 = bt2;
            MiddleSpacing1 = m1;
            MiddleSpacing2 = m2;
        }
        public ColumnStirrupDesignInput(string level, string stirType1, string stirType2, double bt1, double bt2, double m1, double m2, List<Level> levels, List<RebarBarType> rebarTypes)
        {
            Level = levels.Where(x => x.Name == level).First();
            StirrupType1 = rebarTypes.Where(x=> x.Name == stirType1).First();
            StirrupType2 = rebarTypes.Where(x => x.Name == stirType2).First();
            BotTopSpacing1 = bt1;
            BotTopSpacing2 = bt2;
            MiddleSpacing1 = m1;
            MiddleSpacing2 = m2;
        }
    }
}
