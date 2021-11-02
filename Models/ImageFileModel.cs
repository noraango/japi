using Microsoft.AspNetCore.Http;
namespace api.Models
{
    public class ImageFileModel
    {
        public IFormFile imageFile { get; set; }
    }
}