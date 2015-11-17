using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuitarArranger.Models
{
    public class TabTone
    {
        public virtual string Fret { get; set; }

        public virtual string TabModifier { get; set; }

        public virtual string StringNum { get; set; }

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
    }
}