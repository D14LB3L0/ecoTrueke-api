using EcoTrueke.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EcoTrueke.Infrastructure.Upload
{
    public class FileService : IFileService
    {

        private readonly string _basePath;

        public FileService(IConfiguration configuration)
        {
            _basePath = configuration["FileStorage:UploadFolderPath"]; ;
        }

        public async Task DeleteProfilePictureAsync(string relativePath)
        {
            var fullPath = Path.Combine(_basePath, relativePath.Replace("/", "\\"));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);

                // Verify if the file is empty
                var directoryPath = Path.GetDirectoryName(fullPath);

                if (Directory.Exists(directoryPath) && Directory.GetFiles(directoryPath).Length == 0)
                {
                    Directory.Delete(directoryPath);
                }
            }

            await Task.CompletedTask;
        }

        public async Task<string> SaveProductPictureAsync(string personId, IFormFile file)
        {
            var basePath = Path.Combine(_basePath, "Persons", personId, "Product");


            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            var extension = Path.GetExtension(file.FileName);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(basePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // return relative url 
            return Path.Combine("Persons", personId, "Product", fileName).Replace("\\", "/");
        }

        public async Task<string> SaveProfilePictureAsync(string personId, IFormFile file)
        {
            var basePath = Path.Combine(_basePath, "Persons", personId, "Profile");

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            var extension = Path.GetExtension(file.FileName);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(basePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // return relative url 
            return Path.Combine("Persons", personId, "Profile", fileName).Replace("\\", "/");
        }

    }
}
