using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Linq;

namespace SteamBookstoreTests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        protected IWebElement Find(By locator) => Driver.FindElement(locator);

        protected IReadOnlyCollection<IWebElement> FindAll(By locator) => Driver.FindElements(locator);

        protected void Click(By locator)
        {
            var element = Find(locator);
            ScrollToElement(element);
            element.Click();
        }

        protected void ClickWithJs(By locator)
        {
            var element = Find(locator);
            ScrollToElement(element);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", element);
        }

        protected void Type(By locator, string text, bool clear = true)
        {
            var element = Find(locator);
            ScrollToElement(element);
            if (clear) element.Clear();
            element.SendKeys(text);
        }

        protected string GetText(By locator)
        {
            var element = Find(locator);
            ScrollToElement(element);
            return element.Text.Trim();
        }

        protected bool IsDisplayed(By locator)
        {
            var elements = Driver.FindElements(locator);
            if (elements.Count == 0) return false;

            var el = elements.First();
            ScrollToElement(el);
            try
            {
                return el.Displayed && el.Enabled;
            }
            catch (StaleElementReferenceException)
            {
                elements = Driver.FindElements(locator);
                return elements.Count > 0 && elements.First().Displayed;
            }
        }

        protected void ScrollToElement(IWebElement element)
        {
            try
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);
            }
            catch {}
        }

        protected void Hover(By locator)
        {
            var element = Find(locator);
            new Actions(Driver).MoveToElement(element).Perform();
        }
    }
}
