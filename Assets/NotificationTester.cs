using System;
using MadStark.NotificationSystem;
using UnityEngine;

namespace DefaultNamespace
{
	public class NotificationTester : MonoBehaviour
	{
		private void Awake()
		{
			NotificationChannel.Shared.Subscribe<PlayerMessage>(HandleMessageNotification);
			NotificationChannel.Shared.Subscribe<Notification>(HandleAnyNotification);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.M))
			{
				NotificationChannel.Shared.Send(new PlayerMessage("Bobby", "Hello there!"));
			}

			if (Input.GetKeyDown(KeyCode.N))
			{
				NotificationChannel.Shared.Send(new Notification());
			}
		}

		private void OnDestroy()
		{
			NotificationChannel.Shared.Unsubscribe<PlayerMessage>(HandleMessageNotification);
			NotificationChannel.Shared.Unsubscribe<Notification>(HandleAnyNotification);
		}

		private void HandleMessageNotification(Notification notification)
		{
			if (notification is PlayerMessage messageNotification)
			{
				Debug.Log($"[{messageNotification.playerId}]: {messageNotification.Message}");
			}
		}

		private void HandleAnyNotification(Notification notification)
		{
			Debug.Log($"Received a notification of type {notification.GetType()}.");
		}
	}
}
