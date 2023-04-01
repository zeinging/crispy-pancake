using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlane : MonoBehaviour
{

    [SerializeField]
    private float speed, turnSpeed, laserSpeed;

    private Rigidbody myBody;//probably only for detecting collitions


    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    

    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions.Player.Enable();
    }



    void Update(){

        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed * Time.deltaTime);//constant forward movement

        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();//read control inputs

        var UDStep = turnSpeed * Time.deltaTime;
        

        float XMov = Mathf.MoveTowards(transform.localEulerAngles.y, transform.localEulerAngles.y + turnSpeed, inputVector.x);
        float Xtemp = transform.localEulerAngles.y + inputVector.x * 0.5f;
        
        //Debug.Log(inputVector.x);

        //Quaternion XAngle = Quaternion.AngleAxis(inputVector.y * 45, Vector3.right);//clamp plane up and down rotation
        //Quaternion ZAngle = Quaternion.AngleAxis(inputVector.x * 10, Vector3.up);
        Quaternion XAngle = Quaternion.Euler(45 * inputVector.y, Xtemp, -30 * inputVector.x);//add in Right and left rotation
        
        Quaternion XLevel = Quaternion.Euler(0, Xtemp, 0);//level plane rotation


        if(inputVector.magnitude != 0.2){

            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, XAngle, UDStep);//rotate plane from inputs
            

        }else{
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, XLevel, UDStep);//level plane when inpus equal 0
        }

        // if(inputVector.y != 0){
        //     //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, temptest, step);
        //     transform.localRotation = Quaternion.RotateTowards(transform.localRotation, XAngle, step);
        // }else{
        //     //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, XLevel, step);
        // }

    }



    // Update is called once per frame
    //void FixedUpdate()
    //{
        //myBody.velocity = transform.forward * speed * 10;
    //}
}
