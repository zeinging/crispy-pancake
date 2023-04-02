using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class DestoryNearestBuildingDirector : MonoBehaviour
{
    public Transform[] buildings;

    public GameObject sphere;

    private bool needsToFindNextBuilding = true;

    private bool start = false;

    private Transform? FindClosestBuilding()
    {
        float? smallestDistance = null;
        Transform? closestVector3 = null;
        foreach (Transform building in buildings)
        {
            float distance = Vector3.Distance(building.position, this.transform.position);

            if (distance < smallestDistance || closestVector3 == null)
            {
                smallestDistance = distance;
                closestVector3 = building;
            }
        }

        return closestVector3;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            start = true;
        }

        if (start != true)
        {
            return;
        }

        if (!needsToFindNextBuilding)
        {
            // If we don't need to find the next building, simply return.
            return;
        }

        Transform? closestVector = FindClosestBuilding();

        if (closestVector == null)
        {
            // Glube has destroyed all buildings!
            // Level Fail!~!
            // TODO: IMPLEMENT LEVEL FAIL!
            Debug.Log("Level Failed. All Buildings Destroyed");
        }
        else
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            Vector3 target = ((Transform)closestVector).position;
            agent.destination = target;
            needsToFindNextBuilding = false;

            sphere.transform.position = target;
        }
    }
}