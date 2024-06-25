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

    [SerializeField] private bool useTestingAttack = false;
    [SerializeField] private AttackTypes testingAttack;

    [SerializeField] private float attackInterval = 3f;

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
        // ...
        // Too bad, this is the way
        Invoke(nameof(StartNextAttack), attackInterval);
    }

    void DoneAttackingEventHandler()
    {
        Invoke(nameof(StartNextAttack), attackInterval);
    }

    void EnqueueRandomAttacks()
    {
        List<AttackTypes> atks = new List<AttackTypes> { AttackTypes.Hairball, AttackTypes.Slap, AttackTypes.Laser };
        
        // Randomize attacks
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

        if (!useTestingAttack)
            FireOnAttackEvent(bossAttacks.Dequeue());
        else
            FireOnAttackEvent(testingAttack);
    }

    void FireOnAttackEvent(AttackTypes atk)
    {
        OnAttack?.Invoke(atk);
    }


}
