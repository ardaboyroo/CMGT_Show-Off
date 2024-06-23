using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StaffOrbDetector : MonoBehaviour
{
    public static Action<List<GameObject>> OnCollision;

    private List<GameObject> touching;

    void Start()
    {
        touching = new List<GameObject>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!touching.Contains(other.gameObject))
        {
            touching.Add(other.gameObject);
        }

        FireOnCollideEvent();
    }

    void OnTriggerExit(Collider other)
    {
        if (touching.Contains(other.gameObject))
        {
            touching.Remove(other.gameObject);
        }

        FireOnCollideEvent();
    }

    void FireOnCollideEvent()
    {
        OnCollision?.Invoke(touching);
    }
}
