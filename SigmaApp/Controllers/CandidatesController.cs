using Microsoft.AspNetCore.Mvc;
using SigmaApp.Interfaces;
using SigmaApp.Models;

namespace SigmaApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CandidatesController : ControllerBase
{
    private readonly ICandidateService _candidateService;

    public CandidatesController(ICandidateService candidateService)
    {
        _candidateService = candidateService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrUpdateCandidate([FromBody] Candidate candidate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedCandidate = await _candidateService.CreateOrUpdateCandidate(candidate);
            return Ok(updatedCandidate);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetCandidateByEmail(string email)
    {
        try
        {
            var candidate = await _candidateService.GetCandidateByEmail(email);
            if (candidate == null)
            {
                return NotFound();
            }

            return Ok(candidate);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCandidates()
    {
        try
        {
            var candidates = await _candidateService.GetAllCandidates();
            return Ok(candidates);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }
}
