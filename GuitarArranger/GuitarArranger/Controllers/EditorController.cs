using GuitarArranger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuitarArranger.Controllers
{
    public class EditorController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            Song song = new Song();
            song.Artist = "Pink Floyd";

            return View(song);
        }
    }
}