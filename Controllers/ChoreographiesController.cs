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
    public class ChoreographiesController : Controller
    {
        private readonly DanceContext _context;

        public ChoreographiesController(DanceContext context)
        {
            _context = context;
        }

        // GET: Choreographies
        public async Task<IActionResult> Index()
        {
            var danceContext = _context.Choreography.Include(c => c.Dancer).Include(c => c.Style);
            return View(await danceContext.ToListAsync());
            //if (id == null) return RedirectToAction("Dstyle", "Index");
           // ViewBag.StyleId = id;
           // ViewBag.StyleName = name;
            //var danceContext = _context.Choreography.Where(n => n.StyleId == id).Include(n => n.Style).Include(c => c.Dancer);
            //return View(await danceContext.ToListAsync());
        }
        public async Task<IActionResult> Vieew(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Dstyle", "Index");
             ViewBag.StyleId = id;
             ViewBag.StyleName = name;
            var danceContext = _context.Choreography.Where(n => n.StyleId == id).Include(n => n.Style).Include(c => c.Dancer);
            return View(await danceContext.ToListAsync());

        }

        // GET: Choreographies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choreography = await _context.Choreography
                .Include(c => c.Dancer)
                .Include(c => c.Style)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (choreography == null)
            {
                return NotFound();
            }

            return View(choreography);
        }

        // GET: Choreographies/Create
        public IActionResult Create()
        {
            ViewData["DancerId"] = new SelectList(_context.Dancer, "Id", "Name");
            ViewData["StyleId"] = new SelectList(_context.Dstyle, "Id", "Name");
            return View();
           
        }

        // POST: Choreographies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Music,DancerId,Duration,StyleId")] Choreography choreography)
        {
           
            if (ModelState.IsValid)
            {
                _context.Add(choreography);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
               
            }
            ViewData["StyleId"] = new SelectList(_context.Dstyle, "Id", "Name", choreography.StyleId);
            ViewData["DancerId"] = new SelectList(_context.Dancer, "Id", "Name", choreography.DancerId);
             
              return View(choreography);
            
        }

        // GET: Choreographies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choreography = await _context.Choreography.FindAsync(id);
            if (choreography == null)
            {
                return NotFound();
            }
            ViewData["DancerId"] = new SelectList(_context.Dancer, "Id", "Name", choreography.DancerId);
            ViewData["StyleId"] = new SelectList(_context.Dstyle, "Id", "Name", choreography.StyleId);
            return View(choreography);
        }

        // POST: Choreographies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Music,DancerId,Duration,StyleId")] Choreography choreography)
        {
            if (id != choreography.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(choreography);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChoreographyExists(choreography.Id))
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
            ViewData["DancerId"] = new SelectList(_context.Dancer, "Id", "Name", choreography.DancerId);
            ViewData["StyleId"] = new SelectList(_context.Dstyle, "Id", "Name", choreography.StyleId);
            return View(choreography);
        }

        // GET: Choreographies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choreography = await _context.Choreography
                .Include(c => c.Dancer)
                .Include(c => c.Style)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (choreography == null)
            {
                return NotFound();
            }

            return View(choreography);
        }

        // POST: Choreographies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var choreography = await _context.Choreography.FindAsync(id);
                _context.Choreography.Remove(choreography);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                return RedirectToAction("DelErr", "Home");
            }
        }
       
        private bool ChoreographyExists(int id)
        {
            return _context.Choreography.Any(e => e.Id == id);
        }
    }
}
