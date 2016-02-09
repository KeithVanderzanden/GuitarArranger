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

        public ActionResult UsersFiles()
        {
            List<Composition> files = new List<Composition>();
            using (var db = new CompositionContext())
            {
                files = db.Compositions.Where(x => x.User == User.Identity.Name).ToList<Composition>();
                foreach (Composition c in files)
                {
                    c.Content = "";
                    c.TabContent = "";
                }
            }
            return PartialView(files);
        }

        public ActionResult Search(string searchBy, string search)
        {
            List<Composition> files = new List<Composition>();
            using (var db = new CompositionContext())
            {
                if (searchBy == "Difficulty" || searchBy == null)
                {
                    if (search == null)
                        files = db.Compositions.Where(x => x.Difficulty == "Easy").ToList<Composition>();
                    else
                        files = db.Compositions.Where(x => x.Difficulty == search).ToList<Composition>();
                }
                else
                {
                    files = db.Compositions.Where(x => x.Title == search).ToList<Composition>();
                }
            }
            return PartialView(files);
        }
    }
}