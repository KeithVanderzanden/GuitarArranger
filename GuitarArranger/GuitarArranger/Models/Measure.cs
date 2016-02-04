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
    }
}