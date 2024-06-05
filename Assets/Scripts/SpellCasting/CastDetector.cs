using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class CastDetector : MonoBehaviour
{
				public static Action<SpellTypes> OnCast;

				[SerializeField] private SpellTypes type;
				[SerializeField] private GameObject parent;
				[SerializeField] private List<GameObject> points;


				private int currentPoint;

				private void OnEnable()
				{
								SpellSelector.SpellSelected += SpellSelectedEventHandler;
								StaffOrbDetector.OnCollision += StaffOnCollisionEventHandler;
				}

				private void OnDisable()
				{
								SpellSelector.SpellSelected -= SpellSelectedEventHandler;
								StaffOrbDetector.OnCollision -= StaffOnCollisionEventHandler;
				}

				void SpellSelectedEventHandler(SpellTypes selectedSpell)
				{
								if (selectedSpell == type)
								{
												// If there are no points (empty spell)
												// Fire the event immediately
												if (points.Count == 0) FireOnCastEvent(type);
												parent.SetActive(true);
								}
								else
								{
												parent.SetActive(false);

												currentPoint = 0;
												foreach (GameObject obj in points)
												{
																obj.SetActive(true);
												}
								}
				}

				void StaffOnCollisionEventHandler(GameObject other)
				{
								if (points.Count > 0)
								{
												if (other == points[currentPoint])
												{
																other.gameObject.SetActive(false);
																if (currentPoint < points.Count - 1)
																{
																				currentPoint++;
																}
																else
																{
																				FireOnCastEvent(type);
																}
												}
								}
				}

				void FireOnCastEvent(SpellTypes type)
				{
								OnCast?.Invoke(type);
				}
}
