using Autodesk.Revit.UI;
using NUnit.Framework;
using System;
using System.Linq;
using System.Runtime.Loader;

namespace RevitTest.Isolation.Tests
{
    public class RevitCurrentContext
    {
        [Test]
        public void CurrentContext()
        {
            var assembly = typeof(RevitCurrentContext).Assembly;
            Console.WriteLine(assembly);
            Console.WriteLine($"{AssemblyLoadContext.GetLoadContext(assembly)}");
        }

        protected UIApplication UIApplication;
        [OneTimeSetUp]
        public void Setup(UIApplication application)
        {
            this.UIApplication = application;
        }

        [Test]
        public void LoadedApplications_Context()
        {
            foreach (var app in UIApplication.LoadedApplications.OfType<IExternalApplication>())
            {
                var assembly = app.GetType().Assembly;
                Console.WriteLine(assembly);
                Console.WriteLine($"{AssemblyLoadContext.GetLoadContext(assembly)}");
            }
        }
    }
}
