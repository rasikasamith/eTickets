using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eTickets.Data.Services;
using eTickets.Models;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorService _iActorService;

        public ActorsController(IActorService iActorService)
        {
            _iActorService = iActorService;
        }

        public async Task<IActionResult> Index()
        {     
           var data=await _iActorService.GetAllAsync();
            return View(data);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("ProfilePictureURL", "FullName","Bio")]Actor actor)
        {
            if(ModelState.IsValid)
            {
                _iActorService.AddAsync(actor);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(actor);
            }
        }

        //[HttpPost]
        public async Task<IActionResult> Details(int id)
        {
            var data = await  _iActorService.GetByIdAsync(id);

            if (data == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(data);
            }                    
        }

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var actorDetails = await _iActorService.GetByIdAsync(id);

        //    if (actorDetails == null)
        //    {
        //        return View("NotFound");
        //    }
        //    else
        //    {
        //        return View(actorDetails);
        //    }
        //}

        //[HttpPost]
        //public IActionResult Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _iActorService.UpdateAsync(id, actor);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    else
        //    {
        //        return View(actor);
        //    }
        //}

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var actorDetails = _iActorService.GetById(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(actorDetails);
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                _iActorService.Update(id, actor);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(actor);
            }
        }

        //Delete
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _iActorService.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(actorDetails);
            }
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _iActorService.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                await _iActorService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));                
            }
        }
    }
}
