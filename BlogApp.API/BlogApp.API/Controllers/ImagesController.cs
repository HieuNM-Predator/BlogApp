﻿using BlogApp.API.Models.Domain;
using BlogApp.API.Models.DTO;
using BlogApp.API.Respositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await imageRepository.GetAll();

            var response = new List<BlogImage>();
            foreach (var image in images)
            {
                response.Add(new BlogImage
                {
                    Id = image.Id,
                    Title = image.Title,
                    DateCreated = image.DateCreated,
                    FileExtension = image.FileExtension,
                    FileName = image.FileName,
                    Url = image.Url,
                });
            }

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] string title)
        {

            ValidateFileUpload(file);

            if(ModelState.IsValid)
            {
                // File Upload
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now,
                };

                blogImage = await imageRepository.Upload(file, blogImage);


                var response = new BlogImageDto
                {
                    Id = blogImage.Id,
                    Title = blogImage.Title,
                    DateCreated = blogImage.DateCreated,
                    FileExtension = blogImage.FileExtension,
                    FileName = blogImage.FileName,
                    Url = blogImage.Url,
                };

                return Ok(response);
            }

            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            
            if(!allowExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }

            // check size > 10MB
            if(file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB");            };

        }
    }
}