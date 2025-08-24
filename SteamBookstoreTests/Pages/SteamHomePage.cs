using OpenQA.Selenium;
using System.Linq;

namespace SteamBookstoreTests.Pages
{
    public class SteamHomePage : BasePage
    {
        public SteamHomePage(IWebDriver driver) : base(driver) { }

        private By SearchField => By.Id("store_nav_search_term");
        private By SearchResults => By.CssSelector(".search_suggest .match_name");
        private By FirstResult => By.CssSelector(".search_suggest .match_name");
        private By DownloadBtn => By.CssSelector("a.btn_green_steamui.btn_medium");

        public void GoTo()
        {
            Driver.Navigate().GoToUrl("https://store.steampowered.com/");
        }

        public void TypeSearch(string text) => Type(SearchField, text);

        public (string first, string second) GetTopTwoSuggestions()
        {
            var names = FindAll(SearchResults)
                              .Select(e => e.Text.Trim())
                              .Where(t => !string.IsNullOrWhiteSpace(t))
                              .Take(2).ToList();
            return (names.ElementAtOrDefault(0) ?? string.Empty,
                    names.ElementAtOrDefault(1) ?? string.Empty);
        }

        public void ClickFirstSuggestionWithJs() => ClickWithJs(FirstResult);

        public void ClickDownload() => Click(DownloadBtn);
    }
}