using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        //GET:Movies/Details/1

        public async Task<ActionResult> Details(int id) 
        {
           var data=await _iMoviesService.GetMovieByIdAsync(id);

            return View(data);
        }

        //GET
        public async Task<ActionResult> Create()
        {
            var data =await _iMoviesService.GetMovieDropDownItems();

            ViewBag.Actors = new SelectList(data.Actors, "Id", "FullName");
            ViewBag.Cinemas = new SelectList(data.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(data.Producers, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(NewMovieVM newMovieVM)
        {
            if(ModelState.IsValid)
            {
               await _iMoviesService.AddMovie(newMovieVM); 
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var data = await _iMoviesService.GetMovieDropDownItems();

                ViewBag.Actors = new SelectList(data.Actors, "Id", "FullName");
                ViewBag.Cinemas = new SelectList(data.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(data.Producers, "Id", "FullName");

                return View(newMovieVM);
            }
        }

        //Update
        public async Task<ActionResult> Edit(int Id)
        {
            var movieDetails = await _iMoviesService.GetMovieByIdAsync(Id);
            if (movieDetails == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(x => x.ActorId).ToList()
            };

            var data = await _iMoviesService.GetMovieDropDownItems();

            ViewBag.Actors = new SelectList(data.Actors, "Id", "FullName");
            ViewBag.Cinemas = new SelectList(data.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(data.Producers, "Id", "FullName");
            return View(response);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id,NewMovieVM newMovieVM)
        {
            if (id != newMovieVM.Id) return View("NotFound");

            if (ModelState.IsValid)
            {
                var data=await _iMoviesService.UpdateMovie(newMovieVM); 
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var data = await _iMoviesService.GetMovieDropDownItems();

                ViewBag.Actors = new SelectList(data.Actors, "Id", "FullName");
                ViewBag.Cinemas = new SelectList(data.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(data.Producers, "Id", "FullName");

                return View(newMovieVM);
            }
        }

        public async Task<ActionResult> Filter(string searchString)
        {
            var allMovies =await _iMoviesService.GetAllAsync(x=>x.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var fillterdMovie = allMovies.Where(x => x.Name.Contains(searchString)||x.Description.Contains(searchString)).ToList();
                return View("Index", fillterdMovie);
            }
            return View("Index", allMovies);
        }


    }
}
