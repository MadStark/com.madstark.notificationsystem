using System;
using System.Collections.Generic;
using System.Linq;

namespace MadStark.NotificationSystem
{
	/// <summary>
	/// Framework to send and listen to notifications.
	/// </summary>
	public class NotificationChannel
	{
		/// <summary>
		/// A shared notification channel.
		/// </summary>
		public static NotificationChannel Shared { get; } = new NotificationChannel();

		/// <summary>
		/// Called when a notification of any type is received on this channel.
		/// </summary>
		public virtual event NotificationDelegate OnNotificationReceived;

		private readonly Dictionary<Type, CallbackList> _filteredDelegates;


		public NotificationChannel()
		{
			_filteredDelegates = new Dictionary<Type, CallbackList>();
		}

		/// <summary>
		/// Send a notification in this channel.
		/// This will invoke any subscribers to this notification type or any of its subclass.
		/// </summary>
		/// <param name="notification">The notification to send.</param>
		public virtual void Send<T>(T notification) where T : Notification
		{
			OnNotificationReceived?.Invoke(notification);

			IEnumerable<CallbackList<T>> callbacksQuery = _filteredDelegates
				.Where(x => x.Key.IsAssignableFrom(typeof(T)))
				.Select(x => x.Value)
				.Cast<CallbackList<T>>();

			foreach (CallbackList<T> callbacks in callbacksQuery)
			{
				callbacks.Notify(notification);
			}
		}

		/// <summary>
		/// Add a delegate to be invoked when a notification of this type or any of its subclass is sent.
		/// </summary>
		/// <param name="callback">Callback to be invoked.</param>
		/// <typeparam name="T">The type of notification this callback handles.</typeparam>
		public virtual void Subscribe<T>(NotificationDelegate<T> callback) where T : Notification
		{
			if (!_filteredDelegates.TryGetValue(typeof(T), out CallbackList callbacks))
			{
				callbacks = new CallbackList<T>();
				_filteredDelegates.Add(typeof(T), callbacks);
			}

			((CallbackList<T>)callbacks).Add(callback);
		}

		/// <summary>
		/// Remove a callback from the listeners of a given type of notification.
		/// </summary>
		/// <param name="callback">Callback to remove.</param>
		/// <typeparam name="T">The type of notification to stop listening.</typeparam>
		public virtual void Unsubscribe<T>(NotificationDelegate<T> callback) where T : Notification
		{
			if (_filteredDelegates.TryGetValue(typeof(T), out CallbackList callbacks))
			{
				((CallbackList<T>)callbacks).Remove(callback);
			}
		}
	}
}
