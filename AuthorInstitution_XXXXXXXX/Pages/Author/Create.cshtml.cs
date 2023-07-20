using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System.Globalization;

namespace AuthorInstitution_XXXXXXXX.Pages.Author
{
    public class CreateModel : PageModel
    {
        private readonly ServiceBase<CorrespondingAuthor> _context;
        private readonly ServiceBase<InstitutionInformation> _contextInfo;

        public CreateModel()
        {
            _context = new ServiceBase<CorrespondingAuthor>();
            _contextInfo = new ServiceBase<InstitutionInformation>();
        }

        public IActionResult OnGet()
        {
            ViewData["InstitutionId"] = new SelectList(_contextInfo.GetAll().ToList(), "InstitutionId", "InstitutionName");
            return Page();
        }

        [BindProperty]
        public CorrespondingAuthor CorrespondingAuthor { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["InstitutionId"] = new SelectList(_contextInfo.GetAll().ToList(), "InstitutionId", "InstitutionName");
            if (!ModelState.IsValid || CorrespondingAuthor == null)
            {
                return Page();
            }

            if (CorrespondingAuthorExists(CorrespondingAuthor.AuthorId))
            {
                ModelState.AddModelError(string.Empty, "AuthorId already exists.");
                return Page();
            }

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            CorrespondingAuthor.AuthorName = textInfo.ToTitleCase(CorrespondingAuthor.AuthorName);
            _context.Add(CorrespondingAuthor);

            return RedirectToPage("./Index");
        }

        private bool CorrespondingAuthorExists(string id)
        {
            return (_context.GetAll()?.Any(e => e.AuthorId == id)).GetValueOrDefault();
        }
    }
}
