using System.ComponentModel.DataAnnotations;
using VotingApp.Data.Entities;

namespace VotingApp.Test.DataEntities
{
    public class CandidateTest
    {
        [Fact]
        public void CandidateProperties_HaveCorrectAttributes()
        {
            // Arrange
            var candidate = new Candidate
            {
                CandidateId = 1,
                CandidateName = "John Doe",
                Votes = 100
            };

            // Act
            var candidateIdProperty = candidate.GetType().GetProperty("CandidateId");
            var candidateNameProperty = candidate.GetType().GetProperty("CandidateName");
            var votesProperty = candidate.GetType().GetProperty("Votes");

            var candidateIdAttributes = candidateIdProperty!.GetCustomAttributes(typeof(KeyAttribute), true);
            var candidateNameAttributes = candidateNameProperty!.GetCustomAttributes(typeof(RequiredAttribute), true);
            var votesAttributes = votesProperty!.GetCustomAttributes(typeof(RequiredAttribute), true);

            // Assert
            Assert.NotNull(candidateIdAttributes);
            Assert.NotEmpty(candidateIdAttributes);
            Assert.NotNull(candidateNameAttributes);
            Assert.NotEmpty(candidateNameAttributes);
            Assert.NotNull(votesAttributes);
            Assert.Empty(votesAttributes); // As 'Votes' is not required, it should not have the Required attribute
            Assert.IsType<int>(candidate.CandidateId); // CandidateId should be of type int
            Assert.IsType<string>(candidate.CandidateName); // CandidateName should be of type string
            Assert.IsType<int>(candidate.Votes); // Votes should be of type int
        }
    }
}
