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
    public class TelefonoesController : Controller
    {
        private readonly ITelefonoRepository _repo;
        private readonly IPersonaRepository _personaRepo;

        public TelefonoesController(ITelefonoRepository repo, IPersonaRepository personaRepo)
        {
            _repo = repo;
            _personaRepo = personaRepo;
        }

        // GET: Telefonoes
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsync());
        }

        // GET: Telefonoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _repo.GetByIdAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            // ensure owner navigation loaded
            if (telefono.DuenoNavigation == null && telefono.Dueno.HasValue)
            {
                telefono.DuenoNavigation = await _personaRepo.GetByIdAsync(telefono.Dueno.Value);
            }

            return View(telefono);
        }

        // GET: Telefonoes/Create
        public async Task<IActionResult> Create()
        {
            var personas = await _personaRepo.GetAllAsync();
            ViewData["Dueno"] = new SelectList(personas, "Cc", "Cc");
            return View();
        }

        // POST: Telefonoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Num,Oper,Dueno")] Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddAsync(telefono);
                return RedirectToAction(nameof(Index));
            }
            var personas = await _personaRepo.GetAllAsync();
            ViewData["Dueno"] = new SelectList(personas, "Cc", "Cc", telefono.Dueno);
            return View(telefono);
        }

        // GET: Telefonoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _repo.GetByIdAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }
            var personas = await _personaRepo.GetAllAsync();
            ViewData["Dueno"] = new SelectList(personas, "Cc", "Cc", telefono.Dueno);
            return View(telefono);
        }

        // POST: Telefonoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Num,Oper,Dueno")] Telefono telefono)
        {
            if (id != telefono.Num)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.UpdateAsync(telefono);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TelefonoExistsAsync(telefono.Num))
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
            var personas = await _personaRepo.GetAllAsync();
            ViewData["Dueno"] = new SelectList(personas, "Cc", "Cc", telefono.Dueno);
            return View(telefono);
        }

        // GET: Telefonoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _repo.GetByIdAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            if (telefono.DuenoNavigation == null && telefono.Dueno.HasValue)
            {
                telefono.DuenoNavigation = await _personaRepo.GetByIdAsync(telefono.Dueno.Value);
            }

            return View(telefono);
        }

        // POST: Telefonoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TelefonoExistsAsync(string id)
        {
            return await _repo.ExistsAsync(id);
        }
    }
}
