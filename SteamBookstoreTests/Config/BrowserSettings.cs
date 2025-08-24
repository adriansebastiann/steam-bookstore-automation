using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamBookstoreTests.Config
{
    internal class BrowserSettings
    {
        public string BrowserName { get; set; } = "chrome";
        public bool Headless { get; set; } = false;
        public int ImplicitWaitSeconds { get; set; } = 5;
        public int PageLoadTimeoutSeconds { get; set; } = 60;
        public bool Incognito { get; set; } = true;
    }
}
