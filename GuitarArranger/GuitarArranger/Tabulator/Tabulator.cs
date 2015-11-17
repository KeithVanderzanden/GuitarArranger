using GuitarArranger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarArranger.Tabulator
{
    interface Tabulator
    {
        TabTone getTabNote(Tone tone);
    }
}
