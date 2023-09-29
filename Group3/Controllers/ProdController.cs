using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Group3.Models;
using Group3.Reponsitory;
using Group3.Services;
using Lib;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group3.Controllers
{
    public class ProdController : Controller
    {
        private readonly IProd _prodService;
        private readonly IBrand _brandService;
        private readonly IType _typeService;
        private readonly ICat _categoryService;
        private readonly IGender _genderService;
        private IWebHostEnvironment _webHostEnvironment;

        public ProdController(IProd prodService, IBrand brandService, IType typeService, ICat categoryService, IGender genderService, IWebHostEnvironment webHostEnvironment)
        {
          this._prodService = prodService;
            this._brandService = brandService;
            this._typeService = typeService;
            this._categoryService = categoryService;
            this._genderService = genderService;
            this._webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _prodService.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(string id)
        {
            var product = await _prodService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.Brands = _brandService.GetAllAsync();

            // ViewBag.Types = _typeService.GetAllTypesAsync();
            List<TypeMst> typeMsts = new List<TypeMst>();
            IEnumerable<TypeMst> typetmp = await _typeService.GetAllTypesAsync();

            foreach (TypeMst item in typetmp)
            {
                TypeMst typeMst = item;
                typeMsts.Add(typeMst);  
            }   
            ViewBag.Types = typeMsts;
            ViewBag.Categories = _categoryService.GetAllAsync();
            ViewBag.Genders = _genderService.GetAllGendersAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdMst product, List<IFormFile> imageFiles)
        {
            if (ModelState.IsValid)
            {
                await _prodService.CreateAsync(product, imageFiles);
                return RedirectToAction("Index");
            }

            ViewBag.Brands = _brandService.GetAllAsync();
            ViewBag.Types = _typeService.GetAllTypesAsync();
            ViewBag.Categories = _categoryService.GetAllAsync();
            ViewBag.Genders = _genderService.GetAllGendersAsync();
            return View(product);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var product = await _prodService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Brands = _brandService.GetAllAsync();
            ViewBag.Types = _typeService.GetAllTypesAsync();
            ViewBag.Categories = _categoryService.GetAllAsync();
            ViewBag.Genders = _genderService.GetAllGendersAsync();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ProdMst product, List<IFormFile> imageFiles)
        {
            if (id != product.Prod_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _prodService.UpdateAsync(product, imageFiles);
                return RedirectToAction("Index");
            }

            ViewBag.Brands = _brandService.GetAllAsync();
            ViewBag.Types = _typeService.GetAllTypesAsync();
            ViewBag.Categories = _categoryService.GetAllAsync();
            ViewBag.Genders = _genderService.GetAllGendersAsync();
            return View(product);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var product = await _prodService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _prodService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
