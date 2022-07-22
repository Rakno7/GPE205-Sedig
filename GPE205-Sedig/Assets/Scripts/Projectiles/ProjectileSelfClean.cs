using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSelfClean : MonoBehaviour
{
    public float delay = 0.2f;
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
