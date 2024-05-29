using Moq;
using VotingApp.Data.Entities;
using VotingApp.DTO;
using VotingApp.Repository.Repository;
using VotingApp.Services;

namespace VotingApp.Test.Services
{
    public class VoterServiceTest
    {
        private readonly Mock<IDbRepository<Voter>> _voterDbRepositoryMock;
        private readonly VoterService _voterService;

        public VoterServiceTest()
        {
            _voterDbRepositoryMock = new Mock<IDbRepository<Voter>>();
            _voterService = new VoterService(_voterDbRepositoryMock.Object);
        }

        [Fact]
        public void GetVoterList_ReturnsMappedDTOList()
        {
            // Arrange
            var voterEntities = new List<Voter>
        {
            new Voter { VoterId = 1, VoterName = "John Doe", HasVote = false },
            new Voter { VoterId = 2, VoterName = "Jane Smith", HasVote = true }
        };

            _voterDbRepositoryMock.Setup(repo => repo.GetAll()).Returns(voterEntities);

            // Act
            var result = _voterService.GetVoterList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(voterEntities.Count, result.Count);

            for (int i = 0; i < result.Count; i++)
            {
                Assert.Equal(voterEntities[i].VoterId, result[i].VoterId);
                Assert.Equal(voterEntities[i].VoterName, result[i].VoterName);
                Assert.Equal(voterEntities[i].HasVote, result[i].HasVote);
            }
        }

        [Fact]
        public void InsertVoter_CallsInsertMethodWithMappedEntity()
        {
            // Arrange
            var voterDTO = new VoterDTO { VoterId = 1, VoterName = "John Doe", HasVote = false };

            _voterDbRepositoryMock.Setup(repo => repo.Insert(It.IsAny<Voter>()));

            // Act
            _voterService.InsertVoter(voterDTO);

            // Assert
            _voterDbRepositoryMock.Verify(repo => repo.Insert(It.Is<Voter>(v =>
                v.VoterId == voterDTO.VoterId &&
                v.VoterName == voterDTO.VoterName &&
                v.HasVote == voterDTO.HasVote)), Times.Once);
        }

        [Fact]
        public void UpdateVoter_CallsUpdateMethodWithMappedEntity()
        {
            // Arrange
            var voterDTO = new VoterDTO { VoterId = 1, VoterName = "John Doe", HasVote = true };

            _voterDbRepositoryMock.Setup(repo => repo.Update(It.IsAny<Voter>()));

            // Act
            _voterService.UpdateVoter(voterDTO);

            // Assert
            _voterDbRepositoryMock.Verify(repo => repo.Update(It.Is<Voter>(v =>
                v.VoterId == voterDTO.VoterId &&
                v.VoterName == voterDTO.VoterName &&
                v.HasVote == voterDTO.HasVote)), Times.Once);
        }
    }
}
