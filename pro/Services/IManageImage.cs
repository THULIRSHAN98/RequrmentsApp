
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace pro.Services

{
    public interface IManageImage
    {
        Task<string> UploadFile(IFormFile _IFormFile);
        Task<(byte[], string, string)> DownloadFile(string FileName);

        Task<bool> DeleteFile(string FileName);
    }
}
