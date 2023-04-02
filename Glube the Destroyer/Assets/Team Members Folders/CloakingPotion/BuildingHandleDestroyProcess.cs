using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandleDestroyProcess : MonoBehaviour
{
    public DestoryNearestBuildingDirector buildingDestoryingDirector;

    private void HandleStartDestroyBuildingProcess()
    {
        // This should make the agent stop and also animate glube to be attacking the building
        buildingDestoryingDirector.handleStopAgentAndAnimateAttacking();
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleStartDestroyBuildingProcess();
        // Code to execute when the trigger is entered
    }
}