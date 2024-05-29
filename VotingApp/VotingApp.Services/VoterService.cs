using VotingApp.Data.Entities;
using VotingApp.DTO;
using VotingApp.Repository.Repository;

namespace VotingApp.Services
{
    public class VoterService : IVoterService
    {
        private readonly IDbRepository<Voter> _voterDbRepository;
        public VoterService(IDbRepository<Voter> voterDbRepository)
        {
            this._voterDbRepository = voterDbRepository;
        }

        public List<VoterDTO> GetVoterList()
        {
            var voterList = _voterDbRepository.GetAll();
            return MapEnityToDTOList(voterList);// Map entity list to DTO List
        }

        public void InsertVoter(VoterDTO voterDTO)
        {
            Voter voter = MapDTOToEntity(voterDTO);// Map DTO to entity
            _voterDbRepository.Insert(voter);
        }

        public void UpdateVoter(VoterDTO voterDTO)
        {
            Voter voter = MapDTOToEntity(voterDTO);
            _voterDbRepository.Update(voter);
        }

        private Voter MapDTOToEntity(VoterDTO voterDTO)
        {
            Voter voter = new Voter
            {
                VoterName = voterDTO.VoterName,
                HasVote = voterDTO.HasVote,
                VoterId = voterDTO.VoterId,
            };
            return voter;
        }

        private List<VoterDTO> MapEnityToDTOList(List<Voter> voters)
        {
            List<VoterDTO> voterDTOs = new List<VoterDTO>();
            if (voters != null && voters.Count > 0)
            {
                foreach (Voter voter in voters)
                {
                    VoterDTO voterDTO = new VoterDTO
                    {
                        VoterId = voter.VoterId,
                        VoterName = voter.VoterName,
                        HasVote = voter.HasVote,
                    };
                    voterDTOs.Add(voterDTO);
                }
            }
            return voterDTOs;
        }
    }
}
