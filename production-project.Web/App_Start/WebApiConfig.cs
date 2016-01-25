namespace production_project.Web
{
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using Newtonsoft.Json.Serialization;

    internal static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Clear();
            config.Filters.Add(new AuthorizeAttribute());
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add
                (new Newtonsoft.Json.Converters.StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.MapHttpAttributeRoutes();
        }
    }
}