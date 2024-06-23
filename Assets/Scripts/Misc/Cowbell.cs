using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cowbell : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        _audioSource.Play();
    }
}
