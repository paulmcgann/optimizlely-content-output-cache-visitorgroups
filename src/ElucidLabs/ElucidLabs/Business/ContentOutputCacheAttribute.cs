using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebEssentials.AspNetCore.OutputCaching;

namespace ElucidLabs.Business
{
    public class ContentOutputCacheAttribute : OutputCacheAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.HttpContext.EnableOutputCaching(
                    TimeSpan.FromSeconds(Duration),
                    VaryByHeader,
                    VaryByParam,
                    VaryByCustom,
                    UseAbsoluteExpiration);
            }
        }
    }
}
