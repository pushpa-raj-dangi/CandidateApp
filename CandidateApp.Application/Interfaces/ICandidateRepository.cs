
using CandidateApp.Domain.Entities;

namespace CandidateApp.Application.Interfaces
{
    public interface ICandidateRepository
    {
        Task<Candidate> GetByEmailAsync(string email);
        Task AddOrUpdateAsync(Candidate candidate);
    }
}
