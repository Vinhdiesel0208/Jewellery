using Group3.Reponsitory;
using Group3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lib;

namespace Group3.Controllers
{
    public class BlogController : Controller
    {
        private IBlogServices service;
        public BlogController(IBlogServices service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await service.GetBlogsList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Blogs newBlogs, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    string path = Path.Combine("wwwroot/images/Blogs", file.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);
                    newBlogs.Photo = file.FileName;
                }
                await service.addBlogs(newBlogs);
                TempData["msg"] = "Bạn đã tạo mới thành công";
                return RedirectToAction("Index", "Blog");
            }
            catch (Exception ex)
            {
                TempData["errmsg"] = ex.Message;

                /*          TempData["errmsg"] = "Tạo mới tin tức thất bại";*/

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await service.removeBlogs(id);
                TempData["msg"] = "Xóa khóa học thành công";
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                ViewBag.errormsg = "Xóa khóa học thất bại";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var model = await service.GetBlogsById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Blogs editBlogs, IFormFile file)
        {

            var oldBlogs = await service.GetBlogsById(editBlogs.ID);
            if (oldBlogs != null)
            {
                if (file != null)
                {
                    string path = Path.Combine("wwwroot/images/Blogs", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        stream.Dispose();
                    }

                    if (!string.IsNullOrEmpty(oldBlogs.Photo))
                    {

                        string OldPath = Path.Combine("wwwroot/images/Blogs", oldBlogs.Photo);

                        if (System.IO.File.Exists(OldPath))
                        {
                            System.IO.File.Delete(OldPath);
                        }
                    }
                }
            }
            else
            {
                ViewBag.error = "Chỉnh sửa thất bại";
                return View();
            }
            editBlogs.Photo = file != null ? file.FileName : oldBlogs.Photo;
            oldBlogs.Title = editBlogs.Title;
            oldBlogs.Summary = editBlogs.Summary;
            oldBlogs.Content1 = editBlogs.Content1;
            oldBlogs.Content2 = editBlogs.Content2;
            oldBlogs.Content3 = editBlogs.Content3;
            oldBlogs.Author = editBlogs.Author;
            editBlogs.PublishDate = DateTime.Now;
            oldBlogs.Photo = editBlogs.Photo;
            await service.updateBlogs(editBlogs);
            TempData["msg"] = "Chúc mừng bạn đã chỉnh sửa thành công";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> IndexBlogs()
        {
            var model = await service.GetBlogsList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DetailBlog(int id)
        {
            var model = await service.GetBlogsById(id);
            return View(model);
        }
    }
}
