using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GuitarArranger.Models;

namespace GuitarArranger.Tabulator
{
    public class SimpleTabulator : Tabulator
    {
        public TabTone getTabNote(Tone tone)
        {
            return translate(tone.Key, tone.Modifier);
        }

        private TabTone translate(string key, string modifier)
        {
            int fret = 0, str = 0;
            string mod = "";
            switch(key)
            { 
                case "e/3":
                    str = 6;
                    fret = 0;
                    break;
                case "f/3":
                    str = 6;
                    fret = 1;
                    break;
                case "g/3":
                    str = 6;
                    fret = 3;
                    break;
                case "a/3":
                    str = 5;
                    fret = 0;
                    break;
                case "b/3":
                    str = 5;
                    fret = 2;
                    break;
                case "c/4":
                    str = 5;
                    fret = 3;
                    break;
                case "d/4":
                    str = 4;
                    fret = 0;
                    break;
                case "e/4":
                    str = 4;
                    fret = 2;
                    break;
                case "f/4":
                    str = 4;
                    fret = 3;
                    break;
                case "g/4":
                    str = 3;
                    fret = 0;
                    break;
                case "a/4":
                    str = 3;
                    fret = 2;
                    break;
                case "b/4":
                    str = 2;
                    fret = 0;
                    break;
                case "c/5":
                    str = 2;
                    fret = 1;
                    break;
                case "d/5":
                    str = 2;
                    fret = 3;
                    break;
                case "e/5":
                    str = 1;
                    fret = 0;
                    break;
                case "f/5":
                    str = 1;
                    fret = 1;
                    break;
                case "g/5":
                    str = 1;
                    fret = 3;
                    break;
                case "a/5":
                    str = 1;
                    fret = 5;
                    break;
                case "b/5":
                    str = 1;
                    fret = 7;
                    break;
                case "c/6":
                    str = 1;
                    fret = 8;
                    break;
                case "d/6":
                    str = 1;
                    fret = 10;
                    break;
                case "e/6":
                    str = 1;
                    fret = 12;
                    break;
                case "f/6":
                    str = 1;
                    fret = 13;
                    break;
                case "g/6":
                    str = 1;
                    fret = 15;
                    break;
                case "a/6":
                    str = 1;
                    fret = 17;
                    break;
                case "b/6":
                    str = 1;
                    fret = 19;
                    break;
                default:
                    break;
            }
            switch(modifier)
            {
                case "b":
                    fret -= 1;
                    break;
                case "bb":
                    fret -= 2;
                    break;
                case "#":
                    fret += 1;
                    break;
                case "##":
                    fret += 2;
                    break;
                default:
                    break;
            }
            if (fret < 0)
            {
                if (str < 6) //cannot handle e/3 flat yet
                {
                    if (str == 2)
                        fret += 4;
                    else
                        fret += 5;
                    str += 1;
                }
            }
            if (fret > 4)
            {
                if (str > 1)
                {
                    if (str == 3)
                        fret -= 4;
                    else
                        fret -= 5;
                    str -= 1;
                }
            }
            return new TabTone(fret.ToString(), mod, str.ToString());
        }
    }
}