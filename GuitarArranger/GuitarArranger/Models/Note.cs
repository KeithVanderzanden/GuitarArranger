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
            sb.Append(Beat + "-");
            foreach (var tone in Tones)
            {
                sb.Append(tone.ToString());
            }
            return sb.ToString();
        }

        public string getNoteTabContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Beat + "-");
            foreach (var tone in TabTones)
            {
                sb.Append(tone.ToString());
            }
            return sb.ToString();
        }

        public void setContent(string content)
        {
            Tones.Clear();
            string[] tones = content.Split('-');
            tones = tones.Take(tones.Count() - 1).ToArray();
            if (tones.Count() > 0)
            {
                Beat = tones[0];
                for (int i = 1; i <= tones.Count() - 2; i += 2)
                {
                    Tone t = new Tone(tones[i], tones[i + 1]);
                    Tones.Add(t);
                }
            }
        }

        public void setTabContent(string tabContent)
        {
            TabTones.Clear();
            string[] tones = tabContent.Split('-');
            tones = tones.Take(tones.Count() - 1).ToArray();
            if (tones.Count() > 3)
            {
                for (int i = 1; i < tones.Count() - 2; i += 3)
                {
                    TabTone t = new TabTone(tones[i], tones[i + 1], tones[i + 2]);
                    TabTones.Add(t);
                }
            }
        }
    }
}