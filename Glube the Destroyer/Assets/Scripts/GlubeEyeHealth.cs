using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlubeEyeHealth : MonoBehaviour
{

    public int EyeHealth = 10;

    public Color EyeHurt;

    public GameObject EyeBall;

    public bool GotHit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(GotHit){
            //LeanTween.color(EyeBall, EyeHurt, 0.5f).setLoopPingPong(2);
        }
        
    }

     void OnTriggerEnter(Collider other){
        if(other.gameObject.GetComponent<LaserScript>()){
            EyeHealth--;
            GotHit = true;
            //Destroy(other.gameObject);
            Debug.Log("OW, MY EYE!");
        }
    }

}
