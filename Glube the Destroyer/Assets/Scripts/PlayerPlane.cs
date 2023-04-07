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

    public GameObject LaserShot, LaserPistle;

    public Transform UIAimTransform;

    //public GameObject TargetTest;

    private Camera cam;

    // Start is called before the first frame update
    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Shoot.performed += ShootLaser;

        cam = Camera.main;

        //inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();//read control inputs

        //Vector3 tempROt = new Vector3(inputVector.y * 45f, inputVector.x * 3, inputVector.x * -30);

        //LeanTween.rotateLocal(this.gameObject, tempROt, turnSpeed);
    }

    private void Update()
    {
        //QuaternionRotationMethod();
        
        AimToUICrossHair();
    }

    private void FixedUpdate(){
        MyBodyQuaternionRotationMethod();

        
    }

    private void ShootLaser(InputAction.CallbackContext context){
        if(context.performed){
            Instantiate(LaserShot, LaserPistle.transform.position, LaserPistle.transform.rotation);
        }
    }

    private void AimToUICrossHair(){

        //Vector3 temp = transform.forward + UIAimTransform.forward;
        // got to use camera to convert to world space
        //Vector3 pos = cam.ScreenToWorldPoint(UIAimTransform.position);
        //RaycastHit pos = cam.ScreenPointToRay(UIAimTransform.position, cam.stereoActiveEye eye);
        //Vector3 pos = UIAimTransform.position;
        //pos.z *= -1;
        //Debug.Log(pos);

        //pos += cam.transform.forward;
        //pos.x *= -1;
        //pos.y *= -1;
        

        //Vector3 CamPos = cam.transform.position;
        //CamPos.x = pos.x;
        //CamPos.y = pos.y;
        //pos = pos + cam.transform.position;
        //pos.z = transform.position.z;
        //Vector3 UiForwar = UIAimTransform.forward;
        //UiForwar.z = transform.position.z;
        //Debug.dr(cam.transform.position, CamPos + cam.transform.forward * 50f);
        //Debug.DrawLine(UIAimTransform.localPosition, cam.transform.forward * 5f);
        //TargetTest.transform.position = pos;

        Vector3 point = new Vector3();
        //Event   currentEvent = Event.current;
        Vector2 UIPos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        UIPos.x = UIAimTransform.transform.position.x;
        UIPos.y = UIAimTransform.transform.position.y;

        

        point = cam.ScreenToWorldPoint(new Vector3(UIPos.x, UIPos.y, cam.farClipPlane));

        //TargetTest.transform.position = point;

        Vector3 pistleDirection = point - LaserPistle.transform.position;

        Vector3 targetDirection = Vector3.RotateTowards(LaserPistle.transform.forward, pistleDirection, 720, 0);

        LaserPistle.transform.rotation = Quaternion.LookRotation(targetDirection);


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


    void OnTriggerEnter(Collider other){

        if(!other.gameObject.GetComponent<LaserScript>()){//don't take damage if collided with own laser

        //playerInputActions.Player.Disable();
        //Destroy(gameObject);
        if(!other.gameObject.GetComponent<DialogueTriggerAera>()){//don't take damage if fly in dialogue trigger area.

        if(other.gameObject.tag == "Glube"){//testing Glube triggers
            myBody.velocity = Vector3.zero;
            playerInputActions.Player.Disable();
            this.transform.parent = other.transform;
            GetComponent<PlayerPlane>().enabled = false;
        }

        Debug.Log(other.name);
        }

        }
            
    }



    // void OnGUI(){

    //     Vector3 point = new Vector3();
    //     Event   currentEvent = Event.current;
    //     Vector2 mousePos = new Vector2();

    //     // Get the mouse position from Event.
    //     // Note that the y position from Event is inverted.
    //     mousePos.x = currentEvent.mousePosition.x;
    //     mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

    //     point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

    //     GUILayout.BeginArea(new Rect(20, 20, 250, 120));
    //     GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
    //     GUILayout.Label("Mouse position: " + mousePos);
    //     GUILayout.Label("World position: " + point.ToString("F3"));
    //     GUILayout.EndArea();
        

    // }



    // Update is called once per frame
    //void FixedUpdate()
    //{
    //myBody.velocity = transform.forward * speed * 10;
    //}
}