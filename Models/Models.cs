using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace M2M.Models
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Tag> Tags{ get; set; }
        public Movie() {
            Tags = new List<Tag>();
        }
    }

    public class Tag {
        public int tagID { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }

    public class M2MContext : System.Data.Entity.DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }

}

