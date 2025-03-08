using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Service.Interfaces;

namespace FUNewsManagementSystem.Pages.NewsArticles
{
    public class EditModel : PageModel
    {
        private readonly INewArticleService _newArticleService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly ISystemAccountService _systemAccountService;

        public EditModel(INewArticleService newArticleService, ICategoryService categoryService, ITagService tagService, ISystemAccountService systemAccountService)
        {
            _newArticleService = newArticleService;
            _categoryService = categoryService;
            _tagService = tagService;
            _systemAccountService = systemAccountService;
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        [BindProperty]
        public List<int> ListTag { get; set; } = default!;

        public SelectList Categories { get; set; }
        public SelectList Tags { get; set; }
        public SelectList SystemAccounts { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsarticle = await _newArticleService.GetNewsArticleByIdAsync(id);
            if (newsarticle == null)
            {
                return NotFound();
            }
            NewsArticle = newsarticle;
            Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "CategoryDesciption");
            SystemAccounts = new SelectList(await _systemAccountService.GetAllAccountsAsync(), "AccountId", "AccountId");
            Tags = new SelectList(_tagService.GetAllTags(), "TagId", "TagName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _newArticleService.UpdateNewsArticleAsync(NewsArticle, ListTag);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsArticleExists(NewsArticle.NewsArticleId))
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

        private bool NewsArticleExists(string id)
        {
            return _newArticleService.GetNewsArticleByIdAsync(id) == null;
        }
    }
}
