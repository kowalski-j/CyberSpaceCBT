using CyberSpaceCBT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSpaceCBT.Services
{
    public interface ITestScoreRepository
    {
        Task<bool> AddTestScore(TestScore testScore);
        Task<bool> UpdateTestScore(TestScore testScore);

        Task<ICollection<TestScore>> GetTestScoresOfCandidate(int candidateId);
    }
}
