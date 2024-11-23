using CandidateApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Application.Services
{
    public interface ICandidateService
    {
        Task AddOrUpdateCandidateAsync(Candidate candidate);
    }
}
