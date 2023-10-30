using Udlånssystem_API.Models;

namespace Udlånssystem_API.Services.Interfaces
{
    public interface IUdlånService
    {
        Task<List<Udlån>> GetAll();
        Task<Udlån> GetById(int id);
        Task<int> Create(Udlån udlån);
        Task Update(Udlån udlån);
        Task Delete(int id);
        Task<List<Udlån>> GetActiveLoans();
        Task<bool> ReturnLoanByComputerID(int ComputerID);
    }
}

