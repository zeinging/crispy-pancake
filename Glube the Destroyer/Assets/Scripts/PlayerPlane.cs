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
        //playerInputActions.Player.Movement.performed += Movement;
    }



    void Update(){

        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed * Time.deltaTime);//constant forward movement


        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();

        var step = turnSpeed * Time.deltaTime;

        Quaternion ShipRot = transform.rotation;
        Quaternion target = ShipRot;
        Quaternion ShipX = Quaternion.Euler(60 * inputVector.y, 90 * inputVector.x, transform.localEulerAngles.z);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, ShipX, step);

    }

    void Movement(InputAction.CallbackContext context){

        Debug.Log(context);
        Vector2 inputVector = context.ReadValue<Vector2>();
        Quaternion ShipRot = transform.rotation;
        //Quaternion ShipX = Quaternion.Euler(transform.rotation.eulerAngles.x + 45, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        Quaternion target = ShipRot;
        Quaternion ShipX = Quaternion.Euler(45,transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, ShipX, 1000);

    }



    // Update is called once per frame
    //void FixedUpdate()
    //{
        //myBody.velocity = transform.forward * speed * 10;
    //}
}
