namespace MadStark.NotificationSystem
{
	public delegate void NotificationDelegate(Notification notification);

	public delegate void NotificationDelegate<in T>(T notification) where T : Notification;
}
