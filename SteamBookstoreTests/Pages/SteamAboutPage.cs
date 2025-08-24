using OpenQA.Selenium;

namespace SteamBookstoreTests.Pages
{
    public class SteamAboutPage : BasePage
    {
        public SteamAboutPage(IWebDriver driver) : base(driver) { }

        private By InstallSteamBtn => By.CssSelector("div.about_install.win ");
        private By PlayingNowStat => By.CssSelector("div.online_stat:has(.gamers_in_game)\r\n");
        private By OnlineStat => By.CssSelector("div.online_stat:has(.gamers_online)");

        public bool IsInstallSteamButtonClickable()
        {
            return IsDisplayed(InstallSteamBtn);
        }

        public bool IsPlayingNowLessThanOnline()
        {
            var pn = ParseCounter(GetText(PlayingNowStat));
            var onl = ParseCounter(GetText(OnlineStat));

            return pn < onl;
        }

        private static long ParseCounter(string text)
        {
            var cleaned = System.Text.RegularExpressions.Regex.Replace(text, "[^0-9]", "");
            return long.TryParse(cleaned, out var val) ? val : 0;
        }
    }
}