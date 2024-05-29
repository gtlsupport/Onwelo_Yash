using System.ComponentModel.DataAnnotations;

namespace VotingApp.Models
{
    public class VoterModel
    {
        public int VoterId { get; set; }
        [Required]
        public string VoterName { get; set; }
        public bool HasVote { get; set; }
    }
}
