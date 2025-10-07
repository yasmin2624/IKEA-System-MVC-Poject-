using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Servies.AttachementsService
{
    public interface IAttachementService
    {
         public string? Upload (IFormFile file , string FolderName , string? oldFileName, bool keepOldName = false);

         public bool Delete (string filePath);
    }
}
