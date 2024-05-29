using System.ComponentModel.DataAnnotations;

namespace SigmaApp.Models;
public class Candidate
{
    [Key]
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    public string CallTimeInterval { get; set; } = string.Empty;

    [Url]
    public string LinkedInProfileUrl { get; set; } = string.Empty;

    [Url]
    public string GitHubProfileUrl { get; set; } = string.Empty;

    [Required]
    public string Comment { get; set; } = string.Empty;
}

