using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using FUNewsManagementSystem.ViewModel;
using FUNewsManagementSystem.BLL.Interfaces;

namespace FUNewsManagementSystem.Pages.NewsArticles
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly INewArticleService _newArticleService;
        private readonly ISystemAccountService _systemAccountService;

        public CreateModel(ICategoryService categoryService, ITagService tagService, INewArticleService newArticleService, ISystemAccountService systemAccountService)
        {
            _categoryService = categoryService;
            _tagService = tagService;
            _newArticleService = newArticleService;
            _systemAccountService = systemAccountService;
        }

        public SelectList Categories { get; set; }
        public SelectList TagIds { get; set; }

        [BindProperty]
        public CreateNewArticleViewModel NewsArticle { get; set; } = new CreateNewArticleViewModel();

        public string SuccessMessage { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "CategoryName");

            var tags = await _tagService.GetAllTagsAsync() ?? new List<Tag>();
            ViewData["Tags"] = tags.Select(t => new SelectListItem { Value = t.TagId.ToString(), Text = t.TagName }).ToList();

            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "CategoryName");
                ViewData["Tags"] = (await _tagService.GetAllTagsAsync())?
                    .Select(t => new SelectListItem { Value = t.TagId.ToString(), Text = t.TagName })
                    .ToList();
                return Page();
            }

            try
            {
                var selectedTagIds = Request.Form["NewsArticle.TagsId"].Select(int.Parse).ToList();

                await _newArticleService.AddNewsArticleAsync(new NewsArticle
                {
                    NewsArticleId = NewsArticle.NewsArticleId,
                    CreatedById = NewsArticle.CreatedById,
                    CreatedDate = NewsArticle.CreatedDate ?? DateTime.Now,
                    NewsTitle = NewsArticle.NewsTitle,
                    Headline = NewsArticle.Headline,
                    NewsContent = NewsArticle.NewsContent,
                    NewsSource = NewsArticle.NewsSource,
                    CategoryId = NewsArticle.CategoryId,
                    NewsStatus = NewsArticle.NewsStatus
                }, selectedTagIds);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error creating the news article: " + ex.Message;
                return Page();
            }

            TempData["SuccessMessage"] = "News article created successfully!";
            return RedirectToPage("./Index");
        }


    }
}
