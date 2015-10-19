using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuitarArranger.Models
{
    public class Measure
    {
        public int MeasureID { get; set; }

        public int MeasureNum { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

        public Measure()
        {
            Notes = new List<Note>();
        }
    }
}