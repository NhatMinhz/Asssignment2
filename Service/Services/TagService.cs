using BusinessObjects;
using Repository.UOW;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddTag(Tag tag)
        {
            _unitOfWork.TagRepository.AddTag(tag);
        }

        public void DeleteTag(int id)
        {
            _unitOfWork.TagRepository.DeleteTag(id);
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _unitOfWork.TagRepository.GetAllTags();
        }

        public Tag GetTagById(int id)
        {
            return _unitOfWork.TagRepository.GetTagById(id);
        }

        public IEnumerable<Tag> GetTagsByIds(IEnumerable<int> tagIds)
        {
            return _unitOfWork.TagRepository.GetTagsByIds(tagIds);
        }

        public void UpdateTag(Tag tag)
        {
            _unitOfWork.TagRepository.UpdateTag(tag);
        }
    }
}
