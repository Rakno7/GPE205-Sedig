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
}
