using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace ChatApplication
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);
            // set up nancy to look at our webpack generated build folder
            conventions.ViewLocationConventions.Add((viewName, model, context) => string.Concat("App/build/", viewName));
            // we also need to get to the static files
            conventions.StaticContentsConventions.Add((ctx, rootPath) =>
            {
                var staticContentBuilder = StaticContentConventionBuilder
                    .AddDirectory(
                    "static",
                    "App/build/static"
                    );
                return staticContentBuilder(ctx, rootPath);
            });
        }
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
           
        }
    }
}