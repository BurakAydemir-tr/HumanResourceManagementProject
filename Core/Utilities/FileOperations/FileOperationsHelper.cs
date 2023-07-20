using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Utilities.FileOperations
{
    public class FileOperationsHelper
    {
        public static async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string directory = Environment.CurrentDirectory + @"\wwwroot\";
            string uploadPath = Path.Combine(directory, path);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }


            List<(string fileName, string path)> datas = new();
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(file.FileName);

                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}",file);
                if (result)
                {
                    datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
                }
                
            }

            return datas;
        }


        public static async Task<string> UploadSingleAsync(string path, IFormFile file)
        {
            string directory = Environment.CurrentDirectory + @"\wwwroot\";
            string uploadPath = Path.Combine(directory, path);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string fileNewName= await FileRenameAsync(file.FileName);
            bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
            if (result)
            {
                return $"{path}\\{fileNewName}";
            }
            return null;
        }

        private static async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                //todo log
                throw ex;
            }

            
        }

        public static async Task<string> FileRenameAsync(string fileName)
        {
            string fileExtension=Path.GetExtension(fileName);
            string oldName=Path.GetFileNameWithoutExtension(fileName);

            Regex regex = new Regex("[*'\",+-.|&#^@|/<>~]");
            string newName = regex.Replace(oldName,string.Empty);

            DateTime dateTime = DateTime.UtcNow;
            string dateTimeUtcNow = dateTime.ToString("yyyyMMddHHmmss");
            string newFullName = $"{newName}-{dateTimeUtcNow}{fileExtension}";

            return newFullName;
        }

        public static bool Delete(string fileName)
        {
            string path=Path.GetDirectoryName(fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
