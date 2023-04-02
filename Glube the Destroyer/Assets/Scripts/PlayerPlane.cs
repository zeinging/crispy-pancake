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
    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions.Player.Enable();

        //inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();//read control inputs

        //Vector3 tempROt = new Vector3(inputVector.y * 45f, inputVector.x * 3, inputVector.x * -30);

        //LeanTween.rotateLocal(this.gameObject, tempROt, turnSpeed);
    }

    private void Update()
    {
        //QuaternionRotationMethod();

        
    }

    private void FixedUpdate(){
        MyBodyQuaternionRotationMethod();
    }

    private void LeanTweenMethod()
    {
        LeanTween.move(Plane, transform.position + transform.forward, speed * Time.deltaTime);//constant forward movement

        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();//read control inputs

        float tempX = inputVector.x + transform.localEulerAngles.y;

        Vector3 tempROt = new Vector3(inputVector.y * 45f, tempX * 2f, 0);

        LeanTween.rotateX(Plane, inputVector.y * 45, 0.2f);
        LeanTween.rotateY(Plane, inputVector.x + Time.deltaTime, 0.2f);
    }

    private void QuaternionRotationMethod()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed * Time.deltaTime);//constant forward movement

        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();//read control inputs

        var UDStep = turnSpeed * Time.deltaTime;

        float Xtemp = transform.localEulerAngles.y + inputVector.x * 10;

        Quaternion XAngle = Quaternion.Euler(45 * inputVector.y, Xtemp, -45 * inputVector.x);//add in Right and left rotation

        Quaternion XLevel = Quaternion.Euler(0, Xtemp, 0);//level plane rotation

        if (inputVector.magnitude != 0.2)
        {
            //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, XAngle, UDStep);//rotate plane from inputs
            transform.localRotation = Quaternion.Lerp(transform.localRotation, XAngle, UDStep);//rotate plane from inputs
        }
        //else
        //{
            //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, XLevel, UDStep);//level plane when inpus equal 0
        //}
    }

    private void MyBodyQuaternionRotationMethod(){


        //myBody.transform.position = Vector3.MoveTowards(myBody.transform.position, myBody.transform.position + myBody.transform.forward, speed * Time.deltaTime);//constant forward movement
        myBody.velocity = myBody.transform.forward * speed * Time.fixedDeltaTime * 100;

        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();//read control inputs

        var UDStep = turnSpeed * Time.fixedDeltaTime;

        float Xtemp = myBody.transform.localEulerAngles.y + inputVector.x * 10;

        Quaternion XAngle = Quaternion.Euler(45 * inputVector.y, Xtemp, -45 * inputVector.x);//add in Right and left rotation

        Quaternion XLevel = Quaternion.Euler(0, Xtemp, 0);//level plane rotation

        if (inputVector.magnitude != 0.2)
        {
            //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, XAngle, UDStep);//rotate plane from inputs
            myBody.transform.localRotation = Quaternion.Lerp(myBody.transform.localRotation, XAngle, UDStep);//rotate plane from inputs
        }

    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //myBody.velocity = transform.forward * speed * 10;
    //}
}