using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udlånssystem_API.Models;
using Udlånssystem_API.Repositories.Implementations;
using Udlånssystem_API.Repositories.Interfaces;
using Udlånssystem_API.Services.Interfaces;

namespace Udlånssystem_API.Services
{
    public class ComputerService : IComputerService
    {
        private readonly IComputerRepository _computerRepository;
        private readonly IFabrikatRepository _fabrikatRepository;
        private readonly IMusModelRepository _musModelRepository;
        private readonly IComputerModelRepository _computerModelRepository;
        private readonly IUdlånRepository _udlånRepository;

        public ComputerService(
            IComputerRepository computerRepository,
            IFabrikatRepository fabrikatRepository,
            IMusModelRepository musModelRepository,
            IComputerModelRepository computerModelRepository,
            IUdlånRepository udlånRepository)
        {
            _computerRepository = computerRepository;
            _fabrikatRepository = fabrikatRepository;
            _musModelRepository = musModelRepository;
            _computerModelRepository = computerModelRepository;
            _udlånRepository = udlånRepository;

        }

        public async Task<List<Computer>> GetAllComputersWithDetails()
        {
            return await _computerRepository.GetAllComputers();
        }
        public async Task<Computer> GetComputerDetails(int id)
        {
            return await _computerRepository.GetComputerById(id);
        }

        public async Task<Computer> GetComputerDetails(string RegistreringsNummer)
        {
            return await _computerRepository.GetComputerByRegistreringsNummer(RegistreringsNummer);
        }
        public async Task<Computer> RegisterComputer(ComputerRegistrationDTO registrationDTO)
        {
            // Check if the Fabrikat already exists
            var existingFabrikat = await _fabrikatRepository.GetFabrikatByName(registrationDTO.Fabrikat);
            if (existingFabrikat == null)
            {
                existingFabrikat = new Fabrikat { FabrikatNavn = registrationDTO.Fabrikat };
                await _fabrikatRepository.Create(existingFabrikat);
            }

            // Check if the MusModel already exists
            var existingMusModel = await _musModelRepository.GetByTypeAsync(registrationDTO.Mus);
            if (existingMusModel == null)
            {
                existingMusModel = new MusModel { MusType = registrationDTO.Mus };
                await _musModelRepository.CreateAsync(existingMusModel);
            }

            // Check if the ComputerModel already exists
            var existingComputerModel = await _computerModelRepository.GetByModelNavnAsync(registrationDTO.Model);
            if (existingComputerModel == null)
            {
                existingComputerModel = new ComputerModel
                {
                    ModelNavn = registrationDTO.Model,
                    FabrikatID = existingFabrikat.FabrikatID
                };
                // Correcting the method name below
                await _computerModelRepository.CreateAsync(existingComputerModel);
            }

            // Now, we create the Computer with references to the existing or newly created entities.
            var newComputer = new Computer
            {
                MusModelID = existingMusModel.MusModelID,
                ComputerModelID = existingComputerModel.ComputerModelID,
                RegistreringsNummer = registrationDTO.RegistreringsNummer 
            };

            await _computerRepository.AddComputer(newComputer);
            return newComputer;
        }
        public async Task<List<Computer>> GetRentedComputersByUserId(int userId)
        {
            // Use the _udlånRepository to access the Udlån data
            var rentedComputers = await _udlånRepository.GetActiveLoans(); 

            var userRentedComputers = rentedComputers
                .Where(udlån => udlån.BrugerID == userId)                                         
                .Select(udlån => udlån.Computer)
                .ToList(); // ToList is called on the filtered IEnumerable, not on the async task

            return userRentedComputers;
        }

    }
}
