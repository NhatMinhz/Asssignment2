using BusinessObjects;
using FUNewsManagementSystem.BLL.Interfaces;
using Repository.UOW;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _unitOfWork.CategoryRepository.AddCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(short id)
        {
            await _unitOfWork.CategoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.CategoryRepository.GetAllCategoriesAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(short id)
        {
            return await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task<bool> HasNewsInCategoryAsync(int categoryId)
        {
            return await _unitOfWork.NewsArticleRepository.HasNewsInCategoryAsync(categoryId);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _unitOfWork.CategoryRepository.UpdateCategoryAsync(category);
        }
    }
}
