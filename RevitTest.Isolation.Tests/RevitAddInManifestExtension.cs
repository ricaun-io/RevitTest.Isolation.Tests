using System.IO;
using Autodesk.RevitAddIns;

namespace RevitTest.Isolation.Tests;

public static class RevitAddInManifestExtension
{
    public static void SaveAs2(this RevitAddInManifest revitAddInManifest, string fileName)
    {
        revitAddInManifest.SaveAs(fileName);
        if (revitAddInManifest.ManifestSettings.UseRevitContext == false)
        {
            var revitAddInManifestText = File.ReadAllText(fileName);
            if (revitAddInManifestText.Contains("<ManifestSettings>"))
                return;

            revitAddInManifestText = revitAddInManifestText.Replace("</RevitAddIns>", $"""
                    <ManifestSettings>
                        <UseRevitContext>{revitAddInManifest.ManifestSettings.UseRevitContext}</UseRevitContext>
                        <ContextName>{revitAddInManifest.ManifestSettings.ContextName}</ContextName>
                    </ManifestSettings>
                </RevitAddIns>
                """);

            File.WriteAllText(fileName, revitAddInManifestText);
        }
    }
}
