﻿using eTickets.Data;
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
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var data=await _context.Movies.ToListAsync();
            return View(data);
        }
    }
}