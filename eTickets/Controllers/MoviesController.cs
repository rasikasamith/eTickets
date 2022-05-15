using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _iMoviesService;

        public MoviesController(IMoviesService iMoviesService)
        {
            _iMoviesService = iMoviesService;
        }

        public async Task<IActionResult> Index()
        {
           // var data=await _context.Movies.Include(m=>m.Cinema).Include(a=>a.Actors_Movies).OrderBy(x=>x.Name).ToListAsync();
            var data =await _iMoviesService.GetAllAsync(n=>n.Cinema);
            return View(data);
        }
    }
}
