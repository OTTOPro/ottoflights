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
    public class UtilisateursController : Controller
    {
        private readonly VolContext _context;

        public UtilisateursController(VolContext context)
        {
            _context = context;
        }

        // GET: Utilisateurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utilisateur.ToListAsync());
        }

        // GET: Utilisateurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateur
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // GET: Utilisateurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utilisateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,MotDePasse,Role")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utilisateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utilisateur);
        }

        public IActionResult Inscription()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inscription([Bind("Id,Email,MotDePasse,Role")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utilisateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Connexion));
            }
            return View(utilisateur);
        }

        public IActionResult Connexion()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Connexion([Bind("Email,MotDePasse")] Utilisateur utilisateur)
        {
            // Recherche de l'utilisateur par email et mot de passe (sans hachage)
            var utilisateurEnBase = await _context.Utilisateur.FirstOrDefaultAsync(u => u.Email == utilisateur.Email && u.MotDePasse == utilisateur.MotDePasse);

            // Vérification si l'utilisateur existe
            if (utilisateurEnBase != null)
            {
                HttpContext.Session.SetString("UserId", utilisateurEnBase.Id.ToString());
                HttpContext.Session.SetString("UserRole", utilisateurEnBase.Role);

                if (utilisateurEnBase.Role == "Admin")
                {
                    return RedirectToAction("Index", "Vols");
                }
                else if (utilisateurEnBase.Role == "Utilisateur")
                {
                    return RedirectToAction("IndexUtilisateur", "Vols");
                }
                else
                {
                    return RedirectToAction("IndexUtilisateur", "Vols");
                }
            }
            else
            {
                // Échec de la connexion
                ModelState.AddModelError(string.Empty, "Email ou mot de passe incorrect.");
                return View();
            }
        }

        public IActionResult Deconnexion()
        {
            // Effacer les informations de session lors de la déconnexion
            HttpContext.Session.Clear();

            // Rediriger vers une page de déconnexion ou une autre page
            return RedirectToAction("Index", "Home"); // Modifiez cela selon vos besoins
        }




        // GET: Utilisateurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateur.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }
            return View(utilisateur);
        }

        // POST: Utilisateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,MotDePasse,Role")] Utilisateur utilisateur)
        {
            if (id != utilisateur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilisateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilisateurExists(utilisateur.Id))
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
            return View(utilisateur);
        }

        // GET: Utilisateurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateur
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utilisateur = await _context.Utilisateur.FindAsync(id);
            if (utilisateur != null)
            {
                _context.Utilisateur.Remove(utilisateur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilisateurExists(int id)
        {
            return _context.Utilisateur.Any(e => e.Id == id);
        }
    }
}
