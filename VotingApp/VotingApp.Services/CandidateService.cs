using VotingApp.Data.Entities;
using VotingApp.DTO;
using VotingApp.Repository.Repository;

namespace VotingApp.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IDbRepository<Candidate> _candidateDbRepository;
        public CandidateService(IDbRepository<Candidate> candidateDbRepository)
        {
            this._candidateDbRepository = candidateDbRepository;
        }
        public List<CandidateDTO> GetCandidatesList()
        {
            var candidates = _candidateDbRepository.GetAll();

            return MapEntityToDTOList(candidates);
        }

        public void InsertCandidate(CandidateDTO candidateDTO)
        {
            _candidateDbRepository.Insert(MapDTOToEntity(candidateDTO));
        }

        public void UpdateCandidate(CandidateDTO candidateDTO)
        {
            Candidate candidate = MapDTOToEntity(candidateDTO);
            _candidateDbRepository.Update(candidate);
        }

        private Candidate MapDTOToEntity(CandidateDTO candidateDTO)
        {
            Candidate candidate = new Candidate
            {
                CandidateName = candidateDTO.CandidateName,
                CandidateId = candidateDTO.CandidateId,
                Votes = candidateDTO.Votes,
            };
            return candidate;
        }

        private List<CandidateDTO> MapEntityToDTOList(List<Candidate> candidates)
        {
            List<CandidateDTO> candidateDTOs = new List<CandidateDTO>();
            if (candidates != null && candidates.Count > 0)
            {
                foreach (Candidate candidate in candidates)
                {
                    CandidateDTO candidateDTO = new CandidateDTO
                    {
                        CandidateName = candidate.CandidateName,
                        Votes = candidate.Votes,
                        CandidateId = candidate.CandidateId,
                    };
                    candidateDTOs.Add(candidateDTO);
                }
            }
            return candidateDTOs;
        }
    }
}
