using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeilaoWeb.Models;
using LeilaoWeb.Models.ViewModels;
using System.Diagnostics;

namespace LeilaoWeb.Controllers
{
    public class PeoplesController : Controller
    {
        private readonly LeilaoWebContext _context;
        public PeoplesController(LeilaoWebContext context)
        {
            _context = context;
        }


        // GET: People
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _context.People.ToListAsync());
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // GET: People/Details/
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var people = await _context.People
                    .FirstOrDefaultAsync(m => m.PeopleId == id);
                if (people == null)
                {
                    return NotFound();
                }

                return View(people);

            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(People people)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!PeopleExists(people.Name))
                    {
                        _context.Add(people);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = ("Name already exists.");
                        return View(people);
                    }
                }
                return View(people);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var people = await _context.People.FindAsync(id);
                if (people == null)
                {
                    return NotFound();
                }
                return View(people);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // POST: People/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, People people)
        {
            try
            {
                if (id != people.PeopleId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(people);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        if (!PeopleExists(people.PeopleId))
                        {
                            ViewBag.Message = ("Name not found.");
                            return NotFound();
                        }
                        else
                        {
                            return RedirectToAction(nameof(Error), new { message = e.Message });
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(people);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var people = await _context.People
                    .FirstOrDefaultAsync(m => m.PeopleId == id);
                if (people == null)
                {
                    return NotFound();
                }

                return View(people);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var people = await _context.People.FindAsync(id);
                _context.People.Remove(people);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }



        private bool PeopleExists(int id)
        {
            return _context.People.Any(e => e.PeopleId == id);
        }

        private bool PeopleExists(string name)
        {
            return _context.People.Any(e => e.Name == name);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
