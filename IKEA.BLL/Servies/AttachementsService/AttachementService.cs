using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Servies.AttachementsService
{
    public class AttachementService : IAttachementService
    {
        List<string> AllowedExtensions = new List<string> { ".png", ".jpg", ".jpeg", ".gif" };
        const int MaxSize =2_097_152 ; //10MB

        public string? Upload(IFormFile file, string FolderName, string? oldFileName, bool keepOldName = false)
        {
            //Check Extension
            var extension = Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(extension)) return null;

            //check Size
            if (file.Length == 0 || file.Length > MaxSize) return null;

            //Get Located PathFile
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", FolderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // full path of old file
            var oldFilePath = Path.Combine(folderPath, oldFileName);

            // delete old file if exists
            if (File.Exists(oldFilePath))
                File.Delete(oldFilePath);

            // Make Attachements Name Unique
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            // Get file Path
            var filePath = Path.Combine(folderPath, fileName);

            //Create File Stream To Copy File
            using FileStream FS = new FileStream(filePath, FileMode.Create);

            // Use Stream To Copy File
            file.CopyTo(FS);

            //return File Name to Store in DB
            return fileName;


        }
        

        public bool Delete(string filePath)
        {
           if(!File.Exists(filePath)) return false;
            File.Delete(filePath);
            return true;
        }

       
    }
}
