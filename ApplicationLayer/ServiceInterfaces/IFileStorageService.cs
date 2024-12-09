

using Application.Common;
using Microsoft.AspNetCore.Http;

namespace Application.ServiceInterfaces
{
    public interface IFileStorageService
    {
        public Task<ServiceResponse<string>> SaveImagesAsync(ICollection<IFormFile>? formFiles,Guid productId);

        public Task<ServiceResponse<string>> SaveImagesToDbAsync(List<string> ImagePaths, Guid productId);   
    }
}
