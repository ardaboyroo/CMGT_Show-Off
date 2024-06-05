using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellSelector : MonoBehaviour
{
				public static Action<SpellTypes> SpellSelected;

				[SerializeField] private InputActionReference moveActionReference;
				[SerializeField] private GameObject book;

				private SpellTypes selectedSpell = SpellTypes.Fire;
				private Vector2 joystickAxisValue;

				private bool debounce = false;

				// Start is called before the first frame update
				void Start()
				{
								ChangeHandColour(false);
				}

				// Update is called once per frame
				void Update()
				{
								joystickAxisValue = moveActionReference.action.ReadValue<Vector2>();

								if (debounce == false)
								{
												// Right
												if (joystickAxisValue.x > 0.75f)
												{
																debounce = true;
																ChangeHandColour(true);
												}

												// Left
												else if (joystickAxisValue.x < -0.75f)
												{
																debounce = true;
																ChangeHandColour(false);
												}
								}

								if (joystickAxisValue.x < 0.75f && joystickAxisValue.x > -0.75f)
								{
												debounce = false;
								}
				}

				void ChangeHandColour(bool nextSpell)
				{
								int enumSize = Enum.GetNames(typeof(SpellTypes)).Length;

								if (nextSpell)
								{
												if ((int)selectedSpell + 1 < enumSize)
												{
																selectedSpell = (SpellTypes)((int)selectedSpell + 1);
												}
								}
								else
								{
												if ((int)selectedSpell - 1 >= 0)
												{
																selectedSpell = (SpellTypes)((int)selectedSpell - 1);
												}
								}

								book.GetComponent<MeshRenderer>().material.color = GetSpellColour(selectedSpell);
								FireSpellSelectedEvent();
				}

				Color GetSpellColour(SpellTypes type)
				{
								switch (type)
								{
												case SpellTypes.Gun:
																return new Color(0.1f, 0.1f, 1f);

												case SpellTypes.Fire:
																return new Color(1f, 0f, 0f);

												case SpellTypes.Lightning:
																return new Color(1f, 1f, 0f);

												case SpellTypes.Earth: 
																return new Color(0.4f, 0.2f, 0f);

												default:
																return new Color(0f, 0f, 0f);
								}
				}

				void FireSpellSelectedEvent()
				{
								SpellSelected?.Invoke(selectedSpell);
				}
}
