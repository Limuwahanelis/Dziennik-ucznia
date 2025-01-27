using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DziennikUcznia.Data;
using DziennikUcznia.Models;
using DziennikUcznia.Interfaces.Repositories;

namespace DziennikUcznia.Controllers
{
    public class ClassesController : Controller
    {
        private readonly IClassesRepository _classesRepository;

        public ClassesController(IClassesRepository classesRepository)
        {
            _classesRepository = classesRepository;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            return View(await _classesRepository.GetClasses());
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _classesRepository.GetClassById(id.Value);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] SchoolClass schoolClass)
        {
            if (ModelState.IsValid)
            {
               // if(_classesRepository.get)
                await _classesRepository.AddClass(schoolClass.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(schoolClass);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SchoolClass schoolClass = await _classesRepository.GetClassById(id.Value);
            if (schoolClass == null)
            {
                return NotFound();
            }
            return View(schoolClass);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] SchoolClass schoolClass)
        //{
        //    if (id != schoolClass.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _classesRepository.Update(@class);
        //            await _classesRepository.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ClassExists(@class.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(@class);
        //}

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _classesRepository.GetClassById(id.Value);
            if (schoolClass == null)
            {
                return NotFound();
            }

            return View(schoolClass);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SchoolClass schoolClass = await _classesRepository.GetClassById(id);
            if (schoolClass != null)
            {
                await _classesRepository.DeleteClass(schoolClass.Id);
            }
            return RedirectToAction(nameof(Index));
        }

        //private bool ClassExists(int id)
        //{
        //    return _classesRepository.Classes.Any(e => e.Id == id);
        //}
    }
}
