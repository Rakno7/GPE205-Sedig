using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointCluster : MonoBehaviour
{
    public GameObject[] WayPoints;
   
    private void Start()
    {
        if(GameManager.instance !=null)
        {
            if(GameManager.instance.Waypointcluster !=null)
            {
                GameManager.instance.Waypointcluster.Add(this);
            }
        }
    }

    
    private void OnDrawGizmos()
    {
      if(WayPoints.Length > 0)
      {
        for(int i = 0; i < WayPoints.Length; i ++)
        {
           Gizmos.color = Color.red;
           Gizmos.DrawSphere(WayPoints[i].transform.position, 1);
        }
      }     
    }
}
