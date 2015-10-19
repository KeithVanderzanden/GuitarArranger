using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuitarArranger.Models
{
    public class Note
    {
        public int NoteId { get; set; }

        public string Tone { get; set; }

        public string Modifier { get; set; }

        public string Beat { get; set; }
    }
}