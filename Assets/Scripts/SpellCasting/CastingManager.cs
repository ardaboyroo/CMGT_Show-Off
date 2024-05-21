using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellTypes
{
				Fire,
				Lightning,
				Earth
}

public class CastingManager : MonoBehaviour
{
				public static CastingManager Instance;
				public static event Action<SpellTypes> OnCast;

				private void Awake()
				{
								if (Instance != null && Instance != this)
								{
												Debug.LogWarning($"More than one {this.name} detected, destroying this instance...");
												Destroy(this);
								}
								else
												Instance = this;
				}

				// Start is called before the first frame update
				void Start()
				{

				}

				// Update is called once per frame
				void Update()
				{

				}

				private void FireOnCastEvent(SpellTypes type)
				{
								OnCast?.Invoke(type);
				}
}
