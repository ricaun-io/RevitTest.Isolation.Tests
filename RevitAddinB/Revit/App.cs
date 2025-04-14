using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace RevitAddinB.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("RevitAddinB");
            ribbonPanel.CreatePushButton<Commands.Command>()
                .SetText(AssemblyLoadContext.GetLoadContext(typeof(App).Assembly).Name)
                .SetToolTip(AssemblyLoadContext.GetLoadContext(typeof(App).Assembly).ToString())
                .SetLargeImage("Resources/Revit.ico");

            ribbonPanel.CreatePushButton<Commands.Command>()
                .SetText(AssemblyLoadContext.GetLoadContext(typeof(System.Text.Json.JsonDocument).Assembly).Name)
                .SetToolTip(AssemblyLoadContext.GetLoadContext(typeof(System.Text.Json.JsonDocument).Assembly).ToString())
                .SetLongDescription(typeof(System.Text.Json.JsonDocument).Assembly.ToString())
                .SetLargeImage("Resources/Revit.ico");

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }

        public override string ToString()
        {
            return AssemblyLoadContext.GetLoadContext(typeof(System.Text.Json.JsonDocument).Assembly).Name;
        }
    }

}