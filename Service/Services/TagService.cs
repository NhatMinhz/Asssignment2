using FUNewsManagementSystem.DAL.Models;
using FUNewsManagementSystem.BLL.Interfaces;
using Repository.UOW;

namespace Service.Services
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await _unitOfWork.TagRepository.GetAllTagsAsync();
        }

        public async Task<IEnumerable<Tag>> GetTagsByIdsAsync(IEnumerable<int> tagIds)
        {
            return await _unitOfWork.TagRepository.GetTagsByIdsAsync(tagIds);
        }

        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _unitOfWork.TagRepository.GetTagByIdAsync(id);
        }

        public async Task AddTagAsync(Tag tag)
        {
            await _unitOfWork.TagRepository.AddTagAsync(tag);
        }

        public async Task UpdateTagAsync(Tag tag)
        {
            await _unitOfWork.TagRepository.UpdateTagAsync(tag);
        }

        public async Task DeleteTagAsync(int id)
        {
            await _unitOfWork.TagRepository.DeleteTagAsync(id);
        }
    }
}
