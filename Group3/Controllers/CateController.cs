using System;
using System.Threading.Tasks;
using Group3.Reponsitory;
using Group3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lib;

namespace Group3.Controllers
{
    public class CateController : Controller
    {
        private readonly ICat _catService;

        public CateController(ICat cat)
        {
            _catService = cat;
        }

        // GET: Brand/Cate
        public async Task<IActionResult> Index()
        {
            var cats = await _catService.GetAllAsync();
            return View(cats);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CatMst cat)
        {
            if (ModelState.IsValid)
            {
                await _catService.CreateAsync(cat);
                return RedirectToAction("Index"); // Chuyển hướng đến trang "Index" sau khi tạo thành công
            }
            return View(cat);
        }

        // Action để hiển thị form chỉnh sửa
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cat = await _catService.GetByIdAsync(id);
            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        // Action để xử lý việc chỉnh sửa
        [HttpPost]
        public async Task<IActionResult> Edit(string id, CatMst cat)
        {
            if (id != cat.Cat_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _catService.UpdateAsync(cat);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CatExists(id))
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
            return View(cat);
        }

        // Action để hiển thị trang xác nhận xóa
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cat = await _catService.GetByIdAsync(id);
            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        // Action để xác nhận và thực hiện xóa
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cat = await _catService.GetByIdAsync(id);
            if (cat == null)
            {
                return NotFound();
            }

            await _catService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private async Task<bool> CatExists(string id)
        {
            return await _catService.GetByIdAsync(id) != null;
        }
    }
}
