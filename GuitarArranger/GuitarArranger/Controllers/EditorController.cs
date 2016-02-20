using GuitarArranger.Models;
using GuitarArranger.Tabulator;
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
        public ActionResult Index(Composition c)
        {
            System.Web.HttpContext.Current.Session["CompositionID"] = c.CompositionID.ToString();
            return View(c);
        }

        [HttpGet]
        public ActionResult GetSong()
        {
            Song song = new Song();
            if (System.Web.HttpContext.Current.Session["CompositionID"] as string == "0")
            {
                song.Pages.Add(new Page());
                song.Artist = "N/A";
                song.Title = "New Song";
                song.Author = "";
                song.BeatsPerMeasure = 4;
                song.SingleBeat = 4;
            }
            else
            {
                Composition c;
                int compID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CompositionID"] as string);
                using (var db = new CompositionContext())
                {
                    c = db.Compositions.Where(x => x.CompositionID == compID).Single();
                    song.setMetaData(c);
                    song.setContent(c.Content, c.TabContent);
                }
            }

            return Json(song, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetSongFromComposition(Composition c)
        {
            Song song = new Song();
            using (var db = new CompositionContext())
            {
                song.setMetaData(c);
                song.setContent(c.Content, c.TabContent);
            }
            return Json(song, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetChord()
        {
            Note chord = new Note();
            return Json(chord, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveSong(Song song)
        {
            int compID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CompositionID"] as string);
            using (var db = new CompositionContext())
            {
                Composition c;
                if (compID == 0)
                {
                    c = new Composition();
                }
                else
                {
                    c = db.Compositions.Find(compID);
                }
                song.getMetaData(c);
                c.Content = song.getContent();
                c.TabContent = song.getTabContent();
                c.User = User.Identity.Name;
               if (compID == 0)
                { 
                    db.Compositions.Add(c);
                }
                db.SaveChanges();
                song.setMetaData(c);
                song.SongId = c.CompositionID;
            }
            foreach (var p in song.Pages)
            {
                while (p.Measures.Count() < 28)
                {
                    p.Measures.Add(new Measure());
                }
            }
            return Json(song, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TabulateSong(Song song)
        {
            Tabulator.Tabulator tab = new SimpleTabulator();
            Song s = new Song();
            foreach (var page in song.Pages)
            {
                Page p = new Page();
                p.Measures = new List<Measure>();
                foreach (var measure in page.Measures)
                {
                    Measure m = new Measure();
                    foreach (var note in measure.Notes)
                    {
                        Note n = new Note();
                        n.Beat = note.Beat;
                        foreach (var tone in note.Tones)
                        {
                            n.Tones.Add(tone);
                            n.TabTones.Add(tab.getTabNote(tone));
                        }
                        m.Notes.Add(n);
                    }
                    p.Measures.Add(m);
                }
                s.Pages.Add(p);
            }
            Composition c = new Composition();
            song.getMetaData(c);
            s.setMetaData(c);
            foreach (var p in s.Pages)
            {
                while (p.Measures.Count() < 28)
                {
                    p.Measures.Add(new Measure());
                }
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }     
    }
}