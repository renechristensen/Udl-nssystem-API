﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udlånssystem_API.Models;

namespace Udlånssystem_API.Repositories.Interfaces
{
    public interface IUdlånRepository
    {
        Task<List<Udlån>> GetAll();
        Task<Udlån> GetById(int id);
        Task<int> Create(Udlån udlån);
        Task Update(Udlån udlån);
        Task Delete(int id);
        Task<List<Udlån>> GetActiveLoans();
        Task<Udlån> FindActiveLoanByComputerID(int computerID);

    }
}
