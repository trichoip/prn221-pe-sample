using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace AuthorInstitution_XXXXXXXX.Pages.Author
{
    public class DetailsModel : PageModel
    {
        private readonly ServiceBase<CorrespondingAuthor> _context;

        public DetailsModel()
        {
            _context = new ServiceBase<CorrespondingAuthor>();
        }

        public CorrespondingAuthor CorrespondingAuthor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.GetAll() == null)
            {
                return NotFound();
            }

            var correspondingauthor = await _context.GetAll().Include(c => c.Institution).FirstOrDefaultAsync(m => m.AuthorId == id);
            if (correspondingauthor == null)
            {
                return NotFound();
            }
            else
            {
                CorrespondingAuthor = correspondingauthor;
            }
            return Page();
        }
    }
}
