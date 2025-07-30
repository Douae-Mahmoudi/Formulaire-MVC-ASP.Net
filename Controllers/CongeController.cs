using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; 
using WebApplication3.Data; 
using WebApplication3.Models; 

namespace WebApplication3.Controllers
{
    public class CongeController : Controller
    {
        //ApplicationDbContext : Classe EF Core qui repr�sente la connexion � la base de donn�es.


        private readonly ApplicationDbContext _context;

        public CongeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action Index (pour afficher la liste des cong�s 
        public async Task<IActionResult> Index()
        {
            return View(await _context.Conges.ToListAsync());
        }

        // Action Demande (pour afficher le formulaire de demande - vous l'avez d�j�)
        public IActionResult Demande()
        {
            return View();
        }

        //  Soumettre une demande (Demande - POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Demande([Bind("Type,StartDate,EndDate,Reason")] Conge conge)
        {
            if (ModelState.IsValid)
            {
                conge.Status = "En attente"; // Ou votre statut par d�faut
                _context.Add(conge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Confirmation), conge); // Redirection vers la page de confirmation
            }
            return View(conge);
        }

        // Action Confirmation (pour afficher la page de confirmation - vous l'avez d�j�)
        public IActionResult Confirmation(Conge conge)
        {
            return View(conge);
        }


        //  CODE  POUR DETAILS, EDIT, DELETE
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Retourne une erreur 404 si l'ID est manquant dans l'URL
            }

            var conge = await _context.Conges
                .FirstOrDefaultAsync(m => m.Id == id); // R�cup�re le cong� par son ID
            if (conge == null)
            {
                return NotFound(); // Retourne une erreur 404 si le cong� n'est pas trouv�
            }

            return View(conge); // Passe l'objet Conge � la vue Details.cshtml
        }

        // GET: Conge/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Retourne une erreur 404 si l'ID est manquant
            }

            var conge = await _context.Conges.FindAsync(id); // Trouve le cong� par ID
            if (conge == null)
            {
                return NotFound(); // Retourne une erreur 404 si le cong� n'est pas trouv�
            }
            return View(conge); // Passe l'objet Conge � la vue Edit.cshtml pour pr�-remplir le formulaire
        }

        // POST: Conge/Edit/5
        // Pour se prot�ger des attaques de sur-publication, activez les propri�t�s sp�cifiques auxquelles vous voulez vous lier.
        // Pour plus de d�tails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,StartDate,EndDate,Reason,Status")] Conge conge)
        {
            if (id != conge.Id)
            {
                return NotFound(); // Retourne une erreur 404 si l'ID dans l'URL ne correspond pas � l'objet
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conge); // Met � jour l'objet Conge dans le contexte
                    await _context.SaveChangesAsync(); // Enregistre les modifications dans la base de donn�es
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CongeExists(conge.Id)) // V�rifie si le cong� existe toujours
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; // Propage l'exception si ce n'est pas un probl�me de concurrence
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirige vers la liste apr�s la modification
            }
            return View(conge); // Si le mod�le n'est pas valide, r�affiche le formulaire avec les erreurs
        }

        // GET: Conge/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Retourne une erreur 404 si l'ID est manquant
            }

            var conge = await _context.Conges
                .FirstOrDefaultAsync(m => m.Id == id); // R�cup�re le cong� par ID
            if (conge == null)
            {
                return NotFound(); // Retourne une erreur 404 si le cong� n'est pas trouv�
            }

            return View(conge); // Passe l'objet Conge � la vue Delete.cshtml pour confirmation
        }

        // POST: Conge/Delete/5
        [HttpPost, ActionName("Delete")] // Cette action r�pondra aux requ�tes POST sur /Conge/Delete
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conge = await _context.Conges.FindAsync(id); // Trouve le cong� � supprimer
            if (conge != null)
            {
                _context.Conges.Remove(conge); // Supprime le cong� du contexte
            }

            await _context.SaveChangesAsync(); // Enregistre la suppression dans la base de donn�es
            return RedirectToAction(nameof(Index)); // Redirige vers la liste apr�s la suppression
        }

        private bool CongeExists(int id)
        {
            return _context.Conges.Any(e => e.Id == id); // M�thode utilitaire pour v�rifier l'existence
        }
    }
}