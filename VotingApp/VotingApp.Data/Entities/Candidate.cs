using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Data.Entities
{
    public class Candidate
    {
        [Key]
        public int CandidateId { get; set; }
        [Required]
        [MaxLength(200)]
        public string CandidateName { get; set; }
        public int Votes { get; set; }
    }
}
