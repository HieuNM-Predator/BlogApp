using BlogApp.API.Data;
using BlogApp.API.Models.Domain;
using BlogApp.API.Respositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.API.Respositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AppDbContext appDbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, AppDbContext appDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<BlogImage>> GetAll()
        {
            return await appDbContext.BlogImages.ToListAsync();
        }

        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {
            // Upload the Iamge to API/Images
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");

            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            //Update to DB
            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";


            blogImage.Url = urlPath;
            await appDbContext.BlogImages.AddAsync(blogImage);
            await appDbContext.SaveChangesAsync();

            return blogImage;
        }
    }
}
