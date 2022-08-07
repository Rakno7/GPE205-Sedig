using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    private GameObject spawnedPrefab;
    public float spawnDelay;
    private float nextSpawnTime;
    private Transform Spawntransform;
    //gamemanager will randomly enable the number of max player spawners, and disable the rest, each time a player dies.
    //(in GameManager, called from the Health die function) 
    void Awake()
    {
      if(prefabToSpawn !=null)
      {
        if(prefabToSpawn.GetComponent<AiController>())
        {
            if(GameManager.instance != null)
            {
              {
                GameManager.instance.AiSpawners.Add(this);
              }
            }
        }
        if(prefabToSpawn !=null)
        {
          if(prefabToSpawn.GetComponent<PlayerController>())
          {
            if(GameManager.instance != null)
            {
                 GameManager.instance.PlayerSpawners.Add(this);
            }
            }
          }
        }
      
          //Get the current time on start plus our spawn delay
          nextSpawnTime = Time.time + spawnDelay;
    }

    
    void Update()
    { 
        //make sure Prefab no longer exists before spawning
      if(spawnedPrefab == null)
      {
        if(prefabToSpawn.GetComponent<PlayerController>())
           {
           //check if the current time is greater then the next spawn time
           if(Time.time > nextSpawnTime && GameManager.instance.players.Count < GameManager.instance.MaxPlayers)
           {
               spawnedPrefab = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
               nextSpawnTime = Time.time + spawnDelay;
           }
        }
        if(prefabToSpawn.GetComponent<AiController>())
           {
           //check if the current time is greater then the next spawn time
           if(Time.time > nextSpawnTime && GameManager.instance.aiPlayers.Count < GameManager.instance.MaxAIPlayers)
           {
               spawnedPrefab = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
               nextSpawnTime = Time.time + spawnDelay;
           }
        }
      }
      //if there is, reset the timer, so multiple do not keep spawning on top of eachother.
      else
      {
         nextSpawnTime = Time.time + spawnDelay;
      }
    }
}
