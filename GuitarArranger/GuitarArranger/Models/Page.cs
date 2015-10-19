using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuitarArranger.Models
{
    public class Page
    {
        public int PageId { get; set; }

        public int PageNum { get; set; }

        virtual public ICollection<Measure> Measures { get; set; }

        public Page()
        {
            Measures = new List<Measure>();
        }
    }
}