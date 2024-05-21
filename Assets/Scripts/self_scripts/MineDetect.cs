using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDetect : MonoBehaviour
{
public Transform target; // Reference to the detected object

    public float movementSpeed = 5f; // Speed at which the object moves towards the target
    
    private void Update()
    {
        // Perform the SphereCast to detect objects
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit))
        {
            if (hit.transform == target)
            {
                // Calculate the direction towards the target
                Vector3 targetDirection = target.position - transform.position;
                targetDirection.Normalize();

                // Move towards the target
                transform.Translate(targetDirection * movementSpeed * Time.deltaTime);
            }
            
        }
    }
}
