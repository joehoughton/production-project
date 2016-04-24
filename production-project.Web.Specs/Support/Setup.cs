namespace production_project.Web.Specs.Support
{
    using System;
    using System.Threading;
    using OpenQA.Selenium.Firefox;

    public abstract class Setup
    {
        protected static FirefoxDriver Driver;
        protected static string AuthenticatedCookie;

        /// <summary>
        /// Quick alternative to Thread.Sleep, time in seconds.
        /// </summary>
        public void Wait(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }
    }
}
