using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemaService _iCinemaService;

        public CinemasController(ICinemaService iCinemaService)
        {
            _iCinemaService = iCinemaService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _iCinemaService.GetAllAsync();
            return View(data);
        }

        //GET:Producers/details/1
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _iCinemaService.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Logo", "Name", "Description")] Cinema cinema)
        {

            if (ModelState.IsValid)
            {
                _iCinemaService.AddAsync(cinema);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(cinema);
            }
        }

        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var cinemaDetail =await _iCinemaService.GetByIdAsync(Id);
            if(cinemaDetail==null)
            {
                return View("NotFound");
            }
            else
            {
                return View(cinemaDetail);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id,[Bind("Id,Logo,Name,Description")]Cinema cinema)
        {
            if(ModelState.IsValid)
            {
                await _iCinemaService.UpdateAsync(Id, cinema);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete (int Id)
        {
            var data= await _iCinemaService.GetByIdAsync(Id);
            if(data==null)
            {
                return View("NotFound");
            }
            else
            {
                return View(data);
            }
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var data = _iCinemaService.GetByIdAsync(Id);
            if (data == null)
            {
                return View("NotFound");
            }
            else
            {
                await _iCinemaService.DeleteAsync(Id);
                return RedirectToAction(nameof(Index));
            }

        }
    }
}
