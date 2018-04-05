#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;

using Geometry;
using AutoRebaring.RebarLogistic;
using AutoRebaring.Database;
using AutoRebaring.Database.BIM_PORTAL;
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
#endregion

namespace AutoRebaring.Form
{
    [Transaction(TransactionMode.Manual)]
    public class FormCommand : IExternalCommand
    {
        const string r = "Revit";
        View3D view3D { get; set; }
        string Mark { get; set; }
        //public RebarInputForm Form { get { return Window.form; } }
        public RebarInputForm Form { get; set; }
        public WindowForm Window { get; set; }

        public void ShowForm(BIM_PORTALDbContext db, List<string> paraNames, Autodesk.Revit.UI.UIApplication uiapp, Element e, List<Level> levels, RebarHookType hookType, List<RebarBarType> rebarTypes, List<RebarShape> rebarShapes, List<View3D> view3ds)
        {
            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.FromPoint(System.Windows.Forms.Cursor.Position);
            System.Drawing.Rectangle rec = screen.WorkingArea;
            System.Windows.Window window = new System.Windows.Window();
            window.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
            window.Height = 1000; window.Width = 1200;
            window.Title = "Input Offset";
            window.ResizeMode = System.Windows.ResizeMode.NoResize;
            window.Topmost = true;
            window.Left = rec.Right - window.Width - 250;
            window.Top = rec.Top + 20;

            Form = new RebarInputForm(db, paraNames, uiapp, e, levels, hookType, rebarTypes, rebarShapes, view3ds);
            window.Content = Form;
            Form.Window = window;

            window.ShowDialog();
        }
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            Transaction tx = new Transaction(doc, "AutoRebaring");
            tx.Start();

#if Debug2016
            //Plane pl = new Plane(xVec, yVec, origin);
            doc.ActiveView.SketchPlane = SketchPlane.Create(doc, new Plane(doc.ActiveView.RightDirection, doc.ActiveView.UpDirection, doc.ActiveView.Origin));
#elif Debug2017
            //Plane.CreateByOriginAndBasis(origin, basisX, basisY);
            doc.ActiveView.SketchPlane = SketchPlane.Create(doc, Plane.CreateByOriginAndBasis(doc.ActiveView.Origin, doc.ActiveView.RightDirection, doc.ActiveView.UpDirection));
#elif Debug2018
            doc.ActiveView.SketchPlane = SketchPlane.Create(doc, Plane.CreateByOriginAndBasis(doc.ActiveView.Origin, doc.ActiveView.RightDirection, doc.ActiveView.UpDirection));
#endif
            doc.ActiveView.HideActiveWorkPlane();

            BIM_PORTALDbContext db = new BIM_PORTALDbContext();
            PhysicalAddress pa = ComputerInfo.GetMacAddress();
            string s1 = ComputerInfo.GetMacAddress().ToString();
            string s2 = app.Username;

            List<Level> levels = new FilteredElementCollector(doc).OfClass(typeof(Level)).Where(x => x != null).Cast<Level>().OrderBy(x => x.Elevation).ToList();
            List<RebarBarType> rebarTypes = new FilteredElementCollector(doc).OfClass(typeof(RebarBarType)).Where(x => x != null).Cast<RebarBarType>().OrderBy(x => x.BarDiameter).ToList();
            List<RebarShape> rebarshapes = new FilteredElementCollector(doc).OfClass(typeof(RebarShape)).Cast<RebarShape>().Where(x => x != null).ToList();
            List<View3D> view3ds = new FilteredElementCollector(doc).OfClass(typeof(View3D)).Where(x => x != null).Cast<View3D>().ToList();
            RebarHookType hookType = new FilteredElementCollector(doc).OfClass(typeof(RebarHookType)).Cast<RebarHookType>().First();
            Element e = doc.GetElement(sel.PickObject(ObjectType.Element, new ColumnSelection()));
            Element eT = doc.GetElement(e.GetTypeId());
            List<string> paraNames = new List<string>();
            foreach (Parameter p in eT.Parameters)
            {
                if (p.Definition.ParameterGroup == BuiltInParameterGroup.PG_GEOMETRY)
                    paraNames.Add(p.Definition.Name);
            }

            //Window = new WindowForm();
            //Window.SetDimension(1000, 1200, 20, 250, "THÔNG TIN ĐẦU VÀO");
            //Form = new RebarInputForm(db, paraNames, uiapp, e, levels,hookType, rebarTypes, rebarshapes, view3ds);
            //Window.ShowDialog();
            //if (!Window.Form.IsOK)
            //{
            //    tx.Commit();
            //    return Result.Succeeded;
            //}

            ShowForm(db, paraNames, uiapp, e, levels, hookType, rebarTypes, rebarshapes, view3ds);
            if (!Form.IsOK)
            {
                tx.Commit();
                return Result.Succeeded;
            }

            ParameterRebar pr = new ParameterRebar(Form.PartCount, Form.Mark, Form.Levels.Select(x => x.Name).ToList(), Form.MetaLevels, Form.FirstMetaLevel, Form.LastMetaLevel);

            ColumnInfoCollection cic = Form.ColumnInfoCollection;
            cic.ReversedDimension = Form.isReverse;
            GeneralNote gn = Form.GeneralNote;

            cic.SetGeneralNote(gn);
            cic.SetColumnShorten(gn);
            cic.SetRebarInfo(Form.ColumnStandardDesignInputs, Form.ColumnStirrupDesignInputs, gn);

            Variable vari = new Variable(gn.Lmax, gn.Lmin, gn.Step, gn.LStandards, gn.LPlusStandards);
            CheckLoop cl = new CheckLoop(doc, cic, gn, vari);
            cl.RunLogistic();

            #region Stirrup
            for (int i = 0; i < cic.Count; i++)
            {
                cic[i].AddDefaultStirrup();
            }
            for (int i = 0; i < cl.Count; i++)
            {
                ColumnInfo ci = cl.TurnLoop[i].ColumnInfoEnd;
                double z2 = cl.TurnLoop[i].End1;
                double z1 = cl.TurnLoop[i + 1].Start1;
                if (!GeomUtil.IsEqual(z1, z2) && !cl.TurnLoop[i].Finish1)
                {
                    ci.MergeStirrup(z1, z2);
                }

                z2 = cl.TurnLoop[i].End2;
                z1 = cl.TurnLoop[i + 1].Start2;
                if (!GeomUtil.IsEqual(z1, z2) && !cl.TurnLoop[i].Finish2)
                {
                    ci.MergeStirrup(z1, z2);
                }
            }

            RebarShape rebarshape1 = Form.RebarShape1;
            RebarShape rebarshape2 = Form.RebarShape2;
            Mark = Form.Mark;

            for (int i = 0; i < cic.Count; i++)
            {


                double btSpa1 = cic[i].ColumnStirrupDesignInput.BotTopSpacing1;
                double btSpa2 = cic[i].ColumnStirrupDesignInput.BotTopSpacing2;
                double mSpa1 = cic[i].ColumnStirrupDesignInput.MiddleSpacing1;
                double mSpa2 = cic[i].ColumnStirrupDesignInput.MiddleSpacing2;
                RebarInfo ri = cic[i].RebarInfo;
                double nextLocZ1 = 0;
                if (cic[i].StirrupDistributions.Count == 1)
                {
                    CreateStirrupRebar1(doc, rebarshape1, cic[i], cic[i].StirrupDistributions[0].Z1 + GeomUtil.milimeter2Feet(50), cic[i].StirrupDistributions[0].Z2, btSpa1, 1, btSpa1, out nextLocZ1);
                    for (int k = 0; k < ri.TopUVStirrups2.Count; k++)
                    {
                        CreateStirrupRebar2(doc, rebarshape2, cic[i], cic[i].StirrupDistributions[0].Z1 + GeomUtil.milimeter2Feet(50), cic[i].StirrupDistributions[0].Z2, btSpa2,
                            ri.TopUVStirrups2[k], ri.VectorXStirrups2[k], ri.VectorYStirrups2[k], ri.BStirrups2[k], 1);
                    }
                }
                else
                {
                    for (int j = 0; j < cic[i].StirrupDistributions.Count; j++)
                    {
                        StirrupDistribution sd = cic[i].StirrupDistributions[j];
                        StirrupDistribution sdB = (j > 0) ? cic[i].StirrupDistributions[j - 1] : sd;
                        if (j == 0)
                        {
                            CreateStirrupRebar1(doc, rebarshape1, cic[i], sd.Z1, sd.Z2, btSpa1, 2, btSpa1, out nextLocZ1);
                            for (int k = 0; k < ri.TopUVStirrups2.Count; k++)
                            {
                                CreateStirrupRebar2(doc, rebarshape2, cic[i], sd.Z1, sd.Z2, btSpa2, ri.TopUVStirrups2[k], ri.VectorXStirrups2[k], ri.VectorYStirrups2[k], ri.BStirrups2[k], 2);
                            }
                        }
                        else if (j == cic[i].StirrupDistributions.Count - 1)
                        {
                            for (int k = 0; k < ri.TopUVStirrups2.Count; k++)
                            {
                                CreateStirrupRebar2(doc, rebarshape2, cic[i], nextLocZ1, sd.Z1, mSpa2, ri.TopUVStirrups2[k], ri.VectorXStirrups2[k], ri.VectorYStirrups2[k], ri.BStirrups2[k], 0);
                            }
                            CreateStirrupRebar1(doc, rebarshape1, cic[i], nextLocZ1, sd.Z1, mSpa1, 0, btSpa1, out nextLocZ1);
                            for (int k = 0; k < ri.TopUVStirrups2.Count; k++)
                            {
                                CreateStirrupRebar2(doc, rebarshape2, cic[i], sd.Z1, sd.Z2, btSpa2, ri.TopUVStirrups2[k], ri.VectorXStirrups2[k], ri.VectorYStirrups2[k], ri.BStirrups2[k], 1);
                            }
                            CreateStirrupRebar1(doc, rebarshape1, cic[i], sd.Z1, sd.Z2, btSpa1, 1, mSpa1, out nextLocZ1);
                        }
                        else
                        {
                            if (j == 2) nextLocZ1 = sdB.Z2 + mSpa1;
                            for (int k = 0; k < ri.TopUVStirrups2.Count; k++)
                            {
                                CreateStirrupRebar2(doc, rebarshape2, cic[i], nextLocZ1, sd.Z1, mSpa2, ri.TopUVStirrups2[k], ri.VectorXStirrups2[k], ri.VectorYStirrups2[k], ri.BStirrups2[k], 0);
                            }
                            CreateStirrupRebar1(doc, rebarshape1, cic[i], nextLocZ1, sd.Z1, mSpa1, 0, btSpa1, out nextLocZ1);
                            for (int k = 0; k < ri.TopUVStirrups2.Count; k++)
                            {
                                CreateStirrupRebar2(doc, rebarshape2, cic[i], nextLocZ1, sd.Z2, btSpa2, ri.TopUVStirrups2[k], ri.VectorXStirrups2[k], ri.VectorYStirrups2[k], ri.BStirrups2[k], 0);
                            }
                            CreateStirrupRebar1(doc, rebarshape1, cic[i], nextLocZ1, sd.Z2, btSpa1, 0, mSpa1, out nextLocZ1);
                        }
                    }
                }
            }
            #endregion
            cl.CreateRebars(pr, Form.View3d);
            //cl.CreateRebarTests(pr, Form.View3d);

            tx.Commit();
            return Result.Succeeded;
        }
        public void CreateStirrupRebar1(Document doc, RebarShape rebarshape1, ColumnInfo ci, double locZ1, double locZ2, double spac, int Case, double nextSpac, out double nextLocZ1)
        {
            RebarInfo ri = ci.RebarInfo;
            double len = locZ2 - locZ1;
            int n = (int)Math.Floor(len / spac);
            double startZ = 0;
            switch (Case)
            {
                case 0: n = GeomUtil.IsEqual(n, len / spac) ? n : n += 1; startZ = locZ1 + (n - 1) * spac; break; // Đoạn đầu
                case 1: n = len / spac > n + 0.55 ? n += 2 : n += 1; startZ = locZ1 + (n - 1) * spac; break; // Đoạn giữa
                case 2: n = len / spac > n + 0.55 ? n += 2 : n += 1; startZ = locZ2; break; // Đoạn cuối
            }
            double endZ = startZ - (n - 1) * spac;
            nextLocZ1 = startZ + nextSpac;
            if (n < 1) return;
            Rebar rb = Rebar.CreateFromRebarShape(doc, rebarshape1, ri.StirrupType1, ci.Element, new XYZ(ri.TopUVStirrup1.U, ri.TopUVStirrup1.V, startZ), ri.VectorXStirrup1, ri.VectorYStirrup1);
            rb.LookupParameter("B").Set(ri.BStirrup1);
            rb.LookupParameter("D").Set(ri.BStirrup1);
            rb.LookupParameter("C").Set(ri.CStirrup1);
            rb.LookupParameter("E").Set(ri.CStirrup1);
            rb.LookupParameter("Comments").Set("add-in");
            rb.LookupParameter("Mark").Set(Mark);
#if Debug2016
            if (n == 1)
                rb.SetLayoutAsSingle();
            else
                rb.SetLayoutAsNumberWithSpacing(n, spac, true, true, true);
#elif Debug2017
            if (n == 1)
                rb.SetLayoutAsSingle();
            else
                rb.SetLayoutAsNumberWithSpacing(n, spac, true, true, true);
#elif Debug2018
            RebarShapeDrivenAccessor rsda = rb.GetShapeDrivenAccessor();
            if (n == 1)
                rsda.SetLayoutAsSingle();
            else
                rsda.SetLayoutAsNumberWithSpacing(n, spac, true, true, true);
#endif
            doc.Regenerate();
            BoundingBoxXYZ bb = rb.get_BoundingBox(null);
            XYZ midPnt = new XYZ((bb.Min.X + bb.Max.X) / 2, (bb.Min.Y + bb.Max.Y) / 2, (bb.Min.Z + bb.Max.Z) / 2);
            ElementTransformUtils.MoveElement(doc, rb.Id, new XYZ(ri.TopUVStirrup1.U - midPnt.X - ri.StirrupDiameter1 / 4, ri.TopUVStirrup1.V - midPnt.Y - ri.StirrupDiameter1 / 4, (endZ + startZ) / 2 - midPnt.Z + ri.StirrupDiameter1 / 2));
            if (view3D != null)
            {
                rb.SetSolidInView(view3D, true);
                rb.SetUnobscuredInView(view3D, true);
            }
        }
        public void CreateStirrupRebar2(Document doc, RebarShape rebarshape2, ColumnInfo ci, double locZ1, double locZ2, double spac, UV topUVstir, XYZ vecXstir, XYZ vecYstir, double bstir, int Case)
        {
            RebarInfo ri = ci.RebarInfo;
            double len = locZ2 - locZ1;
            int n = (int)Math.Floor(len / spac);
            double startZ = 0;
            switch (Case)
            {
                case 0: n = GeomUtil.IsEqual(n, len / spac) ? n : n += 1; startZ = locZ1 + (n - 1) * spac; break; // Đoạn đầu
                case 1: n = len / spac > n + 0.55 ? n += 2 : n += 1; startZ = locZ1 + (n - 1) * spac; break; // Đoạn giữa
                case 2: n = len / spac > n + 0.55 ? n += 2 : n += 1; startZ = locZ2; break; // Đoạn cuối
            }
            double endZ = startZ - (n - 1) * spac;
            if (n < 1) return;
            Rebar rb = Rebar.CreateFromRebarShape(doc, rebarshape2, ri.StirrupType2, ci.Element, new XYZ(topUVstir.U, topUVstir.V, startZ), vecXstir, vecYstir);
            rb.LookupParameter("B").Set(bstir);
            rb.LookupParameter("Comments").Set("add-in");
            rb.LookupParameter("Mark").Set(Mark);
#if Debug2016
            if (n == 1)
                rb.SetLayoutAsSingle();
            else
                rb.SetLayoutAsNumberWithSpacing(n, spac, true, true, true);
#elif Debug2017
            if (n == 1)
                rb.SetLayoutAsSingle();
            else
                rb.SetLayoutAsNumberWithSpacing(n, spac, true, true, true);
#elif Debug2018
            RebarShapeDrivenAccessor rsda = rb.GetShapeDrivenAccessor();
            if (n == 1)
                rsda.SetLayoutAsSingle();
            else
                rsda.SetLayoutAsNumberWithSpacing(n, spac, true, true, true);
#endif
            doc.Regenerate();
            BoundingBoxXYZ bb = rb.get_BoundingBox(null);
            XYZ midPnt = new XYZ((bb.Min.X + bb.Max.X) / 2, (bb.Min.Y + bb.Max.Y) / 2, (bb.Min.Z + bb.Max.Z) / 2);
            ElementTransformUtils.MoveElement(doc, rb.Id, new XYZ(topUVstir.U - midPnt.X, topUVstir.V - midPnt.Y, (endZ + startZ) / 2 - midPnt.Z));
            if (view3D != null)
            {
                rb.SetSolidInView(view3D, true);
                rb.SetUnobscuredInView(view3D, true);
            }
        }
    }
    [Transaction(TransactionMode.Manual)]
    public class InquireDatabase : IExternalCommand
    {
        const string r = "Revit";
        View3D view3D { get; set; }
        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            Transaction tx = new Transaction(doc, "AutoRebaring");
            tx.Start();

            BIM_PORTALDbContext db = new BIM_PORTALDbContext();
            
            //bimphongnh - 123

            tx.Commit();
            return Result.Succeeded;
        }
       
    }
}
