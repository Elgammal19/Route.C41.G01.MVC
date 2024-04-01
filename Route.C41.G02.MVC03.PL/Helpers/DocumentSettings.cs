using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods.File;

namespace Route.C41.G02.MVC03.PL.Helpers
{
    public static class DocumentSettings
    {
        // File Settings  : Uploud & Download static files || Ducoments --> On server(Server Side) / In The Application(Client Side)
        //                  1. On Server --> 1.1 Create File Location On The Server ,  1.2 Uploade data of the file in the location of the file on server by streaming(Data Per Time)   
    
        public static string UploadeFile(IFormFile file , string folderName)
        {
            // 1. Get The Located Folder Path :
            // string FolderPath = $"C:\\Users\\Mohamed Elgammal\\source\\repos\\Projects\\Route\\ASP.NET Core\\ASP.NET Core MVC\\Route.C41.G02.MVC03\\Route.C41.G02.MVC03.PL\\wwwroot\files\\{folderName}";  --> Path will changed when --> Enviroment Changed
            // string FolderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\files\\{folderName}";   --> May be messing a '\' so the path will be wrong
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "\\wwwroot\files\\", folderName);  // Combine Function --> Combine the Path parts & create path

            // Validate If directory exist or n't & if it does n't exit --> craete path 
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            // 2. Get The File Name & Make It Unique(By the guid) :
            //string fileName = file.Name;   --> Name here mean the extension of the file , N't the name of the file
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // 3. Get File Path --> file path = [folder path + file name]
            string filePath = Path.Combine(FolderPath, fileName);

            // 4. Save File As Stream[Data Per Time]
            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        
        } 

        public static void DeleteFile(string folderName , string fileName)
        {
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "\\wwwroot\files\\", folderName);
            string filePath = Path.Combine(FolderPath, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);

        }
    }
}
