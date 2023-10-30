using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Udlånssystem_API.DTOs;
using Udlånssystem_API.Models;
using Udlånssystem_API.Services.Interfaces;

namespace Udlånssystem_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrugerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBrugerService _brugerService;

        public BrugerController(IMapper mapper, IBrugerService brugerService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _brugerService = brugerService ?? throw new ArgumentNullException(nameof(brugerService));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBruger(int id)
        {
            var bruger = await _brugerService.GetBruger(id);
            var brugerDTO = _mapper.Map<BrugerDTO>(bruger);
            return Ok(brugerDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (login.Email == null || login.Adgangskode == null)
            {
                throw new ValidationException("Email and password must be provided.");
            }

            var user = await _brugerService.Login(login.Email, login.Adgangskode);

            var userDTO = _mapper.Map<LoginResponseDTO>(user);
            return Ok(userDTO);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] OpretBrugerDTO opretBrugerDto)
        {
            try
            {
                var createResult = await _brugerService.CreateUser(opretBrugerDto);

                var userDto = _mapper.Map<BrugerDTO>(createResult);
                return CreatedAtAction(nameof(GetBruger), new { id = userDto.BrugerID }, userDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}