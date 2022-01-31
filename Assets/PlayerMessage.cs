using MadStark.NotificationSystem;

namespace DefaultNamespace
{
	public class PlayerMessage : Notification
	{
		public readonly string playerId;


		public PlayerMessage(string playerId, string message) : base(message)
		{
			this.playerId = playerId;
		}
	}
}
