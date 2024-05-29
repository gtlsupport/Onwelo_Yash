using Microsoft.EntityFrameworkCore;
using VotingApp.Data.Entities;

namespace VotingApp.Repository.ApplicationDBContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<Voter> Voter { get; set; }
    }
}
