using Microsoft.AspNetCore.Mvc;
using Moq;
using VotingApp.Controllers;
using VotingApp.DTO;
using VotingApp.Models;
using VotingApp.Services;

namespace VotingApp.Test.Controllers
{
    public class VoterControllerTest
    {
        private readonly Mock<IVoterService> _voterServiceMock;
        private readonly VoterController _controller;

        public VoterControllerTest()
        {
            _voterServiceMock = new Mock<IVoterService>();
            _controller = new VoterController(_voterServiceMock.Object);
        }

        [Fact]
        public void GetAllVoters_ReturnsListOfVoters()
        {
            // Arrange
            var expectedVoters = new List<VoterDTO> {
            new VoterDTO {  VoterId = 1, VoterName = "John Doe",   HasVote = false},
            new VoterDTO {  VoterId = 2, VoterName = "Jane Smith", HasVote = true}
        };
            _voterServiceMock.Setup(service => service.GetVoterList()).Returns(expectedVoters);

            // Act
            var actionResult = _controller.GetAllVoters();
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var voters = Assert.IsType<List<VoterDTO>>(okResult.Value);

            // Assert
            Assert.Equal(expectedVoters.Count, voters.Count);
        }

        [Fact]
        public void InsertVoter_ValidModel_ReturnsOkWithVoterList()
        {
            // Arrange
            var voterModel = new VoterModel {VoterId=1, VoterName = "John Doe", HasVote = false };
            var voterDTO = new VoterDTO { VoterId = 1, VoterName = "John Doe", HasVote = true };
            var expectedVoters = new List<VoterDTO> { voterDTO };

            _voterServiceMock.Setup(service => service.InsertVoter(It.IsAny<VoterDTO>()));
            _voterServiceMock.Setup(service => service.GetVoterList()).Returns(expectedVoters);

            // Act
            var actionResult = _controller.InsertVoter(voterModel);
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var voters = Assert.IsType<List<VoterDTO>>(okResult.Value);

            // Assert
            Assert.Single(voters);
            Assert.Equal(voterDTO.VoterName, voters[0].VoterName);
            Assert.Equal(voterDTO.HasVote, voters[0].HasVote);
        }

        [Fact]
        public void InsertVoter_InvalidModel_ReturnsBadRequestWithModelState()
        {
            // Arrange
            var invalidModel = new VoterModel(); 
            _controller.ModelState.AddModelError("VoterName", "The VoterName field is required.");

            // Act
            var actionResult = _controller.InsertVoter(invalidModel);

            // Assert
            // Check if ModelState is not valid
            Assert.False(_controller.ModelState.IsValid);

            // Check if the action result is BadRequestObjectResult
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);

            // Check if the returned value is ModelStateDictionary
            var modelState = Assert.IsType<SerializableError>(badRequestResult.Value);

            // Assert that ModelState contains errors
            Assert.True(modelState.ContainsKey("VoterName"));
        }

        [Fact]
        public void InsertVoter_NullModel_ReturnsBadRequest()
        {
            // Arrange
            VoterModel nullModel = null!; // Create a null model

            // Act
            var actionResult = _controller.InsertVoter(nullModel);

            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }
    }
}
