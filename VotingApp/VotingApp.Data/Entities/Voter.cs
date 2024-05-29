using System.ComponentModel.DataAnnotations;

namespace VotingApp.Data.Entities
{
    public class Voter
    {
        [Key]
        public int VoterId { get; set; }
        [Required]
        [MaxLength(200)]
        public string VoterName { get; set; }
        public bool HasVote { get; set; }
    }
}
