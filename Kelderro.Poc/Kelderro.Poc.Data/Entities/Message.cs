namespace Kelderro.Poc.Data.Entities
{
	public class Message
	{
		public Message()
		{
			User = new User();
		}

		public int Id { get; set; }

		public string Content { get; set; }

		public User User { get; set; }
	}
}
