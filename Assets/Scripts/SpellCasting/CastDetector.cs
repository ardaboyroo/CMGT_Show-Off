using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CastDetector : MonoBehaviour
{
				public static Action<SpellTypes> OnCast;

				[SerializeField] private SpellTypes type;
				[SerializeField] private GameObject parent;
				[SerializeField] private List<Material> pointMaterials;

				[Space(10)]
				[SerializeField] private List<GameObject> points;

				private int currentPoint;
				private static SpellTypes? previousCasted = null;

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
												if (points.Count == 0) FireOnCastEvent(type);
												parent.SetActive(true);

												UpdateAllPointMaterials();
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

				void StaffOnCollisionEventHandler(List<GameObject> other)
				{
								if (points.Count > 0 && currentPoint < points.Count)
								{
												foreach (GameObject point in other.ToArray())
												{
																if (point == points[currentPoint])
																{
																				currentPoint++;
																				point.SetActive(false);
																				UpdateAllPointMaterials();
																				other.Remove(point);

																				if (currentPoint < points.Count)
																				{
																								StaffOnCollisionEventHandler(other);
																				}
																				else
																				{
																								//previousCasted = type;
																								FireOnCastEvent(type);
																				}
																}
												}
								}

								/*if (points.Count > 0)
								{
												if (other == points[currentPoint])
												{
																other.gameObject.SetActive(false);
																if (currentPoint < points.Count - 1)
																{
																				currentPoint++;
																				UpdateAllPointMaterials();
																}
																else
																{
																				FireOnCastEvent(type);
																}
												}
								}*/
				}

				void FireOnCastEvent(SpellTypes type)
				{
								OnCast?.Invoke(type);
				}

				void UpdateAllPointMaterials()
				{
								for (int i = 0; i + currentPoint < points.Count; i++)
								{
												MeshRenderer mr = points[i + currentPoint].GetComponent<MeshRenderer>();
												if (i < pointMaterials.Count)
												{
																mr.material = pointMaterials[i];
												}
												else
												{
																mr.material = pointMaterials.Last();
												}
								}
				}
}
