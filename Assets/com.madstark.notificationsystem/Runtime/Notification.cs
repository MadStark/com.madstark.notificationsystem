namespace MadStark.NotificationSystem
{
	public class Notification
	{
		public virtual string Message => m_Message;

		internal readonly string m_Message;


		public Notification() { }

		public Notification(string message)
		{
			m_Message = message;
		}
	}
}
