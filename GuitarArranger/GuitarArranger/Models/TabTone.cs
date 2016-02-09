using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuitarArranger.Models
{
    public class TabTone
    {
        public string Fret { get; set; }

        public string TabModifier { get; set; }

        public string StringNum { get; set; }

        public TabTone()
        {
            Fret = "";
            TabModifier = "";
            StringNum = "";
        }

        public TabTone(string fret, string mod, string str)
        {
            StringNum = str;
            Fret = fret;
            TabModifier = mod;
            if (StringNum == null)
                StringNum = "";
            if (TabModifier == null)
                TabModifier = "";
            if (Fret == null)
                Fret = "";
        }

        public override string ToString()
        {
            return (Fret + "-" + TabModifier + "-" + StringNum + "-");
        }
    }
}