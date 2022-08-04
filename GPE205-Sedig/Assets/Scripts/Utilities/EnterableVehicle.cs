using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterableVehicle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<HumanPawn>())
        {
            other.GetComponent<HumanPawn>().VehicleToEnter = this.gameObject;
            other.GetComponent<HumanPawn>().canEnterVehicle = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
         if(other.GetComponent<HumanPawn>())
        {
            other.GetComponent<HumanPawn>().canEnterVehicle = false;
            other.GetComponent<HumanPawn>().VehicleToEnter = null;
        }
    }
}
