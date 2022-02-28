using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using New_Final_ET1.Data;
using New_Final_ET1.Data.Services;
using New_Final_ET1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace New_Final_ET1.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);

        }
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        // GET: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cinemas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,FileName,File,FileForm")] Cinema cinema)
        {
            if (cinema.FileForm != null)
            {
                byte[] bytes = null;



                using (MemoryStream ms = new MemoryStream())
                {
                    cinema.FileForm.CopyTo(ms);
                    bytes = ms.ToArray();
                }


                cinema.File = bytes;
                cinema.FileName = cinema.FileForm.FileName;
            }

            if (!ModelState.IsValid) return View(cinema);
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        // GET: Cinemas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        // POST: Cinemas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,FileName,File,FileForm")] Cinema cinema)
        {
            if (id != cinema.Id)
            {
                return NotFound();
            }

            else if (cinema.File == null)
            {
                byte[] bytes = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    cinema.FileForm.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                cinema.File = bytes;
                cinema.FileName = cinema.FileForm.FileName;
            }
            if (!ModelState.IsValid) return View(cinema);
            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        // GET: Cinemas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        // POST: Cinemas/Delete/5
        [HttpPost, ActionName("Delete")]


        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
