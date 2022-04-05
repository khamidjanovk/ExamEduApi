using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MyEdu.Service.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEdu.Service.Extensions
{
    public static class SaveFile
    {
        public static async Task<(string FileName, string WebUrl)> SaveFileAsync(this Stream file, IConfiguration config, IWebHostEnvironment env, string fileName)
        {
            string hostUrl = HttpContextHelper.Context?.Request?.Scheme + 
                "://" + HttpContextHelper.Context?.Request?.Host.Value;


            fileName = Guid.NewGuid().ToString("N") + "_" + fileName;

            string storagePath = config.GetSection("Storage:ImageUrl").Value;

            string filePath = Path.Combine(env.WebRootPath, $"{storagePath}/{fileName}");
            
            FileStream mainFile = File.Create(filePath);

            string webUrl = $@"{hostUrl}/{storagePath}/{fileName}";


            await file.CopyToAsync(mainFile);
            mainFile.Close();

            return (fileName, webUrl);
        }
    }
}
