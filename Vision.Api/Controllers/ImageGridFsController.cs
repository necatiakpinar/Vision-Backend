// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using MongoDB.Bson;
// using Vision.Application.Services;
//
// [Route("/gridf/image")]
// [AllowAnonymous]
// public class ImageGridFsController : ControllerBase
// {
//     private readonly IGridFsService _gridFsService;
//
//     public ImageGridFsController(IGridFsService gridFsService)
//     {
//         _gridFsService = gridFsService;
//     }
//
//     [HttpPost("[action]")]
//     public async Task<IActionResult> UploadImage(IFormFile file)
//     {
//         if (file == null || file.Length == 0)
//         {
//             return BadRequest("No file uploaded.");
//         }
//
//         using (var stream = file.OpenReadStream())
//         {
//             var objectId = await _gridFsService.UploadImageAsync(stream, file.FileName);
//
//             if (objectId == ObjectId.Empty)
//             {
//                 return BadRequest("File upload failed.");
//             }
//
//             Console.WriteLine($"File uploaded with ID: {objectId}");
//         }
//
//         return RedirectToPage("/Image/Index");
//     }
//
//     [HttpGet("[action]")]
//     public async Task<IActionResult> GetImage(string id)
//     {
//         if (string.IsNullOrEmpty(id))
//         {
//             return BadRequest("Invalid image ID.");
//         }
//
//         if (!ObjectId.TryParse(id, out ObjectId objectId))
//         {
//             return BadRequest("Invalid ObjectId format.");
//         }
//
//         try
//         {
//             var imageBytes = await _gridFsService.DownloadImageAsync(objectId);
//
//             if (imageBytes == null || imageBytes.Length == 0)
//             {
//                 return NotFound("Image not found.");
//             }
//
//             return File(imageBytes, "image/jpeg");
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, $"Error retrieving image: {ex.Message}");
//         }
//     }
//
//     [HttpGet("[action]")]
//     public async Task<IActionResult> GetAllImages()
//     {
//         var files = await _gridFsService.GetAllFilesAsync();
//         return Ok(files);
//     }
// }