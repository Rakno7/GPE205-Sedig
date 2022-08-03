using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public HealthPowerup powerup;
    public void OnTriggerEnter(Collider other)
    {
        PowerupManager powerupManager = other.GetComponentInParent<PowerupManager>();
        if(powerupManager != null)
        {
            powerupManager.Add(powerup);
            Destroy(gameObject);
        }
    }
}
