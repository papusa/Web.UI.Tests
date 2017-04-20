using System;
using System.IO;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Tracing;

namespace Web.UI.Tests.Selenium.WebApplicationExtensions {
    static class ScreenshotExtension {
        public static void TakeScreenshot(this WebApplication webApplication) {
            var driver = webApplication.WebDriver;
            try {
                var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "TestsResults");

                if (!Directory.Exists(artifactDirectory)) {
                    Directory.CreateDirectory(artifactDirectory);
                }

                var takesScreenshot = driver as ITakesScreenshot;

                if (takesScreenshot != null) {
                    var screenshot = takesScreenshot.GetScreenshot();

                    var screenshotFilePath = Path.Combine(artifactDirectory, GetFileNameBase() + "_screenshot.png");

                    screenshot?.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);

                    Console.WriteLine("Screenshot: {0}", new Uri(screenshotFilePath));
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error while taking screenshot: {0}", ex);
            }
        }

        private static string GetFileNameBase() {
            var fileNameBase =
                $"{ScenarioContext.Current.ScenarioInfo.Title.ToIdentifier()}_{DateTime.Now.ToString("yyyy.MM.dd_HH.mm.ss")}";
            return fileNameBase;
        }
    }
}
