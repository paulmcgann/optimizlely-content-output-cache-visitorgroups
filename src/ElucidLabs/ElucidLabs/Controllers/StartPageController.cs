using ElucidLabs.Models.Pages;
using ElucidLabs.Models.ViewModels;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace ElucidLabs.Controllers
{
    public class StartPageController : PageControllerBase<StartPage>
    {
        [ElucidLabs.Business.ContentOutputCache(Duration = 86400, VaryByCustom = "visitorgroups")]
        public IActionResult Index(StartPage currentPage)
        {
            var model = PageViewModel.Create(currentPage);

            // Check if it is the StartPage or just a page of the StartPage type.
            if (SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(currentPage.ContentLink))
            {
                // Connect the view models logotype property to the start page's to make it editable
                var editHints = ViewData.GetEditHints<PageViewModel<StartPage>, StartPage>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => m.Layout.ProductPages, p => p.ProductPageLinks);
                editHints.AddConnection(m => m.Layout.CompanyInformationPages, p => p.CompanyInformationPageLinks);
                editHints.AddConnection(m => m.Layout.NewsPages, p => p.NewsPageLinks);
                editHints.AddConnection(m => m.Layout.CustomerZonePages, p => p.CustomerZonePageLinks);
            }

            return View(model);
        }
    }
}