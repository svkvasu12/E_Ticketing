using Microsoft.AspNetCore.Mvc;
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
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;
        
        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public  IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Bio,FileName,File,FileForm")] Actor actor)
        {
            if (actor.FileForm != null)
            {
                byte[] bytes = null;



                using (MemoryStream ms = new MemoryStream())
                {
                    actor.FileForm.CopyTo(ms);
                    bytes = ms.ToArray();
                }


                actor.File = bytes;
                actor.FileName = actor.FileForm.FileName;
            }
            if (ModelState.IsValid)
            {
               await _service.AddAsync(actor);

                return RedirectToAction(nameof(Index));
            }
            return View(actor);


        }
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        public async Task<IActionResult> Edit (int id)
        {

            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("Id,FullName,Bio,FileName,File,FileForm")] Actor actor)
        {
            if (id != actor.Id)
            {
                return NotFound();
            }
            
            else if (actor.File == null)
            {
                byte[] bytes = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    actor.FileForm.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                actor.File = bytes;
                actor.FileName = actor.FileForm.FileName;
            }
            if (ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(id,actor);

            return RedirectToAction(nameof(Index));
            


        }

        public async Task<IActionResult> Delete(int id)
        {

            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            
            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
