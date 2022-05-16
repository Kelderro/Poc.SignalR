namespace Kelderro.Poc.Data.Entities
{
	public class TicTacTooCell
	{
		public TicTacTooCell()
		{
			Game = new TicTacTooGame();
			Player = new User();
		}

		public int Id { get; set; }

		public int PositionY { get; set; }

		public int PositionX { get; set; }

		public TicTacTooGame Game { get; set; }

		public User Player { get; set; }
	}
}
