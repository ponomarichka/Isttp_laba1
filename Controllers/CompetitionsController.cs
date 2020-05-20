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
    public class CompetitionsController : Controller
    {
        private readonly DanceContext _context;

        public CompetitionsController(DanceContext context)
        {
            _context = context;
        }

        // GET: Competitions
        public async Task<IActionResult> Index()
        {
            var competitionContext = _context.Competition.Include(c => c.Organizer).Include(c => c.City);
            return View(await competitionContext.ToListAsync());
            //if (id == null) return RedirectToAction("Organizer","Index");

            //ViewBag.OrganizerId = id;
            //ViewBag.OrganizerName = name;
            // var danceContext = _context.Competition.Where(c => c.OrganizerId==id).Include(c => c.Organizer);
            // return View(await danceContext.ToListAsync());
        }
        public async Task<IActionResult> Vieew(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Organizer","Index");

            ViewBag.OrganizerId = id;
            ViewBag.OrganizerName = name;
             var danceContext = _context.Competition.Where(c => c.OrganizerId==id).Include(c => c.Organizer);
             return View(await danceContext.ToListAsync());

        }


        // GET: Competitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competition
                .Include(c => c.City)
                .Include(c => c.Organizer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (competition == null)
            {
                return NotFound();
            }

             return View(competition);
           

        }

        // GET: Competitions/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name");
            ViewData["OrganizerId"] = new SelectList(_context.Organizer, "Id", "Name");
            return View();
        }

        // POST: Competitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Information,OrganizerId,CityId")] Competition competition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", competition.CityId);
            ViewData["OrganizerId"] = new SelectList(_context.Organizer, "Id", "Name", competition.OrganizerId);
            return View(competition);
        }

        // GET: Competitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competition.FindAsync(id);
            if (competition == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", competition.CityId);
            ViewData["OrganizerId"] = new SelectList(_context.Organizer, "Id", "Name", competition.OrganizerId);
            return View(competition);
        }

        // POST: Competitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Information,OrganizerId,CityId")] Competition competition)
        {
            if (id != competition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetitionExists(competition.Id))
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
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", competition.CityId);
            ViewData["OrganizerId"] = new SelectList(_context.Organizer, "Id", "Name", competition.OrganizerId);
            return View(competition);
        }

        // GET: Competitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competition
                .Include(c => c.City)
                .Include(c => c.Organizer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }

        // POST: Competitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var competition = await _context.Competition.FindAsync(id);
                _context.Competition.Remove(competition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("DelErr", "Home");
            }
        }

        private bool CompetitionExists(int id)
        {
            return _context.Competition.Any(e => e.Id == id);
        }
    }
}
