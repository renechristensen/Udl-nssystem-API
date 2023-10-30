using System;
using System.Threading.Tasks;
using AutoMapper;
using Udlånssystem_API.Models;
using Udlånssystem_API.DTOs;
using Udlånssystem_API.Services.Interfaces;
using Udlånssystem_API.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Udlånssystem_API.Services.Implementations
{
    public class BrugerService : IBrugerService
    {
        private readonly IBrugerRepository _repository;
        private readonly IBrugerGruppeService _brugerGruppeService;
        private readonly IStamklasseService _stamklasseService;
        private readonly IPostnrService _postnrService;
        private readonly IMapper _mapper;
        private readonly ILogger<BrugerService> _logger;

        public BrugerService(IBrugerRepository repository, IBrugerGruppeService brugerGruppeService, IStamklasseService stamklasseService, IPostnrService postnrService, IMapper mapper, ILogger<BrugerService> logger)
        {
            _repository = repository;
            _brugerGruppeService = brugerGruppeService;
            _stamklasseService = stamklasseService;
            _postnrService = postnrService;
            _mapper = mapper;
            _logger = logger; // Ensure logging is available for exception handling and other important logs
        }

        public async Task<BrugerDTO> GetBruger(int id)
        {
            var bruger = await _repository.GetById(id);
            if (bruger == null)
            {
                _logger.LogError($"No user found with ID {id}.");
                throw new NotFoundException($"User with ID {id} could not be found.");
            }

            return _mapper.Map<BrugerDTO>(bruger);
        }

        public async Task<BrugerDTO> GetBruger(string elevNummer)
        {
            var bruger = await _repository.GetByElevNummer(elevNummer);
            if (bruger == null)
            {
                _logger.LogError($"No user found with Elev Nummer {elevNummer}.");
                throw new NotFoundException($"User with Elev Nummer {elevNummer} could not be found.");
            }

            return _mapper.Map<BrugerDTO>(bruger);
        }

        public async Task<LoginResponseDTO> Login(string email, string password)
        {
            var user = await _repository.Login(email, password);
            if (user == null)
            {
                _logger.LogError($"Invalid login attempt for user with email: {email}.");
                throw new UnauthorizedAccessException("Invalid login attempt.");
            }

            return _mapper.Map<LoginResponseDTO>(user);
        }

        public async Task<BrugerDTO> CreateUser(OpretBrugerDTO opretBrugerDto)
        {
            bool userExists = await _repository.UserExists(opretBrugerDto.Email, opretBrugerDto.CprNummer);

            if (userExists)
            {
                throw new ValidationException("User already exists with the same Email or CPR number.");
            }

            var brugerGruppe = await _brugerGruppeService.GetOrCreateBrugerGruppeAsync(opretBrugerDto.BrugerGruppeNavn);
            var stamklasse = await _stamklasseService.GetOrCreateStamklasseAsync(opretBrugerDto.StamklasseNavn);
            var postnr = await _postnrService.GetOrCreatePostnrAsync(opretBrugerDto.PostNrByNavn);

            Bruger user = _mapper.Map<Bruger>(opretBrugerDto);

            user.BrugerGruppeID = brugerGruppe.BrugerGruppeID;
            user.StamklasseID = stamklasse.StamklasseID;
            user.PostnrID = postnr.PostnrID;
            user.Blacklisted = false;
            user.BrugerGruppe = await _brugerGruppeService.GetBrugerGruppeAsync(user.BrugerGruppeID);
            user.Stamklasse = await _stamklasseService.GetOrCreateStamklasseAsync(opretBrugerDto.StamklasseNavn);
            user.Postnr = await _postnrService.GetOrCreatePostnrAsync(opretBrugerDto.PostNrByNavn);
            try
            {
                await _repository.Create(user);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw; 
            }

            return _mapper.Map<BrugerDTO>(user);
        }
    }
}