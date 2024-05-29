using System.ComponentModel.DataAnnotations;

namespace VotingApp.Models
{
    public class CastVoteModel
    {
        [Required]
        public int? VoterId { get; set; }
        [Required]
        public int? CandidateId{ get; set; }
    }
}
