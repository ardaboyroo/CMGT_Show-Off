using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlameThrower : MonoBehaviour
{
    [SerializeField] private InputActionReference triggerActionReference;
    [SerializeField] private GameObject particleEmitter;
    [SerializeField] private Transform origin;
    [SerializeField] private Transform target;

    private float triggerValue;
    private ParticleSystem emitter;
    private ParticleSystem.MainModule main;

    // Start is called before the first frame update
    void Start()
    {
        emitter = particleEmitter.GetComponent<ParticleSystem>();
        main = emitter.main;
        main.playOnAwake = false;
        main.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        triggerValue = triggerActionReference.action.ReadValue<float>();

        if (triggerValue > 0.75f)
        {
            if (!emitter.isPlaying)
                emitter.Play();

            // Move and rotate the particle
            particleEmitter.transform.position = origin.position;
            particleEmitter.transform.rotation = Quaternion.LookRotation(target.position - origin.position);
        }
        else
        {
            emitter.Stop();
        }
    }
}
