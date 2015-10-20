using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuitarArranger.Models
{
    public class Song
    {
        public int SongId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Artist { get; set; }
        
        public string Domain { get; set; }

        virtual public List<Page> Pages { get; set; }

        public Song()
        {
            Pages = new List<Page>();
        }
    }
}