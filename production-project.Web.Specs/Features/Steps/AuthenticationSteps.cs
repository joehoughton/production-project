namespace production_project.Web.Specs.Features.Steps
{
    using NUnit.Framework;
    using production_project.Web.Specs.Helpers;
    using production_project.Web.Specs.Support;
    using TechTalk.SpecFlow;

    [Binding]
    public class AuthenticationSteps : Setup
    {
        [Given(@"I am on the login screen")]
        public void GivenIAmOnTheLoginScreen()
        {
            Driver.Navigate().GoToUrl(Navigation.Url(Page.Login));
        }

        [When(@"I submit the correct credentials")]
        public void WhenISubmitTheCorrectCredentials()
        {
            var emailInput = Driver.FindElementById("username");
            var passwordInput = Driver.FindElementById("password");

            emailInput.Clear();
            passwordInput.Clear();

            emailInput.SendKeys("joehoughton");
            passwordInput.SendKeys("Passw0rd!");

            Driver.FindElementByClassName("form-signin").Submit();
        }

        [Then(@"I am authenticated")]
        public void ThenIAmAuthenticated()
        {
            Driver.Navigate().Refresh();

            var authCookie = Driver.Manage().Cookies.GetCookieNamed(".AspNet.ApplicationCookie");

            Assert.IsNotNull(authCookie);
            Assert.IsTrue(authCookie.Value.Length >= 10);
        }

        [Then(@"I am redirected to my account page")]
        public void ThenIAmRedirectedToMyAccountPage()
        {
            Wait(3);

            var accountPageUrl = Navigation.Url(Page.AccountOrganisation);
            var currentUrl = Driver.Url;

            Assert.AreEqual(accountPageUrl, currentUrl);
        }

        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            GivenIAmOnTheLoginScreen();
            WhenISubmitTheCorrectCredentials();
            ThenIAmRedirectedToMyAccountPage();
        }

        [When(@"I log out")]
        public void WhenILogOut()
        {
            Driver.FindElementById("logout").Click();
            Wait(2);
        }

        [Then(@"I am redirected to the login page")]
        public void ThenIAmRedirectedToTheLoginPage()
        {
            var loginPageUrl = Navigation.Url(Page.Login);
            var currentUrl = Driver.Url;

            Assert.AreEqual(loginPageUrl, currentUrl);
        }

        [Then(@"I can not access an authenticated section")]
        public void ThenICanNotAccessAnAuthenticatedSection()
        {
            Driver.Navigate().GoToUrl(Navigation.Url(Page.AccountOrganisation));

            var loginPageUrl = Navigation.Url(Page.Login);
            var currentUrl = Driver.Url;

            Assert.AreEqual(loginPageUrl, currentUrl, "User should be redirected to the login screen");
        }

        [When(@"I submit incorrect credentials")]
        public void WhenISubmitIncorrectCredentials()
        {
            var emailInput = Driver.FindElementById("username");
            var passwordInput = Driver.FindElementById("password");

            emailInput.Clear();
            passwordInput.Clear();

            emailInput.SendKeys("joehoughton");
            passwordInput.SendKeys("WrongPassw0rd!");

            Driver.FindElementByClassName("form-signin").Submit();
        }

        [Then(@"I have been shown an error alert")]
        public void ThenIHaveBeenShownAnErrorAlert()
        {
            Wait(2);

            var errorAlert = Driver.FindElementByClassName("error-alert");
            var alertText = errorAlert.Text.Contains("Unauthorised");

            Assert.IsNotNull(errorAlert);
            Assert.IsTrue(alertText);
        }

        [Then(@"I have stayed on the login screen")]
        public void ThenIHaveStayedOnTheLoginScreen()
        {
            var loginPageUrl = Navigation.Url(Page.Login);
            var currentUrl = Driver.Url;

            Assert.AreEqual(loginPageUrl, currentUrl);
        }

        [Then(@"I have been shown a warning alert")]
        public void ThenIHaveBeenShownAWarningAlert()
        {
            var warningAlert = Driver.FindElementByClassName("warning-alert");

            Assert.IsNotNull(warningAlert);
        }

        [When(@"I try to submit with only password")]
        public void WhenITryToSubmitWithOnlyPassword()
        {
            Driver.FindElementById("username").Clear();
            Driver.FindElementById("password").SendKeys("Passw0rd!");
            Driver.FindElementByClassName("form-signin").Submit();
        }

        [When(@"I try to submit with only username")]
        public void WhenITryToSubmitWithOnlyUsername()
        {
            Driver.FindElementById("password").Clear();
            Driver.FindElementById("username").SendKeys("joehoughton");
            Driver.FindElementByClassName("form-signin").Submit();
        }

    }
}
