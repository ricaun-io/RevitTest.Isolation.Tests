using System;
using System.IO;
using Autodesk.RevitAddIns;

namespace RevitTest.Isolation.Tests
{
    public static class RevitAddInUtils
    {
        public static RevitAddInManifest Create(string assemblyPath, string fullClassName, string contextName = null)
        {
            string vendorId = "Test";
            Guid addInId = Guid.NewGuid();
            string name = Path.GetFileNameWithoutExtension(assemblyPath);

            var revitAddInManifest = new RevitAddInManifest();

            var application = new RevitAddInApplication(name, assemblyPath, addInId, fullClassName, vendorId);
            
            revitAddInManifest.AddInApplications.Add(application);

            if (contextName is not null)
            {
                revitAddInManifest.ManifestSettings.UseRevitContext = false;
                revitAddInManifest.ManifestSettings.ContextName = contextName;
            }

            return revitAddInManifest;
        }
    }

}
