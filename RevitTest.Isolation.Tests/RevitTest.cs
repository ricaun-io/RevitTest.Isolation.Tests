using System;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using NUnit.Framework;

namespace RevitTest.Isolation.Tests
{
    public class RevitTests
    {
        protected Application application;

        [OneTimeSetUp]
        public void Setup(Application application)
        {
            this.application = application;
        }

        [Test]
        public void RevitTests_VersionName()
        {
            Assert.IsNotNull(application);
            Console.WriteLine(application.VersionName);
        }

        [Test]
        public void RevitTests_VersionBuild()
        {
            Assert.IsNotNull(application);
            Console.WriteLine(application.VersionBuild);
        }
    }
}
