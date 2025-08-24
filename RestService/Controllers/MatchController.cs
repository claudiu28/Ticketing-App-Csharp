using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Persistence.Repositories;

namespace RestService.Controllers;

[ApiController]
[Route("api/matches")]
public class MatchController(IRepoMatch repoMatch) : ControllerBase
{
    private readonly IRepoMatch _repoMatch = repoMatch;

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllMatches()
    {
        try
        {
            var matches = await _repoMatch.FindAll();
            if (matches == null || matches.Count == 0)
                return NotFound("No matches found.");

            return Ok(matches);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpGet("search")]
    public async Task<IActionResult> GetMatchesByTeams([FromQuery] string? teamA, [FromQuery] string? teamB)
    {
        try
        {
            if (!string.IsNullOrEmpty(teamA) && !string.IsNullOrEmpty(teamB))
            {
                var match = await _repoMatch.FindByTeamAAndTeamB(teamA, teamB);
                return match == null ? NotFound($"No match found for {teamA} vs {teamB}") : Ok(match);
            }

            if (!string.IsNullOrEmpty(teamA))
            {
                var matches = await _repoMatch.FindByTeamA(teamA);
                return matches == null || matches.Count == 0
                    ? NotFound($"No matches found for team A: {teamA}")
                    : Ok(matches);
            }

            if (!string.IsNullOrEmpty(teamB))
            {
                var matches = await _repoMatch.FindByTeamB(teamB);
                return matches == null || matches.Count == 0
                    ? NotFound($"No matches found for team B: {teamB}")
                    : Ok(matches);
            }

            return BadRequest("You must provide at least one team parameter.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetMatchById(long id)
    {
        try
        {
            var match = await _repoMatch.FindById(id);
            return match == null
                ? NotFound($"No match found with ID: {id}")
                : Ok(match);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateMatch([FromBody] Match match)
    {
        try
        {
            if (match == null)
                return BadRequest("Match data is null.");

            await _repoMatch.Save(match);
            return CreatedAtAction(nameof(GetMatchById), new { id = match.Id }, match);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateMatch(long id, [FromBody] Match match)
    {
        try
        {
            if (match == null)
                return BadRequest("Match data is null.");

            var existing = await _repoMatch.FindById(id);
            if (existing == null)
                return NotFound($"No match found with ID: {id}");

            match.Id = id;
            await _repoMatch.Update(match);
            return Ok(match);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteMatch(long id)
    {
        try
        {
            var match = await _repoMatch.FindById(id);
            if (match == null)
                return NotFound($"No match found with ID: {id}");

            var deleted = await _repoMatch.Delete(match);
            return Ok(deleted);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
