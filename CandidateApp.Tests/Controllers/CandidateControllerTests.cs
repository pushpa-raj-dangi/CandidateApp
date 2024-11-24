using CandidateApp.API.Controllers;
using CandidateApp.Application.DTOs;
using CandidateApp.Application.Services;
using CandidateApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FluentAssertions;
using CandidateApp.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace CandidateApp.Tests.Controllers
{
    public class CandidatesControllerTests
    {
        private readonly Mock<ICandidateRepository> _candidateRepositoryMock;
        private readonly CandidateService _candidateService;
        private readonly CandidatesController _controller;

        public CandidatesControllerTests()
        {
            // Arrange mocks and service
            _candidateRepositoryMock = new Mock<ICandidateRepository>();
            _candidateService = new CandidateService(_candidateRepositoryMock.Object);
            _controller = new CandidatesController(_candidateService);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_ShouldReturnCreated_WhenModelIsValid()
        {
            // Arrange
            var validDto = new CandidateDto
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123456789",
                CallTimeInterval = "9 AM - 5 PM",
                LinkedInUrl = "https://linkedin.com/in/johndoe",
                GitHubUrl = "https://github.com/johndoe",
                Comment = "Test candidate"
            };

            // Simulate no existing candidate in the database
            _candidateRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(validDto.Email))
                .ReturnsAsync((Candidate)null); // Simulate no existing candidate

            // Simulate successful Add/Update
            _candidateRepositoryMock
                .Setup(repo => repo.AddOrUpdateAsync(It.IsAny<Candidate>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddOrUpdateCandidate(validDto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>(); // Change this to check for CreatedAtActionResult
            _candidateRepositoryMock.Verify(repo => repo.AddOrUpdateAsync(It.IsAny<Candidate>()), Times.Once);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_ShouldReturnBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var invalidDto = new CandidateDto(); // Missing required fields
            _controller.ModelState.AddModelError("Email", "The Email field is required.");

            // Act
            var result = await _controller.AddOrUpdateCandidate(invalidDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _candidateRepositoryMock.Verify(repo => repo.AddOrUpdateAsync(It.IsAny<Candidate>()), Times.Never);
        }

        
    }
}
