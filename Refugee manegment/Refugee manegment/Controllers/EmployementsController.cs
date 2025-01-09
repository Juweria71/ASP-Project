using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Refugee_manegment.Models;

namespace Refugee_manegment.Controllers
{
    public class EmployementsController : Controller
    {
        private readonly WebDbContext _context;

        public EmployementsController(WebDbContext context)
        {
            _context = context;
        }

        // GET: Employements
        public async Task<IActionResult> Index()
        {
            var webDbContext = _context.Employements.Include(e => e.Refugee);
            return View(await webDbContext.ToListAsync());
        }

        // GET: Employements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employement = await _context.Employements
                .Include(e => e.Refugee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employement == null)
            {
                return NotFound();
            }

            return View(employement);
        }

        // GET: Employements/Create
        public IActionResult Create()
        {
            ViewData["RefugeeId"] = new SelectList(_context.refugee, "Id", "Id");
            return View();
        }

        // POST: Employements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RefugeeId,JobTitle,CompanyName,StartDate,EndDate")] Employement employement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RefugeeId"] = new SelectList(_context.refugee, "Id", "Id", employement.RefugeeId);
            return View(employement);
        }

        // GET: Employements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employement = await _context.Employements.FindAsync(id);
            if (employement == null)
            {
                return NotFound();
            }
            ViewData["RefugeeId"] = new SelectList(_context.refugee, "Id", "Id", employement.RefugeeId);
            return View(employement);
        }

        // POST: Employements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RefugeeId,JobTitle,CompanyName,StartDate,EndDate")] Employement employement)
        {
            if (id != employement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployementExists(employement.Id))
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
            ViewData["RefugeeId"] = new SelectList(_context.refugee, "Id", "Id", employement.RefugeeId);
            return View(employement);
        }

        // GET: Employements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employement = await _context.Employements
                .Include(e => e.Refugee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employement == null)
            {
                return NotFound();
            }

            return View(employement);
        }

        // POST: Employements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employement = await _context.Employements.FindAsync(id);
            if (employement != null)
            {
                _context.Employements.Remove(employement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployementExists(int id)
        {
            return _context.Employements.Any(e => e.Id == id);
        }
    }
}
