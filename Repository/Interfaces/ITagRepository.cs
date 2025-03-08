using BusinessObjects;
using Repository.Interfaces;

namespace Repository.Interfaces
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAllTags();
        IEnumerable<Tag> GetTagsByIds(IEnumerable<int> tagIds);
        Tag GetTagById(int id);
        void AddTag(Tag tag);
        void UpdateTag(Tag tag);
        void DeleteTag(int id);
    }
}
