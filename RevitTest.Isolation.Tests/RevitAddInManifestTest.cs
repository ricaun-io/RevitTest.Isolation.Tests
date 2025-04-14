using System;
using System.IO;
using Autodesk.RevitAddIns;
using NUnit.Framework;

namespace RevitTest.Isolation.Tests;

public class RevitAddInManifestTest
{
    [Test]
    public void Create_Should_Have_ManifestSettings()
    {
        // Arrange
        var revitAddInManifest = CreateRevitAddInManifest();
        Console.WriteLine(revitAddInManifest);

        // Assert
        Assert.IsNotNull(revitAddInManifest);
        Assert.IsTrue(revitAddInManifest.Contains("<ManifestSettings>"));
    }

    [Test]
    public void Create_Should_Have_Application()
    {
        // Arrange
        var revitAddInManifest = CreateRevitAddInManifest();
        Console.WriteLine(revitAddInManifest);

        // Assert
        Assert.IsNotNull(revitAddInManifest);
        Assert.IsTrue(revitAddInManifest.Contains("<AddIn Type=\"Application\">"));
    }

    public string CreateRevitAddInManifest()
    {
        var revitAddInManifest = new RevitAddInManifest();

        var application = new RevitAddInApplication("RevitAddin", "RevitAddin.dll", Guid.NewGuid(), "RevitAddin.App", "RevitAddin");
        revitAddInManifest.AddInApplications.Add(application);

        revitAddInManifest.ManifestSettings.UseRevitContext = false;
        revitAddInManifest.ManifestSettings.ContextName = "RevitAddin";

        var fileName = "RevitAddin.addin";
        revitAddInManifest.SaveAs(fileName);

        var readAllText = File.ReadAllText(fileName);
        File.Delete(fileName);

        return readAllText;
    }
}
