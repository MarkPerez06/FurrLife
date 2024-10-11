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
    public class PetRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PetRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PetRecords
        public async Task<IActionResult> Index()
        {
              return _context.PetRecord != null ? 
                          View(await _context.PetRecord.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PetRecord'  is null.");
        }

        // GET: PetRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PetRecord == null)
            {
                return NotFound();
            }

            var petRecord = await _context.PetRecord
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petRecord == null)
            {
                return NotFound();
            }

            return View(petRecord);
        }

        // GET: PetRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OwnerFullName,OwnerContact,OwnerAddress,PetName,Age,Breed,Gender,Weight,PhysicalCondition,PreviousIllnessesOrSurgeries,VaccinationStatus,CurrentMedicationsOrTreatments,TemperamentAndPersonality,BehavioralIssuesOrChanges,CurrentDietAndFeedingSchedule,FoodAllergiesOrSensitivities,LivingConditions,InteractionWithOtherPets,SpecificIssuesOrSymptoms,OwnerQuestionsOrTopics,GroomingHabits,ExerciseRoutines,ReasonForVisit,Diagnosis,TreatmentPlan,FollowUpCareInstructions,ConsultationSummary,OwnerConsentGiven,ReferralInformation")] PetRecord petRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(petRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petRecord);
        }

        // GET: PetRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PetRecord == null)
            {
                return NotFound();
            }

            var petRecord = await _context.PetRecord.FindAsync(id);
            if (petRecord == null)
            {
                return NotFound();
            }
            return View(petRecord);
        }

        // POST: PetRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerFullName,OwnerContact,OwnerAddress,PetName,Age,Breed,Gender,Weight,PhysicalCondition,PreviousIllnessesOrSurgeries,VaccinationStatus,CurrentMedicationsOrTreatments,TemperamentAndPersonality,BehavioralIssuesOrChanges,CurrentDietAndFeedingSchedule,FoodAllergiesOrSensitivities,LivingConditions,InteractionWithOtherPets,SpecificIssuesOrSymptoms,OwnerQuestionsOrTopics,GroomingHabits,ExerciseRoutines,ReasonForVisit,Diagnosis,TreatmentPlan,FollowUpCareInstructions,ConsultationSummary,OwnerConsentGiven,ReferralInformation")] PetRecord petRecord)
        {
            if (id != petRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetRecordExists(petRecord.Id))
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
            return View(petRecord);
        }

        // GET: PetRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PetRecord == null)
            {
                return NotFound();
            }

            var petRecord = await _context.PetRecord
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petRecord == null)
            {
                return NotFound();
            }

            return View(petRecord);
        }

        // POST: PetRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PetRecord == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PetRecord'  is null.");
            }
            var petRecord = await _context.PetRecord.FindAsync(id);
            if (petRecord != null)
            {
                _context.PetRecord.Remove(petRecord);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetRecordExists(int id)
        {
          return (_context.PetRecord?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
