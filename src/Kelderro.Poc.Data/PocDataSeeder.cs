using System;
using System.Linq;
using Kelderro.Poc.Data.Entities;
using NLog;

namespace Kelderro.Poc.Data
{
	public class PocDataSeeder
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private readonly PocContext _ctx;

		public PocDataSeeder(PocContext ctx)
		{
			_ctx = ctx;
		}

		public void Seed()
		{
			if (_ctx.Users.Any())
			{
				_logger.Debug("Users found in database. No need to seed the database.");
				return;
			}

			try
			{
				foreach (var userData in Users.Select(SplitValue))
				{
					var user = new User
					{
						FirstName = userData[0],
						MiddleName = userData[1],
						LastName = userData[2],
						UserName = userData[0],
						Password = RandomString(8),
						RegistrationDate = DateTime.UtcNow.AddDays(-new Random().Next(1, 365))
					};

					_logger.Debug("{0} = {1}", user.UserName, user.Password);

					_ctx.Users.Add(user);
				}

				_ctx.SaveChanges();
			}
			catch (Exception ex)
			{
				_logger.Debug(ex);
				throw;
			}
		}

		private static string[] SplitValue(string val)
		{
			return val.Split(',');
		}

		private static string RandomString(int size)
		{
			var rng = new Random((int)DateTime.Now.Ticks);
			var _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			var buffer = new char[size];

			for (var i = 0; i < size; i++)
			{
				buffer[i] = _chars[rng.Next(_chars.Length)];
			}
			return new string(buffer);
		}

		private static readonly string[] Users =
		{
			"Rob,op den,Kelder",
			"Sanne,op den,Kelder",
			"Bernard,,Koenen",
			"Stephan,,Klop",
			"Henk,de,Vogel",
			"Phoebie,de,Hond",
			"Jelly,de,blaffer"
		};


	}
}