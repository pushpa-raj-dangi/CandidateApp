using CandidateApp.Application.Interfaces;
using CandidateApp.Domain.Entities;
using CandidateApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Infrastructure.Repositories;
public class CandidateRepository(AppDbContext context) : ICandidateRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Candidate?> GetByEmailAsync(string email)
    {
        return await _context.Candidates.SingleOrDefaultAsync(c => c.Email == email);
    }

    public async Task AddOrUpdateAsync(Candidate candidate)
    {
        var existing = await GetByEmailAsync(candidate.Email);

        if (existing == null)
        {
            _context.Candidates.Add(candidate);
        }
        else
        {
            _context.Candidates.Update(candidate);
        }

        await _context.SaveChangesAsync();
    }
}

