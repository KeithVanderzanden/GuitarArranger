using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace GuitarArranger.Models
{
    public class Composition
    {
        public int CompositionID { get; set; }

        public string Title { get; set; }

        public string User { get; set; }

        public string Author { get; set; }

        public string Artist { get; set; }

        public string Difficulty { get; set; }

        public int BeatsPerMeasure { get; set; }

        public int SingleBeat { get; set; }

        public string Content { get; set; }

        public string TabContent { get; set; }
    }

    public class CompositionContext : DbContext
    {
        public CompositionContext()
        {
        }

        public DbSet<Composition> Compositions { get; set; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);
        }
    }
}