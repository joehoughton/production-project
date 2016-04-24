namespace production_project.Web.Specs.Helpers
{
    public static class Navigation
    {
        private static readonly string Port = "6662";
        private static readonly string BaseUrl = string.Format("http://localhost:{0}/", Port);

        public static string Url(Page page)
        {
            switch (page)
            {
                case (Page.Ping):
                    return BaseUrl + "api/organisations/1";
                case (Page.ApiTokens):
                    return BaseUrl + "api/tokens";
                default:
                return BaseUrl;
            }
        }
    }
}
