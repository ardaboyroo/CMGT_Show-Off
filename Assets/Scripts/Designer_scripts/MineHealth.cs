using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineHealth : MonoBehaviour
{
    public float mineHealth = 100;
    public ParticleSystem explosionParticle;

    public void AddHealth(float amount)
    {
        mineHealth += amount;
    }

    public void DecreaseHealth(float amount)
    {
        mineHealth -= amount;

        if (mineHealth <= 0)
        {
            mineHealth = 0;
            DestroyMine();
        }
    }

    private void DestroyMine()
    {
        if (explosionParticle != null)
        {
            Instantiate(explosionParticle, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}