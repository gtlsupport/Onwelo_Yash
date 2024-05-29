using System.ComponentModel.DataAnnotations;

namespace VotingApp.Models
{
    public class CandidateModel
    {
        public int CandidateId { get; set; }
        [Required]
        public string CandidateName { get; set; }
        public int Votes { get; set; }
    }
}
