using System;

namespace Nuance.PowerCast.Common
{
	public class NotificationEventArgs : EventArgs
	{
		public Notification Notification;

		public NotificationEventArgs(Notification notification)
		{
			Notification = notification;
		}
	}
}
