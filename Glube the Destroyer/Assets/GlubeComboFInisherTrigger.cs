using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlubeComboFInisherTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        BuildingHandleDestroyProcess buildingDestroyProcessHanlder = other.GetComponentInChildren<BuildingHandleDestroyProcess>();

        if (buildingDestroyProcessHanlder == null)
        {
            return;
        }

        buildingDestroyProcessHanlder.HandleBuildingDestroyed();
    }
}