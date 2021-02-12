using CyberSpaceCBT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSpaceCBT.Services.Implementations
{
    public class CandidateRepository : ICandidateRepository
    {
        private CbtDbContext _cbtDbContext;

        public CandidateRepository(CbtDbContext cbtDbContext)
        {
            _cbtDbContext = cbtDbContext;
        }
        public async Task<bool> Save()
        {
            return await _cbtDbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddCandidate(Candidate candidate)
        {
            await _cbtDbContext.AddAsync(candidate);
            return await Save();
        }

        public async Task<Candidate> GetCandidate(int candidateId)
        {
            return await _cbtDbContext.Candidates.Where(c => c.Id == candidateId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Candidate>> GetCandidates()
        {
            return await _cbtDbContext.Candidates.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<bool> UpdateCandidate(Candidate candidate)
        {
            _cbtDbContext.Update(candidate);
            return await Save();
        }

        public async Task<bool> IsDuplicateCandidateName(int candidateId, string candidateName)
        {
            var candidate = await _cbtDbContext.Candidates.Where(c => c.Name.Trim().ToUpper() == candidateName.Trim().ToUpper()
                                             && c.Id != candidateId).FirstOrDefaultAsync();
            return candidate == null ? false : true;
        }

        public async Task<bool> CandidateExists(int candidateId)
        {
            return await _cbtDbContext.Candidates.AnyAsync(c => c.Id == candidateId);
        }
    }
}
