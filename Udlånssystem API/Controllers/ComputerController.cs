using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udlånssystem_API.DTOs;
using Udlånssystem_API.Services.Implementations;
using Udlånssystem_API.Services.Interfaces;

namespace Udlånssystem_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IComputerService _computerService;
        private readonly IUdlånService _udlånService;

        public ComputersController(IMapper mapper, IComputerService computerService, IUdlånService udlånService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _computerService = computerService ?? throw new ArgumentNullException(nameof(computerService));
            _udlånService = udlånService ?? throw new ArgumentNullException(nameof(udlånService));
        }

        // GET: api/computers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComputerDetailsDTO>>> GetAllComputers()
        {
            var computers = await _computerService.GetAllComputersWithDetails(); 

            if (computers == null)
            {
                return NotFound(); // No computers found.
            }

            // Mapping computer data from the model to DTOs.
            var computerDTOs = _mapper.Map<IEnumerable<ComputerDetailsDTO>>(computers);

            return Ok(computerDTOs);
        }
        // GET: /computers/GetAllNonRentedComputers
        [HttpGet("GetAllNonRentedComputers")]
        public async Task<ActionResult<IEnumerable<ComputerDetailsDTO>>> GetAllNonRentedComputers()
        {
            try
            {
                // Fetch all computers and active loans.
                var allComputers = await _computerService.GetAllComputersWithDetails();
                var activeLoans = await _udlånService.GetActiveLoans();

                // Extract ComputerIDs from active loans.
                var rentedComputerIds = new HashSet<int>(activeLoans.Select(loan => loan.ComputerID));

                // Filter computers that are not in the active loans list.
                var availableComputers = allComputers.Where(computer => !rentedComputerIds.Contains(computer.ComputerID));

                // Mapping available computer data from the model to DTOs.
                var computerDTOs = _mapper.Map<IEnumerable<ComputerDetailsDTO>>(availableComputers);

                return Ok(computerDTOs);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logger)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}

