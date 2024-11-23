using CandidateApp.Application.DTOs;
using CandidateApp.Application.Interfaces;
using CandidateApp.Application.Services;
using CandidateApp.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Tests.Services
{
    public class CandidateServiceTests
    {
        private readonly Mock<ICandidateRepository> _repositoryMock;
        private readonly CandidateService _candidateService;

        public CandidateServiceTests()
        {
            _repositoryMock = new Mock<ICandidateRepository>();
            _candidateService = new CandidateService(_repositoryMock.Object);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_ShouldAddNewCandidate_WhenEmailDoesNotExist()
        {
            // Arrange
            var candidateDto = new CandidateDto
            {
                Email = "newuser@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123456789",
                CallTimeInterval = "9 AM - 5 PM",
                LinkedInUrl = "https://linkedin.com/in/johndoe",
                GitHubUrl = "https://github.com/johndoe",
                Comment = "New candidate"
            };

            _repositoryMock
                .Setup(repo => repo.GetByEmailAsync(candidateDto.Email))
                .ReturnsAsync((Candidate)null);

            _repositoryMock
                .Setup(repo => repo.AddOrUpdateAsync(It.IsAny<Candidate>()))
                .Returns(Task.CompletedTask);

            // Act
            await _candidateService.AddOrUpdateCandidate(candidateDto);

            // Assert
            _repositoryMock.Verify(repo => repo.AddOrUpdateAsync(It.Is<Candidate>(c =>
                c.Email == candidateDto.Email &&
                c.FirstName == candidateDto.FirstName &&
                c.LastName == candidateDto.LastName &&
                c.PhoneNumber == candidateDto.PhoneNumber &&
                c.CallTimeInterval == candidateDto.CallTimeInterval &&
                c.LinkedInProfileUrl == candidateDto.LinkedInUrl &&
                c.GitHubProfileUrl == candidateDto.GitHubUrl &&
                c.Comment == candidateDto.Comment
            )), Times.Once);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_ShouldUpdateExistingCandidate_WhenEmailExists()
        {
            // Arrange
            var existingCandidate = new Candidate
            {
                Id = Guid.NewGuid(),
                Email = "existinguser@example.com",
                FirstName = "Jane",
                LastName = "Smith"
            };

            var updatedDto = new CandidateDto
            {
                Email = "existinguser@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "987654321",
                CallTimeInterval = "10 AM - 4 PM",
                LinkedInUrl = "https://linkedin.com/in/johndoe",
                GitHubUrl = "https://github.com/johndoe",
                Comment = "Updated candidate"
            };

            _repositoryMock
                .Setup(repo => repo.GetByEmailAsync(existingCandidate.Email))
                .ReturnsAsync(existingCandidate);

            _repositoryMock
                .Setup(repo => repo.AddOrUpdateAsync(It.IsAny<Candidate>()))
                .Returns(Task.CompletedTask);

            // Act
            await _candidateService.AddOrUpdateCandidate(updatedDto);

            // Assert
            _repositoryMock.Verify(repo => repo.AddOrUpdateAsync(It.Is<Candidate>(c =>
                c.Email == updatedDto.Email &&
                c.FirstName == updatedDto.FirstName &&
                c.LastName == updatedDto.LastName &&
                c.PhoneNumber == updatedDto.PhoneNumber &&
                c.CallTimeInterval == updatedDto.CallTimeInterval &&
                c.LinkedInProfileUrl == updatedDto.LinkedInUrl &&
                c.GitHubProfileUrl == updatedDto.GitHubUrl &&
                c.Comment == updatedDto.Comment
            )), Times.Once);
        }
    }
}
