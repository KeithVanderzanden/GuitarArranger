using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GuitarArranger.Models
{
    public class Page
    {
        public virtual List<Measure> Measures { get; set; }

        public Page()
        {
            Measures = new List<Measure>();
            for (int i = 0; i < 28; i++)
            {
                Measures.Add(new Measure());
            }
        }

        public Page(List<Measure> measures)
        {
            Measures = measures;
        }

        public string getPageContent()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var measure in Measures)
            {
                sb.Append(measure.getMeasureContent());
                sb.Append(';');
            }
            return sb.ToString();
        }

        public string getPageTabContent()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var measure in Measures)
            {
                sb.Append(measure.getMeasureTabContent());
                sb.Append(';');
            }
            return sb.ToString();
        }

        public void setContent(string content)
        {
            string[] measures = content.Split(';');
            measures = measures.Take(measures.Count() - 1).ToArray();
            for (int i = 0; i < measures.Count(); i++)
            {
                Measures[i].setContent(measures[i]);
            }
        }

        public void setTabContent(string tabContent)
        {
            string[] measures = tabContent.Split(';');
            measures = measures.Take(measures.Count() - 1).ToArray();
            for (int i = 0; i < measures.Count(); i++)
            {
                Measures[i].setTabContent(measures[i]);
            }
        }
    }
}