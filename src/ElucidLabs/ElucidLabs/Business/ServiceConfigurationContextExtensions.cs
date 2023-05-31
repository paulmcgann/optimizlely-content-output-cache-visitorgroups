using EPiServer.Cms.TinyMce.Core;
using EPiServer.ServiceLocation;

namespace ElucidLabs.Business
{
    public static class ServiceConfigurationContextExtensions
    {
        public static void AddTinyMceConfiguration(this IServiceCollection services)
        {
            services.Configure<TinyMceConfiguration>(config =>
            {
                config.Default()
                    .AddPlugin("media wordcount anchor code searchreplace")
                    .Toolbar("blocks fontfamily fontsize | epi-personalized-content epi-link anchor numlist bullist indent outdent bold italic underline code",
                        "alignleft aligncenter alignright alignjustify | image epi-image-editor media | epi-dnd-processor | forecolor backcolor | removeformat | searchreplace fullscreen")
                    .AddSetting("image_caption", true)
                    .AddSetting("image_advtab", true)
                    .AddSetting("resize", "both")
                    .AddSetting("height", 800)
                  .AddSetting("width", 1000);

                config.Default()
                    .AddEpiserverSupport()
                    //.AddExternalPlugin("icons", "/ClientResources/Scripts/fontawesomeicons.js")
                    .AddSetting("extended_valid_elements", "i[class], span");
                //.ContentCss(new[] { "/ClientResources/Styles/fontawesome.min.css",
                //    "https://fonts.googleapis.com/css?family=Roboto:100,100i,300,300i,400,400i,500,500i,700,700i,900,900i",
                //    "/ClientResources/Styles/TinyMCE.css" });
            });
        }
    }
}