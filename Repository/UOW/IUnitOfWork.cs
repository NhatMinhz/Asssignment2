using FUNewsManagementSystem.DAL.Interfaces;

namespace Repository.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        INewsArticleRepository NewsArticleRepository { get; }
        ISystemAccountRepository SystemAccountRepository { get; }
        ITagRepository TagRepository { get; }
        void Save();
        Task SaveAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollBack();
    }
}
