using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuitarArranger.Models
{
    public class Measure
    {
        public int MeasureID { get; set; }

        public virtual List<Note> Notes { get; set; }

        public Measure()
        {
            Notes = new List<Note>();
        }

        public Measure( List<Note> notes)
        {
            Notes = notes;
        }
    }
}