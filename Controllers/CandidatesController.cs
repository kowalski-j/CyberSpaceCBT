using CyberSpaceCBT.Models;
using CyberSpaceCBT.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSpaceCBT.Controllers
{
    [Route("api/[controller]")]
    public class CandidatesController : Controller
    {

        private readonly ICandidateRepository _candidateRepository;

        public CandidatesController(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        //api/Candidates
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Candidate))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddCandidate([FromBody]Candidate candidateToAdd)
        {
            if (candidateToAdd == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _candidateRepository.AddCandidate(candidateToAdd))
            {
                ModelState.AddModelError("", $"Something went wrong saving{candidateToAdd.Name}");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201);

        }

        //api/Candidates/candidateId
        [HttpPut("{candidateId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCandidate(int candidateId, [FromBody]Candidate candidateToUpdate)
        {
            if (candidateToUpdate == null)
                return BadRequest(ModelState);

            if (candidateId != candidateToUpdate.Id)
                return BadRequest(ModelState);

            if (!await _candidateRepository.CandidateExists(candidateId))
                return NotFound();

            if (await _candidateRepository.IsDuplicateCandidateName(candidateId, candidateToUpdate.Name))
            {
                ModelState.AddModelError("", $"Candidate{candidateToUpdate.Name} already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _candidateRepository.UpdateCandidate(candidateToUpdate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {candidateToUpdate.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
