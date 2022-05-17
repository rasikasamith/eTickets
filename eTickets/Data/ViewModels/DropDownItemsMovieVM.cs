using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.ViewModels
{
    public class DropDownItemsMovieVM
    {
        public DropDownItemsMovieVM()
        {
            Actors = new List<Actor>();
            Cinemas = new List<Cinema>();
            Producers = new List<Producer>();
        }

        public List<Actor> Actors { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public List<Producer> Producers { get; set; }

              
    }
}
 