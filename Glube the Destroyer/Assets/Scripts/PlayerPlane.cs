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

    private Vector2 inputVector;

    public GameObject Plane;

    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions.Player.Enable();



        //inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();//read control inputs

        //Vector3 tempROt = new Vector3(inputVector.y * 45f, inputVector.x * 3, inputVector.x * -30);

        //LeanTween.rotateLocal(this.gameObject, tempROt, turnSpeed);

    }



    void Update(){



        QuaternionRotationMethod();

        //LeanTweenMethod();


    }


    private void LeanTweenMethod(){

        LeanTween.move(Plane, transform.position + transform.forward, speed * Time.deltaTime);//constant forward movement

        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();//read control inputs

        float tempX = inputVector.x + transform.localEulerAngles.y;

        Vector3 tempROt = new Vector3(inputVector.y * 45f, tempX * 2f, 0);

        LeanTween.rotateX(Plane, inputVector.y * 45, 0.2f);
        LeanTween.rotateY(Plane, inputVector.x + Time.deltaTime, 0.2f);

    }


    private void QuaternionRotationMethod(){

        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed * Time.deltaTime);//constant forward movement

        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();//read control inputs

        var UDStep = turnSpeed * Time.deltaTime;
        
        float Xtemp = transform.localEulerAngles.y + inputVector.x * 0.5f;
        
        Quaternion XAngle = Quaternion.Euler(45 * inputVector.y, Xtemp, -30 * inputVector.x);//add in Right and left rotation
        
        Quaternion XLevel = Quaternion.Euler(0, Xtemp, 0);//level plane rotation


        if(inputVector.magnitude != 0.2){

            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, XAngle, UDStep);//rotate plane from inputs
            

        }else{
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, XLevel, UDStep);//level plane when inpus equal 0
        }

    }



    // Update is called once per frame
    //void FixedUpdate()
    //{
        //myBody.velocity = transform.forward * speed * 10;
    //}
}
