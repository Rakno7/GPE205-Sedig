using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    private GameObject spawnedPickup;
    public float spawnDelay;
    private float nextSpawnTime;
    private Transform Spawntransform;
    void Start()
    {
        //Get the current time on start plus our spawn delay
        nextSpawnTime = Time.time + spawnDelay;
    }

    
    void Update()
    { 
        //make sure there isnt already a pickup on the spawner
      if(spawnedPickup == null)
      {

      
        //check if the current time is greater then the next spawn time
        if(Time.time > nextSpawnTime)
        {
            spawnedPickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity);
            nextSpawnTime = Time.time + spawnDelay;
        }
      }
      //if there is, reset the timer, so multiple do not keep spawning on top of eachother.
      else
      {
         nextSpawnTime = Time.time + spawnDelay;
      }
    }
}
