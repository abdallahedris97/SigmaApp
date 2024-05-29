using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SigmaApp.DataContext;
using SigmaApp.Interfaces;
using SigmaApp.Mappers;
using SigmaApp.Models;

namespace SigmaApp.Services;

public class CandidateService : ICandidateService
{
    private readonly ApplicationDbContext _context;
    private readonly IMemoryCache _cache;
    private readonly ILogger<CandidateService> _logger;

    public CandidateService(ApplicationDbContext context, IMemoryCache cache, ILogger<CandidateService> logger)
    {
        _context = context;
        _cache = cache;
        _logger = logger;
    }

    public async Task<Candidate> CreateOrUpdateCandidate(Candidate candidate)
    {
        try
        {
            var cacheKey = $"Candidate_{candidate.Email}";
            if (_cache.TryGetValue(cacheKey, out Candidate cachedCandidate))
            {
                _context.Entry(cachedCandidate).State = EntityState.Detached;
                cachedCandidate = candidate.Map();

                _context.Candidates.Update(cachedCandidate);
                _cache.Set(cacheKey, cachedCandidate);
            }
            else
            {
                var existingCandidate = await _context.Candidates.FindAsync(candidate.Email);
                if (existingCandidate != null)
                {
                    existingCandidate = candidate.Map();

                    _cache.Set(cacheKey, existingCandidate);
                }
                else
                {
                    _context.Candidates.Add(candidate);
                    _cache.Set(cacheKey, candidate);
                }
            }

            await _context.SaveChangesAsync();
            return candidate;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating or updating candidate.");
            throw;
        }
    }

    public async Task<Candidate> GetCandidateByEmail(string email)
    {
        try
        {
            var cacheKey = $"Candidate_{email}";
            if (!_cache.TryGetValue(cacheKey, out Candidate? candidate))
            {
                candidate = await _context.Candidates.FindAsync(email);
                if (candidate != null)
                {
                    _cache.Set(cacheKey, candidate);
                }
            }

            return candidate!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching candidate by email.");
            throw;
        }
    }

    public async Task<IEnumerable<Candidate>> GetAllCandidates()
    {
        try
        {
            return await _context.Candidates.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching all candidates.");
            throw;
        }
    }
}
