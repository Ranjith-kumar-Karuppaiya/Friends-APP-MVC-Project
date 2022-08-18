using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practise2.Data;
using Practise2.Models;

namespace Practise2.Controllers
{
    public class friendsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public friendsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: friends
        public async Task<IActionResult> Index()
        {
            return View(await _context.friends.ToListAsync());
        }
        // GET: friends/SearchResult
        public async Task<IActionResult> SearchResult()
        {
            return View();
        }
        // GET: friends/SearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await _context.friends.Where( j => j.Name.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: friends/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friends = await _context.friends
                .FirstOrDefaultAsync(m => m.Id == id);
            if (friends == null)
            {
                return NotFound();
            }

            return View(friends);
        }

        // GET: friends/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: friends/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,Industry")] friends friends)
        {
            if (ModelState.IsValid)
            {
                _context.Add(friends);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(friends);
        }

        // GET: friends/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friends = await _context.friends.FindAsync(id);
            if (friends == null)
            {
                return NotFound();
            }
            return View(friends);
        }

        // POST: friends/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,Industry")] friends friends)
        {
            if (id != friends.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(friends);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!friendsExists(friends.Id))
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
            return View(friends);
        }

        // GET: friends/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friends = await _context.friends
                .FirstOrDefaultAsync(m => m.Id == id);
            if (friends == null)
            {
                return NotFound();
            }

            return View(friends);
        }

        // POST: friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var friends = await _context.friends.FindAsync(id);
            _context.friends.Remove(friends);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool friendsExists(int id)
        {
            return _context.friends.Any(e => e.Id == id);
        }
    }
}
