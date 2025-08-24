using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using SteamBookstoreTests.Drivers;
using Allure.Commons;
using System;
using System.IO;

namespace SteamBookstoreTests.Hooks
{
    [Binding]
    public class Hooks
    {
        public static IWebDriver? Driver { get; private set; }

        [BeforeScenario(Order = 0)]
        public void BeforeScenario()
        {
            Driver = WebDriverFactory.Create();
        }

        [AfterScenario(Order = 100)]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            if (Driver != null)
            {
                try
                {
                    if (scenarioContext.TestError != null)
                    {
                        var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                        var path = Path.Combine(TestContext.CurrentContext.WorkDirectory, $"screenshot_{Guid.NewGuid()}.png");
                        screenshot.SaveAsFile(path);

                        AllureLifecycle.Instance.AddAttachment("Failure screenshot", "image/png", path);
                    }
                }
                catch { }
                finally
                {
                    Driver.Quit();
                    Driver.Dispose();
                    Driver = null;
                }
            }
        }
    }
}
