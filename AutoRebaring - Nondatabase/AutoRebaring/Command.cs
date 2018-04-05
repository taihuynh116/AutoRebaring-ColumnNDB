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

namespace AutoRebaring
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        const string r = "Revit";
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            Transaction tx = new Transaction(doc, "AutoRebaring");
            tx.Start();

            ProjectInfo pi = doc.ProjectInformation;

            tx.Commit();
            return Result.Succeeded;
        }
    }
    [Transaction(TransactionMode.Manual)]
    public class DeleteRebar : IExternalCommand
    {
        const string r = "Revit";
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            Transaction tx = new Transaction(doc, "AutoRebaring");
            tx.Start();

            List<Rebar> rebars = new FilteredElementCollector(doc).OfClass(typeof(Rebar)).Cast<Rebar>().ToList();
            List<ElementId> removeIds = new List<ElementId>();
            foreach (Rebar rebar in rebars)
            {
                string comments = "";
                string mark = "";
                try
                {
                    comments = rebar.LookupParameter("Comments").AsString();
                    //mark = rebar.LookupParameter("Mark").AsString();
                    if (comments == "add-in" /*&& mark == "C1c"*/)
                        removeIds.Add(rebar.Id);
                }
                catch { }
            }
            doc.Delete(removeIds);

            tx.Commit();
            return Result.Succeeded;
        }
    }
}