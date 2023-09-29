using Group3.Reponsitory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lib;
using System.Threading.Tasks;

namespace Group3.Controllers
{
    public class GenderController : Controller
    {
        private readonly IGender _genderService;

        public GenderController(IGender gender)
        {
            _genderService = gender;
        }

        // GET: Gender
        public async Task<IActionResult> Index()
        {
            var genders = await _genderService.GetAllGendersAsync();
            return View(genders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gender gender)
        {
            if (ModelState.IsValid)
            {
                await _genderService.CreateGenderAsync(gender);
                return RedirectToAction("Index");
            }
            return View(gender);
        }

        // Action to display the edit form
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _genderService.GetGenderByIdAsync(id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        // Action to handle the edit operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Gender gender)
        {
            if (id != gender.Gender_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _genderService.UpdateGenderAsync(gender);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GenderExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(gender);
        }

        // Action to display the confirmation page for deletion
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _genderService.GetGenderByIdAsync(id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        // Action to confirm and perform the deletion
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var gender = await _genderService.GetGenderByIdAsync(id);
            if (gender == null)
            {
                return NotFound();
            }

            await _genderService.DeleteGenderAsync(id);
            return RedirectToAction("Index");
        }

        private async Task<bool> GenderExists(string id)
        {
            return await _genderService.GetGenderByIdAsync(id) != null;
        }
    }
}
