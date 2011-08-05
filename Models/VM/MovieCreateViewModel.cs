using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2M.Models;

namespace M2M.ViewModels
{
    public class MovieCreateViewModel
    {
        public Movie Movie { get; set; }
        public ICollection<Tag> Tags{get; set;}
    }
}