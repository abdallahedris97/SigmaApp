using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SigmaApp.Controllers;
using SigmaApp.DataContext;
using SigmaApp.Interfaces;
using SigmaApp.Models;
using SigmaApp.Services;

namespace SigmaApp.Tests;
public class CandidatesControllerTests
{
    private readonly CandidatesController _controller;
    private readonly ICandidateService _service;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CandidateService> _logger;

    public CandidatesControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });
        _logger = loggerFactory.CreateLogger<CandidateService>();
        _service = new CandidateService(_context, memoryCache, _logger);
        _controller = new CandidatesController(_service);
    }

    [Fact]
    public async Task CreateOrUpdateCandidate_NewCandidate_ReturnsOkResult()
    {
        // Arrange
        var candidate = new Candidate
        {
            Email = "john.doe@example.com",
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            CallTimeInterval = "9am-5pm",
            LinkedInProfileUrl = "http://linkedin.com/johndoe",
            GitHubProfileUrl = "http://github.com/johndoe",
            Comment = "New candidate"
        };

        // Act
        var result = await _controller.CreateOrUpdateCandidate(candidate);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task CreateOrUpdateCandidate_ExistingCandidate_ReturnsOkResult()
    {
        // Arrange
        var candidate = new Candidate
        {
            Email = "john.doe@example.com",
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            CallTimeInterval = "9am-5pm",
            LinkedInProfileUrl = "http://linkedin.com/johndoe",
            GitHubProfileUrl = "http://github.com/johndoe",
            Comment = "Existing candidate"
        };

        _context.Candidates.Add(candidate);
        _context.SaveChanges();

        var updatedCandidate = new Candidate
        {
            Email = "john.doe@example.com",
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "0987654321",
            CallTimeInterval = "10am-4pm",
            LinkedInProfileUrl = "http://linkedin.com/johndoe_updated",
            GitHubProfileUrl = "http://github.com/johndoe_updated",
            Comment = "Updated candidate"
        };

        // Act
        var result = await _controller.CreateOrUpdateCandidate(updatedCandidate);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetCandidateByEmail_ExistingCandidate_ReturnsOkResult()
    {
        // Arrange
        var candidate = new Candidate
        {
            Email = "jane.smith@example.com",
            FirstName = "Jane",
            LastName = "Smith",
            PhoneNumber = "1234567890",
            CallTimeInterval = "9am-5pm",
            LinkedInProfileUrl = "http://linkedin.com/janesmith",
            GitHubProfileUrl = "http://github.com/janesmith",
            Comment = "Existing candidate"
        };

        _context.Candidates.Add(candidate);
        _context.SaveChanges();

        // Act
        var result = await _controller.GetCandidateByEmail(candidate.Email);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetCandidateByEmail_NonExistingCandidate_ReturnsNotFoundResult()
    {
        // Act
        var result = await _controller.GetCandidateByEmail("nonexistent@example.com");

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetAllCandidates_ReturnsOkResult()
    {
        // Act
        var result = await _controller.GetAllCandidates();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}
