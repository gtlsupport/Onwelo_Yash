using Microsoft.AspNetCore.Mvc;
using VotingApp.DTO;
using VotingApp.Models;
using VotingApp.Services;

namespace VotingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        public CandidateController(ICandidateService candidateService)
        {
            this._candidateService = candidateService;
        }

        #region candidate
        [HttpPost]
        [Route("insert-candidate")]
        public IActionResult InsertCandidate(CandidateModel candidateModel)
        {
            if (!ModelState.IsValid) // Model validation check
            {
                return BadRequest(ModelState);
            }
            if (candidateModel == null)
            {
                return BadRequest();
            }

            CandidateDTO candidateDTO = MapCandidateModelToDTO(candidateModel); // Mapping model to DTO
            _candidateService.InsertCandidate(candidateDTO);
            var candidateList = _candidateService.GetCandidatesList(); // Calling candidate list to show after inserting candidate
            return Ok(candidateList);
        }

        [HttpGet]
        [Route("candidates-list")]
        public IActionResult GetAllCandidateList()
        {
            var candidateList = _candidateService.GetCandidatesList();
            return Ok(candidateList);
        }

        [NonAction]
        private CandidateDTO MapCandidateModelToDTO(CandidateModel candidateModel)
        {
            CandidateDTO candidateDTO = new CandidateDTO
            {
                CandidateName = candidateModel.CandidateName,
            };
            return candidateDTO;
        }
        #endregion
    }
}
