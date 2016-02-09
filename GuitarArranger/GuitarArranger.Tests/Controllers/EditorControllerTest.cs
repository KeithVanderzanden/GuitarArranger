using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuitarArranger;
using GuitarArranger.Controllers;
using GuitarArranger.Models;

namespace GuitarArranger.Tests.Controllers
{
    [TestClass]
    public class EditorControllerTest
    {
        [TestMethod]
        public void EditorDrawsChromaticScale()
        {
            Song song = new Song();
            Page page = new Page(new List<Measure> {
                new Measure(new List<Note> {
                    new Note(new List<Tone> { new Tone("e/3", "") }, new List<TabTone>(), "q"),
                    new Note(new List<Tone> { new Tone("f/3", "") }, new List<TabTone>(), "q"),
                    new Note(new List<Tone> { new Tone("f/3", "#") }, new List<TabTone>(), "q"),
                    new Note(new List<Tone> { new Tone("g/3", "") }, new List<TabTone>(),  "q")
                }),
                new Measure(new List<Note> {
                    new Note(new List<Tone> { new Tone("e/3", "") }, new List<TabTone>(), "q"),
                    new Note(new List<Tone> { new Tone("f/3", "") }, new List<TabTone>(), "q"),
                    new Note(new List<Tone> { new Tone("f/3", "#") }, new List<TabTone>(), "q"),
                    new Note(new List<Tone> { new Tone("g/3", "") }, new List<TabTone>(), "q")
                }),
                new Measure(new List<Note> {
                    new Note(new List<Tone> { new Tone("e/3", "") }, new List<TabTone>(), "q"),
                    new Note(new List<Tone> { new Tone("f/3", "") }, new List<TabTone>(), "q"),
                    new Note(new List<Tone> { new Tone("f/3", "#") }, new List<TabTone>(), "q"),
                    new Note(new List<Tone> { new Tone("g/3", "") }, new List<TabTone>(), "q")
                }),
                new Measure(new List<Note> {
                    new Note(new List<Tone> { new Tone("g/3", "#") }, new List<TabTone>(), "8"),
                    new Note(new List<Tone> { new Tone("a/3", "") }, new List<TabTone>(), "8"),
                    new Note(new List<Tone> { new Tone("a/3", "#") }, new List<TabTone>(), "8"),
                    new Note(new List<Tone> { new Tone("b/3", "") }, new List<TabTone>(), "8"),
                    new Note(new List<Tone> { new Tone("c/4", "") }, new List<TabTone>(), "8"),
                    new Note(new List<Tone> { new Tone("c/4", "#") }, new List<TabTone>(), "8"),
                    new Note(new List<Tone> { new Tone("d/4", "") }, new List<TabTone>(), "8"),
                    new Note(new List<Tone> { new Tone("d/4", "#") }, new List<TabTone>(), "8")
                }),
                new Measure(new List<Note> {
                    new Note(new List<Tone> { new Tone("e/4", "") }, new List<TabTone>(), "8"),
                    new Note(new List<Tone> { new Tone("f/4", "") }, new List<TabTone>(), "8"),
                    new Note(new List<Tone> { new Tone("f/4", "#") }, new List<TabTone>(), "8"),
                    new Note(new List<Tone> { new Tone("g/4", "") }, new List<TabTone>(), "8"),
                    new Note(new List<Tone> { new Tone("g/4", "#") }, new List<TabTone>(), "8"),
                    new Note(new List<Tone> { new Tone("a/4", "") }, new List<TabTone>(), "8"),
                    new Note(new List<Tone> { new Tone("a/4", "#") }, new List<TabTone>(), "8"),
                    new Note(new List<Tone> { new Tone("b/4", "") }, new List<TabTone>(), "8")
                })
            });
            song.Pages.Add(page);
            // Arrange
            EditorController controller = new EditorController();

        }
    }
}
