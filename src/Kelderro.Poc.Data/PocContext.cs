using System.Data.Entity;
using Kelderro.Poc.Data.Entities;
using Kelderro.Poc.Data.EntityTypeConfiguration;

namespace Kelderro.Poc.Data
{
	public class PocContext : DbContext
	{
		public PocContext() :
			base("PocConnection")
		{
			Configuration.ProxyCreationEnabled = false;
			Configuration.LazyLoadingEnabled = false;
			Database.SetInitializer(new PocDbContextDropCreateDatabaseAlways());
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<TicTacTooGame> TicTacTooGames { get; set; }
		public DbSet<TicTacTooCell> TicTacTooCells { get; set; }

		
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new UserMapper());

			base.OnModelCreating(modelBuilder);
		}
	}
}
