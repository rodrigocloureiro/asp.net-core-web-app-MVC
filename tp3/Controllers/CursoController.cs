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
    public class CursoController : Controller
    {
        private readonly tp3Context _context;

        public CursoController(tp3Context context)
        {
            _context = context;
        }

        // GET: Curso
        public async Task<IActionResult> Index()
        {
            var tp3Context = _context.Curso.Include(c => c.Aluno).Include(c => c.Disciplina);
            return View(await tp3Context.ToListAsync());
        }

        // GET: Curso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Curso == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso
                .Include(c => c.Aluno)
                .Include(c => c.Disciplina)
                .FirstOrDefaultAsync(m => m.CursoId == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // GET: Curso/Create
        public IActionResult Create()
        {
            ViewData["AlunoId"] = new SelectList(_context.Aluno, "AlunoId", "Nome");
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplina, "DisciplinaId", "Nome");
            return View();
        }

        // POST: Curso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CursoId,Nome,DataInicio,DataFim,AlunoId,DisciplinaId")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(curso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunoId"] = new SelectList(_context.Aluno, "AlunoId", "AlunoId", curso.AlunoId);
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplina, "DisciplinaId", "DisciplinaId", curso.DisciplinaId);
            return View(curso);
        }

        // GET: Curso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Curso == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            ViewData["AlunoId"] = new SelectList(_context.Aluno, "AlunoId", "Nome", curso.AlunoId);
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplina, "DisciplinaId", "Nome", curso.DisciplinaId);
            return View(curso);
        }

        // POST: Curso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CursoId,Nome,DataInicio,DataFim,AlunoId,DisciplinaId")] Curso curso)
        {
            if (id != curso.CursoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(curso.CursoId))
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
            ViewData["AlunoId"] = new SelectList(_context.Aluno, "AlunoId", "AlunoId", curso.AlunoId);
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplina, "DisciplinaId", "DisciplinaId", curso.DisciplinaId);
            return View(curso);
        }

        // GET: Curso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Curso == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso
                .Include(c => c.Aluno)
                .Include(c => c.Disciplina)
                .FirstOrDefaultAsync(m => m.CursoId == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Curso == null)
            {
                return Problem("Entity set 'tp3Context.Curso'  is null.");
            }
            var curso = await _context.Curso.FindAsync(id);
            if (curso != null)
            {
                _context.Curso.Remove(curso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(int id)
        {
          return (_context.Curso?.Any(e => e.CursoId == id)).GetValueOrDefault();
        }
    }
}
