using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Interfaces;

namespace personapi_dotnet.Controllers
{
    public class ProfesionsController : Controller
    {
        private readonly IProfesionRepository _repo;

        public ProfesionsController(IProfesionRepository repo)
        {
            _repo = repo;
        }

        // GET: Profesions
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsync());
        }

        // GET: Profesions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _repo.GetByIdAsync(id.Value);
            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        // GET: Profesions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Des")] Profesion profesion)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddAsync(profesion);
                return RedirectToAction(nameof(Index));
            }
            return View(profesion);
        }

        // GET: Profesions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _repo.GetByIdAsync(id.Value);
            if (profesion == null)
            {
                return NotFound();
            }
            return View(profesion);
        }

        // POST: Profesions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Des")] Profesion profesion)
        {
            if (id != profesion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.UpdateAsync(profesion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _repo.ExistsAsync(profesion.Id))
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
            return View(profesion);
        }

        // GET: Profesions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _repo.GetByIdAsync(id.Value);
            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        // POST: Profesions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _repo.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                // Profession has related Estudios — show error on the Delete view
                var profesion = await _repo.GetByIdAsync(id);
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(profesion);
            }
        }

        private bool ProfesionExists(int id)
        {
            return _repo.ExistsAsync(id).GetAwaiter().GetResult();
        }
    }
}
