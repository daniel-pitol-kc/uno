# How to Create Control Libraries

Uno Platform, like WinUI and UWP, supports Control Libraries. Control Libraries are a way to reuse UI components across multiple projects, either inside the solution or by using NuGet to distribute to other projects.


Creating such a library will make UI Controls compatible with all Uno Platform targets as well as WinUI or UWP.

> [!NOTE]
> Control libraries are different from "normal" libraries as they reference WinAppSDK, Uno.UI or Uno.WinUI. Those libraries are special because they have explicit dependencies on platform-specific features. "Normal" libraries (e.g. Newtonsoft.Json) do not need any special treatment to work with Uno.

You can find the [full sample code](https://github.com/unoplatform/Uno.Samples/blob/master/UI/ControlLibrary) for this how-to in our samples repository.

## Create a Control Library

1. In your solution, create a new **Uno Platform Library**, name it `XamlControlLibrary`
1. In each of your platform projects, add a reference to your new library.
   > [!NOTE] The shared project cannot contain project or packages references, see [Uno Platform App structure](../uno-app-solution-structure.md) for more details.

## Create the Control
1. Right-click on the project library, then **Add**, **New Item**
1. In the **Uno Platform** section of **C# Items**, select **Custom Control**, name it `MyTemplatedControl`
   > [!TIP]
   > Choose the template flavor based on your library's flavor: UWP or WinUI. If your project uses the `Uno.UI` NuGet package, it's **UWP** otherwise **WinUI 3** if it uses `Uno.WinUI`.

1. Right click on the project library again, then **Add**, **New Folder**, call it `Themes` (case sentitive)
1. Right click on the `Generic` folder, then **Add**, **New Item**
1. In the **Uno Platform** section of **C# Items**, select **Resource Dictionary**, name it `Generic.xaml` (case sensitive)
1. In the new created file, paste the following:
   ```xml
   <ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:XamlControlLibrary">

        <Style TargetType="local:MyTemplatedControl">
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="local:MyTemplatedControl">
                        <Grid>
                            <TextBlock Text="My templated control !" />
                        </Grid>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>

    </ResourceDictionary>
   ```

## Use the control

1. In your application's `MainPage.xaml`, add the the following namespace `xmlns:myControlLib="using:XamlControlLibrary"` in the `Page` element
2. Add the following code in somewhere in the page:
   ```xml
   <myControlLib:MyTemplatedControl />
   ```

## Library assets

WinUI 3 and Uno Platform (4.6 and later) Libraries support the inclusion of content assets to be used with [`StorageFile.GetFileFromApplicationUriAsync`](../features/file-management.md#support-for-storagefilegetfilefromapplicationuriasync), as well as with the `ms-appx://[libraryname]/[assetname_file_name]` format.

Library assets can be of any type.

Library assets are supported in two configurations:
- `ProjectReference`, where the library project is included in the solution with the application that uses it
- `PackageReference`, where the library project is being packaged as a NuGet package, and used in a separate solution, from a NuGet feed.

In both cases, for the build system to include the assets files, the following property must be set in the library's `.csproj`:

```xml
<PropertyGroup>
    <GenerateLibraryLayout>true</GenerateLibraryLayout>
</PropertyGroup>
```

> [!IMPORTANT]
> WinAppSDK [does not support assets](https://github.com/microsoft/microsoft-ui-xaml/issues/6429) if the application is using the MSIX package mode. To use the unpackaged mode, [see this article](../features/winapp-sdk-specifics.md#unpackaged-application-support).
