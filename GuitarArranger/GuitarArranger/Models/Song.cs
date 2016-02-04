using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace GuitarArranger.Models
{
    public class Song
    {
        public int SongId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Artist { get; set; }
        
        public string Difficulty { get; set; }

        public int BeatsPerMeasure { get; set; }

        public int SingleBeat { get; set; }

        public List<Page> Pages { get; set; }

        public Song()
        {
            Pages = new List<Page>();
        }

        public string getContent()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var page in Pages)
            {
                sb.Append(page.getPageContent());
            }
            return sb.ToString();
        }

        public string getTabContent()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var page in Pages)
            {
                sb.Append(page.getPageTabContent());
            }
            return sb.ToString();
        }

        public void setContent(string content, string tabContent)
        {
            throw new NotImplementedException();
        }

        public void getMetaData(Composition c)
        {
            c.CompositionID = SongId;
            c.Artist = Artist;
            c.Author = Author;
            c.BeatsPerMeasure = BeatsPerMeasure;
            c.Difficulty = Difficulty;
            c.SingleBeat = SingleBeat;
            c.Title = Title;
            return;
        }

        public void setMetaData(Composition c)
        {
            SongId = c.CompositionID;
            Artist = c.Artist;
            Author = c.Author;
            BeatsPerMeasure = c.BeatsPerMeasure;
            Difficulty = c.Difficulty;
            SingleBeat = c.SingleBeat;
            Title = c.Title;
        }
    }

}