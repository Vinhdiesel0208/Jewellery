using Group3.Reponsitory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lib;
using System.Threading.Tasks;

namespace Group3.Controllers
{
    public class TypeController : Controller
    {
        private readonly IType _typeService;

        public TypeController(IType type)
        {
            _typeService = type;
        }

        // GET: Type
        public async Task<IActionResult> Index()
        {
            var types = await _typeService.GetAllTypesAsync();
            return View(types);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TypeMst type)
        {
            if (ModelState.IsValid)
            {
                await _typeService.CreateTypeAsync(type);
                return RedirectToAction("Index");
            }
            return View(type);
        }

        // Action to display the edit form
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var type = await _typeService.GetTypeByIdAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            return View(type);
        }

        // Action to handle the edit operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, TypeMst type)
        {
            if (id != type.Type_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _typeService.UpdateTypeAsync(type);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TypeExists(id))
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
            return View(type);
        }

        // Action to display the confirmation page for deletion
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var type = await _typeService.GetTypeByIdAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            return View(type);
        }

        // Action to confirm and perform the deletion
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var type = await _typeService.GetTypeByIdAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            await _typeService.DeleteTypeAsync(id);
            return RedirectToAction("Index");
        }

        private async Task<bool> TypeExists(string id)
        {
            return await _typeService.GetTypeByIdAsync(id) != null;
        }
    }
}
