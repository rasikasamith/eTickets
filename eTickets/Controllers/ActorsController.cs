using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ActorsController(AppDbContext context)
        {
            _dbContext = context;
        }
        public IActionResult Index()
        {          

            var data = _dbContext.Actors.ToList();
            return View(data);
        }
    }
}
