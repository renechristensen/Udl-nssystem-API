using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udlånssystem_API.Models;
using Udlånssystem_API.Repositories.Interfaces;
using Udlånssystem_API.Services.Interfaces;

namespace Udlånssystem_API.Services.Implementations
{
    public class UdlånService : IUdlånService
    {
        private readonly IUdlånRepository _udlånRepository;

        public UdlånService(IUdlånRepository udlånRepository)
        {
            _udlånRepository = udlånRepository ?? throw new ArgumentNullException(nameof(udlånRepository));
        }

        public async Task<List<Udlån>> GetAll()
        {
            return await _udlånRepository.GetAll();
        }

        public async Task<Udlån> GetById(int id)
        {
            return await _udlånRepository.GetById(id);
        }

        public async Task Create(Udlån udlån)
        {
            await _udlånRepository.Create(udlån);
        }

        public async Task Update(Udlån udlån)
        {
            await _udlånRepository.Update(udlån);
        }

        public async Task Delete(int id)
        {
            await _udlånRepository.Delete(id);
        }

        public async Task<List<Udlån>> GetActiveLoans()
        {
            return await _udlånRepository.GetActiveLoans();
        }
    }
}
