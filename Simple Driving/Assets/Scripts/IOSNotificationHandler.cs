using System.Collections;
using System.Collections.Generic;
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif
using UnityEngine;

public class IOSNotificationHandler : MonoBehaviour
{
#if UNITY_IOS
    public void ScheduleNotification(int minutes)
    {
        iOSNotification notification = new iOSNotification
        {
            Title = "Energy Recharged",
            Subtilte = "Your energy has been recharged",
            Body = "Your energy has been recharged come back to play again",
            ShowInForeground = true,
            ForegroundPresentationOptiopn = (PresentationOption.Alert || PresentationOption.Sound);
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = new iOSNotificationTimeIntervalTrigger
            {
                TimeInterval = new System.TimeSpan(0, minutes, 0),
                Repeats = false
            }
        };

        iOSNotificationCenter.ScheduleNotification(notificiation);
    }
#endif
}
