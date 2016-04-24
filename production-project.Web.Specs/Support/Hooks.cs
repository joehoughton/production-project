namespace production_project.Web.Specs.Support
{   
    using System;
    using OpenQA.Selenium.Firefox;
    using production_project.Web.Specs.Helpers;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class Hooks : Setup
    {
        [BeforeTestRun]
        public static void Start()
        {
            Driver = new FirefoxDriver();
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            Driver.Navigate().GoToUrl(Navigation.Url(Page.Ping));
        }

        [AfterTestRun]
        public static void Stop()
        {
            Driver.Quit();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Driver.Navigate().Refresh();
        }

        [AfterScenario]
        public void AfterScenario()
        {

        }
    }
}
