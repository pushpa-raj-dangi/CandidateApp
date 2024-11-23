using CandidateApp.Domain.Entities;
using CandidateApp.Infrastructure.Data;
using CandidateApp.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CandidateApp.Tests.Repositories
{
    public class CandidateRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly CandidateRepository _repository;

        public CandidateRepositoryTests()
        {
            // Use AppDbContext with In-Memory Database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDatabase") // Ensure the database is in memory
                .Options;

            _context = new AppDbContext(options);
            _repository = new CandidateRepository(_context);

            // Ensure the database is created
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetCandidateByEmailAsync_ShouldReturnCandidate_WhenEmailExists()
        {
            // Arrange
            var email = "test@example.com";
            var candidate = new Candidate
            {
                Email = email,
                FirstName = "John",
                LastName = "Doe",
                Comment = "Test comment" // Set a value for the Comment property
            };

            // Add the candidate to the in-memory database
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByEmailAsync(email);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(email);
            result.Comment.Should().Be("Test comment"); // Verify Comment is set
        }

        [Fact]
        public async Task AddCandidateAsync_ShouldInsertCandidate_WhenDataIsValid()
        {
            // Arrange
            var candidate = new Candidate
            {
                Email = "new@example.com",
                FirstName = "Jane",
                LastName = "Smith",
                Comment = "Test comment"  // Provide a value for the required Comment property
            };

            // Act
            await _repository.AddOrUpdateAsync(candidate);

            // Assert
            var result = await _context.Candidates.FirstOrDefaultAsync(c => c.Email == candidate.Email);
            result.Should().NotBeNull();
            result.FirstName.Should().Be("Jane");
            result.Comment.Should().Be("Test comment");
        }
    }
}
