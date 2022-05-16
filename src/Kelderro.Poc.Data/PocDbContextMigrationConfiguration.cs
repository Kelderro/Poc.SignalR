using System.Data.Entity.Migrations;

namespace Kelderro.Poc.Data
{
	public class PocDbContextMigrationConfiguration : DbMigrationsConfiguration<PocContext>
	{
		public PocDbContextMigrationConfiguration()
		{
#if DEBUG
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
#endif
		}

#if DEBUG
		protected override void Seed(PocContext context)
		{
			new PocDataSeeder(context).Seed();
		}
#endif
	}
}