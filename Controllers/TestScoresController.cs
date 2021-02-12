using CyberSpaceCBT.DTOs;
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
    public class TestScoresController : Controller
    {
        private ITestScoreRepository _testScoreRepository;
        private ICandidateRepository _candidateRepository;

        public TestScoresController(ITestScoreRepository testScoreRepository, ICandidateRepository candidateRepository)
        {
            _testScoreRepository = testScoreRepository;
            _candidateRepository = candidateRepository;
        }

        //api/TestScores
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TestScore))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddTestScore([FromBody]TestScore testScoreToCreate)
        {
            if (testScoreToCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            testScoreToCreate.Candidate = await _candidateRepository.GetCandidate(testScoreToCreate.Candidate.Id);

            if (!await _testScoreRepository.AddTestScore(testScoreToCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving the test score");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201);
        }

        //api/TestScore/testScoreId
        [HttpPut("{testScoreId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateTestScore(int testScoreId, [FromBody]TestScore testScoreToUpdate)
        {
            if (testScoreToUpdate == null)
                return BadRequest(ModelState);

            if (testScoreId != testScoreToUpdate.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return NotFound(ModelState);

            testScoreToUpdate.Candidate = await _candidateRepository.GetCandidate(testScoreToUpdate.Candidate.Id);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _testScoreRepository.UpdateTestScore(testScoreToUpdate))
            {
                ModelState.AddModelError("", $"Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        //api/TestScores/Candidate/candidateId
        [HttpGet("Candidate/{candidateId}")]
        [ProducesResponseType(200, Type = typeof(TestScoreDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTestScoresOfCandidate(int candidateId)
        {
            if (!await _candidateRepository.CandidateExists(candidateId))
                return NotFound();

            var testScores = await _testScoreRepository.GetTestScoresOfCandidate(candidateId);

            if (!ModelState.IsValid)
                return BadRequest();

            var testScoreDTOs = new List<TestScoreDTO>();
            foreach (var testScore in testScores)
            {
                testScoreDTOs.Add(new TestScoreDTO
                {
                    Id = testScore.Id,
                    Name = testScore.Name,
                    Score = testScore.Score
                });
            }
            return Ok(testScoreDTOs);
        }

    }
}
