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
    public class DancersController : Controller
    {
        private readonly DanceContext _context;

        public DancersController(DanceContext context)
        {
            _context = context;
        }

        // GET: Dancers
        public async Task<IActionResult> Index()
        {
            var danceContext = _context.Dancer.Include(d => d.DanceStudio);
            return View(await danceContext.ToListAsync());

           // if (id == null) return RedirectToAction("DanceStudio", "Index");
           // ViewBag.DanceStudioId = id;
            //ViewBag.DanceStudioName = name;
            //var danceContext = _context.Dancer.Where(n => n.DanceStudioId == id).Include(n => n.DanceStudio);
          //  return View(await danceContext.ToListAsync());
        }
        public async Task<IActionResult> Vieew(int? id , string? name)
        {
             if (id == null) return RedirectToAction("DanceStudio", "Index");
             ViewBag.DanceStudioId = id;
            ViewBag.DanceStudioName = name;
            var danceContext = _context.Dancer.Where(n => n.DanceStudioId == id).Include(n => n.DanceStudio);
             return View(await danceContext.ToListAsync());


        }

        // GET: Dancers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dancer = await _context.Dancer
                .Include(d => d.DanceStudio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dancer == null)
            {
                return NotFound();
            }

            return View(dancer);
        }

        // GET: Dancers/Create
        public IActionResult Create()
        {
            ViewData["DanceStudioId"] = new SelectList(_context.DanceStudio, "Id", "Name");
            return View();
        }

        // POST: Dancers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Birthday,Information,DanceStudioId")] Dancer dancer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dancer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanceStudioId"] = new SelectList(_context.DanceStudio, "Id", "Name", dancer.DanceStudioId);
            return View(dancer);
        }

        // GET: Dancers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dancer = await _context.Dancer.FindAsync(id);
            if (dancer == null)
            {
                return NotFound();
            }
            ViewData["DanceStudioId"] = new SelectList(_context.DanceStudio, "Id", "Name", dancer.DanceStudioId);
            return View(dancer);
        }

        // POST: Dancers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Birthday,Information,DanceStudioId")] Dancer dancer)
        {
            if (id != dancer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dancer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DancerExists(dancer.Id))
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
            ViewData["DanceStudioId"] = new SelectList(_context.DanceStudio, "Id", "Name", dancer.DanceStudioId);
            return View(dancer);
        }

        // GET: Dancers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dancer = await _context.Dancer
                .Include(d => d.DanceStudio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dancer == null)
            {
                return NotFound();
            }

            return View(dancer);
        }

        // POST: Dancers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var dancer = await _context.Dancer.FindAsync(id);
                _context.Dancer.Remove(dancer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("DelErr", "Home");
            }
            }

        private bool DancerExists(int id)
        {
            return _context.Dancer.Any(e => e.Id == id);
        }
    }
}
