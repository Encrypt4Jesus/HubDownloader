// Ignore Spelling: Downloader

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace HubDownloader
{
    public class WebDriverService
    {
        public IWebDriver Driver;

        protected OpenQA.Selenium.DriverService? _driverService = null;
        private OpenQA.Selenium.Edge.EdgeDriverService _edgeDriverService = null;
        private OpenQA.Selenium.Chrome.ChromeDriverService _chromeDriverService = null;
        private OpenQA.Selenium.Firefox.FirefoxDriverService _firefoxDriverService = null;

        public bool IsShutdown
        {
            get
            {
                if (_driverService == null || Driver == null)
                {
                    return true;
                }
                else if (!_driverService.IsRunning)
                {
                    return true;
                }
                return false;
            }
        }

        public WebDriverService(BrowserSelection browserSelection)
        {
            switch (browserSelection)
            {
                case BrowserSelection.Edge:
                    _edgeDriverService = EdgeDriverService.CreateDefaultService();// (driverPath, driverFilename);
                    _driverService = _edgeDriverService;
                    break;

                case BrowserSelection.Chrome:
                    _chromeDriverService = ChromeDriverService.CreateDefaultService();
                    _driverService = _chromeDriverService;
                    break;

                case BrowserSelection.Firefox:
                    _firefoxDriverService = FirefoxDriverService.CreateDefaultService();
                    _driverService = _firefoxDriverService;
                    break;
            }

            _driverService.HideCommandPromptWindow = true;
            _driverService.Start();
            TimeSpan implicitWaitTimeout = TimeSpan.FromSeconds(InternalSettings.Selenium_ImplicitWaitTime);

            switch (browserSelection)
            {
                case BrowserSelection.Edge:
                    {
                        var options = new EdgeOptions()
                        {
                            EnableDownloads = true,
                            LeaveBrowserRunning = true,
                            ImplicitWaitTimeout = implicitWaitTimeout
                        };
                        options.AddArguments("-inprivate");
                        Driver = new EdgeDriver(_edgeDriverService, options);
                        break;
                    }

                case BrowserSelection.Chrome:
                    {
                        var options = new ChromeOptions()
                        {
                            EnableDownloads = true,
                            LeaveBrowserRunning = true,
                            ImplicitWaitTimeout = implicitWaitTimeout
                        };
                        options.AddArguments("-incognito");
                        Driver = new ChromeDriver(_chromeDriverService, options);
                        break;
                    }

                case BrowserSelection.Firefox:
                    {
                        var options = new FirefoxOptions()
                        {
                            EnableDownloads = true,
                            ImplicitWaitTimeout = implicitWaitTimeout
                        };
                        options.AddArguments("-private");
                        Driver = new FirefoxDriver(_firefoxDriverService, options);
                        break;
                    }
            }
        }

        public void Shutdown()
        {
            if (Driver != null)
            {
                Driver.Close();
                Driver.Quit();
                Driver.Dispose();
                Driver = null;
            }
            if (_driverService != null)
            {
                _driverService.Dispose();
                _driverService = null;
                _edgeDriverService = null;
                _chromeDriverService = null;
                _firefoxDriverService = null;
            }
        }
    }
}
