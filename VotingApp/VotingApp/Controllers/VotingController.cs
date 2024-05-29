using Microsoft.AspNetCore.Mvc;
using VotingApp.DTO;
using VotingApp.Models;
using VotingApp.Services;

namespace VotingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotingController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        private readonly IVoterService _voterService;
        public VotingController(ICandidateService candidateService, IVoterService voterService)
        {
            this._candidateService = candidateService;
            this._voterService = voterService;
        }

        #region vote casting
        [HttpGet]
        [Route("voting-machine-date")]
        public IActionResult GetVotingMachineData()
        {
            var voterList = _voterService.GetVoterList().Where(voter => voter.HasVote == false); // Get only those voter who have not vote
            var candidateList = _candidateService.GetCandidatesList();

            var returnData = new VotingMachineData
            {
                voterList = voterList,
                candidateList = candidateList,
            };
            return Ok(returnData);
        }

        [HttpPost]
        [Route("caste-vote")]
        public IActionResult CasteVote(CastVoteModel castVote)
        {
            if (!ModelState.IsValid) // Check model validations
            {
                return BadRequest(ModelState);
            }
            if (castVote == null || (castVote.CandidateId == 0 || castVote.VoterId == 0)) //candidate or voter should not be 0
            {
                return BadRequest(new VoteResponse { message = "Select voter and candidate" });
            }

            var voter = _voterService.GetVoterList().SingleOrDefault(v => v.VoterId == castVote.VoterId);
            if (voter == null)
                return BadRequest(new VoteResponse { message = "Voter not found" });

            voter.HasVote = true;

            var candidate = _candidateService.GetCandidatesList().SingleOrDefault(c => c.CandidateId == castVote.CandidateId);
            if (candidate == null)
                return BadRequest(new VoteResponse { message = "Candidate not found" });

            candidate.Votes = candidate.Votes + 1; // Increase vote count for candidate
            _voterService.UpdateVoter(voter);
            _candidateService.UpdateCandidate(candidate);
            return Ok(new VoteResponse { message = "Vote casted successfully" });
        }
        #endregion
    }
}
