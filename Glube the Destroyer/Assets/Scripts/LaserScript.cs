using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    public float laserSpeed = 35f, laserDuration = 2f;
    private Rigidbody myBody;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if(!laserHit)
        myBody.velocity = myBody.transform.forward * laserSpeed * Time.fixedDeltaTime * 100;

        if(laserDuration > 0){
            laserDuration -= Time.fixedDeltaTime;
        }else{
            Destroy(gameObject);//destroy laser after laser duration is up
        }

    }


    void OnTriggerEnter(Collider other){

        if(!other.gameObject.GetComponent<PlayerPlane>()){//don't destroy if laser hits player
            
            //myBody.velocity = Vector3.zero;
            //myBody.transform.position = other.transform.position;
            if(!other.gameObject.GetComponent<DialogueTriggerAera>()){//don't destroy if laser hits Dialogue trigger zone
            
            if(!other.gameObject.GetComponent<BuildingHandleDestroyProcess>()){//don't destroy if laser hits building zone for glube

                if(!other.gameObject.GetComponent<DestoryNearestBuildingDirector>())//don't destroy if laser hits glube's building detection capsole collider
                Destroy(gameObject);
            }

            }
            //this.gameObject.SetActive(false);

        }

    }

}
