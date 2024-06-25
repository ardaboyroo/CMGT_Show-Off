using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class HairballController : MonoBehaviour
{
    [Tooltip("The amount of seconds the player has to ignite it in order to destroy it")]
    public float healthTimer;
    private float totalHealthTimer;

    public Path path;
    public Vector3 target;
    public float speed;

    private Rigidbody rb;

    private int currentPathPoint = 0;

    private SpellTypes currentSpell;

    private Material material;

    private void OnEnable()
    {
        CastDetector.OnCast += SetCurrentSpell;
    }

    private void OnDisable()
    {
        CastDetector.OnCast -= SetCurrentSpell;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GetTarget(currentPathPoint);
        material = GetComponent<MeshRenderer>().material;
        totalHealthTimer = healthTimer;
    }

    void Update()
    {
        // TODO: Don't use a hardcoded value
        if (Vector3.Distance(transform.position, target) < 2f)
        {
            if (currentPathPoint + 1 < path.points.Count)
            {
                target = GetTarget(++currentPathPoint);
            }
            else
            {
                EndReached();
            }
        }
    }

    void SetCurrentSpell(SpellTypes type)
    {
        currentSpell = type;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dot = Vector3.Dot((target - transform.position).normalized, rb.velocity.normalized);
        rb.AddForce((target - transform.position).normalized * speed * Mathf.Lerp(1f, speed, 1 - (1 * dot)), ForceMode.Force);

        if (rb.velocity.magnitude > speed)
            rb.velocity = rb.velocity.normalized * speed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (currentSpell == SpellTypes.Fire)
        {
            healthTimer -= Time.deltaTime;
            material.SetFloat("_Dissolve_Amount", 1 - (healthTimer / totalHealthTimer));

            if (healthTimer < 0)
            {
                DestroySelf();
            }
        }
    }

    Vector3 GetTarget(int index)
    {
        return path.GetPositionFromIndex(index);
    }

    void EndReached()
    {
        DestroySelf();
        DamagePlayer();
    }

    void DestroySelf()
    {
        Destroy(gameObject);
        BossAttacker.OnHairballDestroyed?.Invoke();
    }

    void DamagePlayer()
    {
        PlayerCliff.TakeDamage?.Invoke();
    }
}
