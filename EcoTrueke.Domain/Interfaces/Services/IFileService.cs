using Microsoft.AspNetCore.Http;

namespace EcoTrueke.Domain.Interfaces.Services
{
    public interface IFileService
    {
        Task<string> SaveProfilePictureAsync(string personId, IFormFile file);

        Task<string> SaveProductPictureAsync(string personId, IFormFile file);
        Task DeletPictureAsync(string relativePath);
    }
}
