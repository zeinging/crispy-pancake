using Assets.Team_Members_Folders.CloakingPotion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class BuildingHandleDestroyProcess : MonoBehaviour
{
    public DestoryNearestBuildingDirector buildingDestoryingDirector;
    public GlubeAnimationController animController;
    public float health = 10.0f;
    public GameObject buildingWrapper;

    private void HandleStartDestroyBuildingProcess()
    {
        // This should make the agent stop and also animate glube to be attacking the building
        buildingDestoryingDirector.HandleStopAgentAndAnimateAttacking();
        animController.StartAttackingAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleStartDestroyBuildingProcess();
        // Code to execute when the trigger is entered
    }

    public void HandleBuildingDestroyed()
    {
        if (buildingWrapper != null) { }
        buildingDestoryingDirector.HandleCompletedBulidingDestruction();
        animController.StopAttacking();

        Debug.Log("Deleting");
        Destroy(buildingWrapper.gameObject);
    }
}