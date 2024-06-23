using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CliffPhase
{
    [SerializeField]
    public List<GameObject> objects;
}

public class PlayerCliff : MonoBehaviour
{
    [SerializeField] private List<CliffPhase> lifePhases;

    public static Action TakeDamage;

    private int currentPhase = 0;

    void OnEnable()
    {
        TakeDamage += TakeDamageEventHandler;
    }

    void OnDisable()
    {
        TakeDamage -= TakeDamageEventHandler;
    }

    void TakeDamageEventHandler()
    {
        if (lifePhases.Count > 0 && currentPhase < lifePhases.Count)
        {
            //StartCoroutine(AnimateRocks());
            AnimateRocks();
        }
        else
        {
            // Death
        }
    }

    void AnimateRocks()
    {
        CliffPhase phaseObjects = lifePhases[currentPhase];

        foreach (GameObject obj in phaseObjects.objects)
        {
            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.AddForce(UnityEngine.Random.onUnitSphere * 10f);
            rb.AddTorque(UnityEngine.Random.onUnitSphere * 10f);

            //yield return new WaitForSeconds(0.1f);
        }

        currentPhase++;
    }
}
