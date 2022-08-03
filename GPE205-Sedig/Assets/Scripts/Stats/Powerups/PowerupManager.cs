using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{  
    public List<Powerup> powerups;
    
    private List<Powerup> removedPowerupQueue;
   
    public void Start()
    {
       powerups = new List<Powerup>();
       removedPowerupQueue = new List<Powerup>();
    }

    
    public void Update()
    {
        DecrementPowerupTimers();
    }
    public void LateUpdate()
    {
        ApplyRemovePowerupsQueue();
    }

    public void Add (Powerup powerToAdd)
    {
       powerToAdd.Apply(this);
       
       powerups.Add(powerToAdd);
       Debug.Log("PowerAdded");
    }
    public void Remove(Powerup powerToRemove)
    {
        powerToRemove.Remove(this);
        removedPowerupQueue.Add(powerToRemove);
    }
    private void ApplyRemovePowerupsQueue()
    {
        foreach(Powerup powerup in removedPowerupQueue)
        {
            if(!powerup.isPermanate)
            powerups.Remove(powerup);
            Debug.Log("PowerRemoved");
        }
        removedPowerupQueue.Clear();
    }

    public void DecrementPowerupTimers()
    {
        foreach(Powerup powerup in powerups)
        {
            powerup.duration -= Time.deltaTime;

            if(powerup.duration <= 0 && !powerup.isPermanate)
            {
                Remove(powerup);
            }
        }
    }
}
