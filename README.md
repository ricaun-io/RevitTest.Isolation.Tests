# RevitTest.Isolation.Tests

[![RevitTest](https://img.shields.io/nuget/v/ricaun.RevitTest.TestAdapter?label=RevitTest&color=blue)](https://www.nuget.org/packages/ricaun.RevitTest.TestAdapter)
[![Revit 2026](https://img.shields.io/badge/Revit-2026+-blue.svg)](../..)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

Test project to validate the `Option for Add-in Dependency Isolation` feature in Revit 2026.

### RevitAddInUtility 

The `RevitAddInUtility.dll` now contains the `ManifestSettings` and inside the class `RevitAddInManifest`. With the `UseRevitContext` and `ContextName` to generate the `RevitAddin.addin` file with the isolation settings.

Example:
```
<?xml version="1.0" encoding="utf-8"?>
<RevitAddIns>
  <AddIn Type="Application">
    <Name>RevitAddin</Name>
    <Assembly>RevitAddin.dll</Assembly>
    <AddInId>00000000-1111-2222-3333-444444444444</AddInId>
    <FullClassName>RevitAddin.App</FullClassName>
    <VendorId>RevitAddin</VendorId>
  </AddIn>
  <ManifestSettings>
    <UseRevitContext>False</UseRevitContext>
    <ContextName>RevitAddin</ContextName>
  </ManifestSettings>
</RevitAddIns>
```

**In the `VersionBuild 26.0.4.409` the `Save` and `SaveAs` does not generate the `addin` file with the `ManifestSettings` settings.**

### Add-in Dependency Isolation

The `Add-in Dependency Isolation` feature allows you to run add-ins in a separate context, which can help to isolate add-in dependencies.

**In the `VersionBuild 26.0.4.409` the isolation does not work like should be.**
**If you have two plugin using isolation with a dependency with the same assembly and version, Revit forces the one isolated context to find assemblies form another isolated context.**

### RevitTest.Isolation.Tests

The test is design to validate all the issues with the `Add-in Dependency Isolation` feature in Revit 2026.

`RevitAddInTests.cs` loads two addin `RevitAddinA` and `RevitAddinB` each one in a different context. 
The addin `RevitAddinA` loads in the context `A` and `RevitAddinB` loads in the context `B`.

The `RevitAddinB` loads the a library used by the plugin `RevitAddinA` in the context `A` and not load the dependency inside his folder in the context `B`.

## License

This project is [licensed](LICENSE) under the [MIT License](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!