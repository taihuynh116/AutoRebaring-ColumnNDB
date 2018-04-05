using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using AutoRebaring.Database;
using AutoRebaring.Database.AutoRebaring;
using AutoRebaring.Database.AutoRebaring.Dao;
using AutoRebaring.Database.BIM_PORTAL;
using AutoRebaring.RebarLogistic;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoRebaring.Form
{
    /// <summary>
    /// Interaction logic for RebarInputForm.xaml
    /// </summary>
    public partial class RebarInputForm : UserControl
    {
        public double DevelopmentLenghtDistance { get; set; }
        public bool IsOK { get; set; }
        public GeneralNote GeneralNote { get; set; }
        public Window Window { get; set; }
        public List<Level> Levels { get; set; }
        public List<RebarBarType> RebarTypes { get; set; }
        public List<RebarShape> RebarShapes { get; set; }
        public List<View3D> View3ds { get; set; }
        public List<ComboBox> cbbDesignLevels { get; set; }
        public List<ComboBox> cbbDesignRebarTypes { get; set; }
        public List<TextBox> txtDesignN1s { get; set; }
        public List<TextBox> txtDesignN2s { get; set; }
        public List<ComboBox> cbbDesignStirrupType1s { get; set; }
        public List<ComboBox> cbbDesignStirrupType2s { get; set; }
        public List<TextBox> txtDesignStirrupTB1s { get; set; }
        public List<TextBox> txtDesignStirrupTB2s { get; set; }
        public List<TextBox> txtDesignStirrupM1s { get; set; }
        public List<TextBox> txtDesignStirrupM2s { get; set; }
        public List<Label> txtDesignSums { get; set; }
        public List<TextBox> txtStandardLs { get; set; }
        public List<TextBox> txtStandardPairLs { get; set; }
        public double RebarZ1 { get; set; }
        public double RebarZ2 { get; set; }
        public RebarLayoutRule LayoutRule { get; set; }
        public double ShortenLimit { get; set; }
        public int LockHeadMutliply { get; set; }
        public double RatioLH { get; set; }
        public double CoverTopSmall { get; set; }
        public int DevelopmenMultiply { get; set; }
        public int AnchorMultiply { get; set; }
        public double ConcreteCover { get; set; }
        public double ConcreteTopCover { get; set; }
        public double TopOffset { get; set; }
        public double BottomOffset { get; set; }
        public double TopOffsetRatio { get; set; }
        public double BottomOffsetRatio { get; set; }
        public bool IsOffsetCheck { get; set; }
        public bool IsOffsetRatioCheck { get; set; }
        public double TopOffsetStirrup { get; set; }
        public double BottomOffsetStirrup { get; set; }
        public double TopOffsetRatioStirrup { get; set; }
        public double BottomOffsetRatioStirrup { get; set; }
        public bool IsOffsetCheckStirrup { get; set; }
        public bool IsOffsetRatioCheckStirrup { get; set; }
        public bool IsStirrupInsideBeam { get; set; }
        public double Lmax { get; set; }
        public double Lmin { get; set; }
        public double Step { get; set; }
        public List<double> LStandards { get; set; }
        public List<double> LPlusStandards { get; set; }
        public List<double> LengthImplant1s { get; set; }
        public List<double> LengthImplant2s { get; set; }
        public List<double> RebarImplants { get; set; }
        public List<ColumnStandardDesignInput> ColumnStandardDesignInputs { get; set; }
        public List<ColumnStirrupDesignInput> ColumnStirrupDesignInputs { get; set; }
        public RebarShape RebarShape1 { get; set; }
        public RebarShape RebarShape2 { get; set; }
        public bool isReverse { get; set; }
        public ColumnInfoCollection ColumnInfoCollection { get; set; }
        public View3D View3d { get; set; }
        public List<string> MetaLevels { get; set; }
        public string FirstMetaLevel { get; set; }
        public string LastMetaLevel { get; set; }
        public List<TextBox> txtMetaLevels { get; set; }
        public int PartCount { get; set; }
        public BIM_PORTALDbContext Database { get; set; }
        public List<Project> Projects { get; set; }
        public int ProjectID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public Element Element { get; set; }
        public string B1_Param { get; set; }
        public string B2_Param { get; set; }
        public bool IsColumnParamsOK
        {
            get
            {
                switch (UserType)
                {
                    case UserType.NonAuthorizated:
                        return false;
                    default:
                        return true;

                }
            }
        }
        public bool IsAutoRebaringOK { get; set; }
        public List<TextBox> txtStandardTripLs { get; private set; }
        public List<TextBox> txtImplantLs { get; private set; }
        public List<TextBox> txtImplantPairLs { get; private set; }
        public Autodesk.Revit.UI.UIApplication UIApplication { get; set; }
        public bool IsColumnInfoOK { get { return UserType != UserType.NonAuthorizated; } }
        public string RevitUserName { get { return UIApplication.Application.Username; } }
        public string ProjectName { get { return Document.ProjectInformation.Name; } }
        public Document Document { get { return UIApplication.ActiveUIDocument.Document; } }
        public string Mark { get; set; }
        public List<Label> lblLevels { get; private set; }
        public bool ReinforceStirrupInclue { get; private set; }
        public double DeltaDevelopmentError { get; private set; }
        public int NumberDevelopmentError { get; private set; }
        public bool DevelopErrorInclude { get; private set; }
        public double DevelopLevelOffsetAllowed { get; private set; }
        public bool DevelopLevelOffsetInclude { get; private set; }
        public bool IsInsideBeam { get; private set; }
        public double ImplantLmax { get; private set; }
        public string FamilyStirrup1 { get; private set; }
        public string FamilyStirrup2 { get; private set; }
        public List<double> LTripStandards { get; private set; }
        public List<double> LImplants { get; private set; }
        public List<double> LPlusImplants { get; private set; }
        public string CheckLevel { get; private set; }
        public bool View3dInclude { get; private set; }
        public string View3dText { get; private set; }
        public string StartLevel { get; private set; }
        public string EndLevel { get; private set; }
        public bool IsLockHead { get; private set; }
        public bool IsStartRebar { get; private set; }
        public double Elevation { get; private set; }
        public RebarHookType RebarHookType { get; set; }
        public bool isFirstSetUserName { get; private set; }

        public RebarInputForm(BIM_PORTALDbContext db, List<string> paraNames, Autodesk.Revit.UI.UIApplication uiapp, Element e, List<Level> levels, RebarHookType hookType, List<RebarBarType> rebarTypes, List<RebarShape> rebarShapes, List<View3D> view3ds)
        {
            RebarHookType = hookType;

            IsOK = false; IsAutoRebaringOK = false;
            UserType = UserType.NonAuthorizated;
            InitializeComponent();
            Database = db;
            Projects = Database.Projects;
            foreach (Project pj in Projects)
            {
                cbbProject.Items.Add(pj.Code + "_" + pj.Value);
            }
            foreach (string paraName in paraNames)
            {
                cbbB1.Items.Add(paraName);
                cbbB2.Items.Add(paraName);
            }
            cbbB1.SelectedIndex = 0;
            cbbB2.SelectedIndex = 1;

            colParamGrb.Visibility = System.Windows.Visibility.Hidden;
            genParamGrb.Visibility = System.Windows.Visibility.Hidden;
            rebarChosenGrb.Visibility = System.Windows.Visibility.Hidden;
            devRebarGrb.Visibility = System.Windows.Visibility.Hidden;
            rebarDesGrb.Visibility = System.Windows.Visibility.Hidden;
            levelTitleGrb.Visibility = System.Windows.Visibility.Hidden;
            otherParametersGrb.Visibility = System.Windows.Visibility.Hidden;
            btnOK.IsEnabled = false;

            Element = e; UIApplication = uiapp;

            Levels = levels; RebarTypes = rebarTypes; RebarShapes = rebarShapes; View3ds = view3ds;

            isReverse = false;
            lblCheckb1.Content = ""; lblCheckb2.Content = "";

            ShowDesignInput();
            FirstCheckUserName();
        }

        private void DesignChanged(object sender, EventArgs e)
        {
            bool isStop = false;
            for (int i = 0; i < cbbDesignLevels.Count; i++)
            {
                if (isStop)
                {
                    cbbDesignLevels[i].Visibility = System.Windows.Visibility.Hidden;
                    cbbDesignRebarTypes[i].Visibility = System.Windows.Visibility.Hidden;
                    cbbDesignStirrupType1s[i].Visibility = System.Windows.Visibility.Hidden;
                    cbbDesignStirrupType2s[i].Visibility = System.Windows.Visibility.Hidden;
                    txtDesignN1s[i].Visibility = System.Windows.Visibility.Hidden;
                    txtDesignN2s[i].Visibility = System.Windows.Visibility.Hidden;
                    txtDesignStirrupTB1s[i].Visibility = System.Windows.Visibility.Hidden;
                    txtDesignStirrupTB2s[i].Visibility = System.Windows.Visibility.Hidden;
                    txtDesignStirrupM1s[i].Visibility = System.Windows.Visibility.Hidden;
                    txtDesignStirrupM2s[i].Visibility = System.Windows.Visibility.Hidden;
                    txtDesignSums[i].Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    if (cbbDesignLevels[i].SelectedIndex == -1 || cbbDesignRebarTypes[i].SelectedIndex == -1)
                    {
                        txtDesignSums[i].Content = "";
                        isStop = true; continue;
                    }
                    int n1 = -1, n2 = -1;
                    if (txtDesignN1s[i].Text.Length == 0 || !int.TryParse(txtDesignN1s[i].Text, out n1))
                    {
                        txtDesignSums[i].Content = "";
                        isStop = true; continue;
                    }
                    if (txtDesignN2s[i].Text.Length == 0 || !int.TryParse(txtDesignN2s[i].Text, out n2))
                    {
                        txtDesignSums[i].Content = "";
                        isStop = true; continue;
                    }
                    if (n1 == 0 || n2 == 0)
                    {
                        txtDesignSums[i].Content = "";
                        isStop = true; continue;
                    }
                    txtDesignSums[i].Content = (n1 + n2) * 2 - 4;
                    if (cbbDesignStirrupType1s[i].SelectedIndex == -1 || cbbDesignStirrupType2s[i].SelectedIndex == -1)
                    {
                        isStop = true; continue;
                    }
                    double bt1 = 0, m1 = 0, bt2 = 0, m2 = 0;
                    if (txtDesignStirrupTB1s[i].Text.Length == 0 || !double.TryParse(txtDesignStirrupTB1s[i].Text, out bt1))
                    {
                        isStop = true; continue;
                    }
                    if (txtDesignStirrupM1s[i].Text.Length == 0 || !double.TryParse(txtDesignStirrupM1s[i].Text, out m1))
                    {
                        isStop = true; continue;
                    }
                    if (txtDesignStirrupTB2s[i].Text.Length == 0 || !double.TryParse(txtDesignStirrupTB2s[i].Text, out bt2))
                    {
                        isStop = true; continue;
                    }
                    if (txtDesignStirrupM2s[i].Text.Length == 0 || !double.TryParse(txtDesignStirrupM2s[i].Text, out m2))
                    {
                        isStop = true; continue;
                    }
                    if (bt1 == 0 || bt2 == 0 || m1 == 0 || m2 == 0)
                    {
                        isStop = true; continue;
                    }

                    if (i < cbbDesignLevels.Count - 1)
                    {
                        cbbDesignLevels[i + 1].Visibility = System.Windows.Visibility.Visible;
                        cbbDesignRebarTypes[i + 1].Visibility = System.Windows.Visibility.Visible;
                        cbbDesignStirrupType1s[i + 1].Visibility = System.Windows.Visibility.Visible;
                        cbbDesignStirrupType2s[i + 1].Visibility = System.Windows.Visibility.Visible;
                        txtDesignN1s[i + 1].Visibility = System.Windows.Visibility.Visible;
                        txtDesignN2s[i + 1].Visibility = System.Windows.Visibility.Visible;
                        txtDesignStirrupTB1s[i + 1].Visibility = System.Windows.Visibility.Visible;
                        txtDesignStirrupTB2s[i + 1].Visibility = System.Windows.Visibility.Visible;
                        txtDesignStirrupM1s[i + 1].Visibility = System.Windows.Visibility.Visible;
                        txtDesignStirrupM2s[i + 1].Visibility = System.Windows.Visibility.Visible;
                        txtDesignSums[i + 1].Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
        }
        private void StandardChanged(object sender, EventArgs e)
        {
            bool isStop = false;
            for (int i = 0; i < txtStandardLs.Count; i++)
            {
                if (isStop)
                    txtStandardLs[i].Visibility = System.Windows.Visibility.Hidden;
                else
                {
                    double l = -1;
                    if (txtStandardLs[i].Text.Length == 0 || !double.TryParse(txtStandardLs[i].Text, out l))
                    {
                        isStop = true; continue;
                    }
                    if (l == 0)
                    {
                        isStop = true; continue;
                    }
                    if (i < txtStandardLs.Count - 1)
                        txtStandardLs[i + 1].Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
        private void StandardPairChanged(object sender, EventArgs e)
        {
            bool isStop = false;
            for (int i = 0; i < txtStandardPairLs.Count; i++)
            {
                if (isStop)
                    txtStandardPairLs[i].Visibility = System.Windows.Visibility.Hidden;
                else
                {
                    double l = -1;
                    if (txtStandardPairLs[i].Text.Length == 0 || !double.TryParse(txtStandardPairLs[i].Text, out l))
                    {
                        isStop = true; continue;
                    }
                    if (l == 0)
                    {
                        isStop = true; continue;
                    }
                    if (i < txtStandardPairLs.Count - 1)
                        txtStandardPairLs[i + 1].Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
        private void StandardTripChanged(object sender, TextChangedEventArgs e)
        {
            bool isStop = false;
            for (int i = 0; i < txtStandardTripLs.Count; i++)
            {
                if (isStop)
                    txtStandardTripLs[i].Visibility = System.Windows.Visibility.Hidden;
                else
                {
                    double l = -1;
                    if (txtStandardTripLs[i].Text.Length == 0 || !double.TryParse(txtStandardTripLs[i].Text, out l))
                    {
                        isStop = true; continue;
                    }
                    if (l == 0)
                    {
                        isStop = true; continue;
                    }
                    if (i < txtStandardTripLs.Count - 1)
                        txtStandardTripLs[i + 1].Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
        private void ImplantChanged(object sender, TextChangedEventArgs e)
        {
            bool isStop = false;
            for (int i = 0; i < txtImplantLs.Count; i++)
            {
                if (isStop)
                    txtImplantLs[i].Visibility = System.Windows.Visibility.Hidden;
                else
                {
                    double l = -1;
                    if (txtImplantLs[i].Text.Length == 0 || !double.TryParse(txtImplantLs[i].Text, out l))
                    {
                        isStop = true; continue;
                    }
                    if (l == 0)
                    {
                        isStop = true; continue;
                    }
                    if (i < txtImplantLs.Count - 1)
                        txtImplantLs[i + 1].Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
        private void ImplantPairChanged(object sender, TextChangedEventArgs e)
        {
            bool isStop = false;
            for (int i = 0; i < txtImplantPairLs.Count; i++)
            {
                if (isStop)
                    txtImplantPairLs[i].Visibility = System.Windows.Visibility.Hidden;
                else
                {
                    double l = -1;
                    if (txtImplantPairLs[i].Text.Length == 0 || !double.TryParse(txtImplantPairLs[i].Text, out l))
                    {
                        isStop = true; continue;
                    }
                    if (l == 0)
                    {
                        isStop = true; continue;
                    }
                    if (i < txtImplantPairLs.Count - 1)
                        txtImplantPairLs[i + 1].Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
        private void cbbCheckLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckLevelChanged();
        }
        private void CheckLevelChanged()
        {
            string level = cbbCheckLevel.Text;
            foreach (ColumnInfo ci in ColumnInfoCollection)
            {
                if (ci.Level == level)
                {
                    if (!isReverse)
                    {
                        lblCheckb1.Content = Math.Round(GeomUtil.feet2Milimeter(ci.B1));
                        lblCheckb2.Content = Math.Round(GeomUtil.feet2Milimeter(ci.B2));
                    }
                    else
                    {
                        lblCheckb2.Content = Math.Round(GeomUtil.feet2Milimeter(ci.B1));
                        lblCheckb1.Content = Math.Round(GeomUtil.feet2Milimeter(ci.B2));
                    }
                    return;
                }
            }
            lblCheckb1.Content = ""; lblCheckb2.Content = "";
        }
        private void Reverse_Click(object sender, RoutedEventArgs e)
        {
            isReverse = !isReverse;
            CheckLevelChanged();
        }

        private void SetGeneralParameter()
        {
            ConcreteCover = GeomUtil.DoubleParse(txtConcCover.Text);
            DevelopmenMultiply = GeomUtil.IntParse(txtDevMulti.Text);
            DevelopmentLenghtDistance = GeomUtil.DoubleParse(txtDevsDist.Text);
            ReinforceStirrupInclue = chkReinfStirrInclude.IsChecked.Value;
            DeltaDevelopmentError = GeomUtil.DoubleParse(txtDelDevError.Text);
            NumberDevelopmentError = GeomUtil.IntParse(txtNumDevError.Text);
            DevelopErrorInclude = chkDevErrorInclude.IsChecked.Value;
            DevelopLevelOffsetAllowed = GeomUtil.DoubleParse(txtDevLevelOffAllow.Text);
            DevelopLevelOffsetInclude = chkDevLevelOffInclude.IsChecked.Value;
            ShortenLimit = GeomUtil.DoubleParse(txtShortenLimit.Text);
            AnchorMultiply = GeomUtil.IntParse(txtAncMulti.Text);
            LockHeadMutliply = GeomUtil.IntParse(txtLockHeadMulti.Text);
            ConcreteTopCover = GeomUtil.DoubleParse(txtConcTopCover.Text);
            RatioLH = GeomUtil.DoubleParse(txtRatioLH.Text);
            CoverTopSmall = GeomUtil.DoubleParse(txtCoverTopSmall.Text);

            //var gpi = new GeneralParameterInput()
            //{
            //    ProjectID = ProjectID,
            //    ConcreteCover = ConcreteCover,
            //    DevelopmentMultiply = DevelopmenMultiply,
            //    DevelopmentLengthsDistance = DevelopmentLenghtDistance,
            //    ReinforcementStirrupInclude = ReinforceStirrupInclue,
            //    DeltaDevelopmentError = DeltaDevelopmentError,
            //    NumberDevelopmentError = NumberDevelopmentError,
            //    DevelopmentErrorInclude = DevelopErrorInclude,
            //    DevelopmentLevelOffsetAllowed = DevelopLevelOffsetAllowed,
            //    DevelopmentLevelOffsetInclude = DevelopLevelOffsetInclude,
            //    ShortenLimit = ShortenLimit,
            //    AnchorMultiply = AnchorMultiply,
            //    LockheadMultiply = LockHeadMutliply,
            //    ConcreteTopCover = ConcreteTopCover,
            //    RatioLH = RatioLH,
            //    CoverTopSmall = CoverTopSmall
            //};
            //new GeneralParameterInputDao().Update(gpi);
        }
        private void SetDevelopmentRebar()
        {
            BottomOffset = GeomUtil.DoubleParse(txtBotOff.Text);
            TopOffset = GeomUtil.DoubleParse(txtTopOff.Text);
            BottomOffsetRatio = GeomUtil.DoubleParse(txtBotOffRatio.Text);
            TopOffsetRatio = GeomUtil.DoubleParse(txtTopOffRatio.Text);
            BottomOffsetStirrup = GeomUtil.DoubleParse(txtBotOffStir.Text);
            TopOffsetStirrup = GeomUtil.DoubleParse(txtTopOffStir.Text);
            BottomOffsetRatioStirrup = GeomUtil.DoubleParse(txtBotOffRatioStir.Text);
            TopOffsetRatioStirrup = GeomUtil.DoubleParse(txtTopOffRatioStir.Text);
            IsOffsetCheck = chkOff.IsChecked.Value;
            IsOffsetRatioCheck = chkOffRatio.IsChecked.Value;
            IsOffsetCheckStirrup = chkOffStir.IsChecked.Value;
            IsOffsetRatioCheckStirrup = chkOffRatioStir.IsChecked.Value;
            IsInsideBeam = chkInsideBeam.IsChecked.Value;
            IsStirrupInsideBeam = chkStirInsideBeam.IsChecked.Value;

            //var dr = new DevelopmentRebar()
            //{
            //    ProjectID = ProjectID,
            //    BottomOffset = BottomOffset,
            //    TopOffset = TopOffset,
            //    BottomOffsetRatio = BottomOffsetRatio,
            //    TopOffsetRatio = TopOffsetRatio,
            //    BottomStirrupOffset = BottomOffsetStirrup,
            //    TopStirrupOffset = TopOffsetStirrup,
            //    BottomStirrupOffsetRatio = BottomOffsetRatioStirrup,
            //    TopStirrupOffsetRatio = TopOffsetRatioStirrup,
            //    OffsetInclude = IsOffsetCheck,
            //    OffsetRatioInclude = IsOffsetRatioCheck,
            //    StirrupOffsetInclude = IsOffsetCheckStirrup,
            //    StirrupOffsetRatioInclude = IsOffsetRatioCheckStirrup,
            //    IsInsideBeam = IsInsideBeam,
            //    IsStirrupInsideBeam = IsStirrupInsideBeam
            //};
            //new DevelopmentRebarDao().Update(dr);
        }
        private void SetRebarChosenGeneral()
        {
            Lmax = GeomUtil.DoubleParse(txtLmax.Text);
            Lmin = GeomUtil.DoubleParse(txtLmin.Text);
            Step = GeomUtil.DoubleParse(txtStep.Text);
            ImplantLmax = GeomUtil.DoubleParse(txtImplantLmax.Text);
            FamilyStirrup1 = cbbFamilyStirrup1.Text;
            FamilyStirrup2 = cbbFamilyStirrup2.Text;
            RebarShape1 = cbbFamilyStirrup1.SelectedItem as RebarShape;
            RebarShape2 = cbbFamilyStirrup2.SelectedItem as RebarShape;

            //var rcg = new RebarChosenGeneral()
            //{
            //    ProjectID = ProjectID,
            //    Lmax = Lmax,
            //    Lmin = Lmin,
            //    Step = Step,
            //    LImplantMax = ImplantLmax,
            //    FamilyStirrup1 = FamilyStirrup1,
            //    FamilyStirrup2 = FamilyStirrup2
            //};
            //new RebarChosenGeneralDao().Update(rcg);
        }
        private void SetRebarChosens()
        {
            List<RebarChosen> rcs = new List<RebarChosen>();
            LStandards = GetRebarChosen(txtStandardLs);
            //foreach (double l in LStandards)
            //{
            //    var rc = new RebarChosen()
            //    {
            //        ProjectID = ProjectID,
            //        LStandard = l,
            //        LType = RebarChosenType.Standard.ToString()
            //    };
            //    rcs.Add(rc);
            //}

            LPlusStandards = GetRebarChosen(txtStandardPairLs);
            //foreach (double l in LPlusStandards)
            //{
            //    var rc = new RebarChosen()
            //    {
            //        ProjectID = ProjectID,
            //        LStandard = l,
            //        LType = RebarChosenType.StandardPair.ToString()
            //    };
            //    rcs.Add(rc);
            //}

            LTripStandards = GetRebarChosen(txtStandardTripLs);
            //foreach (double l in LTripStandards)
            //{
            //    var rc = new RebarChosen()
            //    {
            //        ProjectID = ProjectID,
            //        LStandard = l,
            //        LType = RebarChosenType.StandardTrip.ToString()
            //    };
            //    rcs.Add(rc);
            //}

            LImplants = GetRebarChosen(txtImplantLs);
            //foreach (double l in LImplants)
            //{
            //    var rc = new RebarChosen()
            //    {
            //        ProjectID = ProjectID,
            //        LStandard = l,
            //        LType = RebarChosenType.Implant.ToString()
            //    };
            //    rcs.Add(rc);
            //}

            LPlusImplants = GetRebarChosen(txtImplantPairLs);
            //foreach (double l in LPlusImplants)
            //{
            //    var rc = new RebarChosen()
            //    {
            //        ProjectID = ProjectID,
            //        LStandard = l,
            //        LType = RebarChosenType.ImplantPair.ToString()
            //    };
            //    rcs.Add(rc);
            //}

            //new RebarChosenDao().Update(rcs);
        }
        private List<double> GetRebarChosen(List<TextBox> txtStands)
        {
            List<double> lStands = new List<double>();
            for (int i = 0; i < txtStands.Count; i++)
            {
                double l = -1;
                if (txtStands[i].Text.Length == 0 || !double.TryParse(txtStands[i].Text, out l))
                    break;
                if (l == 0)
                    break;
                lStands.Add(l);
            }
            return lStands;
        }
        private void SetOtherParameter()
        {
            CheckLevel = cbbCheckLevel.Text;
            View3dInclude = chkView3d.IsChecked.Value;
            View3dText = cbbView3d.Text;
            PartCount = GeomUtil.IntParse(txtPartCount.Text);

            //var op = new OtherParameter()
            //{
            //    ProjectID = ProjectID,
            //    CheckLevel = CheckLevel,
            //    View3dInclude = View3dInclude,
            //    View3d = View3dText,
            //    PartCount = PartCount,
            //    Mark = Mark
            //};

            //new OtherParameterDao().Update(op);
        }
        private void SetLevelTitles()
        {
            MetaLevels = new List<string>();
            //List<LevelTitle> lts = new List<LevelTitle>();
            for (int i = 0; i < Levels.Count; i++)
            {
                string title = txtMetaLevels[i].Text;
                //var lt = new LevelTitle()
                //{
                //    ProjectID = ProjectID,
                //    Level = Levels[i].Name,
                //    Title = title
                //};
                //lts.Add(lt);
                MetaLevels.Add(title);
            }

            //new LevelTitleDao().Update(lts);
        }
        private void SetRebarDesignGeneral()
        {
            StartLevel = cbbStartLevel.Text;
            Elevation = (cbbStartLevel.SelectedItem as Level).Elevation;
            EndLevel = cbbEndLevel.Text;
            RebarZ1 = GeomUtil.DoubleParse(txtRebarOff1.Text);
            RebarZ2 = GeomUtil.DoubleParse(txtRebarOff2.Text);
            IsLockHead = rbtLockHead.IsChecked.Value;
            IsStartRebar = rbtStartRebar.IsChecked.Value;

            //var rdg = new RebarDesignGeneral()
            //{
            //    ProjectID = ProjectID,
            //    Mark = Mark,
            //    StartLevel = StartLevel,
            //    EndLevel = EndLevel,
            //    IsLockHead = IsLockHead,
            //    IsStartRebar = IsStartRebar,
            //    RebarStartZ1 = RebarZ1,
            //    RebarStartZ2 = RebarZ2,
            //    CreateDate = DateTime.Now
            //};
            //new RebarDesignGeneralDao().Update(rdg);
        }
        private void SetRebarDesigns()
        {
            //List<RebarDesign> rds = new List<RebarDesign>();
            ColumnStandardDesignInputs = new List<ColumnStandardDesignInput>();
            ColumnStirrupDesignInputs = new List<ColumnStirrupDesignInput>();

            for (int i = 0; i < cbbDesignLevels.Count; i++)
            {
                if (cbbDesignLevels[i].SelectedIndex == -1 || cbbDesignRebarTypes[i].SelectedIndex == -1 ||
                    cbbDesignStirrupType1s[i].SelectedIndex == -1 || cbbDesignStirrupType2s[i].SelectedIndex == -1)
                    break;
                int n1 = -1, n2 = -1;
                double bt1 = -1, bt2 = -1, m1 = -1, m2 = -1;
                if (txtDesignN1s[i].Text.Length == 0 || !int.TryParse(txtDesignN1s[i].Text, out n1))
                    break;
                if (txtDesignN2s[i].Text.Length == 0 || !int.TryParse(txtDesignN2s[i].Text, out n2))
                    break;
                if (txtDesignStirrupTB1s[i].Text.Length == 0 || !double.TryParse(txtDesignStirrupTB1s[i].Text, out bt1))
                    break;
                if (txtDesignStirrupTB2s[i].Text.Length == 0 || !double.TryParse(txtDesignStirrupTB2s[i].Text, out bt2))
                    break;
                if (txtDesignStirrupM1s[i].Text.Length == 0 || !double.TryParse(txtDesignStirrupM1s[i].Text, out m1))
                    break;
                if (txtDesignStirrupM2s[i].Text.Length == 0 || !double.TryParse(txtDesignStirrupM2s[i].Text, out m2))
                    break;
                if (n1 == 0 || n2 == 0 || bt1 == 0 || bt2 == 0 || m1 == 0 || m2 == 0)
                    break;

                //var rd = new RebarDesign()
                //{
                //    ProjectID = ProjectID,
                //    Mark = Mark,
                //    StartLevel = StartLevel,
                //    Elevation = (cbbDesignLevels[i].SelectedItem as Level).Elevation,
                //    DesignLevel = cbbDesignLevels[i].Text,
                //    RebarType = cbbDesignRebarTypes[i].Text,
                //    RebarB1 = GeomUtil.IntParse(txtDesignN1s[i].Text),
                //    RebarB2 = GeomUtil.IntParse(txtDesignN2s[i].Text),
                //    StirrupType1 = cbbDesignStirrupType1s[i].Text,
                //    StirrupType2 = cbbDesignStirrupType2s[i].Text,
                //    StirrupTBSpacing1 = GeomUtil.DoubleParse(txtDesignStirrupTB1s[i].Text),
                //    StirrupMSpacing1 = GeomUtil.DoubleParse(txtDesignStirrupM1s[i].Text),
                //    StirrupTBSpacing2 = GeomUtil.DoubleParse(txtDesignStirrupTB2s[i].Text),
                //    StirrupMSpacing2 = GeomUtil.DoubleParse(txtDesignStirrupM2s[i].Text)
                //};
                //rds.Add(rd);

                ColumnStandardDesignInputs.Add(new ColumnStandardDesignInput(cbbDesignLevels[i].SelectedItem as Level,
                    cbbDesignRebarTypes[i].SelectedItem as RebarBarType, n1, n2)
                { Index = i });
                ColumnStirrupDesignInputs.Add(new ColumnStirrupDesignInput(cbbDesignLevels[i].SelectedItem as Level,
                    cbbDesignStirrupType1s[i].SelectedItem as RebarBarType, cbbDesignStirrupType2s[i].SelectedItem as RebarBarType,
                    GeomUtil.milimeter2Feet(bt1), GeomUtil.milimeter2Feet(bt2), GeomUtil.milimeter2Feet(m1), GeomUtil.milimeter2Feet(m2)));
            }

            //new RebarDesignDao().Update(rds);
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Mark = cbbMark.Text;
            //var mark = new Mark()
            //{
            //    ProjectID = ProjectID,
            //    CreateDate = DateTime.Now,
            //    Mark1 = Mark
            //};
            //new MarkDao().Update(mark);

            SetGeneralParameter();
            SetDevelopmentRebar();
            SetRebarChosenGeneral();
            SetRebarChosens();
            SetOtherParameter();
            SetLevelTitles();
            SetRebarDesignGeneral();
            SetRebarDesigns();

            IsOK = true;
            GetGeneralNote();
            Window.Close();
        }
        private void GetGeneralNote()
        {
            GeneralNote = new GeneralNote()
            {
                TopOffset = GeomUtil.milimeter2Feet(TopOffset),
                BottomOffset = GeomUtil.milimeter2Feet(BottomOffset),
                IsOffsetCheck = IsOffsetCheck,

                TopOffsetRatio = TopOffsetRatio,
                BottomOffsetRatio = BottomOffsetRatio,
                IsOffsetRatioCheck = IsOffsetRatioCheck,

                TopOffsetStirrup = GeomUtil.milimeter2Feet(TopOffsetStirrup),
                BottomOffsetStirrup = GeomUtil.milimeter2Feet(BottomOffsetStirrup),
                IsOffsetCheckStirrup = IsOffsetCheckStirrup,

                TopOffsetRatioStirrup = TopOffsetRatioStirrup,
                BottomOffsetRatioStirrup = BottomOffsetRatioStirrup,
                IsOffsetRatioCheckStirrup = IsOffsetRatioCheckStirrup,

                IsStirrupInsideBeam = IsStirrupInsideBeam,
                RebarStandardOffsetControl = ConstantValue.RebarStandardOffsetControl,

                CoverConcrete = GeomUtil.milimeter2Feet(ConcreteCover),
                CoverTop = GeomUtil.milimeter2Feet(ConcreteTopCover),
                CoverTopSmall = GeomUtil.milimeter2Feet(CoverTopSmall),

                FirstRebarZ1 = Elevation + GeomUtil.milimeter2Feet(RebarZ1),
                FirstRebarZ2 = Elevation + GeomUtil.milimeter2Feet(RebarZ2),

                LoopCount = ConstantValue.LoopCount,
                RebarHookType = RebarHookType,
                RebarBarTypes = RebarTypes,
                RebarLayoutRule = RebarLayoutRule.FixedNumber,

                ShortenLimit = GeomUtil.milimeter2Feet(ShortenLimit),
                LockHeadMultiply = LockHeadMutliply,
                RatioLH = RatioLH,
                DevelopmentMultiply = DevelopmenMultiply,
                AnchorMultiply = AnchorMultiply,

                Lmax = GeomUtil.milimeter2Feet(Lmax),
                Lmin = GeomUtil.milimeter2Feet(Lmin),
                Step = GeomUtil.milimeter2Feet(Step),

                LStandards = LStandards.Select(x => GeomUtil.milimeter2Feet(x)).ToList(),
                LPlusStandards = LPlusStandards.Select(x => GeomUtil.milimeter2Feet(x)).ToList(),
                LImplants = LImplants.Select(x => GeomUtil.milimeter2Feet(x)).ToList(),
                LPlusImplants = LPlusImplants.Select(x => GeomUtil.milimeter2Feet(x)).ToList()
            };
            bool isSet = false;
            foreach (ColumnInfo ci in ColumnInfoCollection)
            {
                if (ci.Bottom < GeneralNote.FirstRebarZ1 && GeneralNote.FirstRebarZ1 < ci.Top)
                {
                    isSet = true;
                    GeneralNote.FirstColumnIndex = ci.Index;
                    break;
                }
            }
            if (!isSet) throw new Exception("Wrong First Rebar Top Z!");

            FirstMetaLevel = MetaLevels[cbbStartLevel.SelectedIndex];
            LastMetaLevel = MetaLevels[cbbEndLevel.SelectedIndex];
        }
        private void chkView3d_Checked(object sender, RoutedEventArgs e)
        {
            if (!chkView3d.IsEnabled) return;
            if (chkView3d.IsChecked.Value)
                cbbView3d.Visibility = System.Windows.Visibility.Visible;
            else
                cbbView3d.Visibility = System.Windows.Visibility.Hidden;
        }

        private void chkOff_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (!chk.IsEnabled) return;
            if (chk.IsChecked.Value)
            {
                chkOff.IsChecked = true;
                chkOff2.IsChecked = true;
                txtBotOff.IsEnabled = true;
                txtTopOff.IsEnabled = true;
            }
            else
            {
                chkOff.IsChecked = false;
                chkOff2.IsChecked = false;
                txtBotOff.IsEnabled = false;
                txtTopOff.IsEnabled = false;
            }
        }

        private void chkOffRatio_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (!chk.IsEnabled) return;
            if (chk.IsChecked.Value)
            {
                chkOffRatio.IsChecked = true;
                chkOffRatio2.IsChecked = true;
                txtBotOffRatio.IsEnabled = true;
                txtTopOffRatio.IsEnabled = true;
            }
            else
            {
                chkOffRatio.IsChecked = false;
                chkOffRatio2.IsChecked = false;
                txtBotOffRatio.IsEnabled = false;
                txtTopOffRatio.IsEnabled = false;
            }
        }

        private void chkOffStir_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (!chk.IsEnabled) return;
            if (chk.IsChecked.Value)
            {
                chkOffStir.IsChecked = true;
                chkOffStir2.IsChecked = true;
                txtBotOffStir.IsEnabled = true;
                txtTopOffStir.IsEnabled = true;
            }
            else
            {
                chkOffStir.IsChecked = false;
                chkOffStir2.IsChecked = false;
                txtBotOffStir.IsEnabled = false;
                txtTopOffStir.IsEnabled = false;
            }
        }

        private void chkOffRatioStir_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (!chk.IsEnabled) return;
            if (chk.IsChecked.Value)
            {
                chkOffRatioStir.IsChecked = true;
                chkOffRatioStir2.IsChecked = true;
                txtBotOffRatioStir.IsEnabled = true;
                txtTopOffRatioStir.IsEnabled = true;
            }
            else
            {
                chkOffRatioStir.IsChecked = false;
                chkOffRatioStir2.IsChecked = false;
                txtBotOffRatioStir.IsEnabled = false;
                txtTopOffRatioStir.IsEnabled = false;
            }
        }

        private void btnCheckUser_Click(object sender, RoutedEventArgs e)
        {
            CheckUserName();
        }
        private void FirstCheckUserName()
        {
            isFirstSetUserName = true;
            //var user = new UserManagementDao().GetUserType(UIApplication);
            //if (user != null)
            //{
            //    txtUserName.Text = user.WebUsername;
            //    txtPassword.Password = user.WebPassword;
            //}
            for (int i = 0; i < cbbProject.Items.Count; i++)
            {
                if (ProjectName == (cbbProject.Items[i] as string))
                {
                    cbbProject.SelectedIndex = i;
                    break;
                }
            }
            CheckUserName(null);
        }
        private void CheckUserName(UserManagement user = null)
        {
            int res = 0;
            if (ProjectName != (cbbProject.SelectedItem as string))
                goto L1;
            ProjectID = Projects[cbbProject.SelectedIndex].SYS_ID;
            UserName = user == null ? txtUserName.Text : user.WebUsername;
            Password = user == null ? Encrypting.ToSHA256(txtPassword.Password) : user.WebPassword;

            res = Database.Login(UserName, Password, ProjectID);
            L1:
            switch (res)
            {
                case 0:
                    if (!isFirstSetUserName)
                        MessageBox.Show("Tên đăng nhập, mật khẩu hoặc mã dự án không đúng; hoặc bạn không được phân quyền để vào dự án này!");
                    UserType = UserType.NonAuthorizated;
                    colParamGrb.Visibility = System.Windows.Visibility.Hidden;

                    cbbProject.IsEnabled = true;
                    txtUserName.IsEnabled = true;
                    txtPassword.IsEnabled = true;
                    btnCheckUser.IsEnabled = true;
                    btnLogout.IsEnabled = false;

                    break;
                case 1:
                    //user = new UserManagement()
                    //{
                    //    ProjectID = ProjectID,
                    //    ProjectName = ProjectName,
                    //    ChangeMacAddress = ComputerInfo.GetMacAddress().ToString(),
                    //    WebUsername = UserName,
                    //    WebPassword = Password,
                    //    LoginType = UserType.User.ToString(),
                    //    IsChangePending = true
                    //};
                    //if (!(new UserManagementDao().Update(user, isFirstSetUserName)))
                    //    goto L2;

                    UserType = UserType.User;
                    colParamGrb.Visibility = System.Windows.Visibility.Visible;
                    cbbB1.IsEnabled = false;
                    cbbB2.IsEnabled = false;
                    btnCheckParams.IsEnabled = true;

                    cbbProject.IsEnabled = false;
                    txtUserName.IsEnabled = false;
                    txtPassword.IsEnabled = false;
                    btnCheckUser.IsEnabled = false;
                    btnLogout.IsEnabled = true;
                    break;
                case 2:
                    //user = new UserManagement()
                    //{
                    //    ProjectID = ProjectID,
                    //    ProjectName = ProjectName,
                    //    ChangeMacAddress = ComputerInfo.GetMacAddress().ToString(),
                    //    WebUsername = UserName,
                    //    WebPassword = Password,
                    //    LoginType = UserType.Admin.ToString(),
                    //    IsChangePending = true
                    //};
                    //if (!(new UserManagementDao().Update(user, isFirstSetUserName)))
                    //    goto L2;

                    UserType = UserType.Admin;
                    colParamGrb.Visibility = System.Windows.Visibility.Visible;
                    cbbB1.IsEnabled = true;
                    cbbB2.IsEnabled = true;
                    btnCheckParams.IsEnabled = true;

                    cbbProject.IsEnabled = false;
                    txtUserName.IsEnabled = false;
                    txtPassword.IsEnabled = false;
                    btnCheckUser.IsEnabled = false;
                    btnLogout.IsEnabled = true;
                    break;
            }

            isFirstSetUserName = false;
            FirstCheckColumnParameter();
        }
        private void FirstCheckColumnParameter()
        {
            switch (UserType)
            {
                case UserType.NonAuthorizated:
                    genParamGrb.Visibility = System.Windows.Visibility.Hidden;
                    rebarChosenGrb.Visibility = System.Windows.Visibility.Hidden;
                    devRebarGrb.Visibility = System.Windows.Visibility.Hidden;
                    rebarChosenGrb.Visibility = System.Windows.Visibility.Hidden;
                    levelTitleGrb.Visibility = System.Windows.Visibility.Hidden;
                    rebarDesGrb.Visibility = System.Windows.Visibility.Hidden;
                    otherParametersGrb.Visibility = System.Windows.Visibility.Hidden;
                    btnOK.IsEnabled = false;
                    return;
                case UserType.User:
                    genParamGrb.Visibility = System.Windows.Visibility.Visible;
                    rebarChosenGrb.Visibility = System.Windows.Visibility.Visible;
                    devRebarGrb.Visibility = System.Windows.Visibility.Visible;
                    rebarChosenGrb.Visibility = System.Windows.Visibility.Visible;
                    levelTitleGrb.Visibility = System.Windows.Visibility.Visible;
                    rebarDesGrb.Visibility = System.Windows.Visibility.Visible;
                    otherParametersGrb.Visibility = System.Windows.Visibility.Visible;
                    btnOK.IsEnabled = true;

                    #region General Parameters
                    txtConcCover.IsEnabled = false;
                    txtDevMulti.IsEnabled = false;
                    txtDevsDist.IsEnabled = false;
                    chkReinfStirrInclude.IsEnabled = false;
                    txtDelDevError.IsEnabled = false;
                    txtNumDevError.IsEnabled = false;
                    chkDevErrorInclude.IsEnabled = false;
                    txtDevLevelOffAllow.IsEnabled = false;
                    chkDevLevelOffInclude.IsEnabled = false;
                    txtShortenLimit.IsEnabled = false;
                    txtAncMulti.IsEnabled = false;
                    txtLockHeadMulti.IsEnabled = false;
                    txtConcTopCover.IsEnabled = false;
                    txtRatioLH.IsEnabled = false;
                    txtCoverTopSmall.IsEnabled = false;
                    #endregion

                    #region Development Rebar Specification
                    txtBotOff.IsEnabled = false;
                    txtBotOffRatio.IsEnabled = false;
                    txtTopOff.IsEnabled = false;
                    txtTopOffRatio.IsEnabled = false;
                    chkOff.IsEnabled = false;
                    chkOff2.IsEnabled = false;
                    chkOffRatio.IsEnabled = false;
                    chkOffRatio2.IsEnabled = false;
                    chkInsideBeam.IsEnabled = false;

                    txtBotOffStir.IsEnabled = false;
                    txtBotOffRatioStir.IsEnabled = false;
                    txtTopOffStir.IsEnabled = false;
                    txtTopOffRatioStir.IsEnabled = false;
                    chkOffStir.IsEnabled = false;
                    chkOffStir2.IsEnabled = false;
                    chkOffRatioStir.IsEnabled = false;
                    chkOffRatioStir2.IsEnabled = false;
                    chkStirInsideBeam.IsEnabled = false;
                    #endregion

                    #region Rebar Chosen Length
                    for (int i = 0; i < txtStandardLs.Count; i++)
                    {
                        txtStandardLs[i].IsEnabled = false;
                        txtStandardPairLs[i].IsEnabled = false;
                        txtStandardTripLs[i].IsEnabled = false;
                        txtImplantLs[i].IsEnabled = false;
                        txtImplantPairLs[i].IsEnabled = false;
                    }
                    txtLmax.IsEnabled = false;
                    txtLmin.IsEnabled = false;
                    txtStep.IsEnabled = false;
                    txtImplantLmax.IsEnabled = false;

                    cbbFamilyStirrup1.IsEnabled = false;
                    cbbFamilyStirrup2.IsEnabled = false;
                    #endregion

                    #region Level Title
                    foreach (TextBox txt in txtMetaLevels)
                    {
                        txt.IsEnabled = false;
                    }
                    #endregion
                    break;
                case UserType.Admin:
                    genParamGrb.Visibility = System.Windows.Visibility.Visible;
                    rebarChosenGrb.Visibility = System.Windows.Visibility.Visible;
                    devRebarGrb.Visibility = System.Windows.Visibility.Visible;
                    rebarChosenGrb.Visibility = System.Windows.Visibility.Visible;
                    levelTitleGrb.Visibility = System.Windows.Visibility.Visible;
                    rebarDesGrb.Visibility = System.Windows.Visibility.Visible;
                    otherParametersGrb.Visibility = System.Windows.Visibility.Visible;
                    btnOK.IsEnabled = true;

                    #region General Parameters
                    txtConcCover.IsEnabled = true;
                    txtDevMulti.IsEnabled = true;
                    txtDevsDist.IsEnabled = true;
                    chkReinfStirrInclude.IsEnabled = true;
                    txtDelDevError.IsEnabled = true;
                    txtNumDevError.IsEnabled = true;
                    chkDevErrorInclude.IsEnabled = true;
                    txtDevLevelOffAllow.IsEnabled = true;
                    chkDevLevelOffInclude.IsEnabled = true;
                    txtShortenLimit.IsEnabled = true;
                    txtAncMulti.IsEnabled = true;
                    txtLockHeadMulti.IsEnabled = true;
                    txtConcTopCover.IsEnabled = true;
                    txtRatioLH.IsEnabled = true;
                    txtCoverTopSmall.IsEnabled = true;
                    #endregion

                    #region Development Rebar Specification
                    txtBotOff.IsEnabled = true;
                    txtBotOffRatio.IsEnabled = true;
                    txtTopOff.IsEnabled = true;
                    txtTopOffRatio.IsEnabled = true;
                    chkOff.IsEnabled = true;
                    chkOff2.IsEnabled = true;
                    chkOffRatio.IsEnabled = true;
                    chkOffRatio2.IsEnabled = true;
                    chkInsideBeam.IsEnabled = true;

                    txtBotOffStir.IsEnabled = true;
                    txtBotOffRatioStir.IsEnabled = true;
                    txtTopOffStir.IsEnabled = true;
                    txtTopOffRatioStir.IsEnabled = true;
                    chkOffStir.IsEnabled = true;
                    chkOffStir2.IsEnabled = true;
                    chkOffRatioStir.IsEnabled = true;
                    chkOffRatioStir2.IsEnabled = true;
                    chkStirInsideBeam.IsEnabled = true;
                    #endregion

                    #region Rebar Chosen Length
                    for (int i = 0; i < txtStandardLs.Count; i++)
                    {
                        txtStandardLs[i].IsEnabled = true;
                        txtStandardPairLs[i].IsEnabled = true;
                        txtStandardTripLs[i].IsEnabled = true;
                        txtImplantLs[i].IsEnabled = true;
                        txtImplantPairLs[i].IsEnabled = true;
                    }
                    txtLmax.IsEnabled = true;
                    txtLmin.IsEnabled = true;
                    txtStep.IsEnabled = true;
                    txtImplantLmax.IsEnabled = true;

                    cbbFamilyStirrup1.IsEnabled = true;
                    cbbFamilyStirrup2.IsEnabled = true;
                    #endregion

                    #region Level Title
                    foreach (TextBox txt in txtMetaLevels)
                    {
                        txt.IsEnabled = true;
                    }
                    #endregion
                    break;
            }
            //var res = new ColumnParameterDao().GetColumnParameter(ProjectID);
            //if (res == null)
            //{
                cbbB1.SelectedIndex = 0;
                cbbB2.SelectedIndex = 1;
            //}
            //else
            //{
            //    for (int i = 0; i < cbbB1.Items.Count; i++)
            //    {
            //        if (cbbB1.Items[i] as string == res.B1_Param)
            //            cbbB1.SelectedIndex = i;
            //        if (cbbB2.Items[i] as string == res.B2_Param)
            //            cbbB2.SelectedIndex = i;
            //    }
            //}
            if (cbbB1.SelectedIndex == -1) cbbB1.SelectedIndex = 0;
            if (cbbB2.SelectedIndex == -1) cbbB2.SelectedIndex = 1;

            //var marks = new MarkDao().GetMarks(ProjectID);
            //if (marks.Count != 0)
            //{
            //    List<string> markString = marks.Select(x => x.Mark1).ToList();
            //    cbbMark.ItemsSource = null;
            //    cbbMark.ItemsSource = markString;
            //    cbbMark.SelectedIndex = 0;
            //}
            CheckColumnParameter();
        }
        private void CheckColumnParameter()
        {
            B1_Param = cbbB1.SelectedItem as string;
            B2_Param = cbbB2.SelectedItem as string;
            if (B1_Param == null || B2_Param == null) throw new Exception("Không tồn tại tham biến kích thước cột!");
            Mark = cbbMark.Text;
            //ColumnParameter colParam = new ColumnParameter()
            //{
            //    ProjectID = ProjectID,
            //    B1_Param = B1_Param,
            //    B2_Param = B2_Param
            //};
            //new ColumnParameterDao().Update(colParam);

            ColumnInfoCollection = new ColumnInfoCollection(Document, Element, B1_Param, B2_Param);
            for (int i = 0; i < cbbDesignLevels[0].Items.Count; i++)
            {
                Level lv = cbbDesignLevels[0].Items[i] as Level;
                if (lv.Name == ColumnInfoCollection[0].Level)
                {
                    cbbDesignLevels[0].SelectedIndex = i;
                    cbbDesignLevels[0].IsEnabled = false;
                    break;
                }
            }

            FirstCheckGeneralInput();
        }
        private void FirstCheckGeneralInput()
        {
            GetGeneralParameterInput();
            GetDevelopmentRebarInput();
            GetRebarChosenGeneral();
            GetRebarChosens();
            GetOtherParameters();
            GetLevelTitles();
            GetRebarDesignGeneral();
            GetRebarDesigns();
        }
        private void GetGeneralParameterInput()
        {
            chkDevErrorInclude.IsChecked = true; chkDevLevelOffInclude.IsChecked = true;
            chkDevErrorInclude.IsChecked = false; chkDevLevelOffInclude.IsChecked = false;
            //var genParamInput = new GeneralParameterInputDao().GetGeneralParameterInput(ProjectID);
            //if (genParamInput == null) return;
            //txtConcCover.Text = genParamInput.ConcreteCover.ToString();
            //txtDevMulti.Text = genParamInput.DevelopmentMultiply.ToString();
            //txtDevsDist.Text = genParamInput.DevelopmentLengthsDistance.ToString();
            //txtDelDevError.Text = genParamInput.DeltaDevelopmentError.ToString();
            //txtNumDevError.Text = genParamInput.NumberDevelopmentError.ToString();
            //txtDevLevelOffAllow.Text = genParamInput.DevelopmentLevelOffsetAllowed.ToString();
            //txtShortenLimit.Text = genParamInput.ShortenLimit.ToString();
            //txtAncMulti.Text = genParamInput.AnchorMultiply.ToString();
            //txtLockHeadMulti.Text = genParamInput.LockheadMultiply.ToString();
            //txtConcTopCover.Text = genParamInput.ConcreteTopCover.ToString();
            //txtRatioLH.Text = genParamInput.RatioLH.ToString();
            //txtCoverTopSmall.Text = genParamInput.CoverTopSmall.ToString();

            //chkDevErrorInclude.IsChecked = genParamInput.DevelopmentErrorInclude;
            //chkDevLevelOffInclude.IsChecked = genParamInput.DevelopmentLevelOffsetInclude;
        }
        private void GetDevelopmentRebarInput()
        {
            chkOff.IsChecked = true; chkOffStir.IsChecked = true;
            chkOffRatio.IsChecked = true; chkOffRatioStir.IsChecked = true;
            chkOff.IsChecked = false; chkOffStir.IsChecked = false;
            chkOffRatio.IsChecked = false; chkOffRatioStir.IsChecked = false;
            //var devRebarInput = new DevelopmentRebarDao().GetDevelopmentRebar(ProjectID);
            //if (devRebarInput == null) return;
            //txtBotOff.Text = devRebarInput.BottomOffset.ToString();
            //txtBotOffRatio.Text = devRebarInput.BottomOffsetRatio.ToString();
            //txtTopOff.Text = devRebarInput.TopOffset.ToString();
            //txtTopOffRatio.Text = devRebarInput.TopOffsetRatio.ToString();
            //txtBotOffStir.Text = devRebarInput.BottomStirrupOffset.ToString();
            //txtBotOffRatioStir.Text = devRebarInput.BottomStirrupOffsetRatio.ToString();
            //txtTopOffStir.Text = devRebarInput.TopStirrupOffset.ToString();
            //txtTopOffRatioStir.Text = devRebarInput.TopStirrupOffsetRatio.ToString();
            //chkInsideBeam.IsChecked = devRebarInput.IsInsideBeam;
            //chkStirInsideBeam.IsChecked = devRebarInput.IsStirrupInsideBeam;

            //chkOff.IsChecked = devRebarInput.OffsetInclude;
            //chkOffStir.IsChecked = devRebarInput.StirrupOffsetInclude;
            //chkOffRatio.IsChecked = devRebarInput.OffsetRatioInclude;
            //chkOffRatioStir.IsChecked = devRebarInput.StirrupOffsetRatioInclude;
        }
        private void GetRebarChosenGeneral()
        {
            //var rebarChosenGen = new RebarChosenGeneralDao().GetRebarChosenGeneral(ProjectID);
            //if (rebarChosenGen == null) return;
            //txtLmax.Text = rebarChosenGen.Lmax.ToString();
            //txtLmin.Text = rebarChosenGen.Lmin.ToString();
            //txtStep.Text = rebarChosenGen.Step.ToString();
            //txtImplantLmax.Text = rebarChosenGen.LImplantMax.ToString();
            //for (int i = 0; i < cbbFamilyStirrup1.Items.Count; i++)
            //{
            //    string rs = (cbbFamilyStirrup1.Items[i] as RebarShape).Name;
            //    if (rs == rebarChosenGen.FamilyStirrup1)
            //        cbbFamilyStirrup1.SelectedIndex = i;
            //    if (rs == rebarChosenGen.FamilyStirrup2)
            //        cbbFamilyStirrup2.SelectedIndex = i;
            //}
        }
        private void GetRebarChosens()
        {
            //var rebarChosens = new RebarChosenDao().GetRebarChosens(ProjectID);
            //if (rebarChosens.Count == 0) return;
            //int stand = 0;
            //int standPair = 0;
            //int standTrip = 0;
            //int implant = 0;
            //int implantPair = 0;
            //foreach (RebarChosen rs in rebarChosens)
            //{
            //    RebarChosenType type = (RebarChosenType)Enum.Parse(typeof(RebarChosenType), rs.LType);
            //    switch (type)
            //    {
            //        case RebarChosenType.Standard:
            //            txtStandardLs[stand].Text = rs.LStandard.ToString();
            //            stand++;
            //            break;
            //        case RebarChosenType.StandardPair:
            //            txtStandardPairLs[standPair].Text = rs.LStandard.ToString();
            //            standPair++;
            //            break;
            //        case RebarChosenType.StandardTrip:
            //            txtStandardTripLs[standTrip].Text = rs.LStandard.ToString();
            //            standTrip++;
            //            break;
            //        case RebarChosenType.Implant:
            //            txtImplantLs[implant].Text = rs.LStandard.ToString();
            //            implant++;
            //            break;
            //        case RebarChosenType.ImplantPair:
            //            txtImplantPairLs[implantPair].Text = rs.LStandard.ToString();
            //            implantPair++;
            //            break;
            //    }
            //}

            //for (int i = stand; i < txtStandardLs.Count; i++)
            //{
            //    txtStandardLs[i].Text = "";
            //}
            //for (int i = standPair; i < txtStandardLs.Count; i++)
            //{
            //    txtStandardPairLs[i].Text = "";
            //}
            //for (int i = standTrip; i < txtStandardLs.Count; i++)
            //{
            //    txtStandardTripLs[i].Text = "";
            //}
            //for (int i = implant; i < txtStandardLs.Count; i++)
            //{
            //    txtImplantLs[i].Text = "";
            //}
            //for (int i = implantPair; i < txtStandardLs.Count; i++)
            //{
            //    txtImplantPairLs[i].Text = "";
            //}
        }
        private void GetLevelTitles()
        {
            //var dao = new LevelTitleDao().GetLevelTitles(ProjectID);
            //if (dao == null) return;

            //for (int i = 0; i < Levels.Count; i++)
            //{
            //    for (int j = 0; j < dao.Count; j++)
            //    {
            //        if (Levels[i].Name == dao[j].Level)
            //        {
            //            txtMetaLevels[i].Text = dao[j].Title;
            //            break;
            //        }
            //    }
            //}
        }
        private void GetOtherParameters()
        {
            chkView3d.IsChecked = true; chkView3d.IsChecked = false;
            //var dao = new OtherParameterDao().GetOtherParameter(ProjectID, Mark);
            //if (dao == null) return;

            //isReverse = dao.IsReversed;
            //for (int i = 0; i < cbbCheckLevel.Items.Count; i++)
            //{
            //    string level = (cbbCheckLevel.Items[i] as Level).Name;
            //    if (level == dao.CheckLevel)
            //    {
            //        cbbCheckLevel.SelectedIndex = i;
            //        break;
            //    }
            //}
            //chkView3d.IsChecked = dao.View3dInclude;
            //for (int i = 0; i < cbbView3d.Items.Count; i++)
            //{
            //    string view3d = (cbbView3d.Items[i] as View3D).Name;
            //    if (view3d == dao.View3d)
            //    {
            //        cbbView3d.SelectedIndex = i;
            //        break;
            //    }
            //}
            //txtPartCount.Text = dao.PartCount.ToString();
        }
        private void GetRebarDesignGeneral()
        {
            rbtLockHead.IsChecked = true;
            //var dao = new RebarDesignGeneralDao().GetRebarDesignGeneral(ProjectID, Mark);
            //if (dao == null) return;

            //StartLevel = dao.StartLevel;
            //for (int i = 0; i < cbbStartLevel.Items.Count; i++)
            //{
            //    string level = (cbbStartLevel.Items[i] as Level).Name;
            //    if (level == dao.StartLevel)
            //        cbbStartLevel.SelectedIndex = i;
            //    if (level == dao.EndLevel)
            //        cbbEndLevel.SelectedIndex = i;
            //}
            //txtRebarOff1.Text = dao.RebarStartZ1.ToString();
            //txtRebarOff2.Text = dao.RebarStartZ2.ToString();
            //rbtLockHead.IsChecked = dao.IsLockHead;
            //rbtStartRebar.IsChecked = dao.IsStartRebar;
        }
        private void GetRebarDesigns()
        {
            //var dao = new RebarDesignDao().GetRebarDesigns(ProjectID, Mark, StartLevel);
            //if (dao == null) return;

            //for (int i = 0; i < dao.Count; i++)
            //{
            //    if (i > 0)
            //    {
            //        for (int j = 0; j < cbbDesignLevel0.Items.Count; j++)
            //        {
            //            string level = (cbbDesignLevel0.Items[j] as Level).Name;
            //            if (level == dao[i].DesignLevel)
            //            {
            //                cbbDesignLevels[i].SelectedIndex = j;
            //                break;
            //            }
            //        }
            //    }
            //    for (int j = 0; j < cbbDesignRebarType0.Items.Count; j++)
            //    {
            //        string rebarType = (cbbDesignRebarType0.Items[j] as RebarBarType).Name;
            //        if (rebarType == dao[i].RebarType)
            //            cbbDesignRebarTypes[i].SelectedIndex = j;
            //        if (rebarType == dao[i].StirrupType1)
            //            cbbDesignStirrupType1s[i].SelectedIndex = j;
            //        if (rebarType == dao[i].StirrupType2)
            //            cbbDesignStirrupType2s[i].SelectedIndex = j;
            //    }
            //    txtDesignN1s[i].Text = dao[i].RebarB1.ToString();
            //    txtDesignN2s[i].Text = dao[i].RebarB2.ToString();
            //    txtDesignStirrupTB1s[i].Text = dao[i].StirrupTBSpacing1.ToString();
            //    txtDesignStirrupM1s[i].Text = dao[i].StirrupMSpacing1.ToString();
            //    txtDesignStirrupTB2s[i].Text = dao[i].StirrupTBSpacing2.ToString();
            //    txtDesignStirrupM2s[i].Text = dao[i].StirrupMSpacing2.ToString();
            //}
            //for (int i = dao.Count; i < cbbDesignLevels.Count; i++)
            //{
            //    if (i > 0)
            //    {
            //        cbbDesignLevels[i].SelectedIndex = -1;
            //    }
            //    cbbDesignRebarTypes[i].SelectedIndex = -1;
            //    cbbDesignStirrupType1s[i].SelectedIndex = -1;
            //    cbbDesignStirrupType2s[i].SelectedIndex = -1;
            //    txtDesignN1s[i].Text = "";
            //    txtDesignN2s[i].Text = "";
            //    txtDesignStirrupTB1s[i].Text = "";
            //    txtDesignStirrupM1s[i].Text = "";
            //    txtDesignStirrupTB2s[i].Text = "";
            //    txtDesignStirrupM2s[i].Text = "";
            //}
        }
        private void btnCheckParams_Click(object sender, RoutedEventArgs e)
        {
            CheckColumnParameter();
        }
        private void ShowDesignInput()
        {
            cbbDesignLevels = new List<ComboBox> { cbbDesignLevel0, cbbDesignLevel1, cbbDesignLevel2, cbbDesignLevel3, cbbDesignLevel4, cbbDesignLevel5, cbbDesignLevel6, cbbDesignLevel7 };
            cbbDesignRebarTypes = new List<ComboBox> { cbbDesignRebarType0, cbbDesignRebarType1, cbbDesignRebarType2, cbbDesignRebarType3, cbbDesignRebarType4, cbbDesignRebarType5, cbbDesignRebarType6, cbbDesignRebarType7 };
            txtDesignN1s = new List<TextBox> { txtDesignN10, txtDesignN11, txtDesignN12, txtDesignN13, txtDesignN14, txtDesignN15, txtDesignN16, txtDesignN17 };
            txtDesignN2s = new List<TextBox> { txtDesignN20, txtDesignN21, txtDesignN22, txtDesignN23, txtDesignN24, txtDesignN25, txtDesignN26, txtDesignN27 };
            cbbDesignStirrupType1s = new List<ComboBox> { cbbDesignStirrupType10, cbbDesignStirrupType11, cbbDesignStirrupType12, cbbDesignStirrupType13, cbbDesignStirrupType14, cbbDesignStirrupType15, cbbDesignStirrupType16, cbbDesignStirrupType17 };
            cbbDesignStirrupType2s = new List<ComboBox> { cbbDesignStirrupType20, cbbDesignStirrupType21, cbbDesignStirrupType22, cbbDesignStirrupType23, cbbDesignStirrupType24, cbbDesignStirrupType25, cbbDesignStirrupType26, cbbDesignStirrupType27 };
            txtDesignStirrupTB1s = new List<TextBox> { txtDesignStirrupTB10, txtDesignStirrupTB11, txtDesignStirrupTB12, txtDesignStirrupTB13, txtDesignStirrupTB14, txtDesignStirrupTB15, txtDesignStirrupTB16, txtDesignStirrupTB17 };
            txtDesignStirrupTB2s = new List<TextBox> { txtDesignStirrupTB20, txtDesignStirrupTB21, txtDesignStirrupTB22, txtDesignStirrupTB23, txtDesignStirrupTB24, txtDesignStirrupTB25, txtDesignStirrupTB26, txtDesignStirrupTB27 };
            txtDesignStirrupM1s = new List<TextBox> { txtDesignStirrupM10, txtDesignStirrupM11, txtDesignStirrupM12, txtDesignStirrupM13, txtDesignStirrupM14, txtDesignStirrupM15, txtDesignStirrupM16, txtDesignStirrupM17 };
            txtDesignStirrupM2s = new List<TextBox> { txtDesignStirrupM20, txtDesignStirrupM21, txtDesignStirrupM22, txtDesignStirrupM23, txtDesignStirrupM24, txtDesignStirrupM25, txtDesignStirrupM26, txtDesignStirrupM27 };
            txtDesignSums = new List<Label> { txtDesignSum0, txtDesignSum1, txtDesignSum2, txtDesignSum3, txtDesignSum4, txtDesignSum5, txtDesignSum6, txtDesignSum7 };
            txtStandardLs = new List<TextBox> { txtStandardL0, txtStandardL1, txtStandardL2, txtStandardL3, txtStandardL4, txtStandardL5, txtStandardL6 };
            txtStandardPairLs = new List<TextBox> { txtStandardPairL0, txtStandardPairL1, txtStandardPairL2, txtStandardPairL3, txtStandardPairL4, txtStandardPairL5, txtStandardPairL6 };
            txtStandardTripLs = new List<TextBox> { txtStandardTripL0, txtStandardTripL1, txtStandardTripL2, txtStandardTripL3, txtStandardTripL4, txtStandardTripL5, txtStandardTripL6 };
            txtImplantLs = new List<TextBox> { txtImplantL0, txtImplantL1, txtImplantL2, txtImplantL3, txtImplantL4, txtImplantL5, txtImplantL6 };
            txtImplantPairLs = new List<TextBox> { txtImplantPairL0, txtImplantPairL1, txtImplantPairL2, txtImplantPairL3, txtImplantPairL4, txtImplantPairL5, txtImplantPairL6 };

            txtMetaLevels = new List<TextBox>();
            lblLevels = new List<Label>();
            for (int i = 0; i < Levels.Count; i++)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Vertical;
                sp.HorizontalAlignment = HorizontalAlignment.Left;
                sp.VerticalAlignment = VerticalAlignment.Top;
                Label lb = new Label();
                lb.Content = Levels[i].Name;
                lblLevels.Add(lb);
                sp.Children.Add(lb);

                TextBox txt = new TextBox();
                txtMetaLevels.Add(txt);
                sp.Children.Add(txt);
                spFull.Children.Add(sp);
            }
            foreach (Level level in Levels)
            {
                cbbStartLevel.Items.Add(level);
                cbbEndLevel.Items.Add(level);
                cbbCheckLevel.Items.Add(level);
                foreach (ComboBox cbbDesignLevel in cbbDesignLevels)
                {
                    cbbDesignLevel.Items.Add(level);
                }
            }
            foreach (RebarBarType rbt in RebarTypes)
            {
                for (int i = 0; i < cbbDesignRebarTypes.Count; i++)
                {
                    cbbDesignRebarTypes[i].Items.Add(rbt);
                    cbbDesignStirrupType1s[i].Items.Add(rbt);
                    cbbDesignStirrupType2s[i].Items.Add(rbt);
                }
            }
            foreach (RebarShape rbShape in RebarShapes)
            {
                cbbFamilyStirrup1.Items.Add(rbShape);
                cbbFamilyStirrup2.Items.Add(rbShape);
            }
            foreach (View3D v3d in View3ds)
            {
                cbbView3d.Items.Add(v3d);
            }
            cbbStartLevel.DisplayMemberPath = "Name";
            cbbEndLevel.DisplayMemberPath = "Name";
            cbbCheckLevel.DisplayMemberPath = "Name";
            cbbView3d.DisplayMemberPath = "Name";
            cbbFamilyStirrup1.DisplayMemberPath = "Name";
            cbbFamilyStirrup2.DisplayMemberPath = "Name";

            for (int i = 0; i < cbbDesignLevels.Count; i++)
            {
                cbbDesignLevels[i].DisplayMemberPath = "Name";
                cbbDesignLevels[i].SelectionChanged += DesignChanged;
                cbbDesignRebarTypes[i].DisplayMemberPath = "Name";
                cbbDesignRebarTypes[i].SelectionChanged += DesignChanged;
                cbbDesignStirrupType1s[i].DisplayMemberPath = "Name";
                cbbDesignStirrupType1s[i].SelectionChanged += DesignChanged;
                cbbDesignStirrupType2s[i].DisplayMemberPath = "Name";
                cbbDesignStirrupType2s[i].SelectionChanged += DesignChanged;

                txtDesignN1s[i].TextChanged += DesignChanged;
                txtDesignN2s[i].TextChanged += DesignChanged;
                txtDesignStirrupTB1s[i].TextChanged += DesignChanged;
                txtDesignStirrupTB2s[i].TextChanged += DesignChanged;
                txtDesignStirrupM1s[i].TextChanged += DesignChanged;
                txtDesignStirrupM2s[i].TextChanged += DesignChanged;

                if (i > 0)
                {
                    cbbDesignLevels[i].Visibility = System.Windows.Visibility.Hidden;
                    cbbDesignRebarTypes[i].Visibility = System.Windows.Visibility.Hidden;
                    cbbDesignStirrupType1s[i].Visibility = System.Windows.Visibility.Hidden;
                    cbbDesignStirrupType2s[i].Visibility = System.Windows.Visibility.Hidden;
                    txtDesignN1s[i].Visibility = System.Windows.Visibility.Hidden;
                    txtDesignN2s[i].Visibility = System.Windows.Visibility.Hidden;
                    txtDesignStirrupTB1s[i].Visibility = System.Windows.Visibility.Hidden;
                    txtDesignStirrupTB2s[i].Visibility = System.Windows.Visibility.Hidden;
                    txtDesignStirrupM1s[i].Visibility = System.Windows.Visibility.Hidden;
                    txtDesignStirrupM2s[i].Visibility = System.Windows.Visibility.Hidden;
                    txtDesignSums[i].Visibility = System.Windows.Visibility.Hidden;
                }
            }

            for (int i = 0; i < txtStandardLs.Count; i++)
            {
                txtStandardLs[i].TextChanged += StandardChanged;
                txtStandardPairLs[i].TextChanged += StandardPairChanged;
                txtStandardTripLs[i].TextChanged += StandardTripChanged;
                txtImplantLs[i].TextChanged += ImplantChanged;
                txtImplantPairLs[i].TextChanged += ImplantPairChanged;
                if (i >= 1)
                {
                    txtStandardLs[i].Visibility = System.Windows.Visibility.Hidden;
                    txtStandardPairLs[i].Visibility = System.Windows.Visibility.Hidden;
                    txtStandardTripLs[i].Visibility = System.Windows.Visibility.Hidden;
                    txtImplantLs[i].Visibility = System.Windows.Visibility.Hidden;
                    txtImplantPairLs[i].Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            colParamGrb.Visibility = System.Windows.Visibility.Hidden;
            genParamGrb.Visibility = System.Windows.Visibility.Hidden;
            rebarChosenGrb.Visibility = System.Windows.Visibility.Hidden;
            devRebarGrb.Visibility = System.Windows.Visibility.Hidden;
            levelTitleGrb.Visibility = System.Windows.Visibility.Hidden;
            rebarDesGrb.Visibility = System.Windows.Visibility.Hidden;
            otherParametersGrb.Visibility = System.Windows.Visibility.Hidden;
            btnOK.IsEnabled = false;
            btnCheckUser.IsEnabled = true;
            btnLogout.IsEnabled = false;
            txtUserName.Text = "";
            txtPassword.Password = "";
            cbbProject.IsEnabled = true;
            txtUserName.IsEnabled = true;
            txtPassword.IsEnabled = true;
        }
        private void chkDevErrorInclude_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (!chk.IsEnabled) return;
            if (chk.IsChecked.Value)
            {
                txtDelDevError.IsEnabled = true;
                txtNumDevError.IsEnabled = true;
            }
            else
            {
                txtDelDevError.IsEnabled = false;
                txtNumDevError.IsEnabled = false;
            }
        }
        private void chkDevLevelOffInclude_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (!chk.IsEnabled) return;
            if (chk.IsChecked.Value)
            {
                txtDevLevelOffAllow.IsEnabled = true;
            }
            else
            {
                txtDevLevelOffAllow.IsEnabled = false;
            }
        }
    }

}
