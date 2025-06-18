using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AmyCurr.notifications;


public abstract class NotificationManager : MonoBehaviour
{
    [SerializeField] Notification notif = new();

    [Header("Timings")]
    [Header("Idle")]
    public float idleTime;

    [Header("Intro")]
    public float introTime;
    public float introIncrement = 0.1f;
    [Header("Outro")]
    public float outroTime;
    public float outroIncrement = 0.1f;


    void Start()
    {
        notif.nm = this;
        notif.canvas = GetComponent<Canvas>();

        if (notif.canvas == null)
        {
            Debug.LogError($"Canvas is null in {this}");
        }
    }

    public abstract IEnumerator IntroEffect(float t = 0);
    public abstract IEnumerator OutroEffect(float t = 0);
}
