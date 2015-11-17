using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuitarArranger.Models
{
    public class Note
    {
        public int NoteId { get; set; }

        public virtual List<Tone> Tones{ get; set; }

        public virtual List<TabTone> TabTones { get; set; }

        public string Beat { get; set; }

        public Note()
        {
            Tones = new List<Tone>();
            TabTones = new List<TabTone>();
            Beat = "";
        }

        public Note(List<Tone> tones, List<TabTone> tabTones, string beat)
        {
            Tones = tones;
            TabTones = tabTones;
            Beat = beat;
        }
    }
}