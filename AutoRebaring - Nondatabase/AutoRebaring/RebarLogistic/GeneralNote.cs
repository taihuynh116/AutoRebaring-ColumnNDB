using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry;

namespace AutoRebaring.RebarLogistic
{
    public class GeneralNote
    {
        public double TopOffset { get; set; }
        public double BottomOffset { get; set; }
        public bool IsOffsetCheck { get; set; }
        public double TopOffsetRatio { get; set; }
        public double BottomOffsetRatio { get; set; }
        public bool IsOffsetRatioCheck { get; set; }
        public double TopOffsetStirrup { get; set; }
        public double BottomOffsetStirrup { get; set; }
        public bool IsOffsetCheckStirrup { get; set; }
        public double TopOffsetRatioStirrup { get; set; }
        public double BottomOffsetRatioStirrup { get; set; }
        public bool IsOffsetRatioCheckStirrup { get; set; }
        public bool IsStirrupInsideBeam { get; set; }
        public double RebarStandardOffsetControl { get; set; }
        public double CoverConcrete { get; set;}
        public double CoverTop { get; set; }
        public double CoverTopSmall { get; set; }
        public double FirstRebarZ1 { get; set; }
        public double FirstRebarZ2 { get; set; }
        public int LoopCount { get; set; }
        public int FirstColumnIndex { get; set; }
        public RebarHookType RebarHookType { get; set; }
        public List<RebarBarType> RebarBarTypes { get; set; }
        public RebarLayoutRule RebarLayoutRule { get; set; }
        public double ShortenLimit { get; set; }
        public int LockHeadMultiply { get; set; }
        public double RatioLH { get; set; }
        public int DevelopmentMultiply { get; set; }
        public int AnchorMultiply { get; set; }
        public double Lmax { get; set; }
        public double Lmin { get; set; }
        public double Step { get; set; }
        public List<double> LStandards { get; set; }
        public List<double> LPlusStandards { get; set; }
        public List<double> LImplants { get; set; }
        public List<double> LPlusImplants { get; set; }
        public GeneralNote() { }
    }
}
