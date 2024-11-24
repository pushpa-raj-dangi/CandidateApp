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

        try
        {
            await _service.AddOrUpdateCandidate(dto);

            return CreatedAtAction(nameof(AddOrUpdateCandidate), new { email = dto.Email }, dto); 
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    
    }
