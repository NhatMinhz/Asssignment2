using BusinessObjects;

namespace FUNewsManagementSystem.DAL.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<IEnumerable<Tag>> GetTagsByIdsAsync(IEnumerable<int> tagIds);
        Task<Tag?> GetTagByIdAsync(int id);
        Task AddTagAsync(Tag tag);
        Task UpdateTagAsync(Tag tag);
        Task DeleteTagAsync(int id);
    }
}
