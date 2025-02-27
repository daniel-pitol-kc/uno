﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Private.Infrastructure;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Uno.UI.RuntimeTests.Tests.Windows_UI_Xaml_Controls
{
	[TestClass]
	public class Given_Image
	{
#if !__IOS__ // Currently fails on iOS
		[TestMethod]
#endif
		[RunsOnUIThread]
		public async Task When_Fixed_Height_And_Stretch_Uniform()
		{
			var imageLoaded = new TaskCompletionSource<bool>();

			var image = new Image { Height = 30, Stretch = Stretch.Uniform, Source = new BitmapImage(new Uri("ms-appx:///Assets/storelogo.png")) };
			image.Loaded += (s, e) => imageLoaded.TrySetResult(true);
			image.ImageFailed += (s, e) => imageLoaded.TrySetException(new Exception(e.ErrorMessage));

			var innerGrid = new Grid { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center };
			var outerGrid = new Grid { Height = 750, Width = 430 };
			innerGrid.Children.Add(image);
			outerGrid.Children.Add(innerGrid);

			TestServices.WindowHelper.WindowContent = outerGrid;
			await TestServices.WindowHelper.WaitForIdle();

			await imageLoaded.Task;

			image.InvalidateMeasure();

			await TestServices.WindowHelper.WaitForIdle();
			await TestServices.WindowHelper.WaitForIdle();

			outerGrid.Measure(new Size(1000, 1000));
			var desiredContainer = innerGrid.DesiredSize;

			// Workaround for image.Loaded being raised too early on WebAssembly
			var sw = Stopwatch.StartNew();
			do
			{
				await TestServices.WindowHelper.WaitForIdle();

				if(Math.Round(desiredContainer.Width) != 0 && Math.Round(desiredContainer.Height) != 0)
				{
					break;
				}

				desiredContainer = innerGrid.DesiredSize;
			}
			while (sw.Elapsed < TimeSpan.FromSeconds(5));

			await TestServices.WindowHelper.WaitForIdle();

			using var _ = new AssertionScope();

			Math.Round(desiredContainer.Width).Should().Be(30, "desiredContainer.Width");
			Math.Round(desiredContainer.Height).Should().Be(30, "desiredContainer.Width");

			TestServices.WindowHelper.WindowContent = null;
		}

#if __WASM__
		[TestMethod]
		[RunsOnUIThread]
		public async Task When_Resource_Has_Scale_Qualifier()
		{
			var scales = new List<ResolutionScale>()
			{
				(ResolutionScale)80,
				ResolutionScale.Scale100Percent,
				ResolutionScale.Scale150Percent,
				ResolutionScale.Scale200Percent,
				ResolutionScale.Scale300Percent,
				ResolutionScale.Scale400Percent,
				ResolutionScale.Scale500Percent,
			};

			try
			{
				foreach (var scale in scales)
				{
					var imageOpened = new TaskCompletionSource<bool>();

					var source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/FluentIcon_Medium.png"));
					source.ScaleOverride = scale;

					var image = new Image { Height = 24, Width = 24, Stretch = Stretch.Uniform, Source = source };
					image.ImageOpened += (s, e) => imageOpened.TrySetResult(true);
					image.ImageFailed += (s, e) => imageOpened.TrySetResult(false);

					TestServices.WindowHelper.WindowContent = image;

					await TestServices.WindowHelper.WaitForIdle();

					var result = await imageOpened.Task;

					Assert.IsTrue(result);
				}
			}
			finally
			{
				TestServices.WindowHelper.WindowContent = null;
			}
		}
#endif

		[TestMethod]
		[RunsOnUIThread]
		public void TargetNullValue_Is_Correctly_Applied()
		{
			var SUT = new ImageSource_TargetNullValue();

			var nameIsAppliedSource = SUT.NameIsApplied.Source as BitmapImage;
			Assert.AreEqual("ms-appx:///mypanel", nameIsAppliedSource.UriSource.ToString());

			var targetNullValueSource = SUT.TargetNullValueIsApplied.Source as BitmapImage;
			Assert.AreEqual("ms-appx:///Assets/StoreLogo.png", targetNullValueSource.UriSource.ToString());
		}

		[TestMethod]
		[RunsOnUIThread]
		public async Task When_Transitive_Asset_Loaded()
		{
			string url = "ms-appx://Uno.UI.RuntimeTests/Assets/Transitive-ingredient01.png";
			var img = new Image();
			var SUT = new BitmapImage(new Uri(url));
			img.Source = SUT;

			TestServices.WindowHelper.WindowContent = img;
			await TestServices.WindowHelper.WaitForIdle();
			await TestServices.WindowHelper.WaitFor(() => img.ActualHeight > 0, 3000);

			Assert.IsTrue(img.ActualHeight > 0);
		}

		[TestMethod]
		[RunsOnUIThread]
		public async Task When_Image_Is_Loaded_From_URL()
		{
			string decoded_url = "https://nv-assets.azurewebsites.net/tests/images/image with spaces.jpg";
			var img = new Image();
			var SUT = new BitmapImage(new Uri(decoded_url));
			img.Source = SUT;

			TestServices.WindowHelper.WindowContent = img;
			await TestServices.WindowHelper.WaitForIdle();
			await TestServices.WindowHelper.WaitFor(() => img.ActualHeight > 0, 3000);

			Assert.IsTrue(img.ActualHeight > 0);			
		}
	}
}
