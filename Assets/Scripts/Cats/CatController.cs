using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public Path path;
    public Vector3 target;
    public float speed;

    public float rotateSpeed;

    private Rigidbody rb;
    private float yRotation = 0;

    private int currentPathPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GetTarget(currentPathPoint);
    }

    void Update()
    {
        // Rotate the cat around its local Y axis
        transform.LookAt(transform.position + rb.velocity.normalized);
        transform.RotateAround(transform.position, transform.right, 90);
        transform.RotateAround(transform.position, transform.up, yRotation);

        yRotation += Time.deltaTime * rotateSpeed;

        // Don't use a hardcoded value :skull:
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

    // Update is called once per frame
    void FixedUpdate()
    {
        float dot = Vector3.Dot((target - transform.position).normalized, rb.velocity.normalized);
        rb.AddForce((target - transform.position).normalized * speed * Mathf.Lerp(1f, speed, 1 - (1 * dot)), ForceMode.Force);

        if (rb.velocity.magnitude > speed)
            rb.velocity = rb.velocity.normalized * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MagicMissile>() != null)
        {
            Destroy(other.gameObject);
            DestroySelf();
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
        // Do some other fancy stuff here
    }

    void DestroySelf()
    {
        Destroy(gameObject);
        BossAttacker.OnCatDeath?.Invoke();
    }

    void DamagePlayer()
    {
        PlayerCliff.TakeDamage?.Invoke();
    }
}
