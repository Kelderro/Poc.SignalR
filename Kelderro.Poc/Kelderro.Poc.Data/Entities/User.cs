using System;
using System.Collections.Generic;

namespace Kelderro.Poc.Data.Entities
{
	public class User
	{
		public User()
		{
			Messages = new List<Message>();
		}

		public int Id { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public DateTime RegistrationDate { get; set; }

		public ICollection<Message> Messages { get; private set; }
		
	}
}