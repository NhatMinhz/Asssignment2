﻿using FUNewsManagementSystem.DAL.Models;
using FUNewsManagementSystem.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagementSystem.Pages.NewsArticles
{
    public class DeleteModel : PageModel
    {
        private readonly INewArticleService _newArticleService;

        public DeleteModel(INewArticleService newArticleService)
        {
            _newArticleService = newArticleService;
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

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
            else
            {
                NewsArticle = newsarticle;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsarticle = await _newArticleService.GetNewsArticleByIdAsync(id);
            if (newsarticle != null)
            {
                NewsArticle = newsarticle;
                await _newArticleService.DeleteNewsArticleAsync(id);
            }

            return RedirectToPage("/NewsArticles/Index");
        }
    }
}
