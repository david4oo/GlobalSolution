using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GlobalSolution.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile);

    }
}
