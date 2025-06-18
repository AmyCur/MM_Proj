using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AmyCurr.notifications
{
    // Notification state
    public enum NS
    {
        intro,
        idle,
        outro,
        none
    }

    [System.Serializable]
    public class Notification
    {
        [HideInInspector] public NS s;
        [HideInInspector] public Coroutine currentAction;
        [HideInInspector] public NotificationManager nm;
        [HideInInspector] public Canvas canvas;
        GameObject worldObj;

        public void Create()
        {
            worldObj = GameObject.Instantiate(nm.gameObject, Vector3.zero, Quaternion.identity);
            worldObj.transform.SetParent(canvas.transform, false);

            currentAction = nm.StartCoroutine(nm.IntroEffect());
        }

        public void Remove()
        {
            currentAction = nm.StartCoroutine(nm.OutroEffect());
        }

    }

    public static class NotifActions
    {
        public static IEnumerator HandleNotification(Notification n)
        {
            n.Create();
            yield return new WaitForSeconds(n.nm.idleTime);
            n.Remove();
        }

    }




}


