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
    public class NominationListsController : Controller
    {
        private readonly DanceContext _context;

        public NominationListsController(DanceContext context)
        {
            _context = context;
        }

        // GET: NominationLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.NominationList.ToListAsync());
        }

        // GET: NominationLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nominationList = await _context.NominationList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nominationList == null)
            {
                return NotFound();
            }

             
            return RedirectToAction("Vieew", "Nominations", new { id = nominationList.Id, name = nominationList.Name });
             
        }

        // GET: NominationLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NominationLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] NominationList nominationList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nominationList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nominationList);
        }

        // GET: NominationLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nominationList = await _context.NominationList.FindAsync(id);
            if (nominationList == null)
            {
                return NotFound();
            }
            return View(nominationList);
        }

        // POST: NominationLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] NominationList nominationList)
        {
            if (id != nominationList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nominationList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NominationListExists(nominationList.Id))
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
            return View(nominationList);
        }

        // GET: NominationLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nominationList = await _context.NominationList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nominationList == null)
            {
                return NotFound();
            }

            return View(nominationList);
        }

        // POST: NominationLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var nominationList = await _context.NominationList.FindAsync(id);
                _context.NominationList.Remove(nominationList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("DelErr", "Home");
            }
        }

        private bool NominationListExists(int id)
        {
            return _context.NominationList.Any(e => e.Id == id);
        }
    }
}
