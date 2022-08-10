using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public bool isHorizontalOnly = false;
    void Update()
    {
        if(!isHorizontalOnly)
        {
        transform.Rotate(new Vector3 (15,30,45) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3 (0,10,0) * Time.deltaTime);
        }
    }
}
