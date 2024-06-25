using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttacker : MonoBehaviour
{
    public static Action OnCatDeath;
    public static Action OnHairballDestroyed;

    [SerializeField] private Transform target;

    [Space(15)]
    [Header("Cats Attack")]
    [SerializeField] private GameObject catPrefab;
    [SerializeField] private float spawnTimeInterval = 1f;
    [SerializeField] private int spawnAmount = 3;
    [SerializeField] private float catSpeed = 3f;
    [SerializeField] private float rotateSpeed = 360f;
    [SerializeField] private List<Path> catPaths;


    [Space(15)]
    [Header("Hairball Attack")]
    [SerializeField] private GameObject hairballPrefab;
    [SerializeField] private float hairballSpeed = 10f;
    [SerializeField] private Path hairballPath;
    [SerializeField] private float timeToDestroy = 1.5f;
    [SerializeField] private float growSpeed = 1f;


    [Space(15)]
    [Header("Laser Attack")]
    //[SerializeField] private float windUpTime;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform leftEye;
    [SerializeField] private Transform rightEye;
    [SerializeField] private float timeTillDamage;
    [SerializeField] private float totalAttackTime;
    [SerializeField] private Material earthMeltedMaterial;


    ////////////////////////////////////
    // Private members
    ////////////////////////////////////
    private AttackTypes currentAttack;


    // Cats
    private int currentCatAmount;

    // Hairbal
    private Vector3 hairballTargetScale;
    private float growTimer;
    private GameObject hairballInstance;

    // Laser
    private float laserDamageTimer;
    private float totalLaserTime;
    private bool leftHitEarth = false;
    private bool rightHitEarth = false;
    private GameObject leftLaserInstance;
    private GameObject rightLaserInstance;
    private bool damagedPlayer = false;
    private GameObject earthInstance;
    private Vector3 earthInstanceScale;

    void OnEnable()
    {
        BossAttackChooser.OnAttack += OnAttackEventHandler;
        OnCatDeath += OnCatDeathEventHandler;
        OnHairballDestroyed += OnHairballDestroyedEventHandler;
    }

    void OnDisable()
    {
        BossAttackChooser.OnAttack -= OnAttackEventHandler;
        OnCatDeath -= OnCatDeathEventHandler;
        OnHairballDestroyed -= OnHairballDestroyedEventHandler;
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

    void FixedUpdate()
    {
        switch (currentAttack)
        {
            case AttackTypes.Laser:
                OnFixedUpdateLaser();
                break;
        }
    }

    void OnAttackEventHandler(AttackTypes atk)
    {
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
        EndAllAttacks();
        BossAttackChooser.DoneAttacking?.Invoke();
    }

    void DamagePlayer()
    {
        PlayerCliff.TakeDamage?.Invoke();
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
                GameObject cat = Instantiate(catPrefab, catPaths[0].points[0].position, Quaternion.identity);
                currentCatAmount += 1;

                Rigidbody rb = cat.AddComponent<Rigidbody>();
                rb.useGravity = false;

                CatController mb = cat.AddComponent<CatController>();
                mb.speed = catSpeed;
                mb.path = catPaths[0];
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
        hairballInstance = Instantiate(hairballPrefab, hairballPath.points[0].position, Quaternion.identity);

        hairballTargetScale = hairballInstance.transform.localScale;
        hairballInstance.transform.localScale = Vector3.zero;

        growTimer = 0;

        hairballInstance.transform.LookAt(hairballPath.points[1].position);

        Rigidbody rb = hairballInstance.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.drag = 0;
        rb.angularDrag = 0;
        rb.AddTorque(transform.right * 100f);

        HairballController hb = hairballInstance.AddComponent<HairballController>();
        hb.healthTimer = timeToDestroy;
        hb.path = hairballPath;
        hb.speed = hairballSpeed;
    }

    void OnUpdateHairball()
    {
        if (growTimer < growSpeed)
        {
            growTimer += Time.deltaTime;
            hairballInstance.transform.localScale = Vector3.Lerp(Vector3.zero, hairballTargetScale, growTimer / growSpeed);
        }
    }

    void OnHairballDestroyedEventHandler()
    {
        DoneAttacking();
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
        leftLaserInstance = Instantiate(laserPrefab, leftEye.position, Quaternion.identity);
        leftLaserInstance.transform.LookAt(target.position);
        leftLaserInstance.transform.Rotate(Vector3.right, 90f);
        leftLaserInstance.transform.localScale = new Vector3(1f, Vector3.Distance(target.position, leftLaserInstance.transform.position) / 2f, 1f);

        rightLaserInstance = Instantiate(laserPrefab, rightEye.position, Quaternion.identity);
        rightLaserInstance.transform.LookAt(target.position);
        rightLaserInstance.transform.Rotate(Vector3.right, 90f);
        rightLaserInstance.transform.localScale = new Vector3(1f, Vector3.Distance(target.position, rightLaserInstance.transform.position) / 2f, 1f);

        laserDamageTimer = 0f;
        totalLaserTime = 0f;
        damagedPlayer = false;
        earthInstance = null;
    }

    void OnUpdateLaser()
    {
        if (!(leftHitEarth && rightHitEarth))
        {
            laserDamageTimer += Time.deltaTime;
            if (laserDamageTimer >= timeTillDamage)
            {
                DamagePlayer();
                DoneAttacking();
                damagedPlayer = true;
                laserDamageTimer = -100f;
            }
        }

        if (totalLaserTime + Time.deltaTime < totalAttackTime)
        {
            totalLaserTime += Time.deltaTime;
        }
        else
        {
            if (!damagedPlayer)
                DoneAttacking();

            if (earthInstance != null)
                earthInstance.GetComponent<MeshRenderer>().material = earthMeltedMaterial;

            totalLaserTime = -100f;
        }
    }

    void OnFixedUpdateLaser()
    {
        // Left eye
        RaycastHit[] leftHits = Physics.RaycastAll(leftEye.position, target.position - leftEye.position);
        leftHitEarth = false;
        foreach (RaycastHit hit in leftHits)
        {
            if (hit.transform.tag == "Earth")
            {
                leftHitEarth = true;
                if (!earthInstance)
                {
                    earthInstance = hit.transform.gameObject;
                    earthInstanceScale = new Vector3(10, 10, 10); // don't hardcode
                }

                if (leftLaserInstance)
                    leftLaserInstance.transform.localScale = new Vector3(1f, hit.distance / 2f, 1f);
            }
        }

        // Right eye
        RaycastHit[] rightHits = Physics.RaycastAll(rightEye.position, target.position - rightEye.position);
        rightHitEarth = false;
        foreach (RaycastHit hit in rightHits)
        {
            if (hit.transform.tag == "Earth")
            {
                rightHitEarth = true;
                if (!earthInstance)
                {
                    earthInstance = hit.transform.gameObject;
                    earthInstanceScale = new Vector3(10, 10, 10); // don't hardcode
                }

                if (rightLaserInstance)
                    rightLaserInstance.transform.localScale = new Vector3(1f, hit.distance / 2f, 1f);
            }
        }
    }

    void OnEndLaser()
    {
        Destroy(leftLaserInstance);
        Destroy(rightLaserInstance);
    }
}
