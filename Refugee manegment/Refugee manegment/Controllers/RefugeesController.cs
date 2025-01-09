using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refugee_manegment.Models;

namespace Refugee_manegment.Controllers
{
    public class RefugeesController : Controller
    {
        private readonly WebDbContext _context;

        public RefugeesController(WebDbContext context)
        {
            _context = context;
        }

        // GET: Refugees
        public async Task<IActionResult> Index()
        {
            var refugees = await _context.refugee.ToListAsync();
            return View(refugees);
        }

        // GET: Refugees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Invalid refugee ID.");
            }

            var refugee = await GetRefugeeById(id.Value);
            if (refugee == null)
            {
                return NotFound("Refugee not found.");
            }

            return View(refugee);
        }

        // GET: Refugees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Refugees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Nationality,DateOfBirth,Status")] Refugee refugee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(refugee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                }
            }

            return View(refugee);
        }

        // GET: Refugees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Invalid refugee ID.");
            }

            var refugee = await GetRefugeeById(id.Value);
            if (refugee == null)
            {
                return NotFound("Refugee not found.");
            }

            return View(refugee);
        }

        // POST: Refugees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Nationality,DateOfBirth,Status")] Refugee refugee)
        {
            if (id != refugee.Id)
            {
                return BadRequest("Refugee ID mismatch.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refugee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await RefugeeExists(refugee.Id))
                    {
                        return NotFound("Refugee not found.");
                    }

                    throw;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                }
            }

            return View(refugee);
        }

        // GET: Refugees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Invalid refugee ID.");
            }

            var refugee = await GetRefugeeById(id.Value);
            if (refugee == null)
            {
                return NotFound("Refugee not found.");
            }

            return View(refugee);
        }

        // POST: Refugees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var refugee = await GetRefugeeById(id);
                if (refugee == null)
                {
                    return NotFound("Refugee not found.");
                }

                _context.refugee.Remove(refugee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        // Helper function to get refugee by ID
        private async Task<Refugee> GetRefugeeById(int id)
        {
            return await _context.refugee.FirstOrDefaultAsync(r => r.Id == id);
        }

        // Check if refugee exists
        private async Task<bool> RefugeeExists(int id)
        {
            return await _context.refugee.AnyAsync(e => e.Id == id);
        }
    }
}