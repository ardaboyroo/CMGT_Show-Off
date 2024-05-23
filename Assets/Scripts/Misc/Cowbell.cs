using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cowbell : MonoBehaviour
{
				AudioSource _audioSource;
				ParticleSystem _pSystem;

				private void Start()
				{
								_audioSource = GetComponent<AudioSource>();
								_pSystem = GetComponentInChildren<ParticleSystem>();
				}

				private void OnTriggerEnter(Collider collision)
				{
								_audioSource.Play();
								if (_pSystem != null)
								{
												_pSystem.Play();
								}
								else
								{
												Debug.Log("PARTICLE SYSTEM NOT FOUND");
								}
				}
}
