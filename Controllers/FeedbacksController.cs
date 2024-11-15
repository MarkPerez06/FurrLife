using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurrLife.Data;
using FurrLife.Models;
using FurrLife.Static;

namespace FurrLife.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Feedbacks
        public async Task<IActionResult> Index()
        {
            var user = _context.Users.Where(m => m.UserName == User.Identity.Name).FirstOrDefault();
            var model = _context.Feedbacks.ToList();
            if (user.SecurityStamp == UserRoles.Customer.Id)
            {
                model = _context.Feedbacks.Where(m => m.UserId == user.Id).ToList();
            }

            return View(model) ;
        }

        // GET: Feedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Feedbacks == null)
            {
                return NotFound();
            }

            var feedbacks = await _context.Feedbacks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedbacks == null)
            {
                return NotFound();
            }

            return View(feedbacks);
        }

        // GET: Feedbacks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Feedback,UserId,IsShow,DateCreated")] Feedbacks feedbacks)
        {
            var user = _context.Users.Where(m => m.UserName == User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                feedbacks.UserId = user.Id;

            }
            feedbacks.IsShow = false;
            _context.Add(feedbacks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            return View(feedbacks);
        }

        // GET: Feedbacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Feedbacks == null)
            {
                return NotFound();
            }

            var feedbacks = await _context.Feedbacks.FindAsync(id);
            if (feedbacks == null)
            {
                return NotFound();
            }
            return View(feedbacks);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Feedback,UserId,IsShow,DateCreated")] Feedbacks feedbacks)
        {
            if (id != feedbacks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedbacks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbacksExists(feedbacks.Id))
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
            return View(feedbacks);
        }

        // GET: Feedbacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Feedbacks == null)
            {
                return NotFound();
            }

            var feedbacks = await _context.Feedbacks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedbacks == null)
            {
                return NotFound();
            }

            return View(feedbacks);
        }

        // POST: Feedbacks/Delete/5
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Feedbacks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Feedbacks'  is null.");
            }
            var feedbacks = await _context.Feedbacks.FindAsync(id);
            if (feedbacks != null)
            {
                _context.Feedbacks.Remove(feedbacks);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbacksExists(int id)
        {
            return (_context.Feedbacks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
