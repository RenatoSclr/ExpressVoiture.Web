﻿using ExpressVoiture.Domain.Models;
using ExpressVoiture.Services.IService;
using ExpressVoiture.ViewModel;

namespace ExpressVoiture.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public AddOrUpdateVehicleViewModel CreateFile(AddOrUpdateVehicleViewModel vehicle, IFormFile file)
        {
            if (file == null)
            {
                return vehicle;
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string voiturePath = Path.Combine(wwwRootPath, @"images\vehicles");

            if (!string.IsNullOrEmpty(vehicle.ImagePath))
            {
                var oldImagePath = Path.Combine(wwwRootPath, vehicle.ImagePath.TrimStart('\\'));

                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            using (var fileStream = new FileStream(Path.Combine(voiturePath, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            vehicle.ImagePath = @"images\vehicles\" + fileName;

            return vehicle;
        }

        public VoitureAVendre DeleteFile(VoitureAVendre vehicle)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (!string.IsNullOrEmpty(vehicle.ImagePath))
            {
                var oldImagePath = Path.Combine(wwwRootPath, vehicle.ImagePath.TrimStart('\\'));

                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            return vehicle;
        }
    }
}
