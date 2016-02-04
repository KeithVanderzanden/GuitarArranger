using GuitarArranger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuitarArranger.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

        [HttpGet]
        public ActionResult GetUsersCompositions(string user)
        {
            List<Composition> allComps = new List<Composition>();
            using (var db = new CompositionContext())
            {
                allComps.AddRange(db.Compositions.Where(x => x.Author == user).ToList<Composition>());
            }
            return Json(allComps, JsonRequestBehavior.AllowGet);
        }
    }
}