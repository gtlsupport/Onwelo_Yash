using System.ComponentModel.DataAnnotations;
using VotingApp.Data.Entities;

namespace VotingApp.Test.DataEntities
{
    public class VoterTest
    {
        [Fact]
        public void VoterProperties_HaveCorrectAttributes()
        {
            // Arrange
            var voter = new Voter
            {
                VoterId = 1,
                VoterName = "Jane Smith",
                HasVote = false
            };

            // Act
            var voterIdProperty = voter.GetType().GetProperty("VoterId");
            var voterNameProperty = voter.GetType().GetProperty("VoterName");
            var hasVoteProperty = voter.GetType().GetProperty("HasVote");

            var voterIdAttributes = voterIdProperty!.GetCustomAttributes(typeof(KeyAttribute), true);
            var voterNameAttributes = voterNameProperty!.GetCustomAttributes(typeof(RequiredAttribute), true);
            var hasVoteAttributes = hasVoteProperty!.GetCustomAttributes(typeof(RequiredAttribute), true);

            // Assert
            Assert.NotNull(voterIdAttributes);
            Assert.NotEmpty(voterIdAttributes);
            Assert.NotNull(voterNameAttributes);
            Assert.NotEmpty(voterNameAttributes);
            Assert.NotNull(hasVoteAttributes);
            Assert.Empty(hasVoteAttributes); // As 'HasVote' is not required, it should not have the Required attribute
            Assert.IsType<int>(voter.VoterId); // VoterId should be of type int
            Assert.IsType<string>(voter.VoterName); // VoterName should be of type string
            Assert.IsType<bool>(voter.HasVote); // HasVote should be of type bool
        }
    }
}
