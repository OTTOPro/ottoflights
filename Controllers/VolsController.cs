using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OttoFlights.Data;
using OttoFlights.Models;

namespace OttoFlights.Controllers
{
    public class VolsController : Controller
    {
        private readonly VolContext _context;

        public VolsController(VolContext context)
        {
            _context = context;
        }

        // GET: Vols
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> IndexUtilisateur()
        {
            return View();
        }

        // GET: Vols/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vol = await _context.Vol
                .Include(v => v.Evenements) 
                .FirstOrDefaultAsync(m => m.VolId == id);

            if (vol == null)
            {
                return NotFound();
            }

            return View(vol);
        }

        // GET: Vols/Create
        public IActionResult Create()
        {
            return View();
        }
        // GET: Vols/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vol = await _context.Vol.FindAsync(id);
            if (vol == null)
            {
                return NotFound();
            }
            return View(vol);
        }
       

        // GET: Vols/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vol = await _context.Vol
                .FirstOrDefaultAsync(m => m.VolId == id);
            if (vol == null)
            {
                return NotFound();
            }

            return View(vol);
        }

    }
}
