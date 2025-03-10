using BusinessObjects;
using FUNewsManagementSystem.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        public List<int> ListTag { get; set; } = new List<int>(); 

        public SelectList Categories { get; set; }
        public MultiSelectList Tags { get; set; } 
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
            ListTag = newsarticle.NewsTags?.Select(t => t.TagId).ToList() ?? new List<int>();

            Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "CategoryDesciption");
            SystemAccounts = new SelectList(await _systemAccountService.GetAllAccountsAsync(), "AccountId", "AccountId");
            Tags = new SelectList(await _tagService.GetAllTagsAsync(), "TagId", "TagName");
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var allTags = await _tagService.GetAllTagsAsync();
                Tags = new MultiSelectList(allTags, "TagId", "TagName");
                return Page();
            }

            try
            {
                var selectedTagIds = Request.Form["ListTag"].Select(int.Parse).ToList();
                await _newArticleService.UpdateNewsArticleAsync(NewsArticle, selectedTagIds);
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
            return _newArticleService.GetNewsArticleByIdAsync(id) != null;
        }
    }
}
