using System.Data.Entity;

namespace Kelderro.Poc.Data
{
	public class PocDbContextDropCreateDatabaseAlways : DropCreateDatabaseAlways<PocContext>
	{
#if DEBUG
		protected override void Seed(PocContext context)
		{
			new PocDataSeeder(context).Seed();
		}
#endif
	}
}