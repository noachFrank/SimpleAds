using Microsoft.AspNetCore.Mvc;
using SimpleAds.data;
using SimpleAds.Models;
using SimpleAds.web.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SimpleAds.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=SimpleAds;Integrated Security=true;";


        public IActionResult Index()
        {
            var manager = new DbManager(_connectionString);
            var vm = new AdsViewModel
            {
                Ads = manager.GetAllAds(),
               // Id = HttpContext.Session.Get<int>("id")
            };

            var ids = HttpContext.Session.Get<List<int>>("Ids");

            foreach(var ad in vm.Ads)
            {
                ad.Delete = ids != null && ids.Contains(ad.Id);
            }

            return View(vm);
        }

        public IActionResult NewAdForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewAd(Ad ad)
        {
            var manager = new DbManager(_connectionString);
            manager.NewAd(ad);

            var ids = HttpContext.Session.Get<List<int>>("Ids");
            if (ids == null)
            {
                ids = new List<int>();
            }
            ids.Add(ad.Id);
            HttpContext.Session.Set("Ids", ids);


            return Redirect("/");
        }

        [HttpPost]
        public IActionResult DeleteAd(int id)
        {
            var manager = new DbManager(_connectionString);
            
            manager.DeleteAd(id);

            return Redirect("/");
        }
    }
   
}