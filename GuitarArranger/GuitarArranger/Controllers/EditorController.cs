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
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetSong()
        {
            Song song = new Song();
            song.Pages.Add(new Page());
            song.Artist = "some artist";
            song.BeatsPerMeasure = 4;
            song.SingleBeat = 4;
            /*song.Pages[0].Measures[0] = new Measure(new List<Note> {
                    new Note(new List<Tone> { new Tone("c/4", "") }, 
                        new List<TabTone> (), "q"),
                    new Note(new List<Tone> { new Tone("d/4", "") }, 
                        new List<TabTone> (), "q"),
                    new Note(new List<Tone> { new Tone("e/4", "#") }, 
                        new List<TabTone> (), "q"),
                    new Note(new List<Tone> { new Tone("e/4", "") }, 
                        new List<TabTone> (),  "q")
                });*/ 
            return Json(song, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetChord()
        {
            Note chord = new Note();
            return Json(chord, JsonRequestBehavior.AllowGet);
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
                            n.NoteId = note.NoteId;
                        }
                        m.Notes.Add(n);
                        m.MeasureID = measure.MeasureID;
                    }
                    p.Measures.Add(m);
                    p.PageId = page.PageId;
                }
                s.Artist = song.Artist;
                s.Author = song.Author;
                s.BeatsPerMeasure = song.BeatsPerMeasure;
                s.Pages.Add(p);
                s.SingleBeat = song.SingleBeat;
                s.SongId = song.SongId;
                s.Title = song.Title;
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }
    }
}