using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tp3.Data;
using tp3.Models;

namespace tp3.Controllers
{
    public class DisciplinaController : Controller
    {
        private readonly tp3Context _context;

        public DisciplinaController(tp3Context context)
        {
            _context = context;
        }

        // GET: Disciplina
        public async Task<IActionResult> Index()
        {
            var tp3Context = _context.Disciplina.Include(d => d.Professor);
            return View(await tp3Context.ToListAsync());
        }

        // GET: Disciplina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Disciplina == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplina
                .Include(d => d.Professor)
                .FirstOrDefaultAsync(m => m.DisciplinaId == id);
            if (disciplina == null)
            {
                return NotFound();
            }

            return View(disciplina);
        }

        // GET: Disciplina/Create
        public IActionResult Create()
        {
            ViewData["ProfessorId"] = new SelectList(_context.Professor, "ProfessorId", "ProfessorId");
            ViewData["ProfessorNome"] = new SelectList(_context.Professor, "ProfessorId", "Nome");
            return View();
        }

        // POST: Disciplina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisciplinaId,Nome,Horas,ProfessorId")] Disciplina disciplina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfessorId"] = new SelectList(_context.Professor, "ProfessorId", "ProfessorId", disciplina.ProfessorId);
            return View(disciplina);
        }

        // GET: Disciplina/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Disciplina == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplina.FindAsync(id);
            if (disciplina == null)
            {
                return NotFound();
            }
            ViewData["ProfessorId"] = new SelectList(_context.Professor, "ProfessorId", "ProfessorId", disciplina.ProfessorId);
            ViewData["ProfessorNome"] = new SelectList(_context.Professor, "ProfessorId", "Nome", disciplina.ProfessorId);
            return View(disciplina);
        }

        // POST: Disciplina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisciplinaId,Nome,Horas,ProfessorId")] Disciplina disciplina)
        {
            if (id != disciplina.DisciplinaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplinaExists(disciplina.DisciplinaId))
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
            ViewData["ProfessorId"] = new SelectList(_context.Professor, "ProfessorId", "ProfessorId", disciplina.ProfessorId);
            return View(disciplina);
        }

        // GET: Disciplina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Disciplina == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplina
                .Include(d => d.Professor)
                .FirstOrDefaultAsync(m => m.DisciplinaId == id);
            if (disciplina == null)
            {
                return NotFound();
            }

            return View(disciplina);
        }

        // POST: Disciplina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Disciplina == null)
            {
                return Problem("Entity set 'tp3Context.Disciplina'  is null.");
            }
            var disciplina = await _context.Disciplina.FindAsync(id);
            if (disciplina != null)
            {
                _context.Disciplina.Remove(disciplina);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplinaExists(int id)
        {
          return (_context.Disciplina?.Any(e => e.DisciplinaId == id)).GetValueOrDefault();
        }
    }
}
