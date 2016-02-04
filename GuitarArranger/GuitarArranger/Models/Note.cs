using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace GuitarArranger.Models
{
    public class Note
    {
        public List<Tone> Tones{ get; set; }

        public List<TabTone> TabTones { get; set; }

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

        public string getNoteContent()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var tone in Tones)
            {
                sb.Append(Beat + " ");
                sb.Append(tone.ToString());
                sb.Append('.');
            }
            return sb.ToString();
        }

        public string getNoteTabContent()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var tone in TabTones)
            {
                sb.Append(Beat + " ");
                sb.Append(tone.ToString());
                sb.Append('.');
            }
            return sb.ToString();
        }
    }
}