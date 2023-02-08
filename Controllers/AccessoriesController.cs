using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoJA2.Data;
using ToDoJA2.Models;

namespace ToDoJA2.Controllers
{
    [Authorize]
    public class AccessoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AccessoriesController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Accessories
        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(User)!;
            List<Accessories> accessories= new List<Accessories>();
            accessories = await _context.Accessories.Where(c=>c.AppUserId== userId).Include(c=>c.AppUser).ToListAsync();


            return View(accessories);
                        
        }

        // GET: Accessories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accessories == null)
            {
                return NotFound();
            }

            var accessories = await _context.Accessories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessories == null)
            {
                return NotFound();
            }

            return View(accessories);
        }

        // GET: Accessories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accessories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AppUserId")] Accessories accessories)
        {
            ModelState.Remove("AppUserId");

            if (ModelState.IsValid)
            {
                accessories.AppUserId = _userManager.GetUserId(User);

                _context.Add(accessories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accessories);
        }

        // GET: Accessories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accessories == null)
            {
                return NotFound();
            }

            var accessories = await _context.Accessories.FindAsync(id);
            if (accessories == null)
            {
                return NotFound();
            }
            return View(accessories);
        }

        // POST: Accessories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AppUserId")] Accessories accessories)
        {
            if (id != accessories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessoriesExists(accessories.Id))
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
            return View(accessories);
        }

        // GET: Accessories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accessories == null)
            {
                return NotFound();
            }

            var accessories = await _context.Accessories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessories == null)
            {
                return NotFound();
            }

            return View(accessories);
        }

        // POST: Accessories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accessories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Accessories'  is null.");
            }
            var accessories = await _context.Accessories.FindAsync(id);
            if (accessories != null)
            {
                _context.Accessories.Remove(accessories);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessoriesExists(int id)
        {
            return (_context.Accessories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
