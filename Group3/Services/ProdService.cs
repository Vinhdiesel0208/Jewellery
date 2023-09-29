using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Group3.Db;
using Lib;
using Group3.Reponsitory;

namespace Group3.Services
{
    public class ProdService : IProd
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProdService(DatabaseContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<ProdMst>> GetAllAsync()
        {
            return await _context.ProdMsts.ToListAsync();
        }

        public async Task<ProdMst> GetByIdAsync(string id)
        {
            return await _context.ProdMsts.FindAsync(id);
        }

        public async Task CreateAsync(ProdMst prod, List<IFormFile> imageFiles)
        {
            _context.ProdMsts.Add(prod);
            await _context.SaveChangesAsync();

            // Lưu ảnh sản phẩm
            foreach (var imageFile in imageFiles)
            {
                if (imageFile.Length > 0)
                {
                    var uniqueFileName = GetUniqueFileName(imageFile.FileName);
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images/products");
                    var filePath = Path.Combine(uploads, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    // Lưu đường dẫn hình ảnh vào cơ sở dữ liệu
                    var productImage = new ProductImage
                    {
                        Prod_ID = prod.Prod_ID,
                        ImagePath = uniqueFileName
                    };
                    _context.ProductImages.Add(productImage);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProdMst prod, List<IFormFile> imageFiles)
        {
            _context.ProdMsts.Update(prod);

            // Xóa ảnh cũ
            var existingImages = await _context.ProductImages
                .Where(image => image.Prod_ID == prod.Prod_ID)
                .ToListAsync();
            foreach (var existingImage in existingImages)
            {
                _context.ProductImages.Remove(existingImage);
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images/products", existingImage.ImagePath);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }

            // Lưu ảnh mới
            foreach (var imageFile in imageFiles)
            {
                if (imageFile.Length > 0)
                {
                    var uniqueFileName = GetUniqueFileName(imageFile.FileName);
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images/products");
                    var filePath = Path.Combine(uploads, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    // Lưu đường dẫn hình ảnh vào cơ sở dữ liệu
                    var productImage = new ProductImage
                    {
                        Prod_ID = prod.Prod_ID,
                        ImagePath = uniqueFileName
                    };
                    _context.ProductImages.Add(productImage);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var prod = await _context.ProdMsts.FindAsync(id);
            if (prod != null)
            {
                _context.ProdMsts.Remove(prod);
                await _context.SaveChangesAsync();
            }
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                + "_"
                + Guid.NewGuid().ToString().Substring(0, 4)
                + Path.GetExtension(fileName);
        }
    }
}
