using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class BuildingHandleDestroyProcess : MonoBehaviour
{
    public DestoryNearestBuildingDirector buildingDestoryingDirector;
    public float health = 10.0f;
    public GameObject buildingWrapper;

    private bool IsBeingDestroyed = false;
    private bool destroyed = false;

    private void HandleStartDestroyBuildingProcess()
    {
        // This should make the agent stop and also animate glube to be attacking the building
        buildingDestoryingDirector.HandleStopAgentAndAnimateAttacking();
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleStartDestroyBuildingProcess();
        IsBeingDestroyed = true;
        // Code to execute when the trigger is entered
    }

    private void HandleBuildingDestroyed()
    {
        destroyed = true;
        buildingDestoryingDirector.HandleCompletedBulidingDestruction();

        Debug.Log("Deleting");
        Destroy(buildingWrapper.gameObject);
    }

    private void HandleDestructionCountdownTimer()
    {
        Debug.Log("Health: " + health);
        if (health > 0)
        {
            health -= Time.deltaTime;
            return;
        }
        else
        {
            HandleBuildingDestroyed();
        }
    }

    private void Update()
    {
        if (IsBeingDestroyed && !destroyed)
        // Run this if it's in the process of being destroyed and it's not completely destroyed yet
        {
            HandleDestructionCountdownTimer();
        }
    }
}