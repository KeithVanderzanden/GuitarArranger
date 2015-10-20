using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuitarArranger.Models
{
    public class Note
    {
        public int NoteId { get; set; }

        public List<Tone> Tones{ get; set; }

        public string Beat { get; set; }

        public Note()
        {
            Tones = new List<Tone>();
        }

        public Note(List<Tone> tones, string beat)
        {
            Tones = tones;
            Beat = beat;
        }
    }
}