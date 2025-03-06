using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Service.Interfaces;

namespace FUNewsManagementSystem.Pages.Tags
{
    public class DeleteModel : PageModel
    {
        private readonly ITagService _tagService;

        public DeleteModel(ITagService tagService)
        {
            _tagService = tagService;
        }

        [BindProperty]
        public Tag Tag { get; set; } = default!;

        public IActionResult OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _tagService.GetTagById(id);

            if (tag == null)
            {
                return NotFound();
            }
            else
            {
                Tag = tag;
            }
            return Page();
        }

        public IActionResult OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _tagService.GetTagById(id);
            if (tag != null)
            {
                Tag = tag;
                _tagService.DeleteTag(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
