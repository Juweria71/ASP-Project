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
    public class SponsorshipsController : Controller
    {
        private readonly WebDbContext _context;

        public SponsorshipsController(WebDbContext context)
        {
            _context = context;
        }

        // GET: Sponsorships
        public async Task<IActionResult> Index()
        {
            var webDbContext = _context.Sponsorships.Include(s => s.Refugee);
            return View(await webDbContext.ToListAsync());
        }

        // GET: Sponsorships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponsorship = await _context.Sponsorships
                .Include(s => s.Refugee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sponsorship == null)
            {
                return NotFound();
            }

            return View(sponsorship);
        }

        // GET: Sponsorships/Create
        public IActionResult Create()
        {
            ViewData["RefugeeId"] = new SelectList(_context.refugee, "Id", "Id");
            return View();
        }

        // POST: Sponsorships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RefugeeId,SponsorName,SponsorContact,SponsorshipStartDate,SponsorshipEndDate")] Sponsorship sponsorship)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sponsorship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RefugeeId"] = new SelectList(_context.refugee, "Id", "Id", sponsorship.RefugeeId);
            return View(sponsorship);
        }

        // GET: Sponsorships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponsorship = await _context.Sponsorships.FindAsync(id);
            if (sponsorship == null)
            {
                return NotFound();
            }
            ViewData["RefugeeId"] = new SelectList(_context.refugee, "Id", "Id", sponsorship.RefugeeId);
            return View(sponsorship);
        }

        // POST: Sponsorships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RefugeeId,SponsorName,SponsorContact,SponsorshipStartDate,SponsorshipEndDate")] Sponsorship sponsorship)
        {
            if (id != sponsorship.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sponsorship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SponsorshipExists(sponsorship.Id))
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
            ViewData["RefugeeId"] = new SelectList(_context.refugee, "Id", "Id", sponsorship.RefugeeId);
            return View(sponsorship);
        }

        // GET: Sponsorships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponsorship = await _context.Sponsorships
                .Include(s => s.Refugee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sponsorship == null)
            {
                return NotFound();
            }

            return View(sponsorship);
        }

        // POST: Sponsorships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sponsorship = await _context.Sponsorships.FindAsync(id);
            if (sponsorship != null)
            {
                _context.Sponsorships.Remove(sponsorship);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SponsorshipExists(int id)
        {
            return _context.Sponsorships.Any(e => e.Id == id);
        }
    }
}
