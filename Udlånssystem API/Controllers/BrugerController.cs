using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Udlånssystem_API.DTOs;
using Udlånssystem_API.Models;
using Udlånssystem_API.Services.Interfaces;
using Udlånssystem_API.Repositories.Interfaces;
using Udlånssystem_API.Data;

namespace Udlånssystem_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrugerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBrugerRepository _repository;
        private readonly IBrugerGruppeService _brugerGruppeService;
        private readonly IStamklasseService _stamklasseService;
        private readonly IPostnrService _postnrService;  // New service for Postnr

        public BrugerController(
            IMapper mapper,
            IBrugerRepository repository,
            IBrugerGruppeService brugerGruppeService,
            IStamklasseService stamklasseService,
            IPostnrService postnrService) // Injecting Postnr service
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _brugerGruppeService = brugerGruppeService ?? throw new ArgumentNullException(nameof(brugerGruppeService));
            _stamklasseService = stamklasseService ?? throw new ArgumentNullException(nameof(stamklasseService));
            _postnrService = postnrService ?? throw new ArgumentNullException(nameof(postnrService));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrugerDTO>> GetBruger(int id)
        {
            var bruger = await _repository.GetById(id);

            if (bruger == null)
            {
                return NotFound();
            }

            var brugerDTO = _mapper.Map<BrugerDTO>(bruger);
            return Ok(brugerDTO);
        }

        [HttpPost("login")]
        public async Task<ActionResult<BrugerDTO>> Login([FromBody] LoginDTO login)
        {
            var user = await _repository.Login(login.Email, login.Adgangskode);

            if (user == null)
            {
                return Unauthorized();
            }

            var userDTO = _mapper.Map<LoginResponseDTO>(user);
            return Ok(userDTO);
        }

        [HttpPost("create")]
        public async Task<ActionResult<BrugerDTO>> CreateUser(OpretBrugerDTO opretBrugerDto)
        {
            // Check if the user already exists
            bool userExists = await _repository.UserExists(opretBrugerDto.Email, opretBrugerDto.CprNummer);
            if (userExists)
            {
                return BadRequest("User already exists with the same Email or CPR number.");
            }

            // Get or create BrugerGruppe based on the provided name
            var brugerGruppe = await _brugerGruppeService.GetOrCreateBrugerGruppeAsync(opretBrugerDto.BrugerGruppeNavn);
            opretBrugerDto.BrugerGruppeID = brugerGruppe.BrugerGruppeID; // Assign the ID

            // Get or create Stamklasse based on the provided name
            var stamklasse = await _stamklasseService.GetOrCreateStamklasseAsync(opretBrugerDto.StamklasseNavn);
            opretBrugerDto.StamklasseID = stamklasse.StamklasseID; // Assign the ID

            // Get or create Postnr based on the provided data
            var postnr = await _postnrService.GetOrCreatePostnrAsync(opretBrugerDto.PostnrID.ToString());
            opretBrugerDto.PostnrID = postnr.PostnrID; // Assign the ID

            // Map the DTO to the entity
            Bruger user = _mapper.Map<Bruger>(opretBrugerDto);

            await _repository.Create(user);

            // Map the newly created user to a DTO for the response
            var userDto = _mapper.Map<BrugerDTO>(user);

            return CreatedAtAction(nameof(GetBruger), new { id = user.BrugerID }, userDto);
        }

        // ... Other actions like Update, Delete, etc., can be added here.
    }
}



