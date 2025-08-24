using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using SteamBookstoreTests.Hooks;
using SteamBookstoreTests.Pages;
using System.Linq;

namespace SteamBookstoreTests.Steps
{
    [Binding]
    public class SteamUiSteps
    {
        private readonly ScenarioContext _ctx;
        private IWebDriver Driver => Hooks.Hooks.Driver!;
        private string _firstSuggestion = string.Empty;

        public SteamUiSteps(ScenarioContext ctx) => _ctx = ctx;

        [Given(@"I open Steam in incognito")]
        public void GivenIOpenSteamInIncognito()
        {
            var home = new SteamHomePage(Driver);
            home.GoTo();
        }

        [When(@"I search for ""(.*)""")]
        public void WhenISearchFor(string term)
        {
            new SteamHomePage(Driver).TypeSearch(term);
        }

        [Then(@"the first two suggestions are ""(.*)"" and ""(.*)""")]
        public void ThenTheFirstTwoSuggestionsAre(string expectedFirst, string expectedSecond)
        {
            var (first, second) = new SteamHomePage(Driver).GetTopTwoSuggestions();
            _firstSuggestion = first;
            Assert.That(first, Is.EqualTo(expectedFirst), "First suggestion mismatch");
            Assert.That(second, Is.EqualTo(expectedSecond), "Second suggestion mismatch");
        }

        [When(@"I click the first suggestion using JavaScript")]
        public void WhenIClickTheFirstSuggestionUsingJavaScript()
        {
            new SteamHomePage(Driver).ClickFirstSuggestionWithJs();
        }

        [Then(@"the game page is displayed and the title matches the first suggestion")]
        public void ThenTheGamePageIsDisplayedAndTheTitleMatchesTheFirstSuggestion()
        {
            var game = new SteamGamePage(Driver);
            Assert.That(game.GameTitle, Is.EqualTo(_firstSuggestion), "Game title mismatch");
        }

        [When(@"I click the Download button in the header")]
        public void WhenIClickTheDownloadButtonInTheHeader()
        {
            new SteamHomePage(Driver).ClickDownload();
        }

        [When(@"I click ""(.*)""")]
        public void WhenIClick(string text)
        {
            var link = Driver.FindElements(By.LinkText(text)).FirstOrDefault()
                       ?? Driver.FindElements(By.PartialLinkText("need Steam")).First();
            link.Click();
        }

        [Then(@"the About Steam page is displayed")]
        public void ThenTheAboutSteamPageIsDisplayed()
        {
            Assert.That(Driver.Url.ToLowerInvariant().Contains("about"), "Not on About Steam page");
        }

        [Then(@"the Install Steam button is clickable")]
        public void ThenTheInstallSteamButtonIsClickable()
        {
            var about = new SteamAboutPage(Driver);
            Assert.That(about.IsInstallSteamButtonClickable(), "Install Steam button not clickable");
        }

        [Then(@"the number of Playing Now gamers is less than the number of Online gamers")]
        public void ThenTheNumberComparison()
        {
            var about = new SteamAboutPage(Driver);
            Assert.That(about.IsPlayingNowLessThanOnline(), "Playing Now should be less than Online");
        }
    }
}
