using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba1_ISTTP;

namespace Laba1_ISTTP.Controllers
{
    public class DstylesController : Controller
    {
        private readonly DanceContext _context;

        public DstylesController(DanceContext context)
        {
            _context = context;
        }

        // GET: Dstyles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dstyle.ToListAsync());
        }

        // GET: Dstyles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dstyle = await _context.Dstyle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dstyle == null)
            {
                return NotFound();
            }

             

            return RedirectToAction("Vieew", "Choreographies", new { id = dstyle.Id, name = dstyle.Name });
        }

        // GET: Dstyles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dstyles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Information")] Dstyle dstyle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dstyle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dstyle);
        }

        // GET: Dstyles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dstyle = await _context.Dstyle.FindAsync(id);
            if (dstyle == null)
            {
                return NotFound();
            }
            return View(dstyle);
        }

        // POST: Dstyles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Information")] Dstyle dstyle)
        {
            if (id != dstyle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dstyle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DstyleExists(dstyle.Id))
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
            return View(dstyle);
        }

        // GET: Dstyles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dstyle = await _context.Dstyle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dstyle == null)
            {
                return NotFound();
            }

            return View(dstyle);
        }

        // POST: Dstyles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var dstyle = await _context.Dstyle.FindAsync(id);
                _context.Dstyle.Remove(dstyle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("DelErr", "Home");
            }
        }

        private bool DstyleExists(int id)
        {
            return _context.Dstyle.Any(e => e.Id == id);
        }
    }
}
