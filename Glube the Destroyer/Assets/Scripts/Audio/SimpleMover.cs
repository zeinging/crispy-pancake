using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMover : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float distance = 1;

    void Update()
    {
        transform.position = new Vector3(distance * Mathf.Sin(Time.time * speed), 0, distance * Mathf.Cos(Time.time * speed));
    }
}
