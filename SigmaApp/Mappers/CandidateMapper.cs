using SigmaApp.Models;

namespace SigmaApp.Mappers;

public static class CandidateMapper
{
    public static Candidate Map(this Candidate source)
    {
        return new()
        {
            Email = source.Email,
            FirstName = source.FirstName,
            LastName = source.LastName,
            PhoneNumber = source.PhoneNumber,
            CallTimeInterval = source.CallTimeInterval,
            LinkedInProfileUrl = source.LinkedInProfileUrl,
            GitHubProfileUrl = source.GitHubProfileUrl,
            Comment = source.Comment,
        };
    }
}
