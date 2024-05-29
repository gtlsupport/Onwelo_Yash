using Microsoft.AspNetCore.Mvc;
using VotingApp.DTO;
using VotingApp.Models;
using VotingApp.Services;

namespace VotingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoterController : ControllerBase
    {
        private readonly IVoterService _voterService;
        public VoterController(IVoterService voterService)
        {
            this._voterService = voterService;
        }
        #region voter
        [HttpGet]
        [Route("all-voters")]
        public IActionResult GetAllVoters()
        {
            var allVoters = _voterService.GetVoterList();
            return Ok(allVoters);
        }

        [HttpPost]
        [Route("insert-voter")]
        public IActionResult InsertVoter(VoterModel voterModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (voterModel == null)
            {
                return BadRequest();
            }
            VoterDTO voter = MapCandidateModelToDTO(voterModel); //Map model to DTO
            _voterService.InsertVoter(voter);
            var allVoters = _voterService.GetVoterList();// Fetch voter list after insert 
            return Ok(allVoters);
        }

        [NonAction]
        private VoterDTO MapCandidateModelToDTO(VoterModel voterModel)
        {
            VoterDTO voter = new VoterDTO
            {
                VoterName = voterModel.VoterName,
            };
            return voter;
        }
        #endregion
    }
}
