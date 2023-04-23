using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlane : MonoBehaviour
{
    [SerializeField]
    private float speed, turnSpeed, laserSpeed;

    public float BoostSpeed = 60, BoostDuration = 3, BoostMax, BrakeDuration = 2;

    private float speedCach, brakeCach;

    private Rigidbody myBody;//probably only for detecting collitions

    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private Vector2 inputVector, cachInputs = Vector2.zero;

    public GameObject Plane;

    public GameObject LaserShot, LaserPistle, CMVCamTarget;

    public Transform UIAimTransform;

    private Coroutine BoostCor, BrakeCor;

    //public GameObject TargetTest;

    private Camera cam;

    private bool PlayerStopped = false, CanBoost = true, isBoosting = false, CanBrake = true, isBraking = false, NoseAim = true;

    // Start is called before the first frame update
    private void Start()
    {
        myBody = GetComponent<Rigidbody>();
        playerInputActions = GameplayControllerScript.instance.playerInputActions;
        playerInput = GameplayControllerScript.instance.playerInput;
        playerInputActions.Player.Enable();//might place this inside a cutscene controller

        playerInputActions.Player.Shoot.performed += ShootLaser;
        playerInputActions.Player.Boost.performed += BoostPlane;
        playerInputActions.Player.Boost.canceled += CancelBoost;
        playerInputActions.Player.Brake.performed += Brake;
        playerInputActions.Player.Brake.canceled += CancelBrake;

        BoostMax = BoostDuration;
        BoostDuration = 0;
        speedCach = speed;
        brakeCach = BrakeDuration;


        cam = Camera.main;

        LaserPistle.transform.parent = cam.transform;

        if(GameplayControllerScript.instance.CamOffset)
        CMVCamTarget.transform.localPosition = new Vector3(0, 3, 0);//default offset position
        //LaserPistle.transform.position = transform.position;

        //inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();//read control inputs

        //Vector3 tempROt = new Vector3(inputVector.y * 45f, inputVector.x * 3, inputVector.x * -30);

        //LeanTween.rotateLocal(this.gameObject, tempROt, turnSpeed);
    }

    private void Update()
    {
        //QuaternionRotationMethod();
        LaserPistle.transform.position = transform.position + transform.forward * 3;
        //AimToUICrossHair();
        RotateLaserPistle();

        //ControlCMVCam();
        //Boosting();
        
        
    }



    // private IEnumerator EndBoost(){
    //     while(start.tempBoost > 0){
    //         tempBoost = Mathf.MoveTowards(tempBoost, 0, Time.deltaTime);
    //         yield return null;
    //     }
    // }

    //private void Boosting(){

        //if(CanBoost){
            //isBoosting = true;
            //CanBoost = false;//make false so this only runs once
            //BoostCor = StartCoroutine(StartBoost());
        //}


        // float temp = speed;
        // float tempBoost = BoostTime;

        // if(isBoosting){

        //     if(BoostTime > 0){
        //         BoostTime -= Time.deltaTime;
        //         speed = BoostSpeed;
        //     }else{
        //         speed = temp;
        //         CanBoost = false;
        //         isBoosting = false;
        //     }

        // }else{
        //     speed = temp;
        //     if(BoostTime < tempBoost){
        //     BoostTime = Mathf.MoveTowards(BoostTime, tempBoost, Time.deltaTime);
        //     }
        // }

    //}

    private void FixedUpdate(){
        if(!PlayerStopped){
        MyBodyQuaternionRotationMethod();

        if(GameplayControllerScript.instance.CamOffset)
            ControlCMVCam();//place inside an if statement to check if Cam Offset is true
        }else{

        myBody.velocity = Vector3.zero;
        
        if(transform.localPosition.magnitude > 0.5f){
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
        }else
        {
            GetComponent<PlayerPlane>().enabled = false;
        }
        }

        
    }

    void OnDestroy(){
        GameplayControllerScript.instance.playerInputActions.Player.Disable();//disable player controls if destroyed
        StopAllCoroutines();
        AudioManager.instance.CancelBoost();
    }

    private void ShootLaser(InputAction.CallbackContext context){
        if(context.performed){
            Instantiate(LaserShot, LaserPistle.transform.position, LaserPistle.transform.rotation);
        }
    }

    private void BoostPlane(InputAction.CallbackContext context){
        if(context.performed){
            if(CanBoost){
                isBraking = false;
                isBoosting = true;
                CanBoost = false;//make false so this only runs once
                BoostCor = StartCoroutine(StartBoost());
            }
        }
    }

        private IEnumerator StartBoost(){
        //float tempSpeed = speed;
        //float tempBoost = BoostDuration;
        BoostDuration = 0;//set to zero at beginning.
        AudioManager.instance.PlayerBoost();
        //speed = BoostSpeed;
        while(BoostDuration < BoostMax && isBoosting){
            speed = Mathf.MoveTowards(speed, BoostSpeed, 50 * Time.deltaTime);//place here so speed is gradualy built up.
            BoostDuration = Mathf.MoveTowards(BoostDuration, BoostMax, Time.deltaTime);//boost duration
            yield return null;
        }

        if(BoostDuration == BoostMax)//only wait if player used up all of the boost meter
        yield return new WaitForSeconds(0.5f);//have a little delay before recharge

        AudioManager.instance.CancelBoost();

        if(isBoosting){//make is boosting false if boost duration reaches 0
            isBoosting = false;    
        }
        while(BoostDuration > 0){
            speed = Mathf.MoveTowards(speed, speedCach, 40 * Time.deltaTime);//place here so speed is gradualy built back down to default speed.
            BoostDuration = Mathf.MoveTowards(BoostDuration, 0, Time.deltaTime);//boost duration reset
            yield return null;
        }

        CanBoost = true;//reset boost bool so player can boost again.


    }


        private void CancelBoost(InputAction.CallbackContext context){
            if(context.canceled){
                isBoosting = false;
            }

        }

        private void Brake(InputAction.CallbackContext context){
            if(context.performed){
                if(CanBrake){
                    isBoosting = false;
                    isBraking = true;
                    CanBrake = false;
                    BrakeCor = StartCoroutine(Braking());
                }
            }
        }
        private IEnumerator Braking(){

            while(BrakeDuration > 0 && isBraking){
                speed = Mathf.MoveTowards(speed, 0, 50 * Time.deltaTime);
                BrakeDuration = Mathf.MoveTowards(BrakeDuration, 0, Time.deltaTime);
                yield return null;
            }
            //speed = 0;
            //if(isBraking)
            //yield return new WaitForSeconds(t);

            isBraking = false;
            while(BrakeDuration < brakeCach){
                speed = Mathf.MoveTowards(speed, speedCach, 50 * Time.deltaTime);
                BrakeDuration = Mathf.MoveTowards(BrakeDuration, brakeCach, Time.deltaTime);
                yield return null;
            }
            CanBrake = true;

        }

        private void CancelBrake(InputAction.CallbackContext context){
            if(context.canceled){
                //StopCoroutine(BrakeCor);
                //speed = speedCach;
                isBraking = false;
            }
        }

    private void AimToUICrossHair(){//rotate pistle to UIAim, probably replace with UIAim follows Laser Pistle Rotation

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

    private void RotateLaserPistle(){

        //LaserPistle.transform.rotation = cam.transform.rotation;

        if(NoseAim){
            Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();//read control inputs

            var UDStep = 5 * Time.deltaTime;

            //Quaternion temp = Quaternion.Euler(45 * inputVector.x, 45 * inputVector.y, 0);
            Quaternion temp = LaserPistle.transform.localRotation;
            float tempX = 65f * inputVector.x;
            float tempY = 40f * inputVector.y;
            //tempX = Mathf.Clamp(tempX, -45, 45);
            //tempY = Mathf.Clamp(tempY, -45, 45);
            temp = Quaternion.Euler(tempY, tempX, 0);
            if(inputVector.magnitude != 0){

            //LaserPistle.transform.localRotation = temp;
            LaserPistle.transform.localRotation = Quaternion.Lerp(LaserPistle.transform.localRotation, temp, UDStep);
            }
            else{

            LaserPistle.transform.localRotation = Quaternion.Lerp(LaserPistle.transform.localRotation, Quaternion.Euler(0, 0, 0), UDStep);
            }

        }else{

            Vector2 inputVector = playerInputActions.Player.Aim.ReadValue<Vector2>();

            var UDStep = turnSpeed * Time.deltaTime;

            //Quaternion temp = Quaternion.Euler(45 * inputVector.x, 45 * inputVector.y, 0);
            float tempX = inputVector.x + LaserPistle.transform.localEulerAngles.y;
            float tempY = -inputVector.y + LaserPistle.transform.localEulerAngles.x;
            //tempX = Mathf.Clamp(tempX, -25f, 25f);
            //tempY = Mathf.Clamp(tempY, -45f, 45f);
            
            //Quaternion temp = Quaternion.Euler(25f * -inputVector.y, 45f * inputVector.x, 0);
            Quaternion temp = Quaternion.Euler(tempY, tempX, 0);
            
           
            

            //if(inputVector.magnitude > 0){
            //LaserPistle.transform.localRotation = temp;
            //Debug.Log(Quaternion.Angle(LaserPistle.transform.localRotation, temp));
            //if(Quaternion.Angle(LaserPistle.transform.localRotation, Quaternion.Euler(25f, 45f, 0)) < 10f){

            LaserPistle.transform.localRotation = Quaternion.Lerp(LaserPistle.transform.localRotation, temp, UDStep);
            //}
            //}

        }

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

        if (inputVector.magnitude != 0.2f)
        {
            //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, XAngle, UDStep);//rotate plane from inputs
            myBody.transform.localRotation = Quaternion.Lerp(myBody.transform.localRotation, XAngle, UDStep);//rotate plane from inputs
        }else{
            //myBody.transform.localRotation = Quaternion.Lerp(myBody.transform.localRotation, XLevel, UDStep);
        }

    }

    private void ControlCMVCam(){

        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        

        if(inputVector.magnitude != 0){
            cachInputs = inputVector.normalized;
        }else if(cachInputs == Vector2.zero){
            cachInputs = new Vector2(0, -1);//should point up by default
        }

            CMVCamTarget.transform.localPosition = Vector3.Lerp(CMVCamTarget.transform.localPosition, new Vector3(cachInputs.x, -cachInputs.y, 0) * 3f, 2 * Time.deltaTime);
        if(inputVector.magnitude == 0){
            //CMVCamTarget.transform.localPosition = Vector3.MoveTowards(CMVCamTarget.transform.localPosition, new Vector3(inputVector.x, -inputVector.y, 0) * 3f, 5 * Time.deltaTime);
        }else{
            //CMVCamTarget.transform.localPosition = Vector3.Lerp(CMVCamTarget.transform.localPosition, new Vector3(0, 0, 0) * 3f, 2 * Time.deltaTime);
        }

    }

    public void DisableControls(){
        playerInputActions.Player.Disable();
        playerInputActions.UI.Disable();
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
            //GetComponent<PlayerPlane>().enabled = false;
            PlayerStopped = true;
            //StartCoroutine(SinkIntoGlube());
            GetComponent<BoxCollider>().enabled = false;
            GameplayControllerScript.instance.PlayerCrashedIntoGlube();
        }

            if(!other.gameObject.GetComponent<BuildingHandleDestroyProcess>()){//don't take damage if fly into building destroy trigger

            if(!PlayerStopped)//prevent damage if hit glube
            GameplayControllerScript.instance.PlayerTakeDamage(1);//need update to specific what player got hit by
            
            if(GameplayControllerScript.instance.PlayerHealth <= 0){//player health is zero.
                GameplayControllerScript.instance.PlayerDeath();
                Destroy(gameObject);// tempoary, replace with explosion instructions later.
            }

            Debug.Log(other.name);
            }

        }

        }
            
    }



}