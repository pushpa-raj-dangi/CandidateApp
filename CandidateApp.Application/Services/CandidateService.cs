using CandidateApp.Application.DTOs;
using CandidateApp.Application.Interfaces;
using CandidateApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Application.Services
{
    public class CandidateService(ICandidateRepository repository)
    {
        private readonly ICandidateRepository _repository = repository;

        public async Task AddOrUpdateCandidate(CandidateDto dto)
        {
            var existingCandidate = await _repository.GetByEmailAsync(dto.Email);

            if (existingCandidate != null)
            {
                // Update
                existingCandidate.FirstName = dto.FirstName;
                existingCandidate.LastName = dto.LastName;
                existingCandidate.PhoneNumber = dto.PhoneNumber;
                existingCandidate.CallTimeInterval = dto.CallTimeInterval;
                existingCandidate.LinkedInProfileUrl= dto.LinkedInUrl;
                existingCandidate.GitHubProfileUrl = dto.GitHubUrl;
                existingCandidate.Comment = dto.Comments;
                await _repository.AddOrUpdateAsync(existingCandidate);
            }
            else
            {
                // Create
                var newCandidate = new Candidate
                {
                    Id = Guid.NewGuid(),
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email,
                    CallTimeInterval = dto.CallTimeInterval,
                    LinkedInProfileUrl = dto.LinkedInUrl,
                    GitHubProfileUrl = dto.GitHubUrl,
                    Comment = dto.Comments
                };

                await _repository.AddOrUpdateAsync(newCandidate);
            }
        }
    }
}
