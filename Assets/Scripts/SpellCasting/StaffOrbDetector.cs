using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffOrbDetector : MonoBehaviour
{
    public static Action<GameObject> OnCollision;

				private void OnTriggerEnter(Collider collision)
				{
								FireOnCollideEvent(collision.gameObject);
				}

				void FireOnCollideEvent(GameObject obj)
    {
        OnCollision?.Invoke(obj);
    }
}
