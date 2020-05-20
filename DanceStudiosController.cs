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
    public class DanceStudiosController : Controller
    {
        private readonly DanceContext _context;

        public DanceStudiosController(DanceContext context)
        {
            _context = context;
        }

        // GET: DanceStudios
       public async Task<IActionResult> Index()
        {
           
            var danceContext = _context.DanceStudio.Include(d => d.City);
            return View(await danceContext.ToListAsync()); 
        }
        


        // GET: DanceStudios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danceStudio = await _context.DanceStudio
                .Include(d => d.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (danceStudio == null)
            {
                return NotFound();
            }

            
            return RedirectToAction("Vieew", "Dancers", new { id = danceStudio.Id, name = danceStudio.Name });
        }

        // GET: DanceStudios/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name");
            return View();
        }

        // POST: DanceStudios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CityId,Adress")] DanceStudio danceStudio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danceStudio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", danceStudio.CityId);
            return View(danceStudio);
        }

        // GET: DanceStudios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danceStudio = await _context.DanceStudio.FindAsync(id);
            if (danceStudio == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", danceStudio.CityId);
            return View(danceStudio);
        }

        // POST: DanceStudios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CityId,Adress")] DanceStudio danceStudio)
        {
            if (id != danceStudio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danceStudio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanceStudioExists(danceStudio.Id))
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
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", danceStudio.CityId);
            return View(danceStudio);
        }

        // GET: DanceStudios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danceStudio = await _context.DanceStudio
                .Include(d => d.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (danceStudio == null)
            {
                return NotFound();
            }

            return View(danceStudio);
        }

        // POST: DanceStudios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            { var danceStudio = await _context.DanceStudio.FindAsync(id);
                _context.DanceStudio.Remove(danceStudio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("DelErr", "Home");
            }
            }

        private bool DanceStudioExists(int id)
        {
            return _context.DanceStudio.Any(e => e.Id == id);
        }
    }
}
