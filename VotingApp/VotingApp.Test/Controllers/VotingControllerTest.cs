using Microsoft.AspNetCore.Mvc;
using Moq;
using VotingApp.Controllers;
using VotingApp.DTO;
using VotingApp.Models;
using VotingApp.Services;

namespace VotingApp.Test.Controllers
{
    public class VotingControllerTest
    {
        private readonly Mock<ICandidateService> _candidateServiceMock;
        private readonly Mock<IVoterService> _voterServiceMock;
        private readonly VotingController _controller;

        public VotingControllerTest()
        {
            _candidateServiceMock = new Mock<ICandidateService>();
            _voterServiceMock = new Mock<IVoterService>();
            _controller = new VotingController(_candidateServiceMock.Object, _voterServiceMock.Object);
        }

        [Fact]
        public void GetVotingMachineData_ReturnsExpectedData()
        {
            // Arrange
            var expectedVoters = new List<VoterDTO> {
            new VoterDTO { VoterId = 1, VoterName = "John Doe", HasVote = false },
            new VoterDTO { VoterId = 2, VoterName = "Jane Smith", HasVote = false }
        };
            var expectedCandidates = new List<CandidateDTO> {
            new CandidateDTO { CandidateId = 1, CandidateName = "Candidate 1", Votes = 0 },
            new CandidateDTO { CandidateId = 2, CandidateName = "Candidate 2", Votes = 0 }
        };

            _voterServiceMock.Setup(service => service.GetVoterList()).Returns(expectedVoters);
            _candidateServiceMock.Setup(service => service.GetCandidatesList()).Returns(expectedCandidates);

            // Act
            var actionResult = _controller.GetVotingMachineData();
            var okResult = Assert.IsType<OkObjectResult>(actionResult);

            // Assert
            Assert.NotNull(okResult.Value);

            var returnData = okResult.Value as VotingMachineData;

            Assert.NotNull(returnData);
            Assert.NotNull(returnData.voterList);
            Assert.NotNull(returnData.candidateList);

            Assert.Equal(expectedVoters.Count, returnData.voterList.Count());
            Assert.Equal(expectedCandidates.Count, returnData.candidateList.Count);
        }

        [Fact]
        public void CasteVote_ReturnsOk_WhenVoteIsCastedSuccessfully()
        {
            // Arrange
            var castVote = new CastVoteModel { CandidateId = 1, VoterId = 1 };
            var voters = new List<VoterDTO> { new VoterDTO { VoterId = 1, VoterName = "John Doe", HasVote = false } };
            var candidates = new List<CandidateDTO> { new CandidateDTO { CandidateId = 1, CandidateName = "Candidate 1", Votes = 0 } };

            _voterServiceMock.Setup(service => service.GetVoterList()).Returns(voters);
            _candidateServiceMock.Setup(service => service.GetCandidatesList()).Returns(candidates);

            // Act
            var result = _controller.CasteVote(castVote);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<VoteResponse>(okResult.Value);
            Assert.Equal("Vote casted successfully", response.message);
            _voterServiceMock.Verify(service => service.UpdateVoter(It.Is<VoterDTO>(v => v.VoterId == castVote.VoterId && v.HasVote == true)), Times.Once);
            _candidateServiceMock.Verify(service => service.UpdateCandidate(It.Is<CandidateDTO>(c => c.CandidateId == castVote.CandidateId && c.Votes == 1)), Times.Once);
        }


        [Fact]
        public void CasteVote_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = _controller.CasteVote(new CastVoteModel());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void CasteVote_ReturnsBadRequest_WhenCastVoteIsNull()
        {
            // Act
            var result = _controller.CasteVote(null!);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Select voter and candidate", ((dynamic)badRequestResult.Value!).message);
        }

        [Fact]
        public void CasteVote_ReturnsBadRequest_WhenCandidateIdOrVoterIdIsZero()
        {
            // Arrange
            var castVote = new CastVoteModel { CandidateId = 0, VoterId = 0 };

            // Act
            var result = _controller.CasteVote(castVote);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Select voter and candidate", ((dynamic)badRequestResult.Value!).message);
        }

        [Fact]
        public void CasteVote_ReturnsBadRequest_WhenVoterNotFound()
        {
            // Arrange
            var castVote = new CastVoteModel { CandidateId = 1, VoterId = 1 };
            _voterServiceMock.Setup(service => service.GetVoterList()).Returns(new List<VoterDTO>());

            // Act
            var result = _controller.CasteVote(castVote);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Voter not found", ((dynamic)badRequestResult.Value!).message);
        }

        [Fact]
        public void CasteVote_ReturnsBadRequest_WhenCandidateNotFound()
        {
            // Arrange
            var castVote = new CastVoteModel { CandidateId = 1, VoterId = 1 };
            var voters = new List<VoterDTO> { new VoterDTO { VoterId = 1, VoterName = "John Doe", HasVote = false } };
            _voterServiceMock.Setup(service => service.GetVoterList()).Returns(voters);
            _candidateServiceMock.Setup(service => service.GetCandidatesList()).Returns(new List<CandidateDTO>());

            // Act
            var result = _controller.CasteVote(castVote);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Candidate not found", ((dynamic)badRequestResult.Value!).message);
        }
    }
}
