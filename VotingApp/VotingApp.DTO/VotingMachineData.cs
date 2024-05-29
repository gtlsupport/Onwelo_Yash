namespace VotingApp.DTO
{
    public class VotingMachineData
    {
        public IEnumerable<VoterDTO>? voterList { get; set; }
        public List<CandidateDTO>? candidateList { get; set; }
    }
}
