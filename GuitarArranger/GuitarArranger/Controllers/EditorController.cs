﻿using GuitarArranger.Models;
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
            song.BeatsPerMeasure = 4;
            song.SingleBeat = 4;
            return Json(song, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetChord()
        {
            Note chord = new Note();
            return Json(chord, JsonRequestBehavior.AllowGet);
        }
    }
}