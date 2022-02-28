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
    public class ProducersController : Controller
    {

        private readonly IProducersService _service;

        public ProducersController(IProducersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync();
            return View(allProducers);

        }
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Bio,FileName,File,FileForm")] Producer producer)
        {
            if (producer.FileForm != null)
            {
                byte[] bytes = null;



                using (MemoryStream ms = new MemoryStream())
                {
                    producer.FileForm.CopyTo(ms);
                    bytes = ms.ToArray();
                }


                producer.File = bytes;
                producer.FileName = producer.FileForm.FileName;
            }
            if (ModelState.IsValid)
            {
                await _service.AddAsync(producer);

                return RedirectToAction(nameof(Index));
            }
            return View(producer);


        }
        public async Task<IActionResult> Edit(int id)
        {

            var producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Bio,FileName,File,FileForm")] Producer producer)
        {
            if (id != producer.Id)
            {
                return NotFound();
            }

            else if (producer.File == null)
            {
                byte[] bytes = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    producer.FileForm.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                producer.File = bytes;
                producer.FileName = producer.FileForm.FileName;
            }
            if (ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.UpdateAsync(id, producer);

            return RedirectToAction(nameof(Index));



        }

        public async Task<IActionResult> Delete(int id)
        {

            var producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
