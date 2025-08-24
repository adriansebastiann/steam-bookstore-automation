using OpenQA.Selenium;

namespace SteamBookstoreTests.Pages
{
    public class SteamGamePage : BasePage
    {
        public SteamGamePage(IWebDriver driver) : base(driver) { }

        private By GameTitleLocator => By.CssSelector(".apphub_AppName");
        private By InstallButton => By.CssSelector("a.header_installsteam_btn_content");

        public string GameTitle => GetText(GameTitleLocator);

        public void ClickInstallButton() => Click(InstallButton);
    }
}
