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
    public class NominationsController : Controller
    {
        private readonly DanceContext _context;

        public NominationsController(DanceContext context)
        {
            _context = context;
        }

        // GET: Nominations
        public async Task<IActionResult> Index()
        {
            
            
            var danceContext = _context.Nomination.Include(n => n.NomList).Include(n => n.Choreography).Include(n => n.Competition).Include(n => n.Dancer);
            return View(await danceContext.ToListAsync());
        }
        public async Task<IActionResult> Vieew(int? id , string? name)
        {

            if (id == null) return RedirectToAction("NominatinList", "Index");
             ViewBag.NomListId = id;
             ViewBag.NomListName = name;
            var danceContext = _context.Nomination.Where(n => n.NomListId == id).Include(n => n.NomList).Include(n => n.Choreography).Include(n => n.Competition).Include(n => n.Dancer);
            return View(await danceContext.ToListAsync());
           
        }

        // GET: Nominations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nomination = await _context.Nomination
                .Include(n => n.Choreography)
                .Include(n => n.Competition)
                .Include(n => n.Dancer)
                .Include(n => n.NomList)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nomination == null)
            {
                return NotFound();
            }

            return View(nomination);
        }

        // GET: Nominations/Create
        public IActionResult Create()
        {
            ViewData["ChoreographyId"] = new SelectList(_context.Choreography, "Id", "Music");
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Name");
            ViewData["DancerId"] = new SelectList(_context.Dancer, "Id", "Name");
            ViewData["NomListId"] = new SelectList(_context.NominationList, "Id", "Name");
            return View();
        }

        // POST: Nominations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomListId,Place,DancerId,ChoreographyId,CompetitionId")] Nomination nomination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nomination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChoreographyId"] = new SelectList(_context.Choreography, "Id", "Music", nomination.ChoreographyId);
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Name", nomination.CompetitionId);
            ViewData["DancerId"] = new SelectList(_context.Dancer, "Id", "Name", nomination.DancerId);
            ViewData["NomListId"] = new SelectList(_context.NominationList, "Id", "Name", nomination.NomListId);
            return View(nomination);
        }

        // GET: Nominations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nomination = await _context.Nomination.FindAsync(id);
            if (nomination == null)
            {
                return NotFound();
            }
            ViewData["ChoreographyId"] = new SelectList(_context.Choreography, "Id", "Music", nomination.ChoreographyId);
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Name", nomination.CompetitionId);
            ViewData["DancerId"] = new SelectList(_context.Dancer, "Id", "Name", nomination.DancerId);
            ViewData["NomListId"] = new SelectList(_context.NominationList, "Id", "Name", nomination.NomListId);
            return View(nomination);
        }

        // POST: Nominations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomListId,Place,DancerId,ChoreographyId,CompetitionId")] Nomination nomination)
        {
            if (id != nomination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nomination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NominationExists(nomination.Id))
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
            ViewData["ChoreographyId"] = new SelectList(_context.Choreography, "Id", "Music", nomination.ChoreographyId);
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Name", nomination.CompetitionId);
            ViewData["DancerId"] = new SelectList(_context.Dancer, "Id", "Name", nomination.DancerId);
            ViewData["NomListId"] = new SelectList(_context.NominationList, "Id", "Name", nomination.NomListId);
            return View(nomination);
        }

        // GET: Nominations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nomination = await _context.Nomination
                .Include(n => n.Choreography)
                .Include(n => n.Competition)
                .Include(n => n.Dancer)
                .Include(n => n.NomList)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nomination == null)
            {
                return NotFound();
            }

            return View(nomination);
        }

        // POST: Nominations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var nomination = await _context.Nomination.FindAsync(id);
                _context.Nomination.Remove(nomination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("DelErr", "Home");
            }
        }

        private bool NominationExists(int id)
        {
            return _context.Nomination.Any(e => e.Id == id);
        }
    }
}
