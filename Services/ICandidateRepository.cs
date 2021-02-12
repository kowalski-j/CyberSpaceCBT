using CyberSpaceCBT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSpaceCBT.Services
{
    public interface ICandidateRepository
    {
        Task<Candidate> GetCandidate(int candidateId);
        Task<ICollection<Candidate>> GetCandidates();
        Task<bool> AddCandidate(Candidate candidate);
        Task<bool> UpdateCandidate(Candidate candidate);
        Task<bool> CandidateExists(int candidateId);
        Task<bool> IsDuplicateCandidateName(int candidateId, string candidateName);
        Task<bool> Save();
    }
}
