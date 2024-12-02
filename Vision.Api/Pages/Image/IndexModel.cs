using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using Vision.Application.Services;

namespace Vision.Api.Pages.Image
{
    public class IndexModel : PageModel
    {
        private readonly IGridFsService _gridFsService;

        public IndexModel(IGridFsService gridFsService)
        {
            _gridFsService = gridFsService;
        }

        [BindProperty(SupportsGet = true)]
        public IFormFile UploadFile { get; set; }

        public List<GridFSFileInfo<ObjectId>> Files { get; set; }

        public async Task OnGetAsync()
        {
            Files = await _gridFsService.GetAllFilesAsync();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (UploadFile == null || UploadFile.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "No file uploaded.");
                return Page();
            }

            using (var stream = UploadFile.OpenReadStream())
            {
                await _gridFsService.UploadImageAsync(stream, UploadFile.FileName);
            }

            return RedirectToPage();
        }
    }
}