using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Udlånssystem_API.Data;
using Udlånssystem_API.DTOs;

public class BrugerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IBrugerRepository _repository;

    public BrugerController(IMapper mapper, IBrugerRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BrugerDTO>> GetBruger(int id)
    {
        var bruger = await _repository.GetById(id);

        if (bruger == null)
        {
            return NotFound();
        }

        // Mapping the entity to a DTO
        var brugerDTO = _mapper.Map<BrugerDTO>(bruger);

        return Ok(brugerDTO);
    }
}
