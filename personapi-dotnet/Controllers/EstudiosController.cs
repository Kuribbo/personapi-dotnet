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
    public class EstudiosController : Controller
    {
        private readonly IEstudioRepository _estRepo;
        private readonly PersonaDbContext _context; // used only for SelectLists; could be replaced by repositories

        public EstudiosController(IEstudioRepository estRepo, PersonaDbContext context)
        {
            _estRepo = estRepo;
            _context = context;
        }

        // GET: Estudios
        public async Task<IActionResult> Index()
        {
            return View(await _estRepo.GetAllAsync());
        }

        // GET: Estudios/Details/5
        public async Task<IActionResult> Details(int? id, int? ccPer)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudio = await _estRepo.GetByIdAsync(id.Value, ccPer);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // GET: Estudios/Create
        public IActionResult Create()
        {
            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc");
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id");
            return View();
        }

        // POST: Estudios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProf,CcPer,Fecha,Univ")] Estudio estudio)
        {
            if (ModelState.IsValid)
            {
                await _estRepo.AddAsync(estudio);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // GET: Estudios/Edit/5
        public async Task<IActionResult> Edit(int? id, int? ccPer)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudio = await _estRepo.GetByIdAsync(id.Value, ccPer);
            if (estudio == null)
            {
                return NotFound();
            }
            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // POST: Estudios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int? ccPer, [Bind("IdProf,CcPer,Fecha,Univ")] Estudio estudio)
        {
            if (id != estudio.IdProf)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _estRepo.UpdateAsync(estudio);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EstudioExistsAsync(estudio.IdProf, estudio.CcPer))
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
            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // GET: Estudios/Delete/5
        public async Task<IActionResult> Delete(int? id, int? ccPer)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudio = await _estRepo.GetByIdAsync(id.Value, ccPer);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // POST: Estudios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? ccPer)
        {
            await _estRepo.DeleteAsync(id, ccPer);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EstudioExistsAsync(int id, int? ccPer)
        {
            return await _estRepo.ExistsAsync(id, ccPer);
        }
    }
}
