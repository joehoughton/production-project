﻿namespace production_project.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }

}