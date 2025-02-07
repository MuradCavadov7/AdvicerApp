using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.Extensions;

public static class FileExtension
{
    public static bool IsValidType(this IFormFile file, string type)
       => file.ContentType.StartsWith(type);

    public static bool IsValidSize(this IFormFile file, int mb)
        => file.Length <= mb * 1024 * 1024;

    public static async Task<string> UploadAsync(this IFormFile file, params string[] paths)
    {
        string uploadPath = Path.Combine(paths);
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }
        string newFileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
        string fullPath = Path.Combine(uploadPath, newFileName);
        using (Stream stream = File.Create(fullPath))
        {
            await file.CopyToAsync(stream);
        }
        return newFileName;
    }
}
