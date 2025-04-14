using System;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using Autodesk.Revit.UI;
using NUnit.Framework;

namespace RevitTest.Isolation.Tests
{
    public class RevitAddInTests
    {
        protected UIApplication UIApplication;
        [OneTimeSetUp]
        public void Setup(UIApplication application)
        {
            this.UIApplication = application;
        }

        protected UIControlledApplication UIControlledApplication;
        [OneTimeSetUp]
        public void Setup(UIControlledApplication application)
        {
            this.UIControlledApplication = application;
        }

        [TestCase("RevitAddinA")]
        [TestCase("RevitAddinB")]
        public void DisableAddIn_Using_OnShutdown(string addinName)
        {
            foreach (var app in UIApplication.LoadedApplications.OfType<IExternalApplication>())
            {
                if (app.GetType().FullName == $"{addinName}.Revit.App")
                {
                    app.OnShutdown(UIControlledApplication);
                }
            }
        }

        [TestCase("RevitAddinA", "A")]
        [TestCase("RevitAddinB", "B")]
        public void LoadAddIn_With_ContextName(string addinName, string contextName)
        {
            var assemblyPath = Directory.GetFiles(Directory.GetCurrentDirectory(), $"{addinName}*.dll")
                .FirstOrDefault();

            if (assemblyPath is null)
                Assert.Ignore($"No assembly found for {addinName}");

            var revitAddInManifest = RevitAddInUtils.Create(assemblyPath, $"{addinName}.Revit.App", contextName);
            var addinManifestPath = Path.Combine(Path.GetDirectoryName(assemblyPath), $"{addinName}.addin");
            revitAddInManifest.SaveAs2(addinManifestPath);

            var assemblyFileName = Path.GetFileName(assemblyPath);

            UIApplication.LoadAddIn(addinManifestPath);

            var text = File.ReadAllText(addinManifestPath);
            Console.WriteLine(text);

            File.Delete(addinManifestPath);

            var externalApplication = UIApplication.LoadedApplications.OfType<IExternalApplication>()
                .LastOrDefault();

            Console.WriteLine(externalApplication.GetType().Assembly);

            var appContextName = externalApplication.ToString();

            Assert.AreEqual(contextName, appContextName);
        }

        [TestCase("RevitAddinA")]
        [TestCase("RevitAddinB")]
        public void SingleAddIn_Loaded_OnlyInContext(string addinName)
        {
            UIFrameworkServices.CommandHandlerService.invokeCommandHandler("ID_REVIT_MODEL_BROWSER_CLOSE"); // Close Revit Home
            UIFramework.RevitRibbonControl.RibbonControl.ActiveTab = UIFramework.RevitRibbonControl.FindAddInTab();
            UIFramework.RevitRibbonControl.RibbonControl.Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.ApplicationIdle);

            var app = UIApplication.LoadedApplications.OfType<IExternalApplication>()
                .LastOrDefault(e => e.GetType().FullName == $"{addinName}.Revit.App");

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.Location == app.GetType().Assembly.Location)
                .ToList();

            foreach (var assembly in assemblies)
            {
                Console.WriteLine($"{AssemblyLoadContext.GetLoadContext(assembly)}");
            }

            Assert.AreEqual(assemblies.Count, 1);
        }
    }
}
