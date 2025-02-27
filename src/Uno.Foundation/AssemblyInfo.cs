﻿using global::System.Reflection;
using global::System.Runtime.CompilerServices;
using global::System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("Uno.UI")]
[assembly: InternalsVisibleTo("Uno")]
[assembly: InternalsVisibleTo("Uno.UI.Wasm")]
[assembly: InternalsVisibleTo("Uno.Wasm")]
[assembly: InternalsVisibleTo("Uno.UI.Tests")]
[assembly: InternalsVisibleTo("Uno.Foundation")]
[assembly: InternalsVisibleTo("Uno.Foundation.Wasm")]
[assembly: InternalsVisibleTo("Uno.UI.Wasm.Tests")]
[assembly: InternalsVisibleTo("Uno.Foundation.Runtime.WebAssembly")]
[assembly: InternalsVisibleTo("Uno.UI.Toolkit")]
[assembly: InternalsVisibleTo("Uno.UI.RemoteControl")]
[assembly: InternalsVisibleTo("Uno.UI.FluentTheme")]
[assembly: InternalsVisibleTo("Uno.UI.FluentTheme.v1")]
[assembly: InternalsVisibleTo("Uno.UI.FluentTheme.v2")]
[assembly: InternalsVisibleTo("Uno.UI.Lottie")]
[assembly: InternalsVisibleTo("Uno.UI.Svg")]
[assembly: InternalsVisibleTo("Uno.UI.Svg.Skia")]
[assembly: InternalsVisibleTo("Uno.UI.Runtime.Skia.Gtk")]
[assembly: InternalsVisibleTo("Uno.UI.Runtime.Skia.Wpf")]
[assembly: InternalsVisibleTo("Uno.UI.Maps")]
[assembly: InternalsVisibleTo("Uno.UI.Runtime.Skia.Tizen")]
[assembly: InternalsVisibleTo("Uno.UI.Runtime.Skia.Linux.FrameBuffer")]
[assembly: InternalsVisibleTo("SamplesApp.Skia.Tizen")]
[assembly: InternalsVisibleTo("Uno.UI.RuntimeTests")]
[assembly: InternalsVisibleTo("Uno.UI.DualScreen")]
[assembly: InternalsVisibleTo("Uno.UI.DualScreen.net6")]
[assembly: InternalsVisibleTo("SamplesApp.Wasm")]
[assembly: InternalsVisibleTo("SamplesApp.Skia")]
[assembly: InternalsVisibleTo("SamplesApp")]
[assembly: InternalsVisibleTo("SamplesApp.iOS")]
[assembly: InternalsVisibleTo("SamplesApp.Droid")]
[assembly: InternalsVisibleTo("SamplesApp.MacOS")]
[assembly: InternalsVisibleTo("XamlGenerationTests")]
[assembly: InternalsVisibleTo("XamlGenerationTests.Core")]
[assembly: InternalsVisibleTo("Uno.UI.Adapter.Microsoft.Extensions.Logging")]
[assembly: InternalsVisibleTo("Uno.UI.Runtime.WebAssembly")]
[assembly: InternalsVisibleTo("Uno.UI.Tests.Performance")]
[assembly: InternalsVisibleTo("Uno.UI.Composition")]
[assembly: InternalsVisibleTo("Uno.UI.Dispatching")]

#if NET6_0_OR_GREATER
[assembly: System.Reflection.AssemblyMetadata("IsTrimmable", "True")]
#elif __IOS__
#pragma warning disable CS0618 // Type or member is obsolete
[assembly: Foundation.LinkerSafe]
#pragma warning restore CS0618 // Type or member is obsolete
[assembly: AssemblyMetadata("IsTrimmable", "True")]
#elif __ANDROID__
[assembly: Android.LinkerSafe]
#endif
