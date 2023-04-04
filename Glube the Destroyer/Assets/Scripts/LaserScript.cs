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


    void OnCollisionEnter(Collision other){

        if(!other.gameObject.GetComponent<PlayerPlane>()){//don't destroy if laser hits player
            
            //myBody.velocity = Vector3.zero;
            //myBody.transform.position = other.transform.position;

            Destroy(gameObject);
            //this.gameObject.SetActive(false);

        }

    }

}
