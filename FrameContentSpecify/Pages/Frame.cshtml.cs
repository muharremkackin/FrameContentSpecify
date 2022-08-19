using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrameContentSpecify.Pages
{
    public class FrameModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public FrameModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            
        }
    }
}
