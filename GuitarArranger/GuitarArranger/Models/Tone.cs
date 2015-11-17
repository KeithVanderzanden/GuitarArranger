using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuitarArranger.Models
{
    public class Tone
    {
        public virtual string Key { get; set; }

        public virtual string Modifier { get; set; }

        public Tone()
        {
            Key = "";
            Modifier = "";
        }
        public Tone(string key, string mod)
        {
            Key = key;
            Modifier = mod;
            if (Key == null)
                Key = "";
            if (Modifier == null)
                Modifier = "";
        }
    }
}