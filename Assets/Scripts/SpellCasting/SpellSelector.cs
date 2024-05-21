using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellSelector : MonoBehaviour
{
				[SerializeField] InputActionReference moveActionReference;
				[SerializeField] GameObject book;

				SpellTypes selectedSpell = SpellTypes.Fire;
				Vector2 joystickAxisValue;

				public bool debounce = false;

				// Start is called before the first frame update
				void Start()
				{
								
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
				}

				Color GetSpellColour(SpellTypes type)
				{
								switch (type)
								{
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
}
