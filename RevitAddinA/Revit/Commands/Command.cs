using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Runtime.Loader;

namespace RevitAddinA.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand, IExternalCommandAvailability
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            System.Windows.MessageBox.Show(AssemblyLoadContext.GetLoadContext(typeof(Command).Assembly).ToString() + "\n" + AssemblyLoadContext.GetLoadContext(typeof(System.Text.Json.JsonDocument).Assembly).ToString());

            return Result.Succeeded;
        }

        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}
