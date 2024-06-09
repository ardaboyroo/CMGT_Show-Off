using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BossAttackChooser : MonoBehaviour
{
				public static Action<AttackTypes> OnAttack;

				public static Action DoneAttacking;

				private Queue<AttackTypes> bossAttacks = new Queue<AttackTypes>();
				private System.Random rnd = new();

				void OnEnable()
				{
								DoneAttacking += DoneAttackingEventHandler;
				}

				void OnDisable()
				{
								DoneAttacking -= DoneAttackingEventHandler;
				}

				// Start is called before the first frame update
				void Start()
				{
								// Don't do it this way
								Invoke(nameof(StartNextAttack), 3);
				}

				void DoneAttackingEventHandler()
				{
								//  0_0
								Invoke(nameof(StartNextAttack), 3);
				}

				void EnqueueRandomAttacks()
				{
								List<AttackTypes> atks = new List<AttackTypes> { AttackTypes.Hairball, AttackTypes.Slap, AttackTypes.Laser };
								atks = atks.OrderBy(_ => rnd.Next()).ToList();

								foreach (AttackTypes atk in atks)
								{
												bossAttacks.Enqueue(AttackTypes.Cats);
												bossAttacks.Enqueue(atk);
								}
				}

				void StartNextAttack()
				{
								// Refill queue
								if (bossAttacks.Count == 0)
												EnqueueRandomAttacks();

								FireOnAttackEvent(bossAttacks.Dequeue());
				}

				void FireOnAttackEvent(AttackTypes atk)
				{
								OnAttack?.Invoke(atk);
				}


}
