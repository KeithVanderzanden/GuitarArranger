using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuitarArranger.Models
{
    public class Tone
    {
        public string Key { get; set; }

        public string Modifier { get; set; }

        public Tone(string key, string mod)
        {
            Key = key;
            Modifier = mod;
        }
    }
}