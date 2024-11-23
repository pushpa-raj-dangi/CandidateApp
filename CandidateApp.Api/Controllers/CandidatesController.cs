using CandidateApp.Application.DTOs;
using CandidateApp.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CandidateApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidatesController(CandidateService service) : ControllerBase
{
    private readonly CandidateService _service = service;

    [HttpPost]
    public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _service.AddOrUpdateCandidate(dto);
        return Created();
    }

    
    }
