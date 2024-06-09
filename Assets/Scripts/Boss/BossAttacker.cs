using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacker : MonoBehaviour
{
				private AttackTypes currentAttack;

				void OnEnable()
				{
								BossAttackChooser.OnAttack += OnAttackEventHandler;
				}

				void OnDisable()
				{
								BossAttackChooser.OnAttack -= OnAttackEventHandler;
				}

				#region EVENT_HANDLERS
				void Update()
				{
								switch (currentAttack)
								{
												case AttackTypes.Cats:
																OnUpdateCats();
																break;

												case AttackTypes.Hairball:
																OnUpdateHairball();
																break;

												case AttackTypes.Slap:
																OnUpdateSlap();
																break;

												case AttackTypes.Laser:
																OnUpdateLaser();
																break;
								}
				}

				void OnAttackEventHandler(AttackTypes atk)
				{
								if (currentAttack != atk) EndAllAttacks();

								switch (atk)
								{
												case AttackTypes.Cats:
																currentAttack = AttackTypes.Cats;
																OnAttackCats();
																break;

												case AttackTypes.Hairball:
																currentAttack = AttackTypes.Hairball;
																OnAttackHairball();
																break;

												case AttackTypes.Slap:
																currentAttack = AttackTypes.Slap;
																OnAttackSlap();
																break;

												case AttackTypes.Laser:
																currentAttack = AttackTypes.Laser;
																OnAttackLaser();
																break;
								}
				}

				void EndAllAttacks()
				{
								OnEndCats();
								OnEndHairball();
								OnEndSlap();
								OnEndLaser();
				}
				#endregion

				//////////////////////
				// CATS ATTACK
				//////////////////////
				void OnAttackCats()
				{
								
				}

				void OnUpdateCats()
				{
								
				}

				void OnEndCats()
				{

				}

				//////////////////////
				// HAIRBALL ATTACK
				//////////////////////
				void OnAttackHairball()
				{
								
				}

				void OnUpdateHairball()
				{
								
				}

				void OnEndHairball()
				{

				}

				//////////////////////
				// SLAP ATTACK
				//////////////////////
				void OnAttackSlap()
				{
								
				}

				void OnUpdateSlap()
				{
								
				}

				void OnEndSlap()
				{

				}

				//////////////////////
				// LASER ATTACK
				//////////////////////
				void OnAttackLaser()
				{
								
				}

				void OnUpdateLaser()
				{
								
				}

				void OnEndLaser()
				{

				}
}
