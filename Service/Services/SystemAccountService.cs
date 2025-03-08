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
    public class SystemAccountService : ISystemAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SystemAccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAccountAsync(SystemAccount account)
        {
            await _unitOfWork.SystemAccountRepository.AddAccountAsync(account);
        }

        public async Task DeleteAccountAsync(short id)
        {
            await _unitOfWork.SystemAccountRepository.DeleteAccountAsync(id);
        }   

        public async Task<SystemAccount?> GetAccountByIdAsync(short id)
        {
            return await _unitOfWork.SystemAccountRepository.GetAccountByIdAsync(id);
        }

        public async Task<IEnumerable<SystemAccount>> GetAllAccountsAsync()
        {
            return await _unitOfWork.SystemAccountRepository.GetAllAccountsAsync();
        }

        public async Task UpdateAccountAsync(SystemAccount account)
        {
            await _unitOfWork.SystemAccountRepository.UpdateAccountAsync(account);
        }
    }
}
