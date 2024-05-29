using Microsoft.EntityFrameworkCore;
using VotingApp.Data.Entities;
using VotingApp.Repository.ApplicationDBContext;
using VotingApp.Repository.Repository;

namespace VotingApp.Test.Repositories
{
    public class DBRepositoryTest
    {
        private readonly DbRepository<Voter> _repository;
        private readonly AppDBContext _dbContext;

        public DBRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
          .UseInMemoryDatabase(databaseName: "VotingApp")
          .Options;
            _dbContext = new AppDBContext(options);
            _dbContext.Database.EnsureCreated();
            _repository = new DbRepository<Voter>(_dbContext);

            // Seed the in-memory database with test data
            _dbContext.Voter.AddRange(new List<Voter>
        {
            new Voter { VoterId = 1, VoterName = "John Doe", HasVote = false },
            new Voter { VoterId = 2, VoterName = "Jane Smith", HasVote = true }
        });
            _dbContext.SaveChanges();
        }

        [Fact]
        public void GetAll_ReturnsAllEntities()
        {
            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("John Doe", result[0].VoterName);
            Assert.Equal("Jane Smith", result[1].VoterName);
        }

        [Fact]
        public void Insert_AddsEntityToDatabase()
        {
            // Arrange
            var newVoter = new Voter { VoterId = 25, VoterName = "Alice Johnson", HasVote = false };

            // Act
            _repository.Insert(newVoter);
            var result = _repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            var insertedVoter = result.FirstOrDefault(v => v.VoterId == 25);
            Assert.NotNull(insertedVoter);
            Assert.Equal("Alice Johnson", insertedVoter.VoterName);
        }

        [Fact]
        public void Insert_ThrowsArgumentNullException_WhenEntityIsNull()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _repository.Insert(null!));
            Assert.Equal("entity", exception.ParamName);
        }

        [Fact]
        public void Update_UpdatesEntityInDatabase()
        {
            // Arrange
            var voterToUpdate = _dbContext.Voter.FirstOrDefault(v => v.VoterId == 1);
            voterToUpdate!.VoterName = "John Updated";

            // Act
            _repository.Update(voterToUpdate);
            var result = _repository.GetAll();

            // Assert
            Assert.NotNull(result);
            var updatedVoter = result.Find(v => v.VoterId == 1);
            Assert.NotNull(updatedVoter);
            Assert.Equal("John Updated", updatedVoter.VoterName);
        }


        [Fact]
        public void Update_ThrowsArgumentNullException_WhenEntityIsNull()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _repository.Update(null!));
            Assert.Equal("entity", exception.ParamName);
        }
    }
}

