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
    public class ToDoItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ToDoItemsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager= userManager;
        }

        // GET: ToDoItems
        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(User)!;
            List<ToDoItems> toDoItems= new List<ToDoItems>();
            toDoItems = await _context.ToDoItems.Where(c=>c.AppUserId == userId).Include(c=>c.AppUser).ToListAsync();

            return View(toDoItems);
        }

        // GET: ToDoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToDoItems == null)
            {
                return NotFound();
            }

            var toDoItems = await _context.ToDoItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoItems == null)
            {
                return NotFound();
            }

            return View(toDoItems);
        }

        // GET: ToDoItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AppUserId,DateCreated,DueDate,IsCompleted")] ToDoItems toDoItems)
        {
            ModelState.Remove("AppUserId");

            if (ModelState.IsValid)
            {
                toDoItems.AppUserId = _userManager.GetUserId(User);

                toDoItems.DateCreated = DateTime.UtcNow;
                toDoItems.DueDate = DateTime.SpecifyKind(toDoItems.DueDate, DateTimeKind.Utc);


                _context.Add(toDoItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoItems);
        }

        // GET: ToDoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ToDoItems == null)
            {
                return NotFound();
            }

            var toDoItems = await _context.ToDoItems.FindAsync(id);
            if (toDoItems == null)
            {
                return NotFound();
            }
            string? userId = _userManager.GetUserId(User);

            return View(toDoItems);
        }

        // POST: ToDoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AppUserId,DateCreated,DueDate,IsCompleted")] ToDoItems toDoItems)
        {
            ModelState.Remove("AppUserId");
            
            if (id != toDoItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    toDoItems.AppUserId = _userManager.GetUserId(User);

                    toDoItems.DateCreated = DateTime.SpecifyKind(toDoItems.DateCreated, DateTimeKind.Utc);
                    toDoItems.DueDate = DateTime.SpecifyKind(toDoItems.DueDate, DateTimeKind.Utc);
                    

                    _context.Update(toDoItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoItemsExists(toDoItems.Id))
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
            return View(toDoItems);
        }

        // GET: ToDoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ToDoItems == null)
            {
                return NotFound();
            }

            var toDoItems = await _context.ToDoItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoItems == null)
            {
                return NotFound();
            }

            return View(toDoItems);
        }

        // POST: ToDoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ToDoItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ToDoItems'  is null.");
            }
            var toDoItems = await _context.ToDoItems.FindAsync(id);
            if (toDoItems != null)
            {
                _context.ToDoItems.Remove(toDoItems);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoItemsExists(int id)
        {
            return (_context.ToDoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
