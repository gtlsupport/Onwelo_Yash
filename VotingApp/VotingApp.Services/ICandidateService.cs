using VotingApp.DTO;

namespace VotingApp.Services
{
    public interface ICandidateService
    {
        void InsertCandidate(CandidateDTO candidateDTO);
        List<CandidateDTO> GetCandidatesList();
        void UpdateCandidate(CandidateDTO candidateDTO);
    }
}
