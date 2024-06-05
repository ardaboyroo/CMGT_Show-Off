using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : MonoBehaviour
{
    private float lifeTime = 1f;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (timer < lifeTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLifeTime(float time)
    {
        lifeTime = time;
    }
}
