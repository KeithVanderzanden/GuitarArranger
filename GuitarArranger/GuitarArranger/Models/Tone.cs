using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuitarArranger.Models
{
    public class Tone
    {
        public string Key { get; set; }

        public string Modifier { get; set; }

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

        public override string ToString()
        {
            return (Key + "-" + Modifier + "-");
        }
    }
}