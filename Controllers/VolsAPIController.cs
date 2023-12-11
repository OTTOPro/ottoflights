using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OttoFlights.Data;
using OttoFlights.Models;

namespace OttoFlights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolsAPIController : ControllerBase
    {
        private readonly VolContext _context;

        public VolsAPIController(VolContext context)
        {
            _context = context;
        }

        // GET: api/VolsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vol>>> GetVol()
        {
            return await _context.Vol.ToListAsync();
        }

        // GET: api/VolsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vol>> GetVol(int id)
        {
            //var vol = await _context.Vol.FindAsync(id);
            var vol = await _context.Vol
                .Include(v => v.Evenements)
                .FirstOrDefaultAsync(m => m.VolId == id);

            if (vol == null)
            {
                return NotFound();
            }

            return vol;
        }

        // PUT: api/VolsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVol(int id, Vol vol)
        {
            if (id != vol.VolId)
            {
                return BadRequest();
            }

            // Récupérer le vol avant la modification pour comparer la date de révision
            var existingVol = await _context.Vol.FindAsync(id);

            // Vérifier si la date de révision a changé
            if (existingVol.DateRevue != vol.DateRevue || existingVol.Etat != vol.Etat)
            {
                // Créer un nouvel événement avec l'état et la date de révision du vol
                var evenement = new Evenement
                {
                    DateRevisée = (DateTime)vol.DateRevue,
                    VolId = id,
                    Statut = vol.Etat // Utilisez l'état du vol comme statut de l'événement
                };

                // Ajouter l'événement au contexte
                _context.Evenement.Add(evenement);
            }

            _context.Entry(existingVol).CurrentValues.SetValues(vol);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("PutVol", id, vol);
        }





        // POST: api/VolsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vol>> PostVol(Vol vol)
        {
            _context.Vol.Add(vol);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVol", new { id = vol.VolId }, vol);
        }

        // DELETE: api/VolsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVol(int id)
        {
            var vol = await _context.Vol.FindAsync(id);
            if (vol == null)
            {
                return NotFound();
            }

            _context.Vol.Remove(vol);
            await _context.SaveChangesAsync();

            return CreatedAtAction("DeleteVol", id, vol);
        }

        private bool VolExists(int id)
        {
            return _context.Vol.Any(e => e.VolId == id);
        }
    }
}
