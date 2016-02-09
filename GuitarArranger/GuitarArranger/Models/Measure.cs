using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace GuitarArranger.Models
{
    public class Measure
    {
        public List<Note> Notes { get; set; }

        public Measure()
        {
            Notes = new List<Note>();
        }

        public Measure(List<Note> notes)
        {
            Notes = notes;
        }

        public string getMeasureContent()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var note in Notes)
            {
                sb.Append(note.getNoteContent());
                sb.Append(',');
            }
            return sb.ToString();
        }

        public string getMeasureTabContent()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var note in Notes)
            {
                sb.Append(note.getNoteTabContent());
                sb.Append(',');
            }
            return sb.ToString();
        }

        public void setContent(string content)
        {
            Notes.Clear();
            string[] notes = content.Split(',');
            notes = notes.Take(notes.Count() - 1).ToArray();
            foreach (string n in notes)
            {
                Note note = new Note();
                note.setContent(n);
                Notes.Add(note);
            }
        }

        public void setTabContent(string tabContent)
        {
            string[] notes = tabContent.Split(',');
            notes = notes.Take(notes.Count() - 1).ToArray();
            for (int i = 0; i < notes.Count(); i++)
            {
                Notes[i].setTabContent(notes[i]);
            }
        }
    }
}