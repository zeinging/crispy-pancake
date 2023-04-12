using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlubeEyeHealth : MonoBehaviour
{

    public int DamageAmout = 10;

    public Color EyeHurt, EyeDefault;

    public GameObject EyeBall;

    public bool GotHit = false, changeColor = false;

    // Start is called before the first frame update
    void Start()
    {
        EyeDefault = EyeBall.GetComponent<SkinnedMeshRenderer>().material.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(changeColor){

        EyeBall.GetComponent<SkinnedMeshRenderer>().material.color = EyeDefault;

        if(GotHit){//only handles hit animation
            //LeanTween.color(EyeBall, EyeHurt, 0.5f).setLoopPingPong(2);
            EyeBall.GetComponent<SkinnedMeshRenderer>().material.color = EyeHurt;
            //return;
            GotHit = false;
            //return;

        }
        
        }
        
    }

     void OnTriggerEnter(Collider other){
        if(other.gameObject.GetComponent<LaserScript>()){
            //EyeHealth--;
            GameplayControllerScript.instance.GlubeTakeDamage(DamageAmout);//possible update to specify what glube got hit with
            GotHit = true;
            //Destroy(other.gameObject);
            //Debug.Log("OW, MY EYE!");
        }
    }

}
