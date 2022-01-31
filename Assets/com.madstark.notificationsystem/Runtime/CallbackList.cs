using System;
using System.Collections.Generic;
using System.Threading;

namespace MadStark.NotificationSystem
{
	public abstract class CallbackList { }

	public class CallbackList<T> : CallbackList where T : Notification
	{
		private readonly List<NotificationDelegate<T>> _callbacks;


		public CallbackList()
		{
			_callbacks = new List<NotificationDelegate<T>>();
		}

		public CallbackList(int capacity)
		{
			_callbacks = new List<NotificationDelegate<T>>(capacity);
		}

		public void Add(NotificationDelegate<T> callback)
		{
			_callbacks.Add(callback);
		}

		public void Remove(NotificationDelegate<T> callback)
		{
			if (_callbacks.Contains(callback))
			{
				_callbacks.Remove(callback);
			}
		}

		public void Notify(T notification)
		{
			foreach (NotificationDelegate<T> callback in _callbacks)
			{
				try
				{
					callback.Invoke(notification);
				}
				catch (Exception e)
				{
					if (e is OutOfMemoryException || e is ThreadAbortException || e is StackOverflowException)
						throw;
				}
			}
		}
	}
}
