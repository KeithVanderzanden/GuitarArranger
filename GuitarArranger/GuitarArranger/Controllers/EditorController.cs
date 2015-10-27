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
        private Song song;
        // GET: Index
        public ActionResult Index()
        {
            song = new Song();
            song.Pages.Add(new Page());
            return View(song);
        }

        [HttpPost]
        public ActionResult Add(AddNote note)
        {
            Note addNote = new Note();
            addNote.Beat = note.Beat;
            addNote.Tones.Add(new Tone(note.Key, ""));
            song.Pages[0].Measures[note.Measure].Notes.Add(addNote);
            return View("Index", song);
        }
    }
}