using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockets_on_off : MonoBehaviour
{
    [SerializeField]
private GameObject particles; 

    private void Start()
    {
        particles.SetActive(false); 
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){ 
particles.SetActive(true); 
        }
        
    }
}
