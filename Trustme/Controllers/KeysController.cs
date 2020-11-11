using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trustme.Data;
using Trustme.Models;
using AppContext = Trustme.Data.AppContext;

namespace Trustme.Controllers
{
    public class KeysController : Controller
    {
        private readonly AppContext _context;

        public KeysController(AppContext context)
        {
            _context = context;
        }

        // GET: Keys
        public async Task<IActionResult> Index()
        {
            var appContext = _context.Key.Include(k => k.User);
            return View(await appContext.ToListAsync());
        }

        // GET: Keys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var key = await _context.Key
                .Include(k => k.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (key == null)
            {
                return NotFound();
            }

            return View(key);
        }

        // GET: Keys/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");
            return View();
        }

        // POST: Keys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,PublicKey")] Key key)
        {
            if (ModelState.IsValid)
            {
                _context.Add(key);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", key.UserId);
            return View(key);
        }

        // GET: Keys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var key = await _context.Key.FindAsync(id);
            if (key == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", key.UserId);
            return View(key);
        }

        // POST: Keys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,PublicKey")] Key key)
        {
            if (id != key.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(key);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeyExists(key.UserId))
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
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", key.UserId);
            return View(key);
        }

        // GET: Keys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var key = await _context.Key
                .Include(k => k.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (key == null)
            {
                return NotFound();
            }

            return View(key);
        }

        // POST: Keys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var key = await _context.Key.FindAsync(id);
            _context.Key.Remove(key);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeyExists(int id)
        {
            return _context.Key.Any(e => e.UserId == id);
        }
    }
}
