using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetopiaWebApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PetopiaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetImageController : ControllerBase
    {
        private readonly PetopiaDbContext _context;

        public PetImageController(PetopiaDbContext context)
        {
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImages([FromForm] PetImageUploadDto dto)
        {
            // Validate if the pet exists
            var petExists = await _context.PetProfiles.AnyAsync(p => p.PetId == dto.PetId);
            if (!petExists)
            {
                return NotFound($"No pet found with ID {dto.PetId}.");
            }

            // Check if there are more than 4 images
            if (dto.Images.Length > 4)
            {
                return BadRequest("You can upload up to 4 images at a time.");
            }

            foreach (var image in dto.Images)
            {
                if (image.Length > 0)
                {
                    // Define the folder path to save the images
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                    // Ensure the directory exists
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Create a unique file name
                    var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(folderPath, fileName);

                    // Save the image to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    // Save the image path to the database
                    var petImage = new PetImage
                    {
                        PetId = dto.PetId,
                        ImagePath = $"uploads/{fileName}"
                    };

                    _context.PetImages.Add(petImage);
                }
                else
                {
                    return BadRequest("One or more images are empty.");
                }
            }

            await _context.SaveChangesAsync();

            return Ok("Images uploaded successfully.");
        }

        //[HttpGet("get/{id}")]
        //public async Task<IActionResult> GetImagesByPetId(int id)
        //{
        //    // Retrieve images associated with the given Pet ID
        //    var images = await _context.PetImages
        //        .Where(p => p.PetId == id)
        //        .Select(p => new
        //        {
        //            p.ImageId,
        //            FileName = Path.GetFileName(p.ImagePath) // Get only the file name
        //        })
        //        .ToListAsync();

        //    if (images == null || !images.Any())
        //    {
        //        return NotFound("No images found for the given Pet ID.");
        //    }

        //    return Ok(images);
        //}

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetImagesByPetId(int id)
        {
            var images = await _context.PetImages
                .Where(p => p.PetId == id)
                .Select(p => new
                {
                    p.ImageId,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/uploads/{Path.GetFileName(p.ImagePath)}"
                })
                .ToListAsync();

            if (!images.Any())
            {
                return NotFound(new { Message = "No images found for the given Pet ID." });
            }

            return Ok(images);
        }


        [HttpGet("image/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            // Define the folder path where images are stored
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            // Combine the folder path with the file name to get the full file path
            var filePath = Path.Combine(folderPath, fileName);

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image not found.");
            }

            // Get the file extension to determine the content type
            var fileExtension = Path.GetExtension(filePath).ToLowerInvariant();
            string contentType;

            // Set the content type based on the file extension
            switch (fileExtension)
            {
                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".gif":
                    contentType = "image/gif";
                    break;
                default:
                    return BadRequest("Unsupported file type.");
            }

            // Return the file as a FileResult
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, contentType);
        }

        public class PetImageUploadDto
        {
            public int PetId { get; set; }
            public IFormFile[] Images { get; set; }
        }
    }
}


PetImage Controller