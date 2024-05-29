using SigmaApp.Models;

namespace SigmaApp.Interfaces;

public interface ICandidateService
{
    Task<Candidate> CreateOrUpdateCandidate(Candidate candidate);
    Task<Candidate> GetCandidateByEmail(string email);
    Task<IEnumerable<Candidate>> GetAllCandidates();
}