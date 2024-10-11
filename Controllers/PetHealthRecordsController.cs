using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurrLife.Data;
using FurrLife.Models;

namespace FurrLife.Controllers
{
    public class PetHealthRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PetHealthRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PetHealthRecords
        public async Task<IActionResult> Index()
        {
              return _context.PetHealthRecord != null ? 
                          View(await _context.PetHealthRecord.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PetHealthRecord'  is null.");
        }

        // GET: PetHealthRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PetHealthRecord == null)
            {
                return NotFound();
            }

            var petHealthRecord = await _context.PetHealthRecord
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petHealthRecord == null)
            {
                return NotFound();
            }

            return View(petHealthRecord);
        }

        // GET: PetHealthRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetHealthRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PetName,Age,Birthdate,Breed,Gender,Weight,Color,TemperamentAndPersonalityTraits,BehavioralIssues,GroomingHabits,ExerciseRoutines,Allergies,FeedingSchedule,ExistingConditions,UserId,FullName,Phone,Email,Address,CreatedDate")] PetHealthRecord petHealthRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(petHealthRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petHealthRecord);
        }

        // GET: PetHealthRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PetHealthRecord == null)
            {
                return NotFound();
            }

            var petHealthRecord = await _context.PetHealthRecord.FindAsync(id);
            if (petHealthRecord == null)
            {
                return NotFound();
            }
            return View(petHealthRecord);
        }

        // POST: PetHealthRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PetName,Age,Birthdate,Breed,Gender,Weight,Color,TemperamentAndPersonalityTraits,BehavioralIssues,GroomingHabits,ExerciseRoutines,Allergies,FeedingSchedule,ExistingConditions,UserId,FullName,Phone,Email,Address,CreatedDate")] PetHealthRecord petHealthRecord)
        {
            if (id != petHealthRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petHealthRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetHealthRecordExists(petHealthRecord.Id))
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
            return View(petHealthRecord);
        }

        // GET: PetHealthRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PetHealthRecord == null)
            {
                return NotFound();
            }

            var petHealthRecord = await _context.PetHealthRecord
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petHealthRecord == null)
            {
                return NotFound();
            }

            return View(petHealthRecord);
        }

        // POST: PetHealthRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PetHealthRecord == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PetHealthRecord'  is null.");
            }
            var petHealthRecord = await _context.PetHealthRecord.FindAsync(id);
            if (petHealthRecord != null)
            {
                _context.PetHealthRecord.Remove(petHealthRecord);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetHealthRecordExists(int id)
        {
          return (_context.PetHealthRecord?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
