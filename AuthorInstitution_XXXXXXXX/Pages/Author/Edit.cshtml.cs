using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Globalization;

namespace AuthorInstitution_XXXXXXXX.Pages.Author
{
    public class EditModel : PageModel
    {
        private readonly ServiceBase<CorrespondingAuthor> _context;
        private readonly ServiceBase<InstitutionInformation> _contextInfo;

        public EditModel()
        {
            _context = new ServiceBase<CorrespondingAuthor>();
            _contextInfo = new ServiceBase<InstitutionInformation>();
        }

        [BindProperty]
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
            CorrespondingAuthor = correspondingauthor;
            ViewData["InstitutionId"] = new SelectList(_contextInfo.GetAll().ToList(), "InstitutionId", "InstitutionName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["InstitutionId"] = new SelectList(_contextInfo.GetAll().ToList(), "InstitutionId", "InstitutionName");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                CorrespondingAuthor.AuthorName = textInfo.ToTitleCase(CorrespondingAuthor.AuthorName);
                _context.Update(CorrespondingAuthor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorrespondingAuthorExists(CorrespondingAuthor.AuthorId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CorrespondingAuthorExists(string id)
        {
            return (_context.GetAll()?.Any(e => e.AuthorId == id)).GetValueOrDefault();
        }
    }
}
