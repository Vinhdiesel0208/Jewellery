using Group3.Reponsitory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lib;
using System.Threading.Tasks;
using System;

namespace Group3.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrand _brandService;

        public BrandController(IBrand brand)
        {
            _brandService = brand;
        }

        // GET: Brand
        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllAsync();
            return View(brands);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandMst brand)
        {
            if (ModelState.IsValid)
            {
                await _brandService.CreateAsync(brand);
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        // Action để hiển thị form chỉnh sửa
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // Action để xử lý việc chỉnh sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, BrandMst brand)
        {
            if (id != brand.Brand_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _brandService.UpdateAsync(brand);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BrandExists(id))
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
            return View(brand);
        }

        // Action để hiển thị trang xác nhận xóa
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // Action để xác nhận và thực hiện xóa
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            await _brandService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private async Task<bool> BrandExists(string id)
        {
            return await _brandService.GetByIdAsync(id) != null;
        }
    }
}
