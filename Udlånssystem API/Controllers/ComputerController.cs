using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udlånssystem_API.DTOs;
using Udlånssystem_API.Models;
using Udlånssystem_API.Services.Interfaces;

namespace Udlånssystem_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IComputerService _computerService;
        private readonly IUdlånService _udlånService;

        public ComputerController(IMapper mapper, IComputerService computerService, IUdlånService udlånService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _computerService = computerService ?? throw new ArgumentNullException(nameof(computerService));
            _udlånService = udlånService ?? throw new ArgumentNullException(nameof(udlånService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComputerDetailsDTO>>> GetAllComputers()
        {
            var computers = await _computerService.GetAllComputersWithDetails();
            var computerDTOs = _mapper.Map<IEnumerable<ComputerDetailsDTO>>(computers);
            return Ok(computerDTOs);
        }

        [HttpGet("GetAllNonRentedComputers")]
        public async Task<ActionResult<IEnumerable<ComputerDetailsDTO>>> GetAllNonRentedComputers()
        {
            
            List<Computer> allComputers = await _computerService.GetAllComputersWithDetails();
            List<Udlån> activeLoans = await _udlånService.GetActiveLoans();

            var rentedComputerIds = new HashSet<int>(activeLoans.Select(loan => loan.ComputerID));
            var availableComputers = allComputers.Where(computer => !rentedComputerIds.Contains(computer.ComputerID));

            var computerDTOs = _mapper.Map<IEnumerable<ComputerDetailsDTO>>(availableComputers);
            return Ok(computerDTOs);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterComputer([FromBody] ComputerRegistrationDTO computerDTO)
        {
            var computer = await _computerService.RegisterComputer(computerDTO);

            return CreatedAtAction(nameof(GetComputerDetails), new { id = computer.ComputerID }, computer);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComputerDetailsDTO>> GetComputerDetails(int id)
        {
            var computer = await _computerService.GetComputerDetails(id);
            var computerDTO = _mapper.Map<ComputerDetailsDTO>(computer);
            return Ok(computerDTO);
        }
    }
}

