using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRebaring.RebarLogistic
{
    public class ColumnInfo
    {
        private double b_cot;
        private double h_cot;
        private UV vecU;
        private UV vecV;
        public GeneralNote GeneralNote { get; set; }
        public ColumnStandardDesignInput ColumnStandardDesignInput { get; set; }
        public ColumnStirrupDesignInput ColumnStirrupDesignInput { get; set; }
        public int ColumnStandardDesignIndex { get { return ColumnStandardDesignInput.Index; } }
        public double B1
        {
            get
            {
                if (!ReversedDimension)
                    return b_cot;
                return h_cot;
            }
        }
        public double B2
        {
            get
            {
                if (!ReversedDimension)
                    return h_cot;
                return b_cot;
            }
        }
        public UV VectorU
        {
            get
            {
                if (!ReversedDimension) return vecU;
                return vecV;
            }
        }
        public UV VectorV
        {
            get
            {
                if (!ReversedDimension) return vecV;
                return vecU;
            }
        }
        public List<UV> BoundaryPoints
        {
            get
            {
                return new List<UV> { CentralPoint - VectorU * B1 / 2 - VectorV * B2 / 2, CentralPoint + VectorU * B1 / 2 - VectorV * B2 / 2, CentralPoint + VectorU * B1 / 2 + VectorV * B2 / 2, CentralPoint - VectorU * B1 / 2 + VectorV * B2 / 2 };
            }
        }
        public List<UV> BoundaryPointOrigins
        {
            get
            {
                return new List<UV>
                {
                    CentralPoint - UV.BasisU *B1/2- UV.BasisV *B2/2,
                    CentralPoint + UV.BasisU *B1/2- UV.BasisV *B2/2,
                    CentralPoint + UV.BasisU *B1/2+ UV.BasisV *B2/2,
                    CentralPoint - UV.BasisU *B1/2+ UV.BasisV *B2/2,
                };
            }
        }
        public Polygon Polygon
        {
            get
            {
                return new Polygon(BoundaryPoints.Select(x => new XYZ(x.U, x.V, 0)).ToList());
            }
        }
        public UV CentralPoint { get; set; }
        public bool ReversedDimension { get; set; }
        public bool TransationRebar { get; set; }
        public int Index { get; set; }
        public RebarInfo RebarInfo { get; set; }
        public double DiameterBefore { get; set; }
        public Document Document { get; set; }
        public Element Element { get; set; }
        public string Level{get { return (Document.GetElement(Element.LevelId) as Level).Name; }}
        public string NextLevel{ get {return Document.GetElement(Element.LookupParameter("Top Level").AsElementId()).Name;}}
        public double Top { get; set; }
        public double TopFloor { get; set; }
        public double TopOffset { get { return Top - TopOffsetValue; } }
        public double TopLast { get; set; }
        public double TopLockHead { get { return TopLast - GeneralNote.CoverTop; } }
        public double TopSmall { get { return TopLast - GeneralNote.CoverTopSmall; } }
        public double TopAnchorAfter { get { return TopLast - RebarAfterAnchorLength; } }
        public double TopStirrup1 { get { return TopStirrup2 - TopOffsetValueStirrup; } }
        public double TopStirrup2 { get { return (GeneralNote.IsStirrupInsideBeam) ? TopFloor : Top; } }
        public double Bottom { get; set; }
        public double BottomOffset { get { return Bottom + BottomOffsetValue; } }
        public double BottomStirrup1 { get { return Bottom; } }
        public double BottomStirrup2 { get { return BottomStirrup1 + BottomOffsetValueStirrup; } }
        public double BottomOffsetValue
        {
            get
            {
                double d = 0;
                if (GeneralNote.IsOffsetCheck) d = GeneralNote.BottomOffset;
                if (GeneralNote.IsOffsetRatioCheck) d = Math.Max(d, (Top - Bottom) / GeneralNote.BottomOffsetRatio);
                return d;
            }
        }
        public double TopOffsetValue
        {
            get
            {
                double d = 0;
                if (GeneralNote.IsOffsetCheck) d = GeneralNote.TopOffset;
                if (GeneralNote.IsOffsetRatioCheck) d = Math.Max(d, (Top - Bottom) / GeneralNote.TopOffsetRatio);
                return d;
            }
        }
        public double BottomOffsetValueStirrup
        {
            get
            {
                double d = 0;
                if (GeneralNote.IsOffsetCheckStirrup) d = GeneralNote.BottomOffsetStirrup;
                if (GeneralNote.IsOffsetRatioCheckStirrup) d = Math.Max(d, ((GeneralNote.IsStirrupInsideBeam ? TopFloor : Top) - Bottom) / GeneralNote.BottomOffsetRatioStirrup);
                return d;
            }
        }
        public double TopOffsetValueStirrup
        {
            get
            {
                double d = 0;
                if (GeneralNote.IsOffsetCheckStirrup) d = GeneralNote.TopOffsetStirrup;
                if (GeneralNote.IsOffsetRatioCheckStirrup) d = Math.Max(d, ((GeneralNote.IsStirrupInsideBeam ? TopFloor : Top) - Bottom) / GeneralNote.TopOffsetRatioStirrup);
                return d;
            }
        }
        public List<StirrupDistribution> StirrupDistributions { get; set; }
        public bool ManualRebar { get; set; }
        public ColumnShortenType ShortenType
        {
            get
            {
                if (ManualShorten) return ShortenTypeManual;
                if (GeomUtil.IsSmaller(RebarInfo.Diameter, RebarInfo.DiameterAfter))
                    return ColumnShortenType.LockHeadFull;
                if (ShortenU1 == ColumnShortenType.Big && ShortenU2 == ColumnShortenType.Big && ShortenV1 == ColumnShortenType.Big && ShortenV2 == ColumnShortenType.Big)
                    return ColumnShortenType.LockHeadFull;
                if (ShortenU1 == ColumnShortenType.Big || ShortenU2 == ColumnShortenType.Big || ShortenV1 == ColumnShortenType.Big || ShortenV2 == ColumnShortenType.Big)
                    return ColumnShortenType.Big;
                if (ShortenU1 == ColumnShortenType.Small || ShortenU2 == ColumnShortenType.Small || ShortenV1 == ColumnShortenType.Small || ShortenV2 == ColumnShortenType.Small)
                    return ColumnShortenType.Small;
                return ColumnShortenType.None;
            }
        }
        public bool ManualShorten { get; set; }
        public ColumnShortenType ShortenTypeManual { get; set; }
        public ColumnShortenType ShortenU1 { get; set; }
        public ColumnShortenType ShortenU2 { get; set; }
        public ColumnShortenType ShortenV1 { get; set; }
        public ColumnShortenType ShortenV2 { get; set; }
        public double DeltaU1 { get; set; }
        public double DeltaU2 { get; set; }
        public double DeltaV1 { get; set; }
        public double DeltaV2 { get; set; }
        public double RebarDevelopmentLength
        {
            get
            {
                //if (ManualRebar)
                //{
                //    if (Index != 0)
                //    {
                //        return GeneralNote.DevelopmentMultiply * (RebarInfo.Diameter < DiameterBefore ? RebarInfo.Diameter : DiameterBefore);
                //    }
                //}
                return GeneralNote.DevelopmentMultiply * RebarInfo.Diameter;
            }
        }
        public double RebarAfterAnchorLength
        {
            get
            {
                return GeneralNote.AnchorMultiply * RebarInfo.DiameterAfter;
            }
        }
        public ColumnInfo(Document doc, Element e, string b_param, string h_param)
        {
            this.Element = e;
            this.Document = doc;
            this.TransationRebar = false;
            this.ManualRebar = false;
            this.ReversedDimension = false;
            this.ShortenU1 = ColumnShortenType.None;
            this.ShortenU2 = ColumnShortenType.None;
            this.ShortenV1 = ColumnShortenType.None;
            this.ShortenV2 = ColumnShortenType.None;
            ManualShorten = false;

            GetGeometry(b_param, h_param);
            GetTopBottom();
        }
        public void SetShortenTypeManual(ColumnShortenType type)
        {
            ShortenTypeManual = type;
            ManualShorten = true;
        }
        public void GetShortenTypes(GeneralNote gn, double deltaU1, double deltaU2, double deltaV1, double deltaV2)
        {
            ShortenU1 = GetShortenType(gn, deltaU1);
            DeltaU1 = deltaU1;
            ShortenU2 = GetShortenType(gn, deltaU2);
            DeltaU2 = deltaU2;
            ShortenV1 = GetShortenType(gn, deltaV1);
            DeltaV1 = deltaV1;
            ShortenV2 = GetShortenType(gn, deltaV2);
            DeltaV2 = deltaV2;
        }
        public ColumnShortenType GetShortenType(GeneralNote gn, double delta)
        {
            if (GeomUtil.IsEqual(0, delta))
            {
                return ColumnShortenType.None;
            }
            else if (GeomUtil.IsEqual(gn.ShortenLimit, delta) || gn.ShortenLimit < delta)
            {
                return ColumnShortenType.Big;
            }
            else
            {
                return ColumnShortenType.Small;
            }
        }
        public void GetGeometry(string b_param, string h_param)
        {
            Element etype = Document.GetElement(Element.GetTypeId());
            b_cot = etype.LookupParameter(b_param).AsDouble();
            h_cot = etype.LookupParameter(h_param).AsDouble();
            Transform tf = ((FamilyInstance)Element).GetTransform();
            XYZ vecX = GeomUtil.IsBigger(tf.BasisX, -tf.BasisX) ? tf.BasisX : -tf.BasisX;
            XYZ vecY = GeomUtil.IsBigger(tf.BasisY, -tf.BasisY) ? tf.BasisY : -tf.BasisY;
            vecU = new UV(vecX.X, vecX.Y); vecV = new UV(vecY.X, vecY.Y);

            XYZ pnt = (Element.Location as LocationPoint).Point;
            CentralPoint = new UV(pnt.X, pnt.Y);
        }
        public void GetTopBottom()
        {
            BoundingBoxXYZ bb = Element.get_BoundingBox(null);
            Outline ol = new Outline(bb.Min, bb.Max);
            XYZ midPnt = (bb.Min + bb.Max) / 2;

            Options opt = new Options();
            GeometryElement geoElem = Element.get_Geometry(opt);
            Solid s = null;
            foreach (GeometryObject geoObj in geoElem)
            {
                if (geoObj is Solid)
                {
                    s = geoObj as Solid;
                    if (s != null)
                    {
                        if (s.Faces.Size != 0 && s.Edges.Size != 0)
                        {
                            break;
                        }
                    }
                }
            }
            XYZ centralPnt = s.ComputeCentroid();
            BoundingBoxXYZ bbs = s.GetBoundingBox();
            Transform tf = Transform.Identity;
            tf.BasisX = XYZ.BasisX * 1.025;
            tf.BasisY = XYZ.BasisY * 1.025;
            tf.BasisZ = XYZ.BasisZ * 1.025;
            s = SolidUtils.CreateTransformed(s, tf);

            tf = Transform.CreateTranslation(centralPnt - s.ComputeCentroid());
            s = SolidUtils.CreateTransformed(s, tf);
            XYZ cen2 = s.ComputeCentroid();
            BoundingBoxXYZ bbs2 = s.GetBoundingBox();

            BoundingBoxIntersectsFilter bbiFilter = new BoundingBoxIntersectsFilter(ol);
            ElementIntersectsSolidFilter eisFilter = new ElementIntersectsSolidFilter(s);
            ElementClassFilter flFilter = new ElementClassFilter(typeof(Floor));
            ElementClassFilter fiFilter = new ElementClassFilter(typeof(FamilyInstance));
            ElementCategoryFilter beamCateFilter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            LogicalAndFilter beamFilter = new LogicalAndFilter(new List<ElementFilter> { fiFilter, beamCateFilter });
            LogicalOrFilter floorOrBeamFilter = new LogicalOrFilter(new List<ElementFilter> { flFilter, beamFilter });
            List<Element> elems = new FilteredElementCollector(Document).WherePasses(floorOrBeamFilter).WherePasses(bbiFilter).WherePasses(eisFilter).ToList();

            double middle = centralPnt.Z;
            double min = 0, max = 0, maxLast = 0, maxFloor = 0;
            bool firstSetMax = true, firstSetMin = true, firstSetMaxLast = true, firstSetMaxFloor = true;
            for (int i = 0; i < elems.Count; i++)
            {
                BoundingBoxXYZ bbe = elems[i].get_BoundingBox(null);
                double maxZ = 0, maxLastZ = 0, maxFloorZ = 0;
                maxLastZ = bbe.Max.Z;
                if (maxLastZ > middle)
                {
                    if (firstSetMaxLast)
                    {
                        maxLast = maxLastZ;
                        firstSetMaxLast = false;
                    }
                    else
                    {
                        if (maxLast < maxLastZ)
                        {
                            maxLast = maxLastZ;
                        }
                    }
                }
                maxZ = bbe.Min.Z;
                if (maxZ > middle)
                {
                    if (firstSetMax)
                    {
                        max = maxZ;
                        firstSetMax = false;
                    }
                    else
                    {
                        if (max > maxZ)
                        {
                            max = maxZ;
                        }
                    }
                }
                maxFloorZ = bbe.Min.Z;
                if (elems[i] is Floor)
                {
                    if (maxFloorZ > middle)
                    {
                        if (firstSetMaxFloor)
                        {
                            maxFloor = maxFloorZ;
                            firstSetMaxFloor = false;
                        }
                        else
                        {
                            if (maxFloor > maxFloorZ)
                            {
                                maxFloor = maxFloorZ;
                            }
                        }
                    }
                }
                double minZ = bbe.Max.Z;
                if (minZ < middle)
                {
                    if (firstSetMin)
                    {
                        min = minZ;
                        firstSetMin = false;
                    }
                    else
                    {
                        if (min < minZ)
                        {
                            min = minZ;
                        }
                    }
                }
            }
            this.Top = max;
            this.TopLast = maxLast;
            this.Bottom = min;
            if (firstSetMin) Bottom = bb.Min.Z;
            if (firstSetMax) Top = bb.Max.Z;
            if (firstSetMaxLast) TopLast = bb.Max.Z;
            if (firstSetMaxFloor) TopFloor = Top;
        }
        public void MergeStirrup(double z1, double z2)
        {
            StirrupDistributions.Add(new StirrupDistribution(z1, z2));
            List<StirrupDistribution> stirDiss1 = StirrupDistributions;
            stirDiss1.Sort();
            while (true)
            {
                List<StirrupDistribution> stirDiss2 = new List<StirrupDistribution>();
                bool isMergeAll = false;
                for (int i = 0; i < stirDiss1.Count - 1; i += 2)
                {
                    bool isMerge = false;
                    stirDiss2.AddRange(StirrupDistribution.CheckMerge(stirDiss1[i], stirDiss1[i + 1], ColumnStirrupDesignInput.MiddleSpacing1, out isMerge));
                    if (isMerge) isMergeAll = true;
                }
                if (stirDiss1.Count % 2 == 1)
                {
                    bool isMerge = false;
                    List<StirrupDistribution> temDiss = StirrupDistribution.CheckMerge(stirDiss2[stirDiss2.Count - 1], stirDiss1[stirDiss1.Count - 1], ColumnStirrupDesignInput.MiddleSpacing1, out isMerge);
                    if (isMerge)
                    {
                        if (stirDiss2.Count > 1) isMergeAll = true;
                        else isMergeAll = false;
                        stirDiss2[stirDiss2.Count - 1] = temDiss[0];
                    }
                    else stirDiss2.Add(temDiss[1]);
                }
                stirDiss1 = stirDiss2;
                if (!isMergeAll) break;
            }
            StirrupDistributions = stirDiss1;
        }
        public void AddDefaultStirrup()
        {
            StirrupDistributions = new List<StirrupDistribution> { new StirrupDistribution(BottomStirrup1, BottomStirrup2), new StirrupDistribution(TopStirrup1, TopStirrup2) };
        }
    }
    public class ColumnInfoCollection
    {
        private List<ColumnInfo> columnInfos = new List<ColumnInfo>();
        public bool ReversedDimension
        {
            set
            {
                if (value)
                    columnInfos.ForEach(x => x.ReversedDimension = true);
                else
                    columnInfos.ForEach(x => x.ReversedDimension = false);
            }
        }
        public int Count { get { return columnInfos.Count; } }
        public IEnumerator<ColumnInfo> GetEnumerator() { return this.columnInfos.GetEnumerator(); }
        public void Sort(IComparer<ColumnInfo> comparer)
        {
            columnInfos.Sort(comparer);
            for (int i = 0; i < columnInfos.Count; i++)
            {
                columnInfos[i].Index = i;
            }
        }
        public ColumnInfoCollection(Document doc, Element e, string b_param, string h_param)
        {
            string mark = e.LookupParameter("Mark").AsString();
            ColumnInfo ci = new ColumnInfo(doc, e, b_param, h_param);
            Polygon pl = ci.Polygon;

            List<Element> elems = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_StructuralColumns).ToList();
            foreach (Element ee in elems)
            {
                string marke = string.Empty;
                try
                {
                    marke = ee.LookupParameter("Mark").AsString();
                }
                catch (Exception)
                {

                }
                if (marke == mark)
                {
                    ColumnInfo ci1 = new ColumnInfo(doc, ee, b_param, h_param);
                    Polygon pl1 = ci1.Polygon;
                    pl1 = CheckGeometry.GetProjectPolygon(pl.Plane, pl1);
                    PolygonComparePolygonResult res = new PolygonComparePolygonResult(pl, pl1);
                    if (res.IntersectType == PolygonComparePolygonIntersectType.AreaOverlap)
                    {
                        columnInfos.Add(ci1);
                    }
                }
            }
            Sort(new ColumnInfoSorter());
        }
        public void SetRebarInfo(List<ColumnStandardDesignInput> cdis, List<ColumnStirrupDesignInput> csdis, GeneralNote gn)
        {
            #region Stirrup
            for (int i = 0; i < columnInfos.Count; i++)
            {
                bool isExist = false;
                for (int j = 0; j < csdis.Count; j++)
                {
                    if (columnInfos[i].Level == csdis[j].Level.Name)
                    {
                        columnInfos[i].ColumnStirrupDesignInput = csdis[j];
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    columnInfos[i].ColumnStirrupDesignInput = columnInfos[i - 1].ColumnStirrupDesignInput;
                }
            }
            #endregion

            #region Standard
            for (int i = 0; i < columnInfos.Count; i++)
            {
                ColumnStirrupDesignInput csdiAfter = (i == columnInfos.Count - 1) ? columnInfos[i].ColumnStirrupDesignInput : columnInfos[i + 1].ColumnStirrupDesignInput;
                if (i != 0) columnInfos[i].DiameterBefore = columnInfos[i - 1].RebarInfo.Diameter;
                int nextI = i == columnInfos.Count - 1 ? i : i + 1;
                for (int j = 0; j < cdis.Count; j++)
                {
                    if (columnInfos[i].Level == cdis[j].Level.Name)
                    {
                        ColumnStandardDesignInput cdiAfter = cdis[j];
                        if (i < columnInfos.Count - 1)
                        {
                            if (j < cdis.Count - 1)
                            {
                                if (columnInfos[i + 1].Level == cdis[j + 1].Level.Name)
                                {
                                    cdiAfter = cdis[j + 1];
                                }
                            }
                        }


                        RebarInfo ri = new RebarInfo(columnInfos[i], columnInfos[nextI], cdis[j], cdiAfter, columnInfos[i].ColumnStirrupDesignInput, csdiAfter, gn);
                        columnInfos[i].RebarInfo = ri;
                        columnInfos[i].ColumnStandardDesignInput = cdis[j];
                        columnInfos[i].ManualRebar = true;
                        break;
                    }
                }
                if (!columnInfos[i].ManualRebar)
                {
                    ColumnStandardDesignInput cdiAfter = columnInfos[i - 1].ColumnStandardDesignInput;
                    int index = columnInfos[i - 1].ColumnStandardDesignIndex;
                    if (index < cdis.Count - 1)
                    {
                        if (columnInfos[i + 1].Level == cdis[index + 1].Level.Name)
                        {
                            cdiAfter = cdis[index + 1];
                        }
                    }
                    columnInfos[i].RebarInfo = new RebarInfo(columnInfos[i], columnInfos[nextI], columnInfos[i - 1].ColumnStandardDesignInput, cdiAfter, columnInfos[i].ColumnStirrupDesignInput, csdiAfter, gn);
                }
            }
            columnInfos[columnInfos.Count - 1].RebarInfo.LockHeadManual = true;
            #endregion
        }
        public void SetReversedDimensionOrNot(string columnLevel, double b1, double b2)
        {
            foreach (ColumnInfo ci in columnInfos)
            {
                if (ci.Level == columnLevel)
                {
                    if (GeomUtil.IsEqual(ci.B1, b1) && GeomUtil.IsEqual(ci.B2, b2))
                    {
                        ReversedDimension = false;
                    }
                    else if (GeomUtil.IsEqual(ci.B1, b2) && GeomUtil.IsEqual(ci.B2, b1))
                    {
                        ReversedDimension = true;
                    }
                    else throw new Exception("Kích thước cột trong Revit hay Form không đúng!");
                }
            }
        }
        public void SetGeneralNote(GeneralNote gn)
        {
            columnInfos.ForEach(x => x.GeneralNote = gn);
        }
        public void SetColumnShorten(GeneralNote gn)
        {
            for (int i = 0; i < columnInfos.Count - 1; i++)
            {
                double deltaU1 = Math.Abs(columnInfos[i].BoundaryPointOrigins[0].U - columnInfos[i + 1].BoundaryPointOrigins[0].U);
                double deltaU2 = Math.Abs(columnInfos[i].BoundaryPointOrigins[2].U - columnInfos[i + 1].BoundaryPointOrigins[2].U);
                double deltaV1 = Math.Abs(columnInfos[i].BoundaryPointOrigins[0].V - columnInfos[i + 1].BoundaryPointOrigins[0].V);
                double deltaV2 = Math.Abs(columnInfos[i].BoundaryPointOrigins[2].V - columnInfos[i + 1].BoundaryPointOrigins[2].V);
                columnInfos[i].GetShortenTypes(gn, deltaU1, deltaU2, deltaV1, deltaV2);
            }
            columnInfos[columnInfos.Count - 1].SetShortenTypeManual(ColumnShortenType.LockHeadFull);
        }
        public ColumnInfo this[int i]
        {
            get
            {
                return this.columnInfos[i];
            }
        }
    }
    public class ColumnInfoSorter : IComparer<ColumnInfo>
    {
        public ColumnInfoSorter()
        {
        }
        int IComparer<ColumnInfo>.Compare(ColumnInfo x, ColumnInfo y)
        {
            return x.Top.CompareTo(y.Top);
        }
    }

    public enum ColumnShortenType
    {
        None, Small, Big, LockHeadFull
    }
}
