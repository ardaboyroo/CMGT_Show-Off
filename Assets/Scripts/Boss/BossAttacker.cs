using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacker : MonoBehaviour
{
    [SerializeField] private Transform target;

    public static Action OnCatDeath;

    [Space(15)]
    [Header("Cats Attack")]
    [SerializeField] private GameObject catPrefab;
    [SerializeField] private float spawnTimeInterval = 1f;
    [SerializeField] private int spawnAmount = 3;
    [SerializeField] private float catSpeed = 3f;
    [SerializeField] private float rotateSpeed = 360f;
    [SerializeField] private List<Path> paths;


    ////////////////////////////////////
    // Private members
    ////////////////////////////////////
    private AttackTypes currentAttack;


    // Cats
    private int currentCatAmount;



    void OnEnable()
    {
        BossAttackChooser.OnAttack += OnAttackEventHandler;
        OnCatDeath += OnCatDeathEventHandler;
    }

    void OnDisable()
    {
        BossAttackChooser.OnAttack -= OnAttackEventHandler;
        OnCatDeath -= OnCatDeathEventHandler;
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

    void DoneAttacking()
    {
        BossAttackChooser.DoneAttacking?.Invoke();
    }

    //////////////////////
    // CATS ATTACK
    //////////////////////
    void OnAttackCats()
    {
        StartCoroutine(CatSpawner());
    }

    IEnumerator CatSpawner()
    {
        // while loop will keep spawning enemies (for testing)
        //while (true)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                GameObject cat = Instantiate(catPrefab, paths[0].points[0].position, Quaternion.identity);
                currentCatAmount += 1;

                Rigidbody rb = cat.AddComponent<Rigidbody>();
                rb.useGravity = false;

                CatController mb = cat.AddComponent<CatController>();
                mb.speed = catSpeed;
                mb.path = paths[0];
                mb.rotateSpeed = rotateSpeed;
                yield return new WaitForSeconds(spawnTimeInterval);
            }

            //yield return new WaitForSeconds(5f);
        }
    }

    void OnCatDeathEventHandler()
    {
        if (currentCatAmount - 1 > 0)
        {
            currentCatAmount -= 1;
        }
        else
        {
            currentCatAmount = 0;
            DoneAttacking();
        }
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
        DoneAttacking();
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
        DoneAttacking();
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
        DoneAttacking();
    }

    void OnUpdateLaser()
    {

    }

    void OnEndLaser()
    {

    }
}
