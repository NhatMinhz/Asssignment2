using BusinessObjects;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly FunewsManagementContext _dbContext;

        public UnitOfWork(FunewsManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        private ICategoryRepository? _categoryRepository;
        private INewsArticleRepository? _newsArticleRepository;
        private ISystemAccountRepository? _systemAccountRepository;
        private ITagRepository? _tagRepository;

        public ICategoryRepository CategoryRepository
            => _categoryRepository ??= new CategoryRepository(_dbContext);

        public INewsArticleRepository NewsArticleRepository
            => _newsArticleRepository ??= new NewsArticleRepository(_dbContext);

        public ISystemAccountRepository SystemAccountRepository
            => _systemAccountRepository ??= new SystemAccountRepository(_dbContext);

        public ITagRepository TagRepository
            => _tagRepository ??= new TagRepository(_dbContext);

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void RollBack()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
