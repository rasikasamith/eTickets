using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Services;
using eTickets.Models;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        //private readonly AppDbContext _context;
        private readonly IProducersService _iProducersService;

        public ProducersController(IProducersService iProducersService)
        {
            _iProducersService = iProducersService;
        }
        public async Task<IActionResult> Index()
        {
            //var allProducers = await _context.Producers.ToListAsync();
            var allProducers = await _iProducersService.GetAllAsync();

            return View(allProducers);
        }

        //GET:Producers/details/1
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _iProducersService.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);

        }

        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("ProfilePictureURL", "FullName", "Bio")] Producer producer)
        {

            if (ModelState.IsValid)
            {
                _iProducersService.AddAsync(producer);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(producer);
            }
        }

        //Edit with async
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _iProducersService.GetByIdAsync(id);

            if (producerDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(producerDetails);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                await _iProducersService.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(producer);
            }
        }
        //Delete
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _iProducersService.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(actorDetails);
            }
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _iProducersService.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                await _iProducersService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
