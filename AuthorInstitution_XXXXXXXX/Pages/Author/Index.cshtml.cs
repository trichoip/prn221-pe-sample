using AuthorInstitution_XXXXXXXX.Paging;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace AuthorInstitution_XXXXXXXX.Pages.Author
{
    public class IndexModel : PageModel
    {
        private readonly ServiceBase<CorrespondingAuthor> _context;
        public PaginatedList<CorrespondingAuthor> CorrespondingAuthor { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchField { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? pageIndex { get; set; }

        public IndexModel()
        {
            _context = new ServiceBase<CorrespondingAuthor>();
        }

        public async Task OnGetAsync()
        {

            IQueryable<CorrespondingAuthor> CorrespondingAuthorIQ = _context.GetAll().Include(c => c.Institution).OrderByDescending(c => c.AuthorId);

            ViewData["SearchField"] = new SelectList(new List<string> { "AuthorName", "Skills" });

            if (!String.IsNullOrEmpty(SearchString) && !String.IsNullOrEmpty(SearchField))
            {
                switch (SearchField)
                {
                    case "AuthorName":
                        CorrespondingAuthorIQ = CorrespondingAuthorIQ.Where(s => s.AuthorName.Contains(SearchString));
                        break;
                    case "Skills":
                        CorrespondingAuthorIQ = CorrespondingAuthorIQ.Where(s => s.Skills.Contains(SearchString));
                        break;
                }
            }

            var pageSize = 4;
            CorrespondingAuthor = await PaginatedList<CorrespondingAuthor>.CreateAsync(CorrespondingAuthorIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
