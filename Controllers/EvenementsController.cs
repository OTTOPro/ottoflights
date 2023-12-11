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
    public class EvenementsController : Controller
    {
        private readonly VolContext _context;

        public EvenementsController(VolContext context)
        {
            _context = context;
        }

        // GET: Evenements
        public async Task<IActionResult> Index()
        {
            var query = from evenement in _context.Set<Evenement>()
                        join vol in _context.Set<Vol>() on evenement.VolId equals vol.VolId
                        select new VolEvenementViewModel
                        {
                            Evenement = evenement,
                            Vol = vol
                        };

            return View(await query.ToListAsync());
        }


        // GET: Evenements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenement = await _context.Evenement
                .Include(e => e.Vol)
                .FirstOrDefaultAsync(m => m.EvenementId == id);
            if (evenement == null)
            {
                return NotFound();
            }

            return View(evenement);
        }

        // GET: Evenements/Create
        public IActionResult Create()
        {
            ViewData["VolId"] = new SelectList(_context.Vol, "VolId", "Agence");
            return View();
        }

        // POST: Evenements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EvenementId,DateRevisée,VolId,Statut")] Evenement evenement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evenement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VolId"] = new SelectList(_context.Vol, "VolId", "Agence", evenement.VolId);
            return View(evenement);
        }

        // GET: Evenements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenement = await _context.Evenement.FindAsync(id);
            if (evenement == null)
            {
                return NotFound();
            }
            ViewData["VolId"] = new SelectList(_context.Vol, "VolId", "Agence", evenement.VolId);
            return View(evenement);
        }

        // POST: Evenements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EvenementId,DateRevisée,VolId,Statut")] Evenement evenement)
        {
            if (id != evenement.EvenementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evenement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvenementExists(evenement.EvenementId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VolId"] = new SelectList(_context.Vol, "VolId", "Agence", evenement.VolId);
            return View(evenement);
        }

        // GET: Evenements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenement = await _context.Evenement
                .Include(e => e.Vol)
                .FirstOrDefaultAsync(m => m.EvenementId == id);
            if (evenement == null)
            {
                return NotFound();
            }

            return View(evenement);
        }

        // POST: Evenements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evenement = await _context.Evenement.FindAsync(id);
            if (evenement != null)
            {
                _context.Evenement.Remove(evenement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvenementExists(int id)
        {
            return _context.Evenement.Any(e => e.EvenementId == id);
        }
    }
}
