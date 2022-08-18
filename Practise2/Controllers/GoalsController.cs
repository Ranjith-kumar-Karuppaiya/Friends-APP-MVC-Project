using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practise2.Data;
using Practise2.Models;

namespace Practise2.Controllers
{
    public class GoalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GoalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Goals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Goals.ToListAsync());
        }

        // GET: Goals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goals = await _context.Goals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goals == null)
            {
                return NotFound();
            }

            return View(goals);
        }

        // GET: Goals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Goals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,Industry")] Goals goals)
        {
            if (ModelState.IsValid)
            {
                _context.Add(goals);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(goals);
        }

        // GET: Goals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goals = await _context.Goals.FindAsync(id);
            if (goals == null)
            {
                return NotFound();
            }
            return View(goals);
        }

        // POST: Goals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,Industry")] Goals goals)
        {
            if (id != goals.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goals);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoalsExists(goals.Id))
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
            return View(goals);
        }

        // GET: Goals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goals = await _context.Goals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goals == null)
            {
                return NotFound();
            }

            return View(goals);
        }

        // POST: Goals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var goals = await _context.Goals.FindAsync(id);
            _context.Goals.Remove(goals);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoalsExists(int id)
        {
            return _context.Goals.Any(e => e.Id == id);
        }
    }
}
