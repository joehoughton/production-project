namespace production_project.Web.Specs.Features.Steps
{
    using System;
    using System.Net;
    using System.Text;
    using NUnit.Framework;
    using production_project.Web.Specs.Helpers;
    using production_project.Web.Specs.Support;
    using TechTalk.SpecFlow;
    using System.Web.Script.Serialization;

    [Binding]
    public class ApiAuthenticationSteps : Setup
    {
        private object credentials;
        private string postData;
        private HttpWebResponse response;

        [When(@"I  make a request to the api with valid credentials")]
        public void WhenIMakeARequestToTheApiWithValidCredentials()
        {
            credentials = new { Username = "joehoughton", Password = "Passw0rd!" };
            postData = new JavaScriptSerializer().Serialize(credentials);
            var request = (HttpWebRequest)WebRequest.Create(Navigation.Url(Page.ApiTokens));
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            response = (HttpWebResponse)request.GetResponse();
        }

        [Then(@"my authentication has been set")]
        public void ThenMyAuthenticationHasBeenSet()
        {
            var responseSetCookie = response.Headers.Get("Set-Cookie");
            AuthenticatedCookie = responseSetCookie;
            Assert.NotNull(responseSetCookie);
            Assert.IsTrue(responseSetCookie.Contains(".AspNet.ApplicationCookie="));
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Given(@"I am Api authenticated")]
        public void GivenIAmAuthenticated()
        {
            WhenIMakeARequestToTheApiWithValidCredentials();
            ThenMyAuthenticationHasBeenSet();
        }

        [When(@"I make a token delete request to the api")]
        public void WhenIMakeATokenDeleteRequestToTheApi()
        {
            var setCookie = response.Headers.Get("Set-Cookie");
            var request = (HttpWebRequest)WebRequest.Create(Navigation.Url(Page.ApiTokens));
            request.Headers.Add("Cookie", setCookie);
            request.Method = "DELETE";

            response = (HttpWebResponse)request.GetResponse();
        }

        [Then(@"my session is destroyed")]
        public void ThenMySessionIsDestroyed()
        {
            DateTime cookieExpiryDate;
            DateTime.TryParse(response.Headers.Get("Expires"), out cookieExpiryDate);

            Assert.NotNull(cookieExpiryDate);
            Assert.True(cookieExpiryDate < DateTime.Now);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Then(@"my requests are now unauthorised")]
        public void ThenMyRequestsAreNowUnauthorised()
        {
            var setCookie = response.Headers.Get("Set-Cookie");
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:6662/api/organisations");
            request.Headers.Add("Cookie", setCookie);
            request.Method = "GET";

            var unauthorised = false;
            var exceptionMessage = "";

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                exceptionMessage = e.GetBaseException().Message;
                unauthorised = true;
            }
            finally
            {
                Assert.True(unauthorised);
                Assert.AreEqual("The remote server returned an error: (401) Unauthorized.", exceptionMessage);
            }
        }
    }
}
