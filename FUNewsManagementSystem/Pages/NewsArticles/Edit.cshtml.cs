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
using Microsoft.AspNetCore.SignalR;
using FUNewsManagementSystem.Hubs;

namespace FUNewsManagementSystem.Pages.NewsArticles
{
    public class EditModel : PageModel
    {
        private readonly INewArticleService _newArticleService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly IHubContext<SignalRServer> _hubContext;
        private readonly ISystemAccountService _systemAccountService;

        public EditModel(INewArticleService newArticleService, 
            ICategoryService categoryService, 
            ITagService tagService, 
            ISystemAccountService systemAccountService,
            IHubContext<SignalRServer> hubContext)
        {
            _newArticleService = newArticleService;
            _categoryService = categoryService;
            _tagService = tagService;
            _systemAccountService = systemAccountService;
            _hubContext = hubContext;
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
                return NotFound(new { success = false, message = "Invalid ID" });
            }

            var newsarticle = await _newArticleService.GetNewsArticleByIdAsync(id);
            if (newsarticle == null)
            {
                return NotFound(new { success = false, message = "News article not found" });
            }

            var response = new
            {
                success = true,
                newsArticle = newsarticle,
                categories = await _categoryService.GetAllCategoriesAsync(),
                systemAccounts = await _systemAccountService.GetAllAccountsAsync(),
                tags = _tagService.GetAllTags()
            };

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ListTag == null || !ListTag.Any())
            {
                ModelState.AddModelError("ListTag", "Bạn phải chọn ít nhất 1 tag");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(ms => ms.Value.Errors.Any())
                               .ToDictionary(
                                   kvp => kvp.Key,
                                   kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                               );

                return new JsonResult(new { success = false, errors });
            }

            try
            {
                await _newArticleService.UpdateNewsArticleAsync(NewsArticle, ListTag);
                await _hubContext.Clients.All.SendAsync("LoadNewsArticlesItem");
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
