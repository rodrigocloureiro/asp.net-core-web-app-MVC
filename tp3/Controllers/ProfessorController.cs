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
    public class ProfessorController : Controller
    {
        private readonly tp3Context _context;

        public ProfessorController(tp3Context context)
        {
            _context = context;
        }

        // GET: Professor
        public async Task<IActionResult> Index()
        {
              return _context.Professor != null ? 
                          View(await _context.Professor.ToListAsync()) :
                          Problem("Entity set 'tp3Context.Professor'  is null.");
        }

        // GET: Professor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Professor == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .FirstOrDefaultAsync(m => m.ProfessorId == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfessorId,Nome,DataContratacao,Email")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }

        // GET: Professor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Professor == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        // POST: Professor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfessorId,Nome,DataContratacao,Email")] Professor professor)
        {
            if (id != professor.ProfessorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(professor.ProfessorId))
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
            return View(professor);
        }

        // GET: Professor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Professor == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .FirstOrDefaultAsync(m => m.ProfessorId == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Professor == null)
            {
                return Problem("Entity set 'tp3Context.Professor'  is null.");
            }
            var professor = await _context.Professor.FindAsync(id);
            if (professor != null)
            {
                _context.Professor.Remove(professor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
          return (_context.Professor?.Any(e => e.ProfessorId == id)).GetValueOrDefault();
        }
    }
}
