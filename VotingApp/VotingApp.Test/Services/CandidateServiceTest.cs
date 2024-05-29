using Moq;
using VotingApp.Data.Entities;
using VotingApp.DTO;
using VotingApp.Repository.Repository;
using VotingApp.Services;

namespace VotingApp.Test.Services
{
    public class CandidateServiceTest
    {
        private readonly Mock<IDbRepository<Candidate>> _candidateDbRepositoryMock;
        private readonly CandidateService _candidateService;

        public CandidateServiceTest()
        {
            _candidateDbRepositoryMock = new Mock<IDbRepository<Candidate>>();
            _candidateService = new CandidateService(_candidateDbRepositoryMock.Object);
        }
        [Fact]
        public void GetCandidatesList_ReturnsListOfCandidateDTO()
        {
            // Arrange
            var candidates = new List<Candidate>
        {
            new Candidate { CandidateId = 1, CandidateName = "Candidate 1", Votes = 100 },
            new Candidate { CandidateId = 2, CandidateName = "Candidate 2", Votes = 150 }
        };
            _candidateDbRepositoryMock.Setup(repo => repo.GetAll()).Returns(candidates);

            // Act
            var result = _candidateService.GetCandidatesList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Candidate 1", result[0].CandidateName);
            Assert.Equal("Candidate 2", result[1].CandidateName);
            Assert.Equal(100, result[0].Votes);
            Assert.Equal(150, result[1].Votes);
        }

        [Fact]
        public void InsertCandidate_CallsInsertOnRepository_WithMappedEntity()
        {
            // Arrange
            var candidateDTO = new CandidateDTO
            {
                CandidateId = 1,
                CandidateName = "Candidate 1",
                Votes = 100
            };

            _candidateDbRepositoryMock.Setup(repo => repo.Insert(It.IsAny<Candidate>()));

            // Act
            _candidateService.InsertCandidate(candidateDTO);

            // Assert
            _candidateDbRepositoryMock.Verify(repo => repo.Insert(It.Is<Candidate>(c =>
                c.CandidateId == candidateDTO.CandidateId &&
                c.CandidateName == candidateDTO.CandidateName &&
                c.Votes == candidateDTO.Votes
            )), Times.Once);
        }

        [Fact]
        public void UpdateCandidate_CallsUpdateOnRepository_WithMappedEntity()
        {
            // Arrange
            var candidateDTO = new CandidateDTO
            {
                CandidateId = 1,
                CandidateName = "Updated Candidate",
                Votes = 150
            };

            _candidateDbRepositoryMock.Setup(repo => repo.Update(It.IsAny<Candidate>()));

            // Act
            _candidateService.UpdateCandidate(candidateDTO);

            // Assert
            _candidateDbRepositoryMock.Verify(repo => repo.Update(It.Is<Candidate>(c =>
                c.CandidateId == candidateDTO.CandidateId &&
                c.CandidateName == candidateDTO.CandidateName &&
                c.Votes == candidateDTO.Votes
            )), Times.Once);
        }
    }
}
