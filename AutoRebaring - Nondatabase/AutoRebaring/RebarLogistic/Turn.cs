using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry;
using System.Diagnostics;
using System.Threading;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System.Reflection;
using Autodesk.Revit.UI;

namespace AutoRebaring.RebarLogistic
{
    public class CheckLoop
    {
        public int LoopCount { get { return TrackingTopIndex - TrackingBottomIndex; } }
        public GeneralNote GeneralNote;
        public TurnLoop TurnLoop;
        public int TrackingTopIndex;
        public int TrackingBottomIndex
        {
            get
            {
                int index = TrackingTopIndex - GeneralNote.LoopCount;
                if (index > TrackingBottomLimit)
                    return index;
                return TrackingBottomLimit;
            }
        }
        public int TrackingBottomLimit = 0;
        public List<Turn> TrackingTurns;
        public ColumnInfoCollection ColumnInfoCollection;
        public int Count;
        public CheckLoop(Document doc, ColumnInfoCollection cic, GeneralNote gn, Variable vari)
        {
            GeneralNote = gn;
            TurnLoop = new TurnLoop(doc, cic.Count * 2, gn, vari);
            ColumnInfoCollection = cic;
            TurnLoop[0].Start1 = GeneralNote.FirstRebarZ1 - ColumnInfoCollection[GeneralNote.FirstColumnIndex].RebarDevelopmentLength;
            TurnLoop[0].Start2 = GeneralNote.FirstRebarZ2 - ColumnInfoCollection[GeneralNote.FirstColumnIndex].RebarDevelopmentLength;
            TurnLoop[0].ColumnInfo = ColumnInfoCollection[GeneralNote.FirstColumnIndex];
        }
        public bool RunLogistic()
        {
            Debug.WriteLine("Tầng\tCách\tL1\tL2\tSwap");
            for (int i = 0; i < TurnLoop.Count; i++)
            {
                FirstLine:
                while (true)
                {
                    if (!TurnLoop[i].Swap)
                    {
                        Debug.WriteLine(TurnLoop[i].ColumnIndex + "\t" + i + "\t" + TurnLoop[i].Index + " \t" + GeomUtil.feet2Milimeter(TurnLoop[i].L1) + "\t" + GeomUtil.feet2Milimeter(TurnLoop[i].L2) + "\t" + TurnLoop[i].Swap);
                        if (Check(i))
                        {
                            if (TurnLoop[i].Finish1 && TurnLoop[i].Finish2)
                            {
                                Count = i + 1;
                                goto L1;
                            }
                            break;
                        }
                    }
                    if (TurnLoop[i].CanSwap)
                    {
                        TurnLoop[i].Swap = true;
                        Debug.WriteLine(TurnLoop[i].ColumnIndex + "\t" + i + "\t" + TurnLoop[i].Index + " \t" + GeomUtil.feet2Milimeter(TurnLoop[i].L1) + "\t" + GeomUtil.feet2Milimeter(TurnLoop[i].L2) + "\t" + TurnLoop[i].Swap);
                        if (Check(i))
                        {
                            if (TurnLoop[i].Finish1 && TurnLoop[i].Finish2)
                            {
                                Count = i + 1;
                                goto L1;
                            }
                            break;
                        }
                    }
                    while (!TurnLoop[i].Next())
                    {
                        if (!TurnLoop[i].FirstPass)
                        {
                            for (int k = 0; k <= i; k++)
                            {
                                TurnLoop[k].FirstPass = true;
                            }
                            TrackingTopIndex = i;
                            TrackingTurns = new List<Turn>();
                            for (int k = i - LoopCount; k <= i; k++)
                            {
                                TrackingTurns.Add(new Turn(TurnLoop[k]));
                            }
                        }
                        TurnLoop[i].Index = 0;
                        i--;
                        if (i < TrackingBottomIndex)
                        {
                            i = TrackingTopIndex;
                            if (TurnLoop[i].LongType == LongType.Residual)
                            {
                                //bool caseExist = false;
                                //for (int t = TrackingTopIndex - 1; t >= TrackingBottomIndex; t--)
                                //{
                                //    if (TurnLoop[t].LongType == LongType.Standard)
                                //    {
                                //        TurnLoop[t].LongType = LongType.Residual;
                                //        caseExist = true;
                                //        break;
                                //    }
                                //}
                                //if (!caseExist)
                                //{
                                throw new Exception("There is not any case matching this loop. You should increase loop count.");
                                //}
                                //i = TrackingBottomIndex;
                                //TurnLoop[i].Index = 0;
                                //goto FirstLine;
                            }
                            int j = 0;
                            for (int k = i - LoopCount; k <= i; k++)
                            {
                                TurnLoop[k] = TrackingTurns[j]; j++;
                            }
                            TurnLoop[i].LongType = LongType.Residual;
                            TurnLoop[i].Index = 0;
                            goto FirstLine;
                        }
                    }
                }
            }
            L1:
            return true;
        }
        public void CreateRebars(ParameterRebar pr, View3D view3d = null)
        {
            Debug.WriteLine("Vẽ thép");
            for (int i = 0; i < Count; i++)
            {
                Debug.WriteLine("Turn " + i.ToString());
                TurnLoop[i].CreateRebars(pr, view3d);
            }
        }
        public void CreateRebarTests(ParameterRebar pr, View3D view3d = null)
        {
            Debug.WriteLine("Vẽ thép");
            for (int i = 0; i < Count; i++)
            {
                Debug.WriteLine("Turn " + i.ToString());
                TurnLoop[i].CreateRebarTests(pr,ColumnInfoCollection[0].Document.ActiveView, i, view3d);
            }
        }
        public void CreateDetailLines(List<XYZ> pnts = null)
        {
            Debug.WriteLine("Vẽ thép");
            for (int i = 0; i < Count; i++)
            {
                TurnLoop[i].CreateDetailLines(pnts);
            }
        }
        public bool Check(int i)
        {
            double end1 = TurnLoop[i].End1;
            double end2 = TurnLoop[i].End2;
            int colIndex = TurnLoop[i].ColumnIndex;
            if (colIndex == ColumnInfoCollection.Count - 1)
            {
                double topLimit = ColumnInfoCollection[ColumnInfoCollection.Count - 1].TopLast -GeneralNote.CoverTop;
                if (GeomUtil.IsEqual(TurnLoop[i].Start1 + GeneralNote.Lmax, topLimit) || (TurnLoop[i].Start1 + GeneralNote.Lmax) > topLimit)
                {
                    TurnLoop[i].SetL1Finish(TurnLoop[i].L1 - (end1 - topLimit));
                    TurnLoop[i].ColumnInfoEnd = ColumnInfoCollection[ColumnInfoCollection.Count - 1];
                }
                if (GeomUtil.IsEqual(TurnLoop[i].Start2 + GeneralNote.Lmax, topLimit) || (TurnLoop[i].Start2 + GeneralNote.Lmax) > topLimit)
                {
                    TurnLoop[i].SetL2Finish(TurnLoop[i].L2 - (end2 - topLimit));
                    TurnLoop[i].ColumnInfoEnd = ColumnInfoCollection[ColumnInfoCollection.Count - 1];
                }
            }
            else
            {
                if (TurnLoop[i].ColumnInfo.ShortenType == ColumnShortenType.LockHeadFull)
                {
                    if (GeomUtil.IsEqual(TurnLoop[i].Start1 + GeneralNote.Lmax, TurnLoop[i].ColumnInfo.TopLockHead) || (TurnLoop[i].Start1 + GeneralNote.Lmax > TurnLoop[i].ColumnInfo.TopLockHead))
                    {
                        TurnLoop[i].SetImplantChoice(ColumnInfoCollection[colIndex + 1]);
                        TurnLoop[i].Implant1 = true;
                    }
                    if (GeomUtil.IsEqual(TurnLoop[i].Start2 + GeneralNote.Lmax, TurnLoop[i].ColumnInfo.TopLockHead) || (TurnLoop[i].Start2 + GeneralNote.Lmax > TurnLoop[i].ColumnInfo.TopLockHead))
                    {
                        TurnLoop[i].SetImplantChoice(ColumnInfoCollection[colIndex + 1]);
                        TurnLoop[i].Implant2 = true;
                    }
                    end1 = TurnLoop[i].End1;
                    end2 = TurnLoop[i].End2;
                }
            }

            double endLimit0 = -1;
            if (colIndex < ColumnInfoCollection.Count)
            {
                endLimit0 = ColumnInfoCollection[colIndex].TopOffset;
            }
            else
            {
                throw new Exception("The code should not be here!");
            }
            double startLimit1 = -1, endLimit1 = -1;
            if (colIndex + 1 < ColumnInfoCollection.Count)
            {
                startLimit1 = ColumnInfoCollection[colIndex + 1].BottomOffset + ColumnInfoCollection[colIndex + 1].RebarDevelopmentLength;
                endLimit1 = ColumnInfoCollection[colIndex + 1].TopOffset;
            }
            else
            {
                startLimit1 = endLimit0 + GeomUtil.milimeter2Feet(20000);
                endLimit1 = startLimit1 + GeomUtil.milimeter2Feet(5000);
            }
            double startLimit2 = -1, endLimit2 = -1;
            if (colIndex + 2 < ColumnInfoCollection.Count)
            {
                startLimit2 = ColumnInfoCollection[colIndex + 2].BottomOffset + ColumnInfoCollection[colIndex + 2].RebarDevelopmentLength;
                endLimit2 = ColumnInfoCollection[colIndex + 2].TopOffset;
            }
            else
            {
                startLimit2 = endLimit1 + GeomUtil.milimeter2Feet(20000);
                endLimit2 = startLimit2 + GeomUtil.milimeter2Feet(5000);
            }
            Position pos1 = TurnLoop[i].Finish1 ? Position.Pos3 : CheckPosition(end1, endLimit0, startLimit1, endLimit1, startLimit2, endLimit2);
            Position pos2 = TurnLoop[i].Finish2 ? Position.Pos3 : CheckPosition(end2, endLimit0, startLimit1, endLimit1, startLimit2, endLimit2);
            TurnLoop[i].Position1 = pos1; TurnLoop[i].Position2 = pos2;
            if ((int)pos1 >= 3 || (int)pos2 >= 3) return false;
            if (pos1 == Position.Pos1 || pos2 == Position.Pos1) return false;
            if (pos1 == pos2)
            {
                double delta = 0;
                TurnLoop[i + 1].EqualZero1 = false;
                TurnLoop[i + 1].EqualZero2 = false;
                if (pos1 == Position.Pos1)
                {
                    delta = Math.Abs(end1 - end2) - ColumnInfoCollection[colIndex].RebarDevelopmentLength;
                    if (!(GeomUtil.IsEqual(delta, 0) || delta > 0))
                        return false;

                    TurnLoop[i + 1].Start1 = end1 - ColumnInfoCollection[colIndex].RebarDevelopmentLength;
                    TurnLoop[i + 1].Start2 = end2 - ColumnInfoCollection[colIndex].RebarDevelopmentLength;
                }
                else if (pos1 == Position.Pos2)
                {
                    delta = Math.Abs(end1 - end2) - ColumnInfoCollection[colIndex + 1].RebarDevelopmentLength;
                    if (!(GeomUtil.IsEqual(delta, 0) || delta > 0))
                        return false;

                    TurnLoop[i + 1].Start1 = end1 - ColumnInfoCollection[colIndex + 1].RebarDevelopmentLength;
                    TurnLoop[i + 1].Start2 = end2 - ColumnInfoCollection[colIndex + 1].RebarDevelopmentLength;
                }
                else
                {
                    if (!TurnLoop[i].Finish1)
                    {
                        delta = Math.Abs(end1 - end2) - ColumnInfoCollection[colIndex + 2].RebarDevelopmentLength;
                        if (GeomUtil.IsEqual(delta, 0) || delta > 0)
                        {

                        }
                        else
                        {
                            return false;
                        }

                        TurnLoop[i + 1].Start1 = end1 - ColumnInfoCollection[colIndex + 2].RebarDevelopmentLength;
                        TurnLoop[i + 1].Start2 = end2 - ColumnInfoCollection[colIndex + 2].RebarDevelopmentLength;
                    }
                }
                switch (pos1)
                {
                    case Position.Pos1:
                        TurnLoop[i + 1].ColumnInfo = ColumnInfoCollection[colIndex];
                        TurnLoop[i].ColumnInfoEnd = ColumnInfoCollection[colIndex];
                        break;
                    case Position.Pos2:
                        TurnLoop[i + 1].ColumnInfo = colIndex + 1 < ColumnInfoCollection.Count ? ColumnInfoCollection[colIndex + 1] : null;
                        TurnLoop[i].ColumnInfoEnd = colIndex + 1 < ColumnInfoCollection.Count ? ColumnInfoCollection[colIndex + 1] : null;
                        break;
                    case Position.Pos3:
                        TurnLoop[i + 1].ColumnInfo = colIndex + 2 < ColumnInfoCollection.Count ? ColumnInfoCollection[colIndex + 2] : null;
                        TurnLoop[i].ColumnInfoEnd = colIndex + 2 < ColumnInfoCollection.Count ? ColumnInfoCollection[colIndex + 2] : null;
                        break;
                }
            }
            else if (pos1 < pos2)
            {
                TurnLoop[i + 1].EqualZero1 = false;
                TurnLoop[i + 1].EqualZero2 = true;
                if (pos1 == Position.Pos1)
                {
                    TurnLoop[i + 1].Start1 = end1 - ColumnInfoCollection[colIndex].RebarDevelopmentLength;
                }
                else
                {
                    TurnLoop[i + 1].Start1 = end1 - ColumnInfoCollection[colIndex + 1].RebarDevelopmentLength;
                }
                TurnLoop[i + 1].Start2 = end2;
                switch (pos1)
                {
                    case Position.Pos1:
                        for (int j = 0; j < i; j++)
                        {
                            if ((TurnLoop[j].ColumnIndex == colIndex - 1 && TurnLoop[j].Position2 == Position.Pos2) || (TurnLoop[j].ColumnIndex == colIndex - 2 && TurnLoop[j].Position2 == Position.Pos3))
                            {
                                double delta = Math.Abs(end1 - TurnLoop[j].End2) - ColumnInfoCollection[colIndex].RebarDevelopmentLength;
                                if (!(GeomUtil.IsEqual(delta, 0) || delta > 0))
                                    return false;
                            }
                        }
                        TurnLoop[i + 1].ColumnInfo = ColumnInfoCollection[colIndex];
                        TurnLoop[i].ColumnInfoEnd = ColumnInfoCollection[colIndex];
                        break;
                    case Position.Pos2:
                        TurnLoop[i + 1].ColumnInfo = colIndex + 1 < ColumnInfoCollection.Count ? ColumnInfoCollection[colIndex + 1] : null;
                        TurnLoop[i].ColumnInfoEnd = colIndex + 1 < ColumnInfoCollection.Count ? ColumnInfoCollection[colIndex + 1] : null;
                        break;
                }
            }
            else
            {
                TurnLoop[i + 1].EqualZero1 = true;
                TurnLoop[i + 1].EqualZero2 = false;
                TurnLoop[i + 1].Start1 = end1;
                if (pos2 == Position.Pos1)
                {
                    TurnLoop[i + 1].Start2 = end2 - ColumnInfoCollection[colIndex].RebarDevelopmentLength;
                }
                else
                {
                    TurnLoop[i + 1].Start2 = end2 - ColumnInfoCollection[colIndex + 1].RebarDevelopmentLength;
                }
                switch (pos2)
                {
                    case Position.Pos1:
                        for (int j = 0; j < i; j++)
                        {
                            if ((TurnLoop[j].ColumnIndex == colIndex - 1 && TurnLoop[j].Position1 == Position.Pos2) || (TurnLoop[j].ColumnIndex == colIndex - 2 && TurnLoop[j].Position1 == Position.Pos3))
                            {
                                double delta = Math.Abs(end2 - TurnLoop[j].End1) - ColumnInfoCollection[colIndex].RebarDevelopmentLength;
                                if (!(GeomUtil.IsEqual(delta, 0) || delta > 0))
                                    return false;
                            }
                        }
                        TurnLoop[i + 1].ColumnInfo = ColumnInfoCollection[colIndex];
                        TurnLoop[i].ColumnInfoEnd = ColumnInfoCollection[colIndex];
                        break;
                    case Position.Pos2:
                        TurnLoop[i + 1].ColumnInfo = colIndex + 1 < ColumnInfoCollection.Count ? ColumnInfoCollection[colIndex + 1] : null;
                        TurnLoop[i].ColumnInfoEnd = colIndex + 1 < ColumnInfoCollection.Count ? ColumnInfoCollection[colIndex + 1] : null;
                        break;
                }
            }
            return true;
        }
        Position CheckPosition(double end, double endLim0, double startLim1, double endLim1, double startLim2, double endLim2)
        {
            if (GeomUtil.IsEqual(end, endLim0) || end < endLim0)
                return Position.Pos1;
            if (GeomUtil.IsEqual(end, startLim1) || GeomUtil.IsEqual(end, endLim1) || (end > startLim1 && end < endLim1))
                return Position.Pos2;
            if (GeomUtil.IsEqual(end, startLim2) || GeomUtil.IsEqual(end, endLim2) || (end > startLim2 && end < endLim2))
                return Position.Pos3;
            if (end > endLim0 && end < startLim1)
                return Position.Wro1;
            if (end > endLim1 && end < startLim2)
                return Position.Wro2;
            return Position.Wro3;
        }
    }
    public enum Position
    {
        Pos1, Pos2, Pos3, Wro1, Wro2, Wro3
    }
    public class TurnLoop
    {
        public List<Turn> turns = new List<Turn>();
        public TurnLoop(Document doc, int Count, GeneralNote gn, Variable vari)
        {
            for (int i = 0; i < Count; i++)
            {
                turns.Add(new Turn() { TurnIndex = i, GeneralNote = gn, Variable = vari });
            }
        }
        public int Count { get { return turns.Count; } }
        public Turn this[int i]
        {
            get
            {
                return this.turns[i];
            }
            set
            {
                this.turns[i] = value;
            }
        }
    }
    public class Turn
    {
        public LongType LongType { get; set; }
        public Variable Variable { get; set; }
        public GeneralNote GeneralNote { get; set; }
        public int Count
        {
            get
            {
                if (Implant1 && Implant2) return Variable.CountImplant12;
                if ((Implant1 && EqualZero2) || (Implant2 && EqualZero1)) return Variable.CountImplantEqualZero;
                if (Implant1 || Implant2)
                {
                    switch (LongType)
                    {
                        case LongType.Standard:
                            return Variable.CountStandardImplant;
                        case LongType.Residual:
                            return Variable.CountResidualImplant;
                    }
                }
                if (EqualZero1 || EqualZero2)
                {
                    switch (LongType)
                    {
                        case LongType.Standard:
                            return Variable.CountStandardEqualZero;
                        case LongType.Residual:
                            return Variable.CountResidualEqualZero;
                    }
                }
                switch (LongType)
                {
                    case LongType.Standard:
                        return Variable.CountStandard12;
                    case LongType.Residual:
                        return Variable.CountResidual12;
                }
                throw new Exception("This case have not been checked yet!");
            }
        }
        public bool Swap { get; set; }
        public bool CanSwap
        {
            get
            {
                if (EqualZero1 || EqualZero2 || Implant1 || Implant2)
                    return false;
                if (GeomUtil.IsEqual(L1, L2))
                    return false;
                return true;
            }
        }
        public double Start1 { get; set; }
        public double Start2 { get; set; }
        public double Start1mm { get { return GeomUtil.feet2Meter(Start1); } }
        public double Start2mm { get { return GeomUtil.feet2Meter(Start2); } }
        public double End1 { get { return Start1 + L1; } }
        public double End2 { get { return Start2 + L2; } }
        public double End1mm { get { return GeomUtil.feet2Meter(End1); } }
        public double End2mm { get { return GeomUtil.feet2Meter(End2); } }
        public double L1
        {
            get
            {
                if (Finish1)
                    return L1Finish;
                if (EqualZero1)
                    return 0;
                if (Implant1)
                {
                    double l = 0;
                    if (Implant2)
                        l = Variable.L1Implants[Index];
                    else if (EqualZero2)
                        l = Variable.LImplants[Index];
                    else switch (LongType)
                        {
                            case LongType.Standard: l = Variable.L1ImplantStandards[Index]; break;
                            case LongType.Residual: l = Variable.L1ImplantResiduals[Index]; break;
                        }
                    return (ColumnInfo.TopAnchorAfter + l) - Start1;
                }
                if (Implant2)
                {
                    switch (LongType)
                    {
                        case LongType.Standard: return Variable.L1StandardImplants[Index];
                        case LongType.Residual: return Variable.L1ResidualImplants[Index];
                    }
                }
                if (EqualZero2)
                {
                    switch (LongType)
                    {
                        case LongType.Standard:
                            return Variable.LStandards[Index];
                        case LongType.Residual:
                            return Variable.LResiduals[Index];
                    }
                }
                if (LongType == LongType.Standard)
                {
                    if (!Swap)
                    {
                        return Variable.L1Standards[Index];
                    }
                    return Variable.L2Standards[Index];
                }
                else
                {
                    if (!Swap)
                    {
                        return Variable.L1Residuals[Index];
                    }
                    return Variable.L2Residuals[Index];
                }
            }
        }
        public double L2
        {
            get
            {
                if (Finish2)
                    return L2Finish;
                if (EqualZero2)
                    return 0;
                if (Implant2)
                {
                    double l = 0;
                    if (Implant1)
                        l = Variable.L2Implants[Index];
                    else if (EqualZero1)
                        l = Variable.LImplants[Index];
                    else switch (LongType)
                        {
                            case LongType.Standard: l = Variable.L2StandardImplants[Index]; break;
                            case LongType.Residual: l = Variable.L2ResidualImplants[Index]; break;
                        }
                    return (ColumnInfo.TopAnchorAfter + l) - Start2;
                }
                if (Implant1)
                {
                    switch (LongType)
                    {
                        case LongType.Standard: return Variable.L2ImplantStandards[Index];
                        case LongType.Residual: return Variable.L2ImplantResiduals[Index];
                    }
                }
                if (EqualZero1)
                {
                    switch (LongType)
                    {
                        case LongType.Standard:
                            return Variable.LStandards[Index];
                        case LongType.Residual:
                            return Variable.LResiduals[Index];
                    }
                }
                if (LongType == LongType.Standard)
                {
                    if (!Swap)
                    {
                        return Variable.L2Standards[Index];
                    }
                    return Variable.L1Standards[Index];
                }
                else
                {
                    if (!Swap)
                    {
                        return Variable.L2Residuals[Index];
                    }
                    return Variable.L1Residuals[Index];
                }
            }
        }
        public bool Implant1 { get; set; }
        public bool Implant2 { get; set; }
        public bool Finish1 { get; set; }
        public bool Finish2 { get; set; }
        public double L1Finish { get; set; }
        public double L2Finish { get; set; }
        public double L1mm { get { return GeomUtil.feet2Meter(L1); } }
        public double L2mm { get { return GeomUtil.feet2Meter(L2); } }
        public bool EqualZero1 { get; set; }
        public bool EqualZero2 { get; set; }
        public bool FirstPass { get; set; }
        public int Index { get; set; }
        public Position Position1 { get; set; }
        public Position Position2 { get; set; }
        public bool IsImplanted { get; set; }
        public void SetImplantChoice(ColumnInfo ci)
        {
            if (!IsImplanted)
            {
                Variable.SetImplant(GeneralNote.LImplants, GeneralNote.LPlusImplants, GeneralNote.AnchorMultiply, GeneralNote.DevelopmentMultiply, ci.BottomOffsetValue,
                     ci.TopOffset - ci.Bottom, RebarInfo.DiameterAfter);
                IsImplanted = true;
            }
        }
        public TurnShortenType ShortenType1
        {
            get
            {
                if (Finish1) return TurnShortenType.LockHeadFull;
                if (Position1 == Position.Pos2 || Position1 == Position.Pos3)
                {
                    if (ColumnInfo.ShortenType == ColumnShortenType.LockHeadFull)
                        return TurnShortenType.LockHeadFull;
                    if (ColumnInfo.ShortenType == ColumnShortenType.Big)
                        return TurnShortenType.LockHead;
                    if (ColumnInfo.ShortenType == ColumnShortenType.Small)
                        return TurnShortenType.Shorten;
                    return TurnShortenType.NormalI;
                }
                return TurnShortenType.Normal;
            }
        }
        public TurnShortenType ShortenType2
        {
            get
            {
                if (Finish1) return TurnShortenType.LockHeadFull;
                if (Position2 == Position.Pos2 || Position2 == Position.Pos3)
                {
                    if (ColumnInfo.ShortenType == ColumnShortenType.LockHeadFull)
                        return TurnShortenType.LockHeadFull;
                    if (ColumnInfo.ShortenType == ColumnShortenType.Big)
                        return TurnShortenType.LockHead;
                    if (ColumnInfo.ShortenType == ColumnShortenType.Small)
                        return TurnShortenType.Shorten;
                    return TurnShortenType.NormalI;
                }
                return TurnShortenType.Normal;
            }
        }
        public ColumnInfo ColumnInfo { get; set; }
        public ColumnInfo ColumnInfoEnd { get; set; }
        public int ColumnIndex { get { return ColumnInfo.Index; } }
        public RebarInfo RebarInfo { get { return ColumnInfo.RebarInfo; } }
        public Element Element { get { return ColumnInfo.Element; } }
        public Document Document { get { return ColumnInfo.Document; } }
        public Rebar Rebar1 { get; set; }
        public Rebar Rebar2 { get; set; }
        public int TurnIndex { get; set; }
        public Turn()
        {
            EqualZero1 = false; EqualZero2 = false; Swap = false; Index = 0; FirstPass = false; LongType = LongType.Standard; Implant1 = false; Implant2 = false; Finish1 = false; Finish2 = false; IsImplanted = false;
        }
        public Turn(Turn turn)
        {
            PropertyInfo[] propInfos = typeof(Turn).GetProperties();
            for (int i = 0; i < propInfos.Length; i++)
            {
                if (!propInfos[i].CanWrite) continue;
                propInfos[i].SetValue(this, propInfos[i].GetValue(turn));
            }
        }
        public void SetL1Finish(double l)
        {
            Finish1 = true;
            L1Finish = GeomUtil.milimeter2Feet(Math.Round(GeomUtil.feet2Milimeter(l)));
        }
        public void SetL2Finish(double l)
        {
            Finish2 = true;
            L2Finish = GeomUtil.milimeter2Feet(Math.Round(GeomUtil.feet2Milimeter(l)));
        }
        public bool Next()
        {
            if (!Swap && CanSwap)
            {
                Swap = true;
                return true;
            }
            if (Index < Count - 1)
            {
                Index++;
                Swap = false;
                return true;
            }
            return false;
        }
        public void CreateRebars(ParameterRebar pr, View3D view3d = null)
        {
            List<Rebar> rebar1s = new List<Rebar>();
            List<Rebar> rebar2s = new List<Rebar>();
            if (L1 > 0)
            {
                switch (ShortenType1)
                {
                    case TurnShortenType.Normal:
                        {
                            int sum = 0;
                            for (int i = 0; i < RebarInfo.NumberNormals1.Count; i++)
                                sum += RebarInfo.NumberNormals1[i];

                            for (int i = 0; i < RebarInfo.NumberNormals1.Count; i++)
                            {
                                XYZ pnt = new XYZ(RebarInfo.TopUVNormals1[i].U, RebarInfo.TopUVNormals1[i].V, Start1);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(pnt, pnt + XYZ.BasisZ * L1) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberNormals1[i], RebarInfo.SpacingNormals1[i], RebarInfo.ArrayLengthNormals1[i], RebarInfo.RebarBarType, RebarInfo.NormalNormals1[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar1s.Add(rb);
                            }
                            break;
                        }
                    case TurnShortenType.NormalI:
                        {
                            int sum = 0;
                            for (int i = 0; i < RebarInfo.NumberNormals1.Count; i++)
                                sum += RebarInfo.NumberNormals1[i];

                            for (int i = 0; i < RebarInfo.NumberNormals1.Count; i++)
                            {
                                XYZ pnt = new XYZ(RebarInfo.TopUVNormals1[i].U, RebarInfo.TopUVNormals1[i].V, Start1);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(pnt, pnt + XYZ.BasisZ * L1) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberNormals1[i], RebarInfo.SpacingNormals1[i], RebarInfo.ArrayLengthNormals1[i], RebarInfo.RebarBarType, RebarInfo.NormalNormals1[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar1s.Add(rb);
                            }

                            sum = 0;
                            for (int i = 0; i < RebarInfo.NumberImplants1.Count; i++)
                                sum += RebarInfo.NumberImplants1[i];

                            for (int i = 0; i < RebarInfo.NumberImplants1.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVImplants1[i].U, RebarInfo.TopUVImplants1[i].V, ColumnInfo.TopAnchorAfter);
                                XYZ p2 = new XYZ(RebarInfo.TopUVImplants1[i].U, RebarInfo.TopUVImplants1[i].V, End1);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberImplants1[i], RebarInfo.SpacingImplants1[i], RebarInfo.ArrayLengthImplants1[i], RebarInfo.RebarBarTypeAfter, RebarInfo.NormalImplants1[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar1s.Add(rb);
                            }
                            break;
                        }
                    case TurnShortenType.Shorten:
                        {
                            int sum = 0;
                            for (int i = 0; i < RebarInfo.NumberNones1.Count; i++)
                                sum += RebarInfo.NumberNones1[i];

                            for (int i = 0; i < RebarInfo.NumberNones1.Count; i++)
                            {
                                XYZ pnt = new XYZ(RebarInfo.TopUVNones1[i].U, RebarInfo.TopUVNones1[i].V, Start1);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(pnt, pnt + XYZ.BasisZ * L1) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberNones1[i], RebarInfo.SpacingNones1[i], RebarInfo.ArrayLengthNones1[i], RebarInfo.RebarBarType, RebarInfo.NormalNones1[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar1s.Add(rb);
                            }

                            sum = 0;
                            for (int i = 0; i < RebarInfo.NumberSmalls1.Count; i++)
                                sum += RebarInfo.NumberSmalls1[i];

                            for (int i = 0; i < RebarInfo.NumberSmalls1.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVSmalls1[i].U, RebarInfo.TopUVSmalls1[i].V, Start1);
                                XYZ p3 = p1 + RebarInfo.VecBExpandSmalls1[i].Normalize() * RebarInfo.DimBExpandSmalls1[i];
                                p3 = new XYZ(p3.X, p3.Y, ColumnInfo.TopSmall);
                                XYZ p2 = p3 - RebarInfo.VectorExpandSmalls1[i];
                                XYZ p4 = new XYZ(p3.X, p3.Y, End1);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2), Line.CreateBound(p2, p3), Line.CreateBound(p3, p4) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberSmalls1[i], RebarInfo.SpacingSmalls1[i], RebarInfo.ArrayLengthSmalls1[i], RebarInfo.RebarBarType, RebarInfo.NormalSmalls1[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar1s.Add(rb);
                            }

                            sum = 0;
                            for (int i = 0; i < RebarInfo.NumberImplantShortens1.Count; i++)
                                sum += RebarInfo.NumberImplantShortens1[i];

                            for (int i = 0; i < RebarInfo.NumberImplantShortens1.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVImplantShortens1[i].U, RebarInfo.TopUVImplantShortens1[i].V, ColumnInfo.TopAnchorAfter);
                                XYZ p2 = new XYZ(RebarInfo.TopUVImplantShortens1[i].U, RebarInfo.TopUVImplantShortens1[i].V, End1);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberImplantShortens1[i], RebarInfo.SpacingImplantShortens1[i], RebarInfo.ArrayLengthImplantShortens1[i], RebarInfo.RebarBarTypeAfter, RebarInfo.NormalImplantShortens1[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar1s.Add(rb);
                            }
                            break;
                        }
                    case TurnShortenType.LockHead:
                        {
                            int sum = 0;
                            for (int i = 0; i < RebarInfo.NumberNones1.Count; i++)
                                sum += RebarInfo.NumberNones1[i];

                            for (int i = 0; i < RebarInfo.NumberNones1.Count; i++)
                            {
                                XYZ pnt = new XYZ(RebarInfo.TopUVNones1[i].U, RebarInfo.TopUVNones1[i].V, Start1);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(pnt, pnt + XYZ.BasisZ * L1) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberNones1[i], RebarInfo.SpacingNones1[i], RebarInfo.ArrayLengthNones1[i], RebarInfo.RebarBarType, RebarInfo.NormalNones1[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar1s.Add(rb);
                            }

                            sum = 0;
                            for (int i = 0; i < RebarInfo.NumberSmalls1.Count; i++)
                                sum += RebarInfo.NumberSmalls1[i];

                            for (int i = 0; i < RebarInfo.NumberSmalls1.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVSmalls1[i].U, RebarInfo.TopUVSmalls1[i].V, Start1);
                                XYZ p3 = p1 + RebarInfo.VecBExpandSmalls1[i].Normalize() * RebarInfo.DimBExpandSmalls1[i];
                                p3 = new XYZ(p3.X, p3.Y, ColumnInfo.TopSmall);
                                XYZ p2 = p3 - RebarInfo.VectorExpandSmalls1[i];
                                XYZ p4 = new XYZ(p3.X, p3.Y, End1);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2), Line.CreateBound(p2, p3), Line.CreateBound(p3, p4) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberSmalls1[i], RebarInfo.SpacingSmalls1[i], RebarInfo.ArrayLengthSmalls1[i], RebarInfo.RebarBarType, RebarInfo.NormalSmalls1[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar1s.Add(rb);
                            }

                            sum = 0;
                            for (int i = 0; i < RebarInfo.NumberBigs1.Count; i++)
                                sum += RebarInfo.NumberBigs1[i];

                            for (int i = 0; i < RebarInfo.NumberBigs1.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVBigs1[i].U, RebarInfo.TopUVBigs1[i].V, Start1);
                                XYZ p2 = new XYZ(p1.X, p1.Y, ColumnInfo.TopLockHead);
                                XYZ p3 = p2 + RebarInfo.VectorExpandBigs1[i] * RebarInfo.LockHeadLength;
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2), Line.CreateBound(p2, p3) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberBigs1[i], RebarInfo.SpacingBigs1[i], RebarInfo.ArrayLengthBigs1[i], RebarInfo.RebarBarType, RebarInfo.NormalBigs1[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar1s.Add(rb);
                            }

                            sum = 0;
                            for (int i = 0; i < RebarInfo.NumberImplantShortens1.Count; i++)
                                sum += RebarInfo.NumberImplantShortens1[i];

                            for (int i = 0; i < RebarInfo.NumberImplantShortens1.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVImplantShortens1[i].U, RebarInfo.TopUVImplantShortens1[i].V, ColumnInfo.TopAnchorAfter);
                                XYZ p2 = new XYZ(RebarInfo.TopUVImplantShortens1[i].U, RebarInfo.TopUVImplantShortens1[i].V, End1);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberImplantShortens1[i], RebarInfo.SpacingImplantShortens1[i], RebarInfo.ArrayLengthImplantShortens1[i], RebarInfo.RebarBarTypeAfter, RebarInfo.NormalImplantShortens1[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar1s.Add(rb);
                            }
                            break;
                        }
                    case TurnShortenType.LockHeadFull:
                        {
                            int sum = 0;
                            for (int i = 0; i < RebarInfo.NumberLockHeads1.Count; i++)
                                sum += RebarInfo.NumberLockHeads1[i];

                            for (int i = 0; i < RebarInfo.NumberLockHeads1.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVLockHeads1[i].U, RebarInfo.TopUVLockHeads1[i].V, Start1);
                                XYZ p2 = new XYZ(p1.X, p1.Y, ColumnInfo.TopLockHead);
                                XYZ p3 = p2 + RebarInfo.VectorExpandLockHeads1[i] * RebarInfo.LockHeadLength;
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2), Line.CreateBound(p2, p3) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberLockHeads1[i], RebarInfo.SpacingLockHeads1[i], RebarInfo.ArrayLengthLockHeads1[i], RebarInfo.RebarBarType, RebarInfo.NormalLockHeads1[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar1s.Add(rb);
                            }
                            if (Finish1) break;

                            sum = 0;
                            for (int i = 0; i < RebarInfo.NumberImplantLockHeads1.Count; i++)
                                sum += RebarInfo.NumberImplantLockHeads1[i];

                            for (int i = 0; i < RebarInfo.NumberImplantLockHeads1.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVImplantLockHeads1[i].U, RebarInfo.TopUVImplantLockHeads1[i].V, ColumnInfo.TopAnchorAfter);
                                XYZ p2 = new XYZ(RebarInfo.TopUVImplantLockHeads1[i].U, RebarInfo.TopUVImplantLockHeads1[i].V, End1);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberImplantLockHeads1[i], RebarInfo.SpacingImplantLockHeads1[i], RebarInfo.ArrayLengthImplantLockHeads1[i], RebarInfo.RebarBarTypeAfter, RebarInfo.NormalImplantLockHeads1[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar1s.Add(rb);
                            }
                            break;
                        }
                }
            }

            if (L2 > 0)
            {
                switch (ShortenType2)
                {
                    case TurnShortenType.Normal:
                        {
                            int sum = 0;
                            for (int i = 0; i < RebarInfo.NumberNormals2.Count; i++)
                                sum += RebarInfo.NumberNormals2[i];

                            for (int i = 0; i < RebarInfo.NumberNormals2.Count; i++)
                            {
                                XYZ pnt = new XYZ(RebarInfo.TopUVNormals2[i].U, RebarInfo.TopUVNormals2[i].V, Start2);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(pnt, pnt + XYZ.BasisZ * L2) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberNormals2[i], RebarInfo.SpacingNormals2[i], RebarInfo.ArrayLengthNormals2[i], RebarInfo.RebarBarType, RebarInfo.NormalNormals2[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar2s.Add(rb);
                            }
                            break;
                        }
                    case TurnShortenType.NormalI:
                        {
                            int sum = 0;
                            for (int i = 0; i < RebarInfo.NumberNormals2.Count; i++)
                                sum += RebarInfo.NumberNormals2[i];

                            for (int i = 0; i < RebarInfo.NumberNormals2.Count; i++)
                            {
                                XYZ pnt = new XYZ(RebarInfo.TopUVNormals2[i].U, RebarInfo.TopUVNormals2[i].V, Start2);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(pnt, pnt + XYZ.BasisZ * L2) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberNormals2[i], RebarInfo.SpacingNormals2[i], RebarInfo.ArrayLengthNormals2[i], RebarInfo.RebarBarType, RebarInfo.NormalNormals2[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar2s.Add(rb);
                            }

                            sum = 0;
                            for (int i = 0; i < RebarInfo.NumberImplants2.Count; i++)
                                sum += RebarInfo.NumberImplants2[i];

                            for (int i = 0; i < RebarInfo.NumberImplants2.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVImplants2[i].U, RebarInfo.TopUVImplants2[i].V, ColumnInfo.TopAnchorAfter);
                                XYZ p2 = new XYZ(RebarInfo.TopUVImplants2[i].U, RebarInfo.TopUVImplants2[i].V, End2);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberImplants2[i], RebarInfo.SpacingImplants2[i], RebarInfo.ArrayLengthImplants2[i], RebarInfo.RebarBarTypeAfter, RebarInfo.NormalImplants2[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar2s.Add(rb);
                            }
                            break;
                        }
                    case TurnShortenType.Shorten:
                        {
                            int sum = 0;
                            for (int i = 0; i < RebarInfo.NumberNones2.Count; i++)
                                sum += RebarInfo.NumberNones2[i];

                            for (int i = 0; i < RebarInfo.NumberNones2.Count; i++)
                            {
                                XYZ pnt = new XYZ(RebarInfo.TopUVNones2[i].U, RebarInfo.TopUVNones2[i].V, Start2);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(pnt, pnt + XYZ.BasisZ * L2) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberNones2[i], RebarInfo.SpacingNones2[i], RebarInfo.ArrayLengthNones2[i], RebarInfo.RebarBarType, RebarInfo.NormalNones2[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar2s.Add(rb);
                            }

                            sum = 0;
                            for (int i = 0; i < RebarInfo.NumberSmalls2.Count; i++)
                                sum += RebarInfo.NumberSmalls2[i];

                            for (int i = 0; i < RebarInfo.NumberSmalls2.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVSmalls2[i].U, RebarInfo.TopUVSmalls2[i].V, Start2);
                                XYZ p3 = p1 + RebarInfo.VecBExpandSmalls2[i].Normalize() * RebarInfo.DimBExpandSmalls2[i];
                                p3 = new XYZ(p3.X, p3.Y, ColumnInfo.TopSmall);
                                XYZ p2 = p3 - RebarInfo.VectorExpandSmalls2[i];
                                XYZ p4 = new XYZ(p3.X, p3.Y, End2);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2), Line.CreateBound(p2, p3), Line.CreateBound(p3, p4) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberSmalls2[i], RebarInfo.SpacingSmalls2[i], RebarInfo.ArrayLengthSmalls2[i], RebarInfo.RebarBarType, RebarInfo.NormalSmalls2[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar2s.Add(rb);
                            }

                            sum = 0;
                            for (int i = 0; i < RebarInfo.NumberImplantShortens2.Count; i++)
                                sum += RebarInfo.NumberImplantShortens2[i];

                            for (int i = 0; i < RebarInfo.NumberImplantShortens2.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVImplantShortens2[i].U, RebarInfo.TopUVImplantShortens2[i].V, ColumnInfo.TopAnchorAfter);
                                XYZ p2 = new XYZ(RebarInfo.TopUVImplantShortens2[i].U, RebarInfo.TopUVImplantShortens2[i].V, End2);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberImplantShortens2[i], RebarInfo.SpacingImplantShortens2[i], RebarInfo.ArrayLengthImplantShortens2[i], RebarInfo.RebarBarTypeAfter, RebarInfo.NormalImplantShortens2[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar2s.Add(rb);
                            }
                            break;
                        }
                    case TurnShortenType.LockHead:
                        {
                            int sum = 0;
                            for (int i = 0; i < RebarInfo.NumberNones2.Count; i++)
                                sum += RebarInfo.NumberNones2[i];

                            for (int i = 0; i < RebarInfo.NumberNones2.Count; i++)
                            {
                                XYZ pnt = new XYZ(RebarInfo.TopUVNones2[i].U, RebarInfo.TopUVNones2[i].V, Start2);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(pnt, pnt + XYZ.BasisZ * L2) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberNones2[i], RebarInfo.SpacingNones2[i], RebarInfo.ArrayLengthNones2[i], RebarInfo.RebarBarType, RebarInfo.NormalNones2[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar2s.Add(rb);
                            }

                            sum = 0;
                            for (int i = 0; i < RebarInfo.NumberSmalls2.Count; i++)
                                sum += RebarInfo.NumberSmalls2[i];

                            for (int i = 0; i < RebarInfo.NumberSmalls2.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVSmalls2[i].U, RebarInfo.TopUVSmalls2[i].V, Start2);
                                XYZ p3 = p1 + RebarInfo.VecBExpandSmalls2[i].Normalize() * RebarInfo.DimBExpandSmalls2[i];
                                p3 = new XYZ(p3.X, p3.Y, ColumnInfo.TopSmall);
                                XYZ p2 = p3 - RebarInfo.VectorExpandSmalls2[i];
                                XYZ p4 = new XYZ(p3.X, p3.Y, End2);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2), Line.CreateBound(p2, p3), Line.CreateBound(p3, p4) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberSmalls2[i], RebarInfo.SpacingSmalls2[i], RebarInfo.ArrayLengthSmalls2[i], RebarInfo.RebarBarType, RebarInfo.NormalSmalls2[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar2s.Add(rb);
                            }

                            sum = 0;
                            for (int i = 0; i < RebarInfo.NumberBigs2.Count; i++)
                                sum += RebarInfo.NumberBigs2[i];

                            for (int i = 0; i < RebarInfo.NumberBigs2.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVBigs2[i].U, RebarInfo.TopUVBigs2[i].V, Start2);
                                XYZ p2 = new XYZ(p1.X, p1.Y, ColumnInfo.TopLockHead);
                                XYZ p3 = p2 + RebarInfo.VectorExpandBigs2[i] * RebarInfo.LockHeadLength;
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2), Line.CreateBound(p2, p3) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberBigs2[i], RebarInfo.SpacingBigs2[i], RebarInfo.ArrayLengthBigs2[i], RebarInfo.RebarBarType, RebarInfo.NormalBigs2[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar2s.Add(rb);
                            }

                            sum = 0;
                            for (int i = 0; i < RebarInfo.NumberImplantShortens2.Count; i++)
                                sum += RebarInfo.NumberImplantShortens2[i];

                            for (int i = 0; i < RebarInfo.NumberImplantShortens2.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVImplantShortens2[i].U, RebarInfo.TopUVImplantShortens2[i].V, ColumnInfo.TopAnchorAfter);
                                XYZ p2 = new XYZ(RebarInfo.TopUVImplantShortens2[i].U, RebarInfo.TopUVImplantShortens2[i].V, End2);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberImplantShortens2[i], RebarInfo.SpacingImplantShortens2[i], RebarInfo.ArrayLengthImplantShortens2[i], RebarInfo.RebarBarTypeAfter, RebarInfo.NormalImplantShortens2[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar2s.Add(rb);
                            }
                            break;
                        }
                    case TurnShortenType.LockHeadFull:
                        {
                            int sum = 0;
                            for (int i = 0; i < RebarInfo.NumberLockHeads2.Count; i++)
                                sum += RebarInfo.NumberLockHeads2[i];

                            for (int i = 0; i < RebarInfo.NumberLockHeads2.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVLockHeads2[i].U, RebarInfo.TopUVLockHeads2[i].V, Start2);
                                XYZ p2 = new XYZ(p1.X, p1.Y, ColumnInfo.TopLockHead);
                                XYZ p3 = p2 + RebarInfo.VectorExpandLockHeads2[i] * RebarInfo.LockHeadLength;
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2), Line.CreateBound(p2, p3) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberLockHeads2[i], RebarInfo.SpacingLockHeads2[i], RebarInfo.ArrayLengthLockHeads2[i], RebarInfo.RebarBarType, RebarInfo.NormalLockHeads2[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar2s.Add(rb);
                            }
                            if (Finish2) break;

                            sum = 0;
                            for (int i = 0; i < RebarInfo.NumberImplantLockHeads2.Count; i++)
                                sum += RebarInfo.NumberImplantLockHeads2[i];

                            for (int i = 0; i < RebarInfo.NumberImplantLockHeads2.Count; i++)
                            {
                                XYZ p1 = new XYZ(RebarInfo.TopUVImplantLockHeads2[i].U, RebarInfo.TopUVImplantLockHeads2[i].V, ColumnInfo.TopAnchorAfter);
                                XYZ p2 = new XYZ(RebarInfo.TopUVImplantLockHeads2[i].U, RebarInfo.TopUVImplantLockHeads2[i].V, End2);
                                List<Curve> curves = new List<Curve> { Line.CreateBound(p1, p2) };
                                Rebar rb = CreateSingleRebar(curves, RebarInfo.NumberImplantLockHeads2[i], RebarInfo.SpacingImplantLockHeads2[i], RebarInfo.ArrayLengthImplantLockHeads2[i], RebarInfo.RebarBarTypeAfter, RebarInfo.NormalImplantLockHeads2[i], RebarInfo.RebarLayoutRule);
                                try
                                {
                                    rb.LookupParameter("HB_SoLuong").Set(sum);
                                    rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                    rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                    rb.LookupParameter("Partition").Set(pr.Partition);
                                    rb.LookupParameter("Mark").Set(pr.Mark);
                                }
                                catch
                                {

                                }
                                rebar2s.Add(rb);
                            }
                            break;
                        }
                }
            }

            if (rebar1s.Count != 0) Rebar1 = rebar1s[0];
            if (rebar2s.Count != 0) Rebar2 = rebar2s[0];

            if (view3d != null)
            {
                rebar1s.ForEach(x =>
                {
                    x.SetSolidInView(view3d, true);
                    x.SetUnobscuredInView(view3d, true);
                });
                rebar2s.ForEach(x =>
                {
                    x.SetSolidInView(view3d, true);
                    x.SetUnobscuredInView(view3d, true);
                });
            }
        }
        public void CreateRebarTests(ParameterRebar pr, View view,int index, View3D view3d = null)
        {
            XYZ vec = view.RightDirection;
            if (L1 > 0)
            {
                switch (ShortenType1)
                {
                    case TurnShortenType.Normal:
                        {
                            int i = 0;
                            XYZ pnt = new XYZ(RebarInfo.TopUVNormals1[i].U, RebarInfo.TopUVNormals1[i].V, Start1) + vec* (index % 2) *GeomUtil.milimeter2Feet(100);
                            List<Curve> curves = new List<Curve> { Line.CreateBound(pnt, pnt + XYZ.BasisZ * L1) };
                            Rebar rb = CreateSingleRebar(curves, 1, RebarInfo.SpacingNormals1[i], 0, RebarInfo.RebarBarType, RebarInfo.NormalNormals1[i], RebarInfo.RebarLayoutRule);
                            try
                            {
                                rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                rb.LookupParameter("Partition").Set(pr.Partition);
                                rb.LookupParameter("HB_LuoiTrinh").Set(1);
                                rb.LookupParameter("Mark").Set(pr.Mark);
                                if (LongType == LongType.Standard)
                                {
                                    bool isStandard = false;
                                    foreach (double len in Variable.LStandards)
                                    {
                                        if (L1 == len || GeomUtil.IsEqual(L1, 0))
                                        {
                                            rb.LookupParameter("HB_Type").Set("Standard");
                                            isStandard = true;
                                            break;
                                        }
                                    }
                                    if (!isStandard)
                                    {
                                        double l = L1 + L2;
                                        double l1mm = Math.Round(GeomUtil.feet2Milimeter(L1), 0);
                                        double l2mm = Math.Round(GeomUtil.feet2Milimeter(L2), 0);
                                        double lmm = Math.Round(GeomUtil.feet2Milimeter(l), 0);
                                        rb.LookupParameter("HB_Type").Set("Standard (" + l1mm + "+" + l2mm + "=" + lmm + ")");
                                    }
                                }
                                else
                                {
                                    rb.LookupParameter("HB_Type").Set("Residual");
                                }
                                if (Finish1)
                                {
                                    bool isStandard = false;
                                    foreach (double len in Variable.LStandards)
                                    {
                                        if (L1 == len || GeomUtil.IsEqual(L1, 0))
                                        {
                                            rb.LookupParameter("HB_Type").Set("Standard");
                                            isStandard = true;
                                            break;
                                        }
                                    }
                                    if (!isStandard)
                                    {
                                        rb.LookupParameter("HB_Type").Set("Residual");
                                    }
                                }
                            }
                            catch
                            {

                            }
                        }
                        break;
                    default:
                        {
                            int i = 0;
                            XYZ pnt = new XYZ(RebarInfo.TopUVNormals1[i].U, RebarInfo.TopUVNormals1[i].V, Start1) + vec * (index % 2) * GeomUtil.milimeter2Feet(100);
                            List<Curve> curves = new List<Curve> { Line.CreateBound(pnt, pnt + XYZ.BasisZ * L1) };
                            Rebar rb = CreateSingleRebar(curves, 1, RebarInfo.SpacingNormals1[i],0, RebarInfo.RebarBarType, RebarInfo.NormalNormals1[i], RebarInfo.RebarLayoutRule);
                            try
                            {
                                rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                rb.LookupParameter("Partition").Set(pr.Partition);
                                rb.LookupParameter("HB_LuoiTrinh").Set(1);
                                rb.LookupParameter("Mark").Set(pr.Mark);
                                if (LongType == LongType.Standard)
                                {
                                    bool isStandard = false;
                                    foreach (double len in Variable.LStandards)
                                    {
                                        if (L1 == len || GeomUtil.IsEqual(L1, 0))
                                        {
                                            rb.LookupParameter("HB_Type").Set("Standard");
                                            isStandard = true;
                                            break;
                                        }
                                    }
                                    if (!isStandard)
                                    {
                                        double l = L1 + L2;
                                        double l1mm = Math.Round(GeomUtil.feet2Milimeter(L1), 0);
                                        double l2mm = Math.Round(GeomUtil.feet2Milimeter(L2), 0);
                                        double lmm = Math.Round(GeomUtil.feet2Milimeter(l), 0);
                                        rb.LookupParameter("HB_Type").Set("Standard (" + l1mm + "+" + l2mm + "=" + lmm + ")");
                                    }
                                }
                                else
                                {
                                    rb.LookupParameter("HB_Type").Set("Residual");
                                }
                                if (Finish1)
                                {
                                    bool isStandard = false;
                                    foreach (double len in Variable.LStandards)
                                    {
                                        if (L1 == len || GeomUtil.IsEqual(L1, 0))
                                        {
                                            rb.LookupParameter("HB_Type").Set("Standard");
                                            isStandard = true;
                                            break;
                                        }
                                    }
                                    if (!isStandard)
                                    {
                                        rb.LookupParameter("HB_Type").Set("Residual");
                                    }
                                }
                            }
                            catch
                            {

                            }

                        }
                        break;
                }
            }
            if (L2 > 0)
            {
                switch (ShortenType2)
                {
                    case TurnShortenType.Normal:
                        {
                            int i = 0;
                            XYZ pnt = new XYZ(RebarInfo.TopUVNormals1[i].U, RebarInfo.TopUVNormals1[i].V, Start2) + vec * ((index % 2) * GeomUtil.milimeter2Feet(100)+ GeomUtil.milimeter2Feet(100));
                            List<Curve> curves = new List<Curve> { Line.CreateBound(pnt, pnt + XYZ.BasisZ * L2) };
                            Rebar rb = CreateSingleRebar(curves, 1, RebarInfo.SpacingNormals2[i], 0, RebarInfo.RebarBarType, RebarInfo.NormalNormals2[i], RebarInfo.RebarLayoutRule);
                            try
                            {
                                rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                rb.LookupParameter("Partition").Set(pr.Partition);
                                rb.LookupParameter("HB_LuoiTrinh").Set(2);
                                rb.LookupParameter("Mark").Set(pr.Mark);
                                if (LongType == LongType.Standard)
                                {
                                    bool isStandard = false;
                                    foreach (double len in Variable.LStandards)
                                    {
                                        if (L2 == len || GeomUtil.IsEqual(L2, 0))
                                        {
                                            rb.LookupParameter("HB_Type").Set("Standard");
                                            isStandard = true;
                                            break;
                                        }
                                    }
                                    if (!isStandard)
                                    {
                                        double l = L1 + L2;
                                        double l1mm = Math.Round(GeomUtil.feet2Milimeter(L1), 0);
                                        double l2mm = Math.Round(GeomUtil.feet2Milimeter(L2), 0);
                                        double lmm = Math.Round(GeomUtil.feet2Milimeter(l), 0);
                                        rb.LookupParameter("HB_Type").Set("Standard (" + l1mm + "+" + l2mm + "=" + lmm+")");
                                    }
                                }
                                else
                                {
                                    rb.LookupParameter("HB_Type").Set("Residual");
                                }
                                if (Finish2)
                                {
                                    bool isStandard = false;
                                    foreach (double len in Variable.LStandards)
                                    {
                                        if (L2 == len || GeomUtil.IsEqual(L2, 0))
                                        {
                                            rb.LookupParameter("HB_Type").Set("Standard");
                                            isStandard = true;
                                            break;
                                        }
                                    }
                                    if (!isStandard)
                                    {
                                        rb.LookupParameter("HB_Type").Set("Residual");
                                    }
                                }
                            }
                            catch
                            {

                            }
                            break;
                        }

                    default:
                        {
                            int i = 0;
                            XYZ pnt = new XYZ(RebarInfo.TopUVNormals1[i].U, RebarInfo.TopUVNormals1[i].V, Start2) + vec * ((index % 2) * GeomUtil.milimeter2Feet(100) + GeomUtil.milimeter2Feet(100));
                            List<Curve> curves = new List<Curve> { Line.CreateBound(pnt, pnt + XYZ.BasisZ * L2) };
                            Rebar rb = CreateSingleRebar(curves, 1, RebarInfo.SpacingNormals2[i], 0, RebarInfo.RebarBarType, RebarInfo.NormalNormals2[i], RebarInfo.RebarLayoutRule);
                            try
                            {
                                rb.LookupParameter("HB_SLCauKien").Set(pr.PartCount);
                                rb.LookupParameter("HB_Level").Set(pr.FindMetaLevel(ColumnInfo.Level));
                                rb.LookupParameter("Partition").Set(pr.Partition);
                                rb.LookupParameter("HB_LuoiTrinh").Set(2);
                                rb.LookupParameter("Mark").Set(pr.Mark);
                                if (LongType == LongType.Standard)
                                {
                                    bool isStandard = false;
                                    foreach (double len in Variable.LStandards)
                                    {
                                        if (L2 == len || GeomUtil.IsEqual(L2, 0))
                                        {
                                            rb.LookupParameter("HB_Type").Set("Standard");
                                            isStandard = true;
                                            break;
                                        }
                                    }
                                    if (!isStandard)
                                    {
                                        double l = L1 + L2;
                                        double l1mm = Math.Round(GeomUtil.feet2Milimeter(L1), 0);
                                        double l2mm = Math.Round(GeomUtil.feet2Milimeter(L2), 0);
                                        double lmm = Math.Round(GeomUtil.feet2Milimeter(l), 0);
                                        rb.LookupParameter("HB_Type").Set("Standard (" + l1mm + "+" + l2mm + "=" + lmm + ")");
                                    }
                                }
                                else
                                {
                                    rb.LookupParameter("HB_Type").Set("Residual");
                                }
                                if (Finish2)
                                {
                                    bool isStandard = false;
                                    foreach (double len in Variable.LStandards)
                                    {
                                        if (L2 == len || GeomUtil.IsEqual(L2, 0))
                                        {
                                            rb.LookupParameter("HB_Type").Set("Standard");
                                            isStandard = true;
                                            break;
                                        }
                                    }
                                    if (!isStandard)
                                    {
                                        rb.LookupParameter("HB_Type").Set("Residual");
                                    }
                                }
                            }
                            catch
                            {

                            }
                        }
                        break;
                }
            }
        }

        public void CreateDetailLines(List<XYZ> points)
        {
            if (L1 > 0)
            {
                XYZ p1S = new XYZ(points[0].X, points[0].Y, Start1); XYZ p2S = new XYZ(points[1].X, points[1].Y, Start1);
                XYZ p1E = new XYZ(points[0].X, points[0].Y, End1); XYZ p2E = new XYZ(points[1].X, points[1].Y, End1);
                DetailCurve dl1 = Document.Create.NewDetailCurve(Document.ActiveView, Line.CreateBound(p1S, p2S));
                DetailCurve dl2 = Document.Create.NewDetailCurve(Document.ActiveView, Line.CreateBound(p1E, p2E));
                Rebar1.LookupParameter("LineId1").Set(dl1.Id.IntegerValue);
                Rebar1.LookupParameter("LineId2").Set(dl2.Id.IntegerValue);
            }
            if (L2 > 0)
            {
                XYZ p1S = new XYZ(points[0].X, points[0].Y, Start2); XYZ p2S = new XYZ(points[1].X, points[1].Y, Start2);
                XYZ p1E = new XYZ(points[0].X, points[0].Y, End2); XYZ p2E = new XYZ(points[1].X, points[1].Y, End2);
                DetailCurve dl1 = Document.Create.NewDetailCurve(Document.ActiveView, Line.CreateBound(p1S, p2S));
                DetailCurve dl2 = Document.Create.NewDetailCurve(Document.ActiveView, Line.CreateBound(p1E, p2E));
                Rebar2.LookupParameter("LineId1").Set(dl1.Id.IntegerValue);
                Rebar2.LookupParameter("LineId2").Set(dl2.Id.IntegerValue);
            }
        }
        public Rebar CreateSingleRebar(List<Curve> curves, int num, double spac, double arrayLen, RebarBarType rbt, XYZ norm, RebarLayoutRule layout)
        {
            //XYZ pnt = new XYZ(point.U, point.V, startZ);
            //List<Curve> curves = new List<Curve> { Line.CreateBound(pnt, pnt + XYZ.BasisZ * l) };
            Rebar rb = null;
            try
            {
                rb = Rebar.CreateFromCurves(Document, RebarStyle.Standard, rbt, null, null, Element, norm, curves, RebarHookOrientation.Left, RebarHookOrientation.Left, true, true);
            }
            catch (Exception)
            {
                throw new Exception("ABC");
            }
            try
            {
                rb.LookupParameter("Comments").Set("add-in");
                rb.LookupParameter("Mark").Set(TurnIndex.ToString());
                rb.LookupParameter("Cach").Set(Index.ToString());
                rb.LookupParameter("Dimension").Set(L1mm.ToString() + " - " + L2mm.ToString());
                rb.LookupParameter("Position").Set(Position1.ToString() + " - " + Position2.ToString());
                rb.LookupParameter("Manual").Set(Implant1.ToString() + " - " + Implant2.ToString());
                rb.LookupParameter("ColumnName").Set(ColumnInfo.Level);
            }
            catch
            { }
#if Debug2016
            if (GeomUtil.IsEqual(arrayLen, 0))
            {
                rb.SetLayoutAsSingle();
                return rb;
            }
            switch (layout)
            {
                case RebarLayoutRule.Single:
                    rb.SetLayoutAsSingle();
                    break;
                case RebarLayoutRule.FixedNumber:
                    rb.SetLayoutAsFixedNumber(num, arrayLen, true, true, true);
                    break;
                case RebarLayoutRule.MaximumSpacing:
                    rb.SetLayoutAsMaximumSpacing(spac, arrayLen, true, true, true);
                    break;
                case RebarLayoutRule.MinimumClearSpacing:
                    rb.SetLayoutAsMinimumClearSpacing(spac, arrayLen, true, true, true);
                    break;
                case RebarLayoutRule.NumberWithSpacing:
                    rb.SetLayoutAsNumberWithSpacing(num, spac, true, true, true);
                    break;
            }
#elif Debug2017
            if (GeomUtil.IsEqual(arrayLen, 0))
            {
                rb.SetLayoutAsSingle();
                return rb;
            }
            switch (layout)
            {
                case RebarLayoutRule.Single:
                    rb.SetLayoutAsSingle();
                    break;
                case RebarLayoutRule.FixedNumber:
                    rb.SetLayoutAsFixedNumber(num, arrayLen, true, true, true);
                    break;
                case RebarLayoutRule.MaximumSpacing:
                    rb.SetLayoutAsMaximumSpacing(spac, arrayLen, true, true, true);
                    break;
                case RebarLayoutRule.MinimumClearSpacing:
                    rb.SetLayoutAsMinimumClearSpacing(spac, arrayLen, true, true, true);
                    break;
                case RebarLayoutRule.NumberWithSpacing:
                    rb.SetLayoutAsNumberWithSpacing(num, spac, true, true, true);
                    break;
            }
#elif Debug2018
            RebarShapeDrivenAccessor rsda = rb.GetShapeDrivenAccessor();
            if (GeomUtil.IsEqual(arrayLen, 0))
            {
                rsda.SetLayoutAsSingle();
                return rb;
            }
            switch (layout)
            {
                case RebarLayoutRule.Single:
                    rsda.SetLayoutAsSingle();
                    break;
                case RebarLayoutRule.FixedNumber:
                    rsda.SetLayoutAsFixedNumber(num, arrayLen, true, true, true);
                    break;
                case RebarLayoutRule.MaximumSpacing:
                    rsda.SetLayoutAsMaximumSpacing(spac, arrayLen, true, true, true);
                    break;
                case RebarLayoutRule.MinimumClearSpacing:
                    rsda.SetLayoutAsMinimumClearSpacing(spac, arrayLen, true, true, true);
                    break;
                case RebarLayoutRule.NumberWithSpacing:
                    rsda.SetLayoutAsNumberWithSpacing(num, spac, true, true, true);
                    break;
            }
#endif
            return rb;
        }
    }
    public enum LongType
    {
        Standard, Residual
    }
}
