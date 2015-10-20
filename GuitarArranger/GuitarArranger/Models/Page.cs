using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuitarArranger.Models
{
    public class Page
    {
        public int PageId { get; set; }
        
        virtual public List<Measure> Measures { get; set; }

        public Page()
        {
            Measures = new List<Measure>();
        }

        public Page(List<Measure> measures)
        {
            Measures = measures;
        }
    }
}