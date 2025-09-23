using System;
using System.IO;
using System.Linq;
using Autodesk.Revit.UI;
using NUnit.Framework;
using RevitTest.Isolation.Tests.Utils;

namespace RevitTest.Isolation.Tests
{
    public class RevitAddInInstallFileTests
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

        [Explicit]
        [Test]
        public void DeleteAllAddinFile()
        {
            Console.WriteLine(UIApplication.Application.CurrentUserAddinsLocation);
            var files = Directory.GetFiles(UIApplication.Application.CurrentUserAddinsLocation, "*.addin");
            foreach (var file in files)
            {
                Console.WriteLine(file);
                File.Delete(file);
            }
        }

        [Explicit]
        //[TestCase("RevitAddinA", "A")]
        //[TestCase("RevitAddinB", "B")]
        public void CreateRevitAddinFile(string addinName, string contextName)
        {
            var assemblyPath = Directory.GetFiles(Directory.GetCurrentDirectory(), $"{addinName}*.dll")
                .FirstOrDefault();

            if (assemblyPath is null)
                Assert.Ignore($"No assembly found for {addinName}");

            // copy files to Revit Addins folder
            var directory = Path.Combine(Path.GetTempPath(), addinName, DateTime.Now.Ticks.ToString());

            if (Directory.Exists(directory) == false)
                Directory.CreateDirectory(directory);

            var outputPath = Path.Combine(directory, Path.GetFileName(assemblyPath));

            DirectoryUtils.CopyFilesRecursively(Path.GetDirectoryName(assemblyPath), directory);
            Console.WriteLine(outputPath);

            assemblyPath = outputPath;

            var currentUserAddinsLocation = UIApplication.Application.CurrentUserAddinsLocation;
            var revitAddInManifest = RevitAddInUtils.Create(assemblyPath, $"{addinName}.Revit.App", contextName);
            var addinManifestPath = Path.Combine(currentUserAddinsLocation, $"{addinName}.addin");
            revitAddInManifest.SaveAs2(addinManifestPath);
        }

    }
}
