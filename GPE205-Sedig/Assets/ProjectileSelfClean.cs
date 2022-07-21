using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSelfClean : MonoBehaviour
{
    private float delay = 1;
    private bool isPrepDestroy = false;
    
    
    private void Update()
    {
        if(isPrepDestroy)
        {
            delay -= Time.deltaTime;
            if(delay < 0)
            {
               Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        isPrepDestroy = true;
    }
}
