using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udlånssystem_API.DTOs;
using Udlånssystem_API.Models;
using Udlånssystem_API.Services.Implementations;
using Udlånssystem_API.Services.Interfaces;

namespace Udlånssystem_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UdlånController : ControllerBase
    {
        private readonly IUdlånService _udlånService;
        private readonly IComputerService _computerService;
        private readonly IBrugerService _brugerService;
        private readonly IMapper _mapper;

        public UdlånController(IUdlånService udlånService, IMapper mapper, IComputerService computerService, IBrugerService brugerService)
        {
            _udlånService = udlånService ?? throw new ArgumentNullException(nameof(udlånService));
            _computerService = computerService ?? throw new ArgumentNullException(nameof(computerService));
            _brugerService = brugerService ?? throw new ArgumentNullException(nameof(brugerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("GetAllRentedComputers")]
        public async Task<ActionResult<IEnumerable<RentedComputerDetailsDTO>>> GetAllRentedComputers()
        {
            List<Udlån> activeLoans = await _udlånService.GetActiveLoans();

            var rentedComputersDetails = activeLoans.Select(loan => _mapper.Map<RentedComputerDetailsDTO>(loan)).ToList();
            return Ok(rentedComputersDetails);
        }

        [HttpPost("RentComputer")]
        public async Task<IActionResult> RentComputer([FromBody] ReserveComputerRequestDTO request)
        {
            // Map the DTO to your domain entity
            //var newLoan = _mapper.Map<Udlån>(request);
            Udlån newLoan = new Udlån();
            BrugerDTO brugerDTO = await _brugerService.GetBruger(request.ElevNummer);
            if (brugerDTO == null)
            {
                return NotFound(new { message = "No Bruger found with the provided elev nummer." });
            }

            var computer = await _computerService.GetComputerDetails(request.RegistreringsNummer);
            if (computer == null)
            {
                return NotFound(new { message = "No computer found with the provided ID." });
            }
            newLoan.BrugerID = brugerDTO.BrugerID;
            newLoan.ComputerID = computer.ComputerID;
            newLoan.Udlånsdato = request.Udlånsdato;
            newLoan.Udløbsdato = request.Udløbsdato;
            newLoan.Computer = computer;
            newLoan.Status = "Reserveret";
            int udlånID = await _udlånService.Create(newLoan);
            Console.WriteLine(newLoan.UdlånID);

            return Ok(new { UdlånID = udlånID });
        }

        [HttpPost("AfleverLån")]
        public async Task<IActionResult> AfleverLån([FromBody] ReturnLoanRequestDTO returnRequest)
        {
            var details = await _computerService.GetComputerDetails(returnRequest.RegistreringsNummer);
            var success = await _udlånService.ReturnLoanByComputerID(details.ComputerID);

            if (!success)
            {
                return NotFound(new { message = "No active loan found with the provided computer ID." });
            }

            return Ok(new { message = "Loan successfully returned." });
        }

        [HttpGet("GetRentedComputersByUserID/{userId}")]
        public async Task<ActionResult<IEnumerable<RentedComputerDetailsDTO>>> GetRentedComputersByUserID(int userId)
        {
            // Retrieve all active loans.
            List<Udlån> activeLoans = await _udlånService.GetActiveLoans();

            // Retrieve the computers rented by the specific user.
            var rentedComputers = await _computerService.GetRentedComputersByUserId(userId);

            // Prepare a list to hold the data transfer objects.
            var rentedComputerDTOs = new List<RentedComputerDetailsDTO>();

            // Iterate through all computers rented by the user.
            foreach (var computer in rentedComputers)
            {
                // Find the corresponding loan for the current computer.
                var correspondingLoan = activeLoans.FirstOrDefault(loan => loan.ComputerID == computer.ComputerID && loan.BrugerID == userId);

                // If a corresponding loan is found, create a DTO containing details from both the computer and the loan.
                if (correspondingLoan != null)
                {
                    var dto = new RentedComputerDetailsDTO
                    {
                        UdlånID = correspondingLoan.UdlånID,
                        ComputerID = computer.ComputerID,
                        RegistreringsNummer = computer.RegistreringsNummer,
                        ModelNavn = computer.ComputerModel?.ModelNavn, // This assumes your Computer object is eager loading ComputerModel
                        FabrikatNavn = computer.ComputerModel?.Fabrikat?.FabrikatNavn, // This assumes your ComputerModel object is eager loading Fabrikat
                        MusType = computer.MusModel?.MusType, // This assumes your Computer object is eager loading MusModel
                        Udlånsdato = correspondingLoan.Udlånsdato,
                        Udløbsdato = correspondingLoan.Udløbsdato,
                        Status = correspondingLoan.Status
                    };

                    rentedComputerDTOs.Add(dto);
                }
            }

            // Return the list of constructed DTOs.
            return Ok(rentedComputerDTOs);
        }
        [HttpDelete("DeleteUdlaan/{id}")]
        public async Task<IActionResult> DeleteUdlaan(int id)
        {
            var loanToDelete = await _udlånService.GetById(id);
            if (loanToDelete == null)
            {
                return NotFound(new { message = "No loan found with the provided ID." });
            }

            await _udlånService.Delete(id);
            return NoContent();
        }
        [HttpGet("/Udlån/GetUdlånsOversigt")]
        public async Task<ActionResult<IEnumerable<UdlånOversigtDetailsDTO>>> GetUdlaansOversigt()
        {
            List<Udlån> udlånnene = await _udlånService.GetActiveLoans();
            List<UdlånOversigtDetailsDTO> udlånOversigt = new List<UdlånOversigtDetailsDTO>();
            foreach (Udlån udlån in udlånnene)
            {
                UdlånOversigtDetailsDTO udlånDetails = new UdlånOversigtDetailsDTO();
                udlånDetails.Udlånsdato = udlån.Udlånsdato;
                udlånDetails.Udløbsdato = udlån.Udløbsdato;
                udlånDetails.UdlånID = udlån.UdlånID;
                Computer computer = await _computerService.GetComputerDetails(udlån.ComputerID);
                udlånDetails.RegistreringsNummer = computer.RegistreringsNummer;
                BrugerDTO bruger = await _brugerService.GetBruger(udlån.BrugerID);
                udlånDetails.ElevNummer = bruger.ElevNummer;
                udlånOversigt.Add(udlånDetails);
            }
            return udlånOversigt;
        }
    }
}