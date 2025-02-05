using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DziennikUcznia.Data;
using DziennikUcznia.Models;
using DziennikUcznia.Repositories;
using DziennikUcznia.Interfaces.Repositories;
using DziennikUcznia.Identity;

namespace DziennikUcznia.Controllers
{
    [AuthorizeRole(IdentityRoles.Role.ADMIN)]
    public class SubjectsController : Controller
    {
        private ISubjectsRepository _subjectsRepository;

        public SubjectsController(ISubjectsRepository subjectsRepository)
        {
            _subjectsRepository = subjectsRepository;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            return View(await _subjectsRepository.GetSubjects());
        }
        // GET: Subjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                string subjectName = string.Concat(subject.Name[0].ToString().ToUpper(), subject.Name.ToLower().AsSpan(1));
                subject.Name = subjectName;
                if(await _subjectsRepository.GetSubjectByName(subjectName)!=null)
                {
                    ModelState.AddModelError("SubjectAlreadyExistsError", "Subject with this name already exists");
                    return View(subject);
                }
                await _subjectsRepository.AddSubject(subject);
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Subject? subject = await _subjectsRepository.GetSubjectById(id.Value);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _subjectsRepository.UpdateSubject(subject);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_subjectsRepository.GetSubjectById(id)==null)
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
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Subject? subject = await _subjectsRepository.GetSubjectById(id.Value);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Subject? subject = await _subjectsRepository.GetSubjectById(id);
            if (subject != null)
            {
                await _subjectsRepository.DeleteSubject(subject);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
