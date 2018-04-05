using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry;
using System.Reflection;

namespace AutoRebaring.RebarLogistic
{
    public class RebarInfo
    {
        public GeneralNote GeneralNote { get; set; }
        public RebarBarType RebarBarType { get; set; }
        public RebarBarType RebarBarTypeAfter { get; set; }
        public RebarBarType StirrupType1 { get; set; }
        public RebarBarType StirrupTypeAfter1 { get; set; }
        public RebarBarType StirrupType2 { get; set; }
        public List<XYZ> NormalNormals1 { get; set; }
        public List<XYZ> NormalNormals2 { get; set; }
        public List<XYZ> NormalNones1 { get; set; }
        public List<XYZ> NormalNones2 { get; set; }
        public List<XYZ> NormalSmalls1 { get; set; }
        public List<XYZ> NormalSmalls2 { get; set; }
        public List<XYZ> NormalBigs1 { get; set; }
        public List<XYZ> NormalBigs2 { get; set; }
        public List<XYZ> NormalLockHeads1 { get; set; }
        public List<XYZ> NormalLockHeads2 { get; set; }
        public List<XYZ> NormalImplants1 { get; set; }
        public List<XYZ> NormalImplants2 { get; set; }
        public List<XYZ> NormalImplantShortens1 { get; set; }
        public List<XYZ> NormalImplantShortens2 { get; set; }
        public List<XYZ> NormalImplantLockHeads1 { get; set; }
        public List<XYZ> NormalImplantLockHeads2 { get; set; }
        public RebarLayoutRule RebarLayoutRule { get; set; }
        public RebarHookType RebarHookType { get; set; }
        public List<int> NumberNormals1 { get; set; }
        public List<int> NumberNormals2 { get; set; }
        public List<int> NumberNones1 { get; set; }
        public List<int> NumberNones2 { get; set; }
        public List<int> NumberSmalls1 { get; set; }
        public List<int> NumberSmalls2 { get; set; }
        public List<int> NumberBigs1 { get; set; }
        public List<int> NumberBigs2 { get; set; }
        public List<int> NumberLockHeads1 { get; set; }
        public List<int> NumberLockHeads2 { get; set; }
        public List<int> NumberImplants1 { get; set; }
        public List<int> NumberImplants2 { get; set; }
        public List<int> NumberImplantShortens1 { get; set; }
        public List<int> NumberImplantShortens2 { get; set; }
        public List<int> NumberImplantLockHeads1 { get; set; }
        public List<int> NumberImplantLockHeads2 { get; set; }
        public List<double> ArrayLengthNormals1
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberNormals1.Count; i++)
                {
                    lens.Add(SpacingNormals1[i] * (NumberNormals1[i] - 1));
                }
                return lens;
            }
        }
        public List<double> ArrayLengthNormals2
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberNormals2.Count; i++)
                {
                    lens.Add(SpacingNormals2[i] * (NumberNormals2[i] - 1));
                }
                return lens;
            }
        }
        public List<double> ArrayLengthNones1
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberNones1.Count; i++)
                {
                    lens.Add(SpacingNones1[i] * (NumberNones1[i] - 1));
                }
                return lens;
            }
        }
        public List<double> ArrayLengthNones2
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberNones2.Count; i++)
                {
                    lens.Add(SpacingNones2[i] * (NumberNones2[i] - 1));
                }
                return lens;
            }
        }
        public List<double> ArrayLengthSmalls1
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberSmalls1.Count; i++)
                {
                    lens.Add(SpacingSmalls1[i] * (NumberSmalls1[i] - 1));
                }
                return lens;
            }
        }
        public List<double> ArrayLengthSmalls2
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberSmalls2.Count; i++)
                {
                    lens.Add(SpacingSmalls2[i] * (NumberSmalls2[i] - 1));
                }
                return lens;
            }
        }
        public List<double> ArrayLengthBigs1
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberBigs1.Count; i++)
                {
                    lens.Add(SpacingBigs1[i] * (NumberBigs1[i] - 1));
                }
                return lens;
            }
        }
        public List<double> ArrayLengthBigs2
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberBigs2.Count; i++)
                {
                    lens.Add(SpacingBigs2[i] * (NumberBigs2[i] - 1));
                }
                return lens;
            }
        }
        public List<double> ArrayLengthLockHeads1
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberLockHeads1.Count; i++)
                {
                    lens.Add(SpacingLockHeads1[i] * (NumberLockHeads1[i] - 1));
                }
                return lens;
            }
        }
        public List<double> ArrayLengthLockHeads2
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberLockHeads2.Count; i++)
                {
                    lens.Add(SpacingLockHeads2[i] * (NumberLockHeads2[i] - 1));
                }
                return lens;
            }
        }
        public List<double> ArrayLengthImplants1
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberImplants1.Count; i++)
                {
                    lens.Add(SpacingImplants1[i] * (NumberImplants1[i] - 1));
                }
                return lens;
            }
        }
        public List<double> ArrayLengthImplants2
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberImplants2.Count; i++)
                {
                    lens.Add(SpacingImplants2[i] * (NumberImplants2[i] - 1));
                }
                return lens;
            }
        }
        public List<double> ArrayLengthImplantShortens1
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberImplantShortens1.Count; i++)
                {
                    lens.Add(SpacingImplantShortens1[i] * (NumberImplantShortens1[i] - 1));
                }
                return lens;
            }
        }
        public List<double> ArrayLengthImplantShortens2
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberImplantShortens2.Count; i++)
                {
                    lens.Add(SpacingImplantShortens2[i] * (NumberImplantShortens2[i] - 1));
                }
                return lens;
            }
        }
        public List<double> ArrayLengthImplantLockHeads1
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberImplantLockHeads1.Count; i++)
                {
                    lens.Add(SpacingImplantLockHeads1[i] * (NumberImplantLockHeads1[i] - 1));
                }
                return lens;
            }
        }
        public List<double> ArrayLengthImplantLockHeads2
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < NumberImplantLockHeads2.Count; i++)
                {
                    lens.Add(SpacingImplantLockHeads2[i] * (NumberImplantLockHeads2[i] - 1));
                }
                return lens;
            }
        }
        public List<double> SpacingNormals1 { get; set; }
        public List<double> SpacingNormals2 { get; set; }
        public List<double> SpacingNones1 { get; set; }
        public List<double> SpacingNones2 { get; set; }
        public List<double> SpacingSmalls1 { get; set; }
        public List<double> SpacingSmalls2 { get; set; }
        public List<double> SpacingBigs1 { get; set; }
        public List<double> SpacingBigs2 { get; set; }
        public List<double> SpacingLockHeads1 { get; set; }
        public List<double> SpacingLockHeads2 { get; set; }
        public List<double> SpacingImplants1 { get; set; }
        public List<double> SpacingImplants2 { get; set; }
        public List<double> SpacingImplantShortens1 { get; set; }
        public List<double> SpacingImplantShortens2 { get; set; }
        public List<double> SpacingImplantLockHeads1 { get; set; }
        public List<double> SpacingImplantLockHeads2 { get; set; }
        public List<UV> TopUVNormals1 { get; set; }
        public List<UV> TopUVNormals2 { get; set; }
        public List<UV> TopUVNones1 { get; set; }
        public List<UV> TopUVNones2 { get; set; }
        public List<UV> TopUVSmalls1 { get; set; }
        public List<UV> TopUVSmalls2 { get; set; }
        public List<UV> TopUVBigs1 { get; set; }
        public List<UV> TopUVBigs2 { get; set; }
        public List<UV> TopUVLockHeads1 { get; set; }
        public List<UV> TopUVLockHeads2 { get; set; }
        public List<UV> TopUVImplants1 { get; set; }
        public List<UV> TopUVImplants2 { get; set; }
        public List<UV> TopUVImplantShortens1 { get; set; }
        public List<UV> TopUVImplantShortens2 { get; set; }
        public List<UV> TopUVImplantLockHeads1 { get; set; }
        public List<UV> TopUVImplantLockHeads2 { get; set; }
        public UV TopUVStirrup1 { get; set; }
        public List<UV> TopUVStirrups2 { get; set; }
        public List<double> DimBExpandSmalls1 { get; set; }
        public List<double> DimBExpandSmalls2 { get; set; }
        public List<double> DimHExpandSmalls1
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < DimBExpandSmalls1.Count; i++)
                {
                    lens.Add(DimBExpandSmalls1[i] * GeneralNote.RatioLH);
                }
                return lens;
            }
        }
        public List<double> DimHExpandSmalls2
        {
            get
            {
                List<double> lens = new List<double>();
                for (int i = 0; i < DimBExpandSmalls2.Count; i++)
                {
                    lens.Add(DimBExpandSmalls2[i] * GeneralNote.RatioLH);
                }
                return lens;
            }
        }
        public List<double> DimensionExpandSmalls1
        {
            get
            {
                return VectorExpandSmalls1.Select(x => x.GetLength()).ToList();
            }
        }
        public List<double> DimensionExpandSmalls2
        {
            get
            {
                return VectorExpandSmalls2.Select(x => x.GetLength()).ToList();
            }
        }
        public List<XYZ> VecBExpandSmalls1 { get; set; }
        public List<XYZ> VecBExpandSmalls2 { get; set; }
        public List<XYZ> VectorExpandSmalls1
        {
            get
            {
                List<XYZ> vecs = new List<XYZ>();
                for (int i = 0; i < DimBExpandSmalls1.Count; i++)
                {
                    vecs.Add(DimBExpandSmalls1[i] * VecBExpandSmalls1[i].Normalize() + DimHExpandSmalls1[i] * XYZ.BasisZ);
                }
                return vecs;
            }
        }
        public List<XYZ> VectorExpandSmalls2
        {
            get
            {
                List<XYZ> vecs = new List<XYZ>();
                for (int i = 0; i < DimBExpandSmalls2.Count; i++)
                {
                    vecs.Add(DimBExpandSmalls2[i] * VecBExpandSmalls2[i].Normalize() + DimHExpandSmalls2[i] * XYZ.BasisZ);
                }
                return vecs;
            }
        }
        public List<XYZ> VectorExpandBigs1 { get; set; }
        public List<XYZ> VectorExpandBigs2 { get; set; }
        public List<XYZ> VectorExpandLockHeads1 { get; set; }
        public List<XYZ> VectorExpandLockHeads2 { get; set; }
        public XYZ VectorXStirrup1 { get; set; }
        public XYZ VectorYStirrup1 { get; set; }
        public List<XYZ> VectorXStirrups2 { get; set; }
        public List<XYZ> VectorYStirrups2 { get; set; }
        public double BStirrup1 { get; set; }
        public List<double> BStirrups2 { get; set; }
        public double CStirrup1 { get; set; }
        public double LockHeadLength { get { return GeneralNote.LockHeadMultiply * Diameter; } }
        public bool IsLockHead
        {
            get
            {
                if (LockHeadManual) return true;
                if (NumberBigs1.Count != 0 || NumberBigs2.Count != 0)
                    return true;
                return false;
            }
        }
        public bool LockHeadManual { get; set; }
        public double Diameter { get { return RebarBarType.BarDiameter; } }
        public double DiameterAfter { get { return RebarBarTypeAfter.BarDiameter; } }
        public double StirrupDiameter1 { get { return StirrupType1.BarDiameter; } }
        public double StirrupDiameterAfter1 { get { return StirrupTypeAfter1.BarDiameter; } }
        public string Name
        {
            get { return RebarBarType.Name; }
        }
        public RebarInfo(ColumnInfo ci, ColumnInfo ciAfter, ColumnStandardDesignInput cdi, ColumnStandardDesignInput cdiAfter, ColumnStirrupDesignInput csdi, ColumnStirrupDesignInput csdiAfter, GeneralNote gn)
        {
            GeneralNote = gn;
            LockHeadManual = false;

            RebarBarTypeAfter = cdiAfter.RebarType;
            RebarBarType = cdi.RebarType;
            RebarHookType = gn.RebarHookType;
            RebarLayoutRule = gn.RebarLayoutRule;
            StirrupType1 = csdi.StirrupType1;
            StirrupType2 = csdi.StirrupType2;
            StirrupTypeAfter1 = csdiAfter.StirrupType1;

            ci.ColumnStandardDesignInput = cdi;

            double spacb1 = (ci.B1 - gn.CoverConcrete * 2 - StirrupDiameter1 * 2 - Diameter) / (cdi.N1 - 1) * 2;
            double spacb2 = (ci.B2 - gn.CoverConcrete * 2 - StirrupDiameter1 * 2 - Diameter) / (cdi.N2 - 1) * 2;
            double spacAb1 = (ciAfter.B1 - gn.CoverConcrete * 2 - StirrupDiameter1 * 2 - DiameterAfter) / (cdiAfter.N1 - 1) * 2;
            double spacAb2 = (ciAfter.B2 - gn.CoverConcrete * 2 - StirrupDiameter1 * 2 - DiameterAfter) / (cdiAfter.N2 - 1) * 2;

            List<UV> pnts = new List<UV>
            {
                ci.BoundaryPoints[0] + (ci.VectorU +ci.VectorV) *(gn.CoverConcrete +StirrupDiameter1*gn.RebarStandardOffsetControl+ Diameter/2),
                ci.BoundaryPoints[1] + (-ci.VectorU +ci.VectorV) *(gn.CoverConcrete+StirrupDiameter1*gn.RebarStandardOffsetControl + Diameter/2),
                ci.BoundaryPoints[2] + (-ci.VectorU -ci.VectorV) *(gn.CoverConcrete+StirrupDiameter1*gn.RebarStandardOffsetControl + Diameter/2),
                ci.BoundaryPoints[3] + (ci.VectorU -ci.VectorV) *(gn.CoverConcrete +StirrupDiameter1*gn.RebarStandardOffsetControl+ Diameter/2)
            };
            List<UV> pntAs = new List<UV>
            {
                ciAfter.BoundaryPoints[0] + (ciAfter.VectorU +ciAfter.VectorV) *(gn.CoverConcrete +StirrupDiameterAfter1*gn.RebarStandardOffsetControl+ DiameterAfter/2),
                ciAfter.BoundaryPoints[1] + (-ciAfter.VectorU +ciAfter.VectorV) *(gn.CoverConcrete +StirrupDiameterAfter1*gn.RebarStandardOffsetControl+ DiameterAfter/2),
                ciAfter.BoundaryPoints[2] + (-ciAfter.VectorU -ciAfter.VectorV) *(gn.CoverConcrete +StirrupDiameterAfter1*gn.RebarStandardOffsetControl+ DiameterAfter/2),
                ciAfter.BoundaryPoints[3] + (ciAfter.VectorU -ciAfter.VectorV) *(gn.CoverConcrete +StirrupDiameterAfter1*gn.RebarStandardOffsetControl+ DiameterAfter/2)
            };
            List<UV> pntStirs = new List<UV>
            {
                ci.BoundaryPoints[0] + (ci.VectorU +ci.VectorV) *(gn.CoverConcrete +StirrupDiameter1/2),
                ci.BoundaryPoints[1] + (-ci.VectorU +ci.VectorV) *(gn.CoverConcrete+StirrupDiameter1/2),
                ci.BoundaryPoints[2] + (-ci.VectorU -ci.VectorV) *(gn.CoverConcrete+StirrupDiameter1/2),
                ci.BoundaryPoints[3] + (ci.VectorU -ci.VectorV) *(gn.CoverConcrete +StirrupDiameter1/2)
            };

            XYZ norm1 = new XYZ(ci.VectorU.U, ci.VectorU.V, 0);
            XYZ norm2 = new XYZ(ci.VectorV.U, ci.VectorV.V, 0);

            GetInfomationNormal(ci, cdi, cdiAfter, gn, spacb1, spacb2, spacAb1, spacAb2, norm1, norm2, pnts, pntAs);
            GetInfomationShorten(ci, cdi, cdiAfter, gn, spacb1, spacb2, spacAb1, spacAb2, norm1, norm2, pnts, pntAs);
            GetInfomationLockHead(ci, cdi, cdiAfter, gn, spacb1, spacb2, spacAb1, spacAb2, norm1, norm2, pnts, pntAs);

            GetInformationStirrup(ci, cdi, gn, spacb1, spacb2, norm1, norm2, pntStirs,pnts);
        }
        public void GetInformationStirrup(ColumnInfo ci, ColumnStandardDesignInput cdi, GeneralNote gn, double spacb1, double spacb2, XYZ norm1, XYZ norm2, List<UV> pntStirs, List<UV> pnts)
        {
            TopUVStirrups2 = new List<UV>();
            VectorXStirrups2 = new List<XYZ>();
            VectorYStirrups2 = new List<XYZ>();
            BStirrups2 = new List<double>();

            TopUVStirrup1 = new UV((pntStirs[0].U + pntStirs[2].U)/2, (pntStirs[0].V + pntStirs[2].V) / 2);
            VectorXStirrup1 = norm1; VectorYStirrup1 = norm1.CrossProduct(XYZ.BasisZ);
            BStirrup1 = ci.B1 - gn.CoverConcrete * 2;
            CStirrup1 = ci.B2 - gn.CoverConcrete * 2;

            UV pnt1 = new UV((pnts[0].U + pnts[3].U) / 2, (pnts[0].V + pnts[3].V) / 2);
            UV pnt2 = new UV((pnts[1].U + pnts[2].U) / 2, (pnts[1].V + pnts[2].V) / 2);
            int n = cdi.N1 - 4;
            if (n > 0)
            {
                for (int i = 0; i < n / 2; i++)
                {
                    AddRebarStirrup2(TopUVStirrups2, VectorXStirrups2, VectorYStirrups2, BStirrups2, pnt1 + ((i+1) * spacb1 + Diameter) * ci.VectorU, norm2, norm2.CrossProduct(XYZ.BasisZ), ci.B2 - gn.CoverConcrete * 2);
                }
                AddRebarStirrup2(TopUVStirrups2, VectorXStirrups2, VectorYStirrups2, BStirrups2, pnt1 + ((cdi.N1-3)*spacb1/2 + Diameter) * ci.VectorU, norm2, norm2.CrossProduct(XYZ.BasisZ), ci.B2 - gn.CoverConcrete * 2);
            }

            pnt1 = new UV((pnts[0].U + pnts[1].U) / 2, (pnts[0].V + pnts[1].V) / 2);
            pnt2 = new UV((pnts[2].U + pnts[3].U) / 2, (pnts[2].V + pnts[3].V) / 2);
            n = cdi.N2 - 4;
            if (n > 0)
            {
                for (int i = 0; i < n / 2; i++)
                {
                    AddRebarStirrup2(TopUVStirrups2, VectorXStirrups2, VectorYStirrups2, BStirrups2, pnt1 + ((i + 1) * spacb2 - Diameter) * ci.VectorV, norm1, norm1.CrossProduct(XYZ.BasisZ), ci.B1 - gn.CoverConcrete * 2);
                }
                AddRebarStirrup2(TopUVStirrups2, VectorXStirrups2, VectorYStirrups2, BStirrups2, pnt1 + ((cdi.N2 - 3) * spacb2 / 2 - Diameter) * ci.VectorV, norm1, norm1.CrossProduct(XYZ.BasisZ), ci.B1 - gn.CoverConcrete * 2);
            }
        }
        public bool AddRebarStirrup2(List<UV> topUVs, List<XYZ> vecXs, List<XYZ> vecYs, List<double> bs, UV topUV, XYZ vecX, XYZ vecY, double b)
        {
            topUVs.Add(topUV); vecXs.Add(vecX); vecYs.Add(vecY); bs.Add(b);
            return true;
        }
        public void GetInfomationNormal(ColumnInfo ci, ColumnStandardDesignInput cdi, ColumnStandardDesignInput cdiAfter, GeneralNote gn, double spacb1, double spacb2, double spacAb1, double spacAb2, XYZ norm1, XYZ norm2, List<UV> pnts, List<UV> pntAs)
        {
            #region Initiate
            NormalNormals1 = new List<XYZ>();
            NormalNormals2 = new List<XYZ>();
            NormalImplants1 = new List<XYZ>();
            NormalImplants2 = new List<XYZ>();

            NumberNormals1 = new List<int>();
            NumberNormals2 = new List<int>();
            NumberImplants1 = new List<int>();
            NumberImplants2 = new List<int>();

            TopUVNormals1 = new List<UV>();
            TopUVNormals2 = new List<UV>();
            TopUVImplants1 = new List<UV>();
            TopUVImplants2 = new List<UV>();

            SpacingNormals1 = new List<double>();
            SpacingNormals2 = new List<double>();
            SpacingImplants1 = new List<double>();
            SpacingImplants2 = new List<double>();
            #endregion

            #region U Direction
            AddRebarArrayNormal(TopUVNormals1, SpacingNormals1, NumberNormals1, NormalNormals1, pnts[0], spacb1, cdi.N1 % 2 == 0 ? cdi.N1 / 2 : cdi.N1 / 2 + 1, norm1);
            AddRebarArrayNormal(TopUVNormals1, SpacingNormals1, NumberNormals1, NormalNormals1, pnts[3], spacb1, cdi.N1 % 2 == 0 ? cdi.N1 / 2 : cdi.N1 / 2 + 1, norm1);
            AddRebarArrayNormal(TopUVNormals2, SpacingNormals2, NumberNormals2, NormalNormals2, pnts[0] + ci.VectorU * spacb1 / 2, spacb1, cdi.N1 / 2, norm1);
            AddRebarArrayNormal(TopUVNormals2, SpacingNormals2, NumberNormals2, NormalNormals2, pnts[3] + ci.VectorU * spacb1 / 2, spacb1, cdi.N1 / 2, norm1);

            int numImpU = cdiAfter.N1 - cdi.N1;
            if (numImpU > 0)
            {
                AddRebarArrayNormal(TopUVImplants2, SpacingImplants2, NumberImplants2, NormalImplants2, pnts[0] + ci.VectorU * spacb1 / 4, spacb1, numImpU % 2 == 0 ? numImpU / 2 : numImpU / 2 + 1, norm1);
                AddRebarArrayNormal(TopUVImplants2, SpacingImplants2, NumberImplants2, NormalImplants2, pnts[3] + ci.VectorU * spacb1 / 4, spacb1, numImpU % 2 == 0 ? numImpU / 2 : numImpU / 2 + 1, norm1);
                AddRebarArrayNormal(TopUVImplants1, SpacingImplants1, NumberImplants1, NormalImplants1, pnts[0] + ci.VectorU * spacb1 * 3 / 4, spacb1, numImpU / 2, norm1);
                AddRebarArrayNormal(TopUVImplants1, SpacingImplants1, NumberImplants1, NormalImplants1, pnts[3] + ci.VectorU * spacb1 * 3 / 4, spacb1, numImpU / 2, norm1);
            }
            #endregion

            #region V Direction
            AddRebarArrayNormal(TopUVNormals1, SpacingNormals1, NumberNormals1, NormalNormals1, pnts[0] + ci.VectorV * spacb2, spacb2, (cdi.N2 - 2) / 2, norm2);
            AddRebarArrayNormal(TopUVNormals1, SpacingNormals1, NumberNormals1, NormalNormals1, pnts[1] + ci.VectorV * spacb2, spacb2, (cdi.N2 - 2) / 2, norm2);
            AddRebarArrayNormal(TopUVNormals2, SpacingNormals2, NumberNormals2, NormalNormals2, pnts[0] + ci.VectorV * spacb2 / 2, spacb2, cdi.N2 % 2 == 0 ? (cdi.N2 - 2) / 2 : (cdi.N2 - 2) / 2 + 1, norm2);
            AddRebarArrayNormal(TopUVNormals2, SpacingNormals2, NumberNormals2, NormalNormals2, pnts[1] + ci.VectorV * spacb2 / 2, spacb2, cdi.N2 % 2 == 0 ? (cdi.N2 - 2) / 2 : (cdi.N2 - 2) / 2 + 1, norm2);

            int numImpV = cdiAfter.N2 - cdi.N2;
            if (numImpV > 0)
            {
                AddRebarArrayNormal(TopUVImplants1, SpacingImplants1, NumberImplants1, NormalImplants1, pnts[0] + ci.VectorV * spacb2 / 4, spacb2, numImpV % 2 == 0 ? numImpV / 2 : numImpV / 2 + 1, norm2);
                AddRebarArrayNormal(TopUVImplants1, SpacingImplants1, NumberImplants1, NormalImplants1, pnts[1] + ci.VectorV * spacb2 / 4, spacb2, numImpV % 2 == 0 ? numImpV / 2 : numImpV / 2 + 1, norm2);
                AddRebarArrayNormal(TopUVImplants2, SpacingImplants2, NumberImplants2, NormalImplants2, pnts[0] + ci.VectorV * spacb2 * 3 / 4, spacb2, numImpV / 2, norm2);
                AddRebarArrayNormal(TopUVImplants2, SpacingImplants2, NumberImplants2, NormalImplants2, pnts[1] + ci.VectorV * spacb2 * 3 / 4, spacb2, numImpV / 2, norm2);
            }
            #endregion
        }
        public void GetInfomationShorten(ColumnInfo ci, ColumnStandardDesignInput cdi, ColumnStandardDesignInput cdiAfter, GeneralNote gn, double spacb1, double spacb2, double spacAb1, double spacAb2, XYZ norm1, XYZ norm2, List<UV> pnts, List<UV> pntAs)
        {
            #region Initiate
            NormalNones1 = new List<XYZ>();
            NormalNones2 = new List<XYZ>();
            NormalSmalls1 = new List<XYZ>();
            NormalSmalls2 = new List<XYZ>();
            NormalBigs1 = new List<XYZ>();
            NormalBigs2 = new List<XYZ>();
            NormalImplantShortens1 = new List<XYZ>();
            NormalImplantShortens2 = new List<XYZ>();

            NumberNones1 = new List<int>();
            NumberNones2 = new List<int>();
            NumberSmalls1 = new List<int>();
            NumberSmalls2 = new List<int>();
            NumberBigs1 = new List<int>();
            NumberBigs2 = new List<int>();
            NumberImplantShortens1 = new List<int>();
            NumberImplantShortens2 = new List<int>();

            TopUVNones1 = new List<UV>();
            TopUVNones2 = new List<UV>();
            TopUVSmalls1 = new List<UV>();
            TopUVSmalls2 = new List<UV>();
            TopUVBigs1 = new List<UV>();
            TopUVBigs2 = new List<UV>();
            TopUVImplantShortens1 = new List<UV>();
            TopUVImplantShortens2 = new List<UV>();

            SpacingNones1 = new List<double>();
            SpacingNones2 = new List<double>();
            SpacingSmalls1 = new List<double>();
            SpacingSmalls2 = new List<double>();
            SpacingBigs1 = new List<double>();
            SpacingBigs2 = new List<double>();
            SpacingImplantShortens1 = new List<double>();
            SpacingImplantShortens2 = new List<double>();

            DimBExpandSmalls1 = new List<double>();
            DimBExpandSmalls2 = new List<double>();

            VecBExpandSmalls1 = new List<XYZ>();
            VecBExpandSmalls2 = new List<XYZ>();

            VectorExpandBigs1 = new List<XYZ>();
            VectorExpandBigs2 = new List<XYZ>();
            #endregion

            GetInformationShortenV(ci, cdi, cdiAfter, gn, spacb1, spacb2, spacAb1, spacAb2, norm1, norm2, pnts, pntAs, ci.ShortenV1, 0);
            GetInformationShortenV(ci, cdi, cdiAfter, gn, spacb1, spacb2, spacAb1, spacAb2, norm1, norm2, pnts, pntAs, ci.ShortenV2, 3);
            GetInformationShortenU(ci, cdi, cdiAfter, gn, spacb1, spacb2, spacAb1, spacAb2, norm1, norm2, pnts, pntAs, ci.ShortenU1, 0);
            GetInformationShortenU(ci, cdi, cdiAfter, gn, spacb1, spacb2, spacAb1, spacAb2, norm1, norm2, pnts, pntAs, ci.ShortenU2, 1);
        }
        public void GetInformationShortenV(ColumnInfo ci, ColumnStandardDesignInput cdi, ColumnStandardDesignInput cdiAfter, GeneralNote gn, double spacb1, double spacb2, double spacAb1, double spacAb2, XYZ norm1, XYZ norm2, List<UV> pnts, List<UV> pntAs, ColumnShortenType shortenV, int index)
        {
            XYZ vecExpBig = (index == 0 ? -norm2 : norm2);
            XYZ vecExpSmall = (index == 0 ? norm2 : -norm2);
            double dimExpSmall = (index == 0 ? ci.DeltaV1 : ci.DeltaV2) + (DiameterAfter - Diameter) / 2;
            switch (shortenV)
            {
                case ColumnShortenType.Big:
                    {
                        AddRebarArrayBig(TopUVBigs1, SpacingBigs1, NumberBigs1, NormalBigs1, VectorExpandBigs1, pnts[index], spacb1, cdi.N1 % 2 == 0 ? cdi.N1 / 2 : cdi.N1 / 2 + 1, norm1, vecExpBig);
                        AddRebarArrayBig(TopUVBigs2, SpacingBigs2, NumberBigs2, NormalBigs2, VectorExpandBigs2, pnts[index] + ci.VectorU * spacb1 / 2, spacb1, cdi.N1 / 2, norm1, vecExpBig);

                        AddRebarArrayNormal(TopUVImplantShortens1, SpacingImplantShortens1, NumberImplantShortens1, NormalImplantShortens1, pntAs[index], spacAb1, cdiAfter.N1 % 2 == 0 ? cdiAfter.N1 / 2 : cdiAfter.N1 / 2 + 1, norm1);
                        AddRebarArrayNormal(TopUVImplantShortens2, SpacingImplantShortens2, NumberImplantShortens2, NormalImplantShortens2, pntAs[index] + ci.VectorU * spacAb1 / 2, spacAb1, cdiAfter.N1 / 2, norm1);
                        break;
                    }
                case ColumnShortenType.Small:
                    {
                        int numBigU1 = 0, numSmallU1 = 0;
                        for (int i = 0; i < cdi.N1; i++)
                        {
                            double del = ci.DeltaU1 - spacb1 / 2 * i;
                            if (GeomUtil.IsEqual(del, gn.ShortenLimit) || del > gn.ShortenLimit)
                            {
                                numBigU1 = i + 1;
                            }
                            else if (GeomUtil.IsBigger(del, 0) && GeomUtil.IsSmaller(del, gn.ShortenLimit))
                            {
                                numSmallU1 = i + 1 - numBigU1;
                            }
                            else if (GeomUtil.IsEqual(del, 0) || del < 0)
                            {
                                break;
                            }
                        }

                        int numBigU2 = 0, numSmallU2 = 0;
                        for (int i = 0; i < cdi.N1; i++)
                        {
                            double del = ci.DeltaU2 - spacb1 / 2 * i;
                            if (GeomUtil.IsEqual(del, gn.ShortenLimit) || del > gn.ShortenLimit)
                            {
                                numBigU2 = i + 1;
                            }
                            else if (GeomUtil.IsBigger(del, 0) && GeomUtil.IsSmaller(del, gn.ShortenLimit))
                            {
                                numSmallU2 = i + 1 - numBigU2;
                            }
                            else if (GeomUtil.IsEqual(del, 0) || del < 0)
                            {
                                break;
                            }
                        }

                        AddRebarArrayBig(TopUVBigs1, SpacingBigs1, NumberBigs1, NormalBigs1, VectorExpandBigs1, pnts[index], spacb1, numBigU1 % 2 == 0 ? numBigU1 / 2 : numBigU1 / 2 + 1, norm1, vecExpBig);
                        AddRebarArrayBig(TopUVBigs2, SpacingBigs2, NumberBigs2, NormalBigs2, VectorExpandBigs2, pnts[index] + ci.VectorU * spacb1 / 2, spacb1, numBigU1 / 2, norm1, vecExpBig);

                        int num = numBigU1, num2 = numSmallU1;
                        for (int i = 0; i < numSmallU1; i++)
                        {
                            UV uvExpSmall2 = ci.VectorU * (ci.DeltaU1 + (DiameterAfter - Diameter) / 2 - spacb1 / 2 * (numBigU1 + i)) + ci.VectorV * dimExpSmall * (index == 0 ? 1 : -1);
                            XYZ vecExpSmall2 = new XYZ(uvExpSmall2.U, uvExpSmall2.V, 0);
                            XYZ normExpSmall2 = vecExpSmall2.Normalize().CrossProduct(XYZ.BasisZ);
                            if (num % 2 == 0)
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                            else
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                        }

                        num = numBigU1 + numSmallU1; num2 = cdi.N1 - numBigU1 - numSmallU1 - numBigU2 - numSmallU2;
                        if (num % 2 == 0)
                        {
                            AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorU * spacb1 / 2 * num, spacb1, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm1, dimExpSmall, vecExpSmall);
                            AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorU * spacb1 / 2 * (num + 1), spacb1, num2 / 2, norm1, dimExpSmall, vecExpSmall);
                        }
                        else
                        {
                            AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorU * spacb1 / 2 * num, spacb1, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm1, dimExpSmall, vecExpSmall);
                            AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorU * spacb1 / 2 * (num + 1), spacb1, num2 / 2, norm1, dimExpSmall, vecExpSmall);
                        }

                        num = num + num2; num2 = numSmallU2;
                        for (int i = 0; i < numSmallU2; i++)
                        {
                            UV uvExpSmall2 = -ci.VectorU * (ci.DeltaU2 + (DiameterAfter - Diameter ) / 2 - spacb1 / 2 * (numBigU2 + (numSmallU2 - 1 - i))) + ci.VectorV * dimExpSmall * (index == 0 ? 1 : -1);
                            XYZ vecExpSmall2 = new XYZ(uvExpSmall2.U, uvExpSmall2.V, 0);
                            XYZ normExpSmall2 = vecExpSmall2.Normalize().CrossProduct(XYZ.BasisZ);
                            if (num % 2 == 0)
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                            else
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                        }

                        num = num + num2; num2 = numBigU2;
                        if (num % 2 == 0)
                        {
                            AddRebarArrayBig(TopUVBigs1, SpacingBigs1, NumberBigs1, NormalBigs1, VectorExpandBigs1, pnts[index] + ci.VectorU * spacb1 / 2 * num, spacb1, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm1, vecExpBig);
                            AddRebarArrayBig(TopUVBigs2, SpacingBigs2, NumberBigs2, NormalBigs2, VectorExpandBigs2, pnts[index] + ci.VectorU * spacb1 / 2 * (num + 1), spacb1, num2 / 2, norm1, vecExpBig);
                        }
                        else
                        {
                            AddRebarArrayBig(TopUVBigs2, SpacingBigs2, NumberBigs2, NormalBigs2, VectorExpandBigs2, pnts[index] + ci.VectorU * spacb1 / 2 * num, spacb1, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm1, vecExpBig);
                            AddRebarArrayBig(TopUVBigs1, SpacingBigs1, NumberBigs1, NormalBigs1, VectorExpandBigs1, pnts[index] + ci.VectorU * spacb1 / 2 * (num + 1), spacb1, num2 / 2, norm1, vecExpBig);
                        }

                        int numImpU = cdiAfter.N1 - cdi.N1 + numBigU1 + numBigU2;
                        if (numImpU > 0)
                        {
                            UV firstPnt = new UV(pntAs[index].U, (pnts[index] + ci.VectorU * spacb1 / 2 * (numBigU1 + numSmallU1 + 1 / 2)).V);
                            AddRebarArrayNormal(TopUVImplantShortens1, SpacingImplantShortens1, NumberImplantShortens1, NormalImplantShortens1, firstPnt, spacb1, numImpU % 2 == 0 ? numImpU / 2 : numImpU / 2 + 1, norm1);
                            AddRebarArrayNormal(TopUVImplantShortens2, SpacingImplantShortens2, NumberImplantShortens2, NormalImplantShortens2, firstPnt + ci.VectorU * spacb1 / 2, spacb1, numImpU / 2, norm1);
                        }
                        break;
                    }
                case ColumnShortenType.None:
                    {
                        int numBigU1 = 0, numSmallU1 = 0;
                        for (int i = 0; i < cdi.N1; i++)
                        {
                            double del = ci.DeltaU1 - spacb1 / 2 * i;
                            if (GeomUtil.IsEqual(del, gn.ShortenLimit) || del > gn.ShortenLimit)
                            {
                                numBigU1 = i + 1;
                            }
                            else if (GeomUtil.IsBigger(del, 0) && GeomUtil.IsSmaller(del, gn.ShortenLimit))
                            {
                                numSmallU1 = i + 1 - numBigU1;
                            }
                            else
                            {
                                break;
                            }
                        }

                        int numBigU2 = 0, numSmallU2 = 0;
                        for (int i = 0; i < cdi.N1; i++)
                        {
                            double del = ci.DeltaU2 - spacb1 / 2 * i;
                            if (GeomUtil.IsEqual(del, gn.ShortenLimit) || del > gn.ShortenLimit)
                            {
                                numBigU2 = i + 1;
                            }
                            else if (GeomUtil.IsBigger(del, 0) && GeomUtil.IsSmaller(del, gn.ShortenLimit))
                            {
                                numSmallU2 = i + 1 - numBigU2;
                            }
                            else
                            {
                                break;
                            }
                        }

                        AddRebarArrayBig(TopUVBigs1, SpacingBigs1, NumberBigs1, NormalBigs1, VectorExpandBigs1, pnts[index], spacb1, numBigU1 % 2 == 0 ? numBigU1 / 2 : numBigU1 / 2 + 1, norm1, vecExpBig);
                        AddRebarArrayBig(TopUVBigs2, SpacingBigs2, NumberBigs2, NormalBigs2, VectorExpandBigs2, pnts[index] + ci.VectorU * spacb1 / 2, spacb1, numBigU1 / 2, norm1, vecExpBig);

                        int num = numBigU1, num2 = numSmallU1;
                        for (int i = 0; i < numSmallU1; i++)
                        {
                            UV uvExpSmall2 = ci.VectorU * (ci.DeltaU1 + (DiameterAfter - Diameter) / 2 - spacb1 / 2 * (numBigU1 + i));
                            XYZ vecExpSmall2 = new XYZ(uvExpSmall2.U, uvExpSmall2.V, 0);
                            XYZ normExpSmall2 = vecExpSmall2.Normalize().CrossProduct(XYZ.BasisZ);
                            if (num % 2 == 0)
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                            else
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                        }

                        num2 = cdi.N1 - numBigU1 - numSmallU1 - numBigU2 - numSmallU2;
                        num = num + numSmallU1;
                        if (num % 2 == 0)
                        {
                            AddRebarArrayNormal(TopUVNones1, SpacingNones1, NumberNones1, NormalNones1, pnts[index] + ci.VectorU * spacb1 / 2 * num, spacb1, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm1);
                            AddRebarArrayNormal(TopUVNones2, SpacingNones2, NumberNones2, NormalNones2, pnts[index] + ci.VectorU * spacb1 / 2 * (num + 1), spacb1, num2 / 2, norm1);
                        }
                        else
                        {
                            AddRebarArrayNormal(TopUVNones2, SpacingNones2, NumberNones2, NormalNones2, pnts[index] + ci.VectorU * spacb1 / 2 * num, spacb1, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm1);
                            AddRebarArrayNormal(TopUVNones1, SpacingNones1, NumberNones1, NormalNones1, pnts[index] + ci.VectorU * spacb1 / 2 * (num + 1), spacb1, num2 / 2, norm1);
                        }

                        num = num + num2; num2 = numSmallU2;
                        for (int i = 0; i < numSmallU2; i++)
                        {
                            UV uvExpSmall2 = -ci.VectorU * (ci.DeltaU2 + (DiameterAfter - Diameter) / 2 - spacb1 / 2 * (numBigU2 + (numSmallU2 - 1 - i)));
                            XYZ vecExpSmall2 = new XYZ(uvExpSmall2.U, uvExpSmall2.V, 0);
                            XYZ normExpSmall2 = vecExpSmall2.Normalize().CrossProduct(XYZ.BasisZ);
                            if (num % 2 == 0)
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                            else
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorU * spacb1 / 2 * (num + i), spacb1, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                        }

                        num = num + num2; num2 = numBigU2;
                        if (num % 2 == 0)
                        {
                            AddRebarArrayBig(TopUVBigs1, SpacingBigs1, NumberBigs1, NormalBigs1, VectorExpandBigs1, pnts[index] + ci.VectorU * spacb1 / 2 * num, spacb1, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm1, vecExpBig);
                            AddRebarArrayBig(TopUVBigs2, SpacingBigs2, NumberBigs2, NormalBigs2, VectorExpandBigs2, pnts[index] + ci.VectorU * spacb1 / 2 * (num + 1), spacb1, num2 / 2, norm1, vecExpBig);
                        }
                        else
                        {
                            AddRebarArrayBig(TopUVBigs2, SpacingBigs2, NumberBigs2, NormalBigs2, VectorExpandBigs2, pnts[index] + ci.VectorU * spacb1 / 2 * num, spacb1, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm1, vecExpBig);
                            AddRebarArrayBig(TopUVBigs1, SpacingBigs1, NumberBigs1, NormalBigs1, VectorExpandBigs1, pnts[index] + ci.VectorU * spacb1 / 2 * (num + 1), spacb1, num2 / 2, norm1, vecExpBig);
                        }

                        int numImpU = cdiAfter.N1 - cdi.N1 + numBigU1 + numBigU2;
                        if (numImpU > 0)
                        {
                            UV firstPnt = new UV(pntAs[index].U, (pnts[index] + ci.VectorU * spacb1 / 2 * (numBigU1 + numSmallU1 + 1 / 2)).V);
                            AddRebarArrayNormal(TopUVImplantShortens1, SpacingImplantShortens1, NumberImplantShortens1, NormalImplantShortens1, firstPnt, spacb1, numImpU % 2 == 0 ? numImpU / 2 : numImpU / 2 + 1, norm1);
                            AddRebarArrayNormal(TopUVImplantShortens2, SpacingImplantShortens2, NumberImplantShortens2, NormalImplantShortens2, firstPnt + ci.VectorU * spacb1 / 2, spacb1, numImpU / 2, norm1);
                        }
                        break;
                    }
            }
        }
        public void GetInformationShortenU(ColumnInfo ci, ColumnStandardDesignInput cdi, ColumnStandardDesignInput cdiAfter, GeneralNote gn, double spacb1, double spacb2, double spacAb1, double spacAb2, XYZ norm1, XYZ norm2, List<UV> pnts, List<UV> pntAs, ColumnShortenType shortenU, int index)
        {
            XYZ vecExpBig = (index == 0 ? -norm1 : norm1);
            XYZ vecExpSmall = (index == 0 ? norm1 : -norm1);
            double dimExpSmall = (index == 0 ? ci.DeltaU1 : ci.DeltaU2) + (DiameterAfter - Diameter) / 2;
            switch (shortenU)
            {
                case ColumnShortenType.Big:
                    {
                        AddRebarArrayBig(TopUVBigs2, SpacingBigs2, NumberBigs2, NormalBigs2, VectorExpandBigs2, pnts[index] + ci.VectorV * spacb2 / 2, spacb2, cdi.N2 % 2 == 0 ? (cdi.N2 - 2) / 2 : (cdi.N2 - 2) / 2 + 1, norm2, vecExpBig);
                        AddRebarArrayBig(TopUVBigs1, SpacingBigs1, NumberBigs1, NormalBigs1, VectorExpandBigs1, pnts[index] + ci.VectorV * spacb2, spacb2, (cdi.N2 - 2) / 2, norm2, vecExpBig);

                        AddRebarArrayNormal(TopUVImplantShortens2, SpacingImplantShortens2, NumberImplantShortens2, NormalImplantShortens2, pntAs[index] + ci.VectorV * spacAb2 / 2, spacAb2, cdiAfter.N2 % 2 == 0 ? (cdiAfter.N2 - 2) / 2 : (cdiAfter.N2 - 2) / 2 + 1, norm2);
                        AddRebarArrayNormal(TopUVImplantShortens1, SpacingImplantShortens1, NumberImplantShortens1, NormalImplantShortens1, pntAs[index] + ci.VectorV * spacAb2, spacAb2, (cdiAfter.N2 - 2) / 2, norm2);
                        break;
                    }
                case ColumnShortenType.Small:
                    {
                        int numBigV1 = 0, numSmallV1 = 0;
                        for (int i = 0; i < cdi.N2; i++)
                        {
                            double del = ci.DeltaV1 - spacb2 / 2 * (i + 1);
                            if (GeomUtil.IsEqual(del, gn.ShortenLimit) || del > gn.ShortenLimit)
                            {
                                numBigV1 = i + 1;
                            }
                            else if (GeomUtil.IsEqual(del, 0) && GeomUtil.IsSmaller(del, gn.ShortenLimit))
                            {
                                numSmallV1 = i + 1 - numBigV1;
                            }
                            else
                            {
                                break;
                            }
                        }

                        int numBigV2 = 0, numSmallV2 = 0;
                        for (int i = 0; i < cdi.N2; i++)
                        {
                            double del = ci.DeltaV2 - spacb2 / 2 * (i + 1);
                            if (GeomUtil.IsEqual(del, gn.ShortenLimit) || del > gn.ShortenLimit)
                            {
                                numBigV2 = i + 1;
                            }
                            else if (GeomUtil.IsEqual(del, 0) && GeomUtil.IsSmaller(del, gn.ShortenLimit))
                            {
                                numSmallV2 = i + 1 - numBigV2;
                            }
                            else
                            {
                                break;
                            }
                        }

                        AddRebarArrayBig(TopUVBigs2, SpacingBigs2, NumberBigs2, NormalBigs2, VectorExpandBigs2, pnts[index] + ci.VectorV * spacb2 / 2, spacb2, numBigV1 % 2 == 0 ? numBigV1 / 2 : numBigV1 / 2 + 1, norm2, vecExpBig);
                        AddRebarArrayBig(TopUVBigs1, SpacingBigs1, NumberBigs1, NormalBigs1, VectorExpandBigs1, pnts[index] + ci.VectorV * spacb2, spacb2, numBigV1 / 2, norm2, vecExpBig);

                        int num = numBigV1, num2 = numSmallV1;
                        for (int i = 0; i < numSmallV1; i++)
                        {

                            UV uvExpSmall2 = ci.VectorV * (ci.DeltaV1 + (DiameterAfter - Diameter) / 2 - spacb2 / 2 * (numBigV1 + i + 1)) + ci.VectorU * dimExpSmall * (index == 0 ? 1 : -1);
                            XYZ vecExpSmall2 = new XYZ(uvExpSmall2.U, uvExpSmall2.V, 0);
                            XYZ normExpSmall2 = vecExpSmall2.Normalize().CrossProduct(XYZ.BasisZ);
                            if (num % 2 == 0)
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                            else
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                        }

                        num = num + num2; num2 = cdi.N2 - 2 - numBigV1 - numSmallV1 - numBigV2 - numSmallV2;
                        if (num % 2 == 0)
                        {
                            AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1), spacb2, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm2, dimExpSmall, vecExpSmall);
                            AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 2), spacb2, num2 / 2, norm2, dimExpSmall, vecExpSmall);
                        }
                        else
                        {
                            AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1), spacb2, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm2, dimExpSmall, vecExpSmall);
                            AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 2), spacb2, num2 / 2, norm2, dimExpSmall, vecExpSmall);
                        }

                        num = num + num2; num2 = numSmallV2;
                        for (int i = 0; i < numSmallV2; i++)
                        {
                            UV uvExpSmall2 = -ci.VectorV * (ci.DeltaV2 + (DiameterAfter - Diameter) / 2 - spacb2 / 2 * (numBigV2 + (numSmallV2 - 1 - i) + 1)) + ci.VectorU * dimExpSmall * (index == 0 ? 1 : -1);
                            XYZ vecExpSmall2 = new XYZ(uvExpSmall2.U, uvExpSmall2.V, 0);
                            XYZ normExpSmall2 = vecExpSmall2.Normalize().CrossProduct(XYZ.BasisZ);
                            if (num % 2 == 0)
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                            else
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                        }

                        num = num + num2; num2 = numBigV2;
                        if (num % 2 == 0)
                        {
                            AddRebarArrayBig(TopUVBigs2, SpacingBigs2, NumberBigs2, NormalBigs2, VectorExpandBigs2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1), spacb2, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm2, vecExpBig);
                            AddRebarArrayBig(TopUVBigs1, SpacingBigs1, NumberBigs1, NormalBigs1, VectorExpandBigs1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 2), spacb2, num2 / 2, norm2, vecExpBig);
                        }
                        else
                        {
                            AddRebarArrayBig(TopUVBigs1, SpacingBigs1, NumberBigs1, NormalBigs1, VectorExpandBigs1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1), spacb2, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm2, vecExpBig);
                            AddRebarArrayBig(TopUVBigs2, SpacingBigs2, NumberBigs2, NormalBigs2, VectorExpandBigs2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 2), spacb2, num2 / 2, norm2, vecExpBig);
                        }

                        int numImpV = cdiAfter.N2 - cdi.N2 + numBigV1 + numBigV2;
                        if (numImpV > 0)
                        {
                            UV firstPnt = new UV((pnts[index] + ci.VectorV * spacb2 / 2 * (1 + numBigV1 + numSmallV1 + 1 / 2)).U, pntAs[index].V);
                            AddRebarArrayNormal(TopUVImplantShortens2, SpacingImplantShortens2, NumberImplantShortens2, NormalImplantShortens2, firstPnt, spacb2, numImpV % 2 == 0 ? numImpV / 2 : numImpV / 2 + 1, norm2);
                            AddRebarArrayNormal(TopUVImplantShortens1, SpacingImplantShortens1, NumberImplantShortens1, NormalImplantShortens1, firstPnt + ci.VectorV * spacb2 / 2, spacb2, numImpV / 2, norm2);
                        }
                        break;
                    }
                case ColumnShortenType.None:
                    {
                        int numBigV1 = 0, numSmallV1 = 0;
                        for (int i = 0; i < cdi.N2; i++)
                        {
                            double del = ci.DeltaV1 - spacb2 / 2 * (i + 1);
                            if (GeomUtil.IsEqual(del, gn.ShortenLimit) || del > gn.ShortenLimit)
                            {
                                numBigV1 = i + 1;
                            }
                            else if ((GeomUtil.IsEqual(del, 0) || del > 0) && GeomUtil.IsSmaller(del, gn.ShortenLimit))
                            {
                                numSmallV1 = i + 1 - numBigV1;
                            }
                            else if (GeomUtil.IsSmaller(del, 0))
                            {
                                break;
                            }
                        }

                        int numBigV2 = 0, numSmallV2 = 0;
                        for (int i = 0; i < cdi.N2; i++)
                        {
                            double del = ci.DeltaV2 - spacb2 / 2 * (i + 1);
                            if (GeomUtil.IsEqual(del, gn.ShortenLimit) || del > gn.ShortenLimit)
                            {
                                numBigV2 = i + 1;
                            }
                            else if ((GeomUtil.IsEqual(del, 0) || del > 0) && GeomUtil.IsSmaller(del, gn.ShortenLimit))
                            {
                                numSmallV2 = i + 1 - numBigV2;
                            }
                            else if (GeomUtil.IsSmaller(del, 0))
                            {
                                break;
                            }
                        }

                        AddRebarArrayBig(TopUVBigs2, SpacingBigs2, NumberBigs2, NormalBigs2, VectorExpandBigs2, pnts[index] + ci.VectorV * spacb2 / 2, spacb2, numBigV1 % 2 == 0 ? numBigV1 / 2 : numBigV1 / 2 + 1, norm2, vecExpBig);
                        AddRebarArrayBig(TopUVBigs1, SpacingBigs1, NumberBigs1, NormalBigs1, VectorExpandBigs1, pnts[index] + ci.VectorV * spacb2, spacb2, numBigV1 / 2, norm2, vecExpBig);

                        int num = numBigV1, num2 = numSmallV1;
                        for (int i = 0; i < numSmallV1; i++)
                        {
                            UV uvExpSmall2 = ci.VectorV * (ci.DeltaV1 + (DiameterAfter - Diameter) / 2 - spacb2 / 2 * (numBigV1 + i + 1));
                            XYZ vecExpSmall2 = new XYZ(uvExpSmall2.U, uvExpSmall2.V, 0);
                            XYZ normExpSmall2 = vecExpSmall2.Normalize().CrossProduct(XYZ.BasisZ);
                            if (num % 2 == 0)
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                            else
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                        }

                        num = num + num2;
                        num2 = cdi.N2 - 2 - numBigV1 - numSmallV1 - numBigV2 - numSmallV2;
                        if (num % 2 == 0)
                        {
                            AddRebarArrayNormal(TopUVNones2, SpacingNones2, NumberNones2, NormalNones2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1), spacb2, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm2);
                            AddRebarArrayNormal(TopUVNones1, SpacingNones1, NumberNones1, NormalNones1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 2), spacb2, num2 / 2, norm2);
                        }
                        else
                        {
                            AddRebarArrayNormal(TopUVNones1, SpacingNones1, NumberNones1, NormalNones1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1), spacb2, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm2);
                            AddRebarArrayNormal(TopUVNones2, SpacingNones2, NumberNones2, NormalNones2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 2), spacb2, num2 / 2, norm2);
                        }

                        num = num + num2; num2 = numSmallV2;
                        for (int i = 0; i < numSmallV2; i++)
                        {
                            UV uvExpSmall2 = -ci.VectorV * (ci.DeltaV2 + (DiameterAfter - Diameter) / 2 - spacb2 / 2 * (numBigV2 + (numSmallV2 - 1 - i) + 1));
                            XYZ vecExpSmall2 = new XYZ(uvExpSmall2.U, uvExpSmall2.V, 0);
                            XYZ normExpSmall2 = vecExpSmall2.Normalize().CrossProduct(XYZ.BasisZ);
                            if (num % 2 == 0)
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                            else
                            {
                                if (i % 2 == 0)
                                {
                                    AddRebarArraySmall(TopUVSmalls1, SpacingSmalls1, NumberSmalls1, NormalSmalls1, DimBExpandSmalls1, VecBExpandSmalls1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                                else
                                {
                                    AddRebarArraySmall(TopUVSmalls2, SpacingSmalls2, NumberSmalls2, NormalSmalls2, DimBExpandSmalls2, VecBExpandSmalls2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1 + i), spacb2, 1, normExpSmall2, vecExpSmall2.GetLength(), vecExpSmall2);
                                }
                            }
                        }

                        num = num + num2; num2 = numBigV2;
                        if (num % 2 == 0)
                        {
                            AddRebarArrayBig(TopUVBigs2, SpacingBigs2, NumberBigs2, NormalBigs2, VectorExpandBigs2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1), spacb2, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm2, vecExpBig);
                            AddRebarArrayBig(TopUVBigs1, SpacingBigs1, NumberBigs1, NormalBigs1, VectorExpandBigs1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 2), spacb2, num2 / 2, norm2, vecExpBig);
                        }
                        else
                        {
                            AddRebarArrayBig(TopUVBigs1, SpacingBigs1, NumberBigs1, NormalBigs1, VectorExpandBigs1, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 1), spacb2, num2 % 2 == 0 ? num2 / 2 : num2 / 2 + 1, norm2, vecExpBig);
                            AddRebarArrayBig(TopUVBigs2, SpacingBigs2, NumberBigs2, NormalBigs2, VectorExpandBigs2, pnts[index] + ci.VectorV * spacb2 / 2 * (num + 2), spacb2, num2 / 2, norm2, vecExpBig);
                        }

                        int numImpV = cdiAfter.N2 - cdi.N2 + numBigV1 + numBigV2;
                        if (numImpV > 0)
                        {
                            UV firstPnt = new UV((pnts[index] + ci.VectorV * spacb2 / 2 * (1 + numBigV1 + numSmallV1 + 1 / 2)).U, pntAs[index].V);
                            AddRebarArrayNormal(TopUVImplantShortens2, SpacingImplantShortens2, NumberImplantShortens2, NormalImplantShortens2, firstPnt, spacb2, numImpV % 2 == 0 ? numImpV / 2 : numImpV / 2 + 1, norm2);
                            AddRebarArrayNormal(TopUVImplantShortens1, SpacingImplantShortens1, NumberImplantShortens1, NormalImplantShortens1, firstPnt + ci.VectorV * spacb2 / 2, spacb2, numImpV / 2, norm2);
                        }
                        break;
                    }
            }
        }
        public void GetInfomationLockHead(ColumnInfo ci, ColumnStandardDesignInput cdi, ColumnStandardDesignInput cdiAfter, GeneralNote gn, double spacb1, double spacb2, double spacAb1, double spacAb2, XYZ norm1, XYZ norm2, List<UV> pnts, List<UV> pntAs)
        {
            #region Initiate
            NormalLockHeads1 = new List<XYZ>();
            NormalLockHeads2 = new List<XYZ>();
            NormalImplantLockHeads1 = new List<XYZ>();
            NormalImplantLockHeads2 = new List<XYZ>();

            NumberLockHeads1 = new List<int>();
            NumberLockHeads2 = new List<int>();
            NumberImplantLockHeads1 = new List<int>();
            NumberImplantLockHeads2 = new List<int>();

            TopUVLockHeads1 = new List<UV>();
            TopUVLockHeads2 = new List<UV>();
            TopUVImplantLockHeads1 = new List<UV>();
            TopUVImplantLockHeads2 = new List<UV>();

            SpacingLockHeads1 = new List<double>();
            SpacingLockHeads2 = new List<double>();
            SpacingImplantLockHeads1 = new List<double>();
            SpacingImplantLockHeads2 = new List<double>();

            VectorExpandLockHeads1 = new List<XYZ>();
            VectorExpandLockHeads2 = new List<XYZ>();
            #endregion

            XYZ vecExpBigU1 = -norm2, vecExpBigU2 = norm2;
            XYZ vecExpBigV1 = -norm1, vecExpBigV2 = norm1;

            #region U Direction
            AddRebarArrayBig(TopUVLockHeads1, SpacingLockHeads1, NumberLockHeads1, NormalLockHeads1, VectorExpandLockHeads1, pnts[0], spacb1, cdi.N1 % 2 == 0 ? cdi.N1 / 2 : cdi.N1 / 2 + 1, norm1, vecExpBigU1);
            AddRebarArrayBig(TopUVLockHeads1, SpacingLockHeads1, NumberLockHeads1, NormalLockHeads1, VectorExpandLockHeads1, pnts[3], spacb1, cdi.N1 % 2 == 0 ? cdi.N1 / 2 : cdi.N1 / 2 + 1, norm1, vecExpBigU2);
            AddRebarArrayBig(TopUVLockHeads2, SpacingLockHeads2, NumberLockHeads2, NormalLockHeads2, VectorExpandLockHeads2, pnts[0] + ci.VectorU * spacb1 / 2, spacb1, cdi.N1 / 2, norm1, vecExpBigU1);
            AddRebarArrayBig(TopUVLockHeads2, SpacingLockHeads2, NumberLockHeads2, NormalLockHeads2, VectorExpandLockHeads2, pnts[3] + ci.VectorU * spacb1 / 2, spacb1, cdi.N1 / 2, norm1, vecExpBigU2);

            AddRebarArrayNormal(TopUVImplantLockHeads1, SpacingImplantLockHeads1, NumberImplantLockHeads1, NormalImplantLockHeads1, pntAs[0], spacAb1, cdiAfter.N1 % 2 == 0 ? cdiAfter.N1 / 2 : cdiAfter.N1 / 2 + 1, norm1);
            AddRebarArrayNormal(TopUVImplantLockHeads1, SpacingImplantLockHeads1, NumberImplantLockHeads1, NormalImplantLockHeads1, pntAs[3], spacAb1, cdiAfter.N1 % 2 == 0 ? cdiAfter.N1 / 2 : cdiAfter.N1 / 2 + 1, norm1);
            AddRebarArrayNormal(TopUVImplantLockHeads2, SpacingImplantLockHeads2, NumberImplantLockHeads2, NormalImplantLockHeads2, pntAs[0] + ci.VectorU * spacAb1 / 2, spacAb1, cdiAfter.N1 / 2, norm1);
            AddRebarArrayNormal(TopUVImplantLockHeads2, SpacingImplantLockHeads2, NumberImplantLockHeads2, NormalImplantLockHeads2, pntAs[3] + ci.VectorU * spacAb1 / 2, spacAb1, cdiAfter.N1 / 2, norm1);
            #endregion

            #region V Direction
            AddRebarArrayBig(TopUVLockHeads1, SpacingLockHeads1, NumberLockHeads1, NormalLockHeads1, VectorExpandLockHeads1, pnts[0] + ci.VectorV * spacb2, spacb2, (cdi.N2 - 2) / 2, norm2, vecExpBigV1);
            AddRebarArrayBig(TopUVLockHeads1, SpacingLockHeads1, NumberLockHeads1, NormalLockHeads1, VectorExpandLockHeads1, pnts[1] + ci.VectorV * spacb2, spacb2, (cdi.N2 - 2) / 2, norm2, vecExpBigV2);
            AddRebarArrayBig(TopUVLockHeads2, SpacingLockHeads2, NumberLockHeads2, NormalLockHeads2, VectorExpandLockHeads2, pnts[0] + ci.VectorV * spacb2 / 2, spacb2, cdi.N2 % 2 == 0 ? (cdi.N2 - 2) / 2 : (cdi.N2 - 2) / 2 + 1, norm2, vecExpBigV1);
            AddRebarArrayBig(TopUVLockHeads2, SpacingLockHeads2, NumberLockHeads2, NormalLockHeads2, VectorExpandLockHeads2, pnts[1] + ci.VectorV * spacb2 / 2, spacb2, cdi.N2 % 2 == 0 ? (cdi.N2 - 2) / 2 : (cdi.N2 - 2) / 2 + 1, norm2, vecExpBigV2);

            AddRebarArrayNormal(TopUVImplantLockHeads2, SpacingImplantLockHeads2, NumberImplantLockHeads2, NormalImplantLockHeads2, pntAs[0] + ci.VectorV * spacAb2 / 2, spacAb2, cdiAfter.N2 % 2 == 0 ? (cdiAfter.N2 - 2) / 2 : (cdiAfter.N2 - 2) / 2 + 1, norm2);
            AddRebarArrayNormal(TopUVImplantLockHeads2, SpacingImplantLockHeads2, NumberImplantLockHeads2, NormalImplantLockHeads2, pntAs[1] + ci.VectorV * spacAb2 / 2, spacAb2, cdiAfter.N2 % 2 == 0 ? (cdiAfter.N2 - 2) / 2 : (cdiAfter.N2 - 2) / 2 + 1, norm2);
            AddRebarArrayNormal(TopUVImplantLockHeads1, SpacingImplantLockHeads1, NumberImplantLockHeads1, NormalImplantLockHeads1, pntAs[0] + ci.VectorV * spacAb2, spacAb2, (cdiAfter.N2 - 2) / 2, norm2);
            AddRebarArrayNormal(TopUVImplantLockHeads1, SpacingImplantLockHeads1, NumberImplantLockHeads1, NormalImplantLockHeads1, pntAs[1] + ci.VectorV * spacAb2, spacAb2, (cdiAfter.N2 - 2) / 2, norm2);
            #endregion
        }
        public bool AddRebarArrayNormal(List<UV> uvs, List<double> spacs, List<int> nums, List<XYZ> norms, UV uv, double spa, int num, XYZ norm)
        {
            if (num == 0) return false;
            uvs.Add(uv); spacs.Add(spa); nums.Add(num); norms.Add(norm);
            return true;
        }
        public bool AddRebarArraySmall(List<UV> uvs, List<double> spacs, List<int> nums, List<XYZ> norms, List<double> dimExps, List<XYZ> vecExps, UV uv, double spa, int num, XYZ norm, double dimExp, XYZ vecExp)
        {
            if (num == 0) return false;
            uvs.Add(uv); spacs.Add(spa); nums.Add(num); norms.Add(norm); dimExps.Add(dimExp); vecExps.Add(vecExp);
            return true;
        }
        public bool AddRebarArrayBig(List<UV> uvs, List<double> spacs, List<int> nums, List<XYZ> norms, List<XYZ> vecExps, UV uv, double spa, int num, XYZ norm, XYZ vecExp)
        {
            if (num == 0) return false;
            uvs.Add(uv); spacs.Add(spa); nums.Add(num); norms.Add(norm); vecExps.Add(vecExp);
            return true;
        }
        public RebarInfo(RebarInfo ri, List<RebarBarType> rebarTypes, string rebarName)
        {
            PropertyInfo[] propInfos = typeof(RebarInfo).GetProperties();
            for (int i = 0; i < propInfos.Length; i++)
            {
                if (!propInfos[i].CanWrite) continue;
                propInfos[i].SetValue(this, propInfos[i].GetValue(ri));
            }
            RebarBarType = rebarTypes.Where(x => x.Name == rebarName).First();
        }
    }
}
