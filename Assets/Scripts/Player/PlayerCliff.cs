using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

[Serializable]
public class CliffPhase
{
    [SerializeField]
    public List<GameObject> objects;
}

public class PlayerCliff : MonoBehaviour
{
    [SerializeField] AudioClip cliffDamageClip;
    [SerializeField] private GameObject XROrigin;
    [SerializeField] private List<CliffPhase> lifePhases;

    public static Action TakeDamage;

    private int currentPhase = 0;

    private bool dead;

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
        if (lifePhases.Count - 1 > 0 && currentPhase < lifePhases.Count - 1)
        {
            AudioManager.Play2DOneShot(cliffDamageClip);
            AnimateRocks();
        }
        else if (!dead)
        {
            // Death
            dead = true;
            XROrigin.AddComponent<Rigidbody>().AddForce(Vector3.down, ForceMode.Impulse);
            AnimateRocks();
        }
    }

    void AnimateRocks()
    {
        
        CliffPhase phaseObjects = lifePhases[currentPhase];

        foreach (GameObject obj in phaseObjects.objects)
        {
            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.AddForce(UnityEngine.Random.onUnitSphere * 10f, ForceMode.Impulse);
            rb.AddTorque(UnityEngine.Random.onUnitSphere * 100f);

            //yield return new WaitForSeconds(0.1f);
        }

        currentPhase++;
    }
}
