namespace Kelderro.Poc.Data.Entities
{
	public class TicTacTooGame
	{
		public TicTacTooGame()
		{
			Player1 = new User();
			Player2 = new User();
		}

		public int Id { get; set; }

		public User Player1 { get; set; }

		public User Player2 { get; set; }
	}
}
