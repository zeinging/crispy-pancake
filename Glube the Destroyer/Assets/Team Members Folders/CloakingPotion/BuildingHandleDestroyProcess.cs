using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandleDestroyProcess : MonoBehaviour
{
    public GameObject taco;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");
        // Code to execute when the trigger is entered
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Test");
        //Hello
    }
}