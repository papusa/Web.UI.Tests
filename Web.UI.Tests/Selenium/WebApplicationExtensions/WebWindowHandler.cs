using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Web.UI.Tests.Environment;

namespace Web.UI.Tests.Selenium.WebApplicationExtensions {
    static class WebWindowHandler {
        public static void SwitchToWindow(this WebApplication webApplication, string window) {
            webApplication.WebDriver.SwitchTo().Window(window);
        }

        public static string GetNewWindow(this WebApplication webApplication) {
            var timeout = Config.TimeOut;
            var currentTimeoutInSeconds = 0;
            List<string> unregisteredWindows;

            do {
                Thread.Sleep(1000);
                var registeredWindows = webApplication.SessionWindows.Values;
                var allWindows = webApplication.WebDriver.WindowHandles;
                unregisteredWindows = allWindows.Except(registeredWindows).ToList();

                if (unregisteredWindows.Count == 1) {
                    return unregisteredWindows.Last();
                }

                if (unregisteredWindows.Count > 1) {
                    throw new Exception("More than 1 browser window is unregistered!");
                }

                currentTimeoutInSeconds++;
            }
            while (unregisteredWindows.Count < 1 && currentTimeoutInSeconds < timeout);

            throw new Exception("There where no new browser window to unregister");
        }
    }
}
