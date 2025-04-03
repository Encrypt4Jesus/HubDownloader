using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;

namespace HubDownloader
{
    public class WebDriverService
    {
        public IWebDriver Driver;
        protected EdgeDriverService? DriverService;


        public bool IsShutdown
        {
            get
            {
                if (DriverService == null || Driver == null)
                {
                    return true;
                }
                else if (!DriverService.IsRunning)
                {
                    return true;
                }
                return false;
            }
        }


        public WebDriverService()
        {
            Initialize();
        }

        public void Initialize()
        {
            DriverService = EdgeDriverService.CreateDefaultService();// (driverPath, driverFilename);
            DriverService.HideCommandPromptWindow = true;
            DriverService.Start();

            var options = new EdgeOptions()
            {
            };
            options.AddArguments("-inprivate");

            Driver = new EdgeDriver(DriverService, options);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(InternalSettings.Selenium_ImplicitWaitTime);
        }

        public void Shutdown()
        {
            if (Driver != null)
            {
                Driver.Close();
                Driver.Quit();
                Driver.Dispose();
            }
            if (DriverService != null)
            {
                DriverService.Dispose();
            }
        }
    }
}
