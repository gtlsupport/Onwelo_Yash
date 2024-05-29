using Microsoft.AspNetCore.Mvc;
using Moq;
using VotingApp.Controllers;
using VotingApp.DTO;
using VotingApp.Models;
using VotingApp.Services;

namespace VotingApp.Test.Controllers
{
    public class CandidateControllerTest
    {
        private readonly Mock<ICandidateService> _candidateServiceMock;
        private readonly CandidateController _controller;

        public CandidateControllerTest()
        {
            _candidateServiceMock = new Mock<ICandidateService>();
            _controller = new CandidateController(_candidateServiceMock.Object);
        }

        [Fact]
        public void InsertCandidate_ReturnsOkResult_WithCandidateList()
        {
            // Arrange
            var candidateModel = new CandidateModel
            {
                CandidateId = 1,
                CandidateName = "John Doe",
                Votes = 100
            };

            var candidateDTO = new CandidateDTO
            {
                CandidateId = 1,
                CandidateName = "Jane Smith",
                Votes = 150
            };

            var candidatesList = new List<CandidateDTO>
        {
            candidateDTO
        };

            _candidateServiceMock.Setup(service => service.InsertCandidate(It.IsAny<CandidateDTO>()));
            _candidateServiceMock.Setup(service => service.GetCandidatesList()).Returns(candidatesList);

            // Act
            var result = _controller.InsertCandidate(candidateModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCandidatesList = Assert.IsAssignableFrom<IEnumerable<CandidateDTO>>(okResult.Value);
            Assert.Single(returnedCandidatesList);
        }

        [Fact]
        public void InsertCandidate_ReturnsBadRequest_WhenModelStateIsNotValid()
        {
            // Arrange
            var invalidModel = new CandidateModel(); 

            _controller.ModelState.AddModelError("CandidateName", "The CandidateName field is required.");

            // Act
            var result = _controller.InsertCandidate(invalidModel);

            // Assert
            // Check if the action result is BadRequestObjectResult
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            // Check if the returned value is ModelStateDictionary
            var modelState = Assert.IsType<SerializableError>(badRequestResult.Value);

            // Assert that ModelState contains errors
            Assert.True(modelState.ContainsKey("CandidateName"));
        }

        [Fact]
        public void InsertCandidate_ReturnsBadRequest_WhenCandidateModelIsNull()
        {
            // Arrange
            CandidateModel nullModel = null!; // Create a null model

            // Act
            var result = _controller.InsertCandidate(nullModel);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetAllCandidateList_ReturnsOkResult_WithCandidateList()
        {
            // Arrange
            var candidatesList = new List<CandidateDTO>
            {
                new CandidateDTO { CandidateId = 1, CandidateName = "John Doe", Votes = 100 },
                new CandidateDTO { CandidateId = 2, CandidateName = "Jane Smith", Votes = 75 }
            };

            _candidateServiceMock.Setup(service => service.GetCandidatesList()).Returns(candidatesList);

            // Act
            var actionResult = _controller.GetAllCandidateList();
            var okResult = actionResult as OkObjectResult;
            var model = okResult!.Value as List<CandidateDTO>;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(model);
            Assert.Equal(candidatesList.Count, model.Count);
        }
    }
}