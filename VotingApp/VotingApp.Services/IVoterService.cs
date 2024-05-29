using VotingApp.DTO;

namespace VotingApp.Services
{
    public interface IVoterService
    {
        void InsertVoter(VoterDTO voterDTO);
        List<VoterDTO> GetVoterList();
        void UpdateVoter(VoterDTO voterDTO);
    }
}
