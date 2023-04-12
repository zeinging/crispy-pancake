using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VcamRotateScript : MonoBehaviour
{

    public float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float temp = transform.rotation.eulerAngles.y;
        temp += 1;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,temp,0), speed * Time.deltaTime);
    }
}
