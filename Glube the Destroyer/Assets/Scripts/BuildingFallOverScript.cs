using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFallOverScript : MonoBehaviour
{

    public GameObject FallTarget, BuildingMesh, BuildingEmpty;

    public float FallSpeed = 20f;

    private Quaternion TargetRotation;

    private bool hitGround = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(BuildingMesh.transform.parent == FallTarget.transform){
            RotateToGround();
        }
    }

    private void RotateToGround(){
        //transform.parent = FallTarget.transform;
        //Quaternion temp = FallTarget.transform.rotation;
        //Quaternion temp = transform.localRotation;
        //temp = Quaternion.Euler(90f, temp.eulerAngles.y, temp.eulerAngles.z);
        FallTarget.transform.localRotation = Quaternion.RotateTowards(FallTarget.transform.localRotation, TargetRotation, FallSpeed * Time.deltaTime);
        if(FallTarget.transform.localRotation == TargetRotation && !hitGround){
        AudioManager.instance.BuildingExplodeStart();
        hitGround = true;
        //Destroy(gameObject);
        }

    }

    void OnTriggerEnter(Collider other){
        if(other.GetComponent<GlubeComboFInisherTrigger>()){
            FallTarget.transform.rotation = other.GetComponentInParent<DestoryNearestBuildingDirector>().gameObject.transform.rotation;
            BuildingMesh.transform.parent = FallTarget.transform;
            TargetRotation = FallTarget.transform.localRotation;
            TargetRotation = Quaternion.Euler(90f, TargetRotation.eulerAngles.y, TargetRotation.eulerAngles.z);//put here so it only changes once
            FallTarget.transform.parent = null;
            Destroy(BuildingEmpty);
        }
    }


}
