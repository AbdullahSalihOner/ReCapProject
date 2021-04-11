using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        public static string AddAsync(IFormFile file)
        {
            var result = newPath(file);

            try
            {
                var sourcePath = Path.GetTempFileName();

                if (file.Length > 0)
                {
                    using (var stream = new FileStream(sourcePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                File.Move(sourcePath, result.newPath);
            }
            catch (Exception exception)
            {
                return exception.Message;
            }

            return result.Path2;
        }

        public static string UpdateAsync(string sourcePath, IFormFile file)
        {
            var result = newPath(file);

            try
            {
                using (var stream = new FileStream(result.newPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                File.Delete(sourcePath);
            }
            catch (Exception excepiton)
            {
                return excepiton.Message;
            }

            return result.Path2;
        }

        public static IResult DeleteAsync(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }

            return new SuccessResult();
        }

        public static (string newPath, string Path2) newPath(IFormFile file)
        {
            FileInfo ff = new FileInfo(file.FileName);

            string fileExtension = ff.Extension;

            var creatingUniqueFilename = Guid.NewGuid().ToString("N") + fileExtension;

            string result = $@"{Environment.CurrentDirectory + @"\wwwroot\Images"}\{creatingUniqueFilename}";

            return (result, $"\\Images\\{creatingUniqueFilename}");
        }


        //public static string Add(IFormFile file)
        //{
        //    string path = Environment.CurrentDirectory + @"\wwwroot";
        //    var sourcePath = Path.GetTempFileName();
        //    if (file.Length > 0)
        //    {
        //        using (var stream = new FileStream(sourcePath, FileMode.Create))
        //        {
        //            file.CopyTo(stream);
        //        }
        //    }
        //    var result = newPath(file);
        //    File.Move(sourcePath, path + result);
        //    return result.Replace("\\", "/");
        //}
        //public static IResult Delete(string path)
        //{
        //    string path2 = Environment.CurrentDirectory + @"\wwwroot";
        //    path = path.Replace("/", "\\");
        //    try
        //    {
        //        File.Delete(path2 + path);
        //    }
        //    catch (Exception exception)
        //    {
        //        return new ErrorResult(exception.Message);
        //    }
        //    return new SuccessResult();
        //}
        //public static string Update(string sourcePath, IFormFile file)
        //{
        //    string path = Environment.CurrentDirectory + @"\wwwroot";
        //    var result = newPath(file);
        //    if (sourcePath.Length > 0)
        //    {
        //        using (var stream = new FileStream(path + result, FileMode.Create))
        //        {
        //            file.CopyTo(stream);
        //        }
        //    }
        //    File.Delete(path + sourcePath);
        //    return result.Replace("\\", "/");
        //}
        //public static string newPath(IFormFile file)
        //{
        //    FileInfo ff = new FileInfo(file.FileName);
        //    string fileExtension = ff.Extension;


        //    var newPath = Guid.NewGuid().ToString() + fileExtension;


        //    return @"\Images\" + newPath;
        //}
    }
}
