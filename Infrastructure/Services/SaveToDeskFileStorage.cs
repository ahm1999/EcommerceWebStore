
using Application.Common;
using Application.ServiceInterfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class SaveToDeskFileStorage : IFileStorageService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public SaveToDeskFileStorage(IWebHostEnvironment webHost,AppDbContext context)
        {
            _webHost = webHost;
            _context = context;
        }
        public async Task<ServiceResponse<string>> SaveImagesAsync(ICollection<IFormFile>? formFiles, Guid productId)
        {

            string relativePath = "upload";
            string uploadPath = Path.Combine(_webHost.WebRootPath, relativePath);

            if (!Directory.Exists(uploadPath)) { 
                Directory.CreateDirectory(uploadPath);
            }

            List<string> filePathes = new();

            if (formFiles is null) {
                return new ServiceResponse<string>(false, "error occoured");

            }
            foreach (var file in formFiles)
            {
                string fileExtention = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid().ToString()+fileExtention;
                string filePath = Path.Combine(uploadPath, fileName);

                using (FileStream stream = new FileStream(filePath, FileMode.Create)) {
                    await file.CopyToAsync(stream);
                   
                }
                filePathes.Add(Path.Combine(relativePath,fileName));

            }

            await SaveImagesToDbAsync(filePathes, productId);

            return new ServiceResponse<string>(true, "files added", filePathes);
        }

        public async Task<ServiceResponse<string>> SaveImagesToDbAsync(List<string> ImagePaths, Guid productId)
        {
            //List<ProductImage> productImages = new List<ProductImage>(ImagePaths.Count);

            List<ProductImage> Images = ImagePaths.Select<string, ProductImage>(ip => new ProductImage()
            {
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                ProductId = productId,
                ImagePath = ip,
                ImageName = "genericName"

            }).ToList();

            await _context.ProductImages.AddRangeAsync(Images);

            return new ServiceResponse<string>(true, "Image Added Succesfully");

        }
    }
}
