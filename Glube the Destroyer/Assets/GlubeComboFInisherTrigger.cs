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
        //Debug.Log("Move to next Building");
        //StartCoroutine(DelayBeforeFindNextBuilding(other));

    }

    public IEnumerator DelayBeforeFindNextBuilding(Collider other){

        BuildingHandleDestroyProcess buildingDestroyProcessHanlder = other.GetComponentInChildren<BuildingHandleDestroyProcess>();

        yield return new WaitForSeconds(1f);
        Debug.Log("Move to next Building");

        buildingDestroyProcessHanlder.HandleBuildingDestroyed();
    }

}