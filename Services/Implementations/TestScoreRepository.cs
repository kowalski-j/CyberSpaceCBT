using CyberSpaceCBT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSpaceCBT.Services.Implementations
{
    public class TestScoreRepository : ITestScoreRepository
    {
        private CbtDbContext _cbtDbContext;

        public TestScoreRepository(CbtDbContext cbtDbContext)
        {
            _cbtDbContext = cbtDbContext;
        }
        public async Task<bool> Save()
        {
            return await _cbtDbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddTestScore(TestScore testScore)
        {
            await _cbtDbContext.AddAsync(testScore);
            return await Save();
        }

        public async Task<ICollection<TestScore>> GetTestScoresOfCandidate(int candidateId)
        {
            return await _cbtDbContext.TestScores.Where(c => c.Candidate.Id == candidateId).ToListAsync();
        }

        public async Task<bool> UpdateTestScore(TestScore testScore)
        {
            _cbtDbContext.Update(testScore);
            return await Save();
        }
    }
}
