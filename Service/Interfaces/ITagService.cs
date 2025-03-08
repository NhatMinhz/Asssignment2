using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAllTags();
        IEnumerable<Tag> GetTagsByIds(IEnumerable<int> tagIds);
        Tag GetTagById(int id);
        void AddTag(Tag tag);
        void UpdateTag(Tag tag);
        void DeleteTag(int id);
    }
}
