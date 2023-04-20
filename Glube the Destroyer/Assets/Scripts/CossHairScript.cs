using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CossHairScript : MonoBehaviour
{


    public Image SmallAimCrossHair, BigAimCrossHair;

    public Transform Plane;

    //public GameObject CrossHair;

    public float AimXDis = 5f, AimYDis = 5f, DisFromCamera = 70f, DisFromPlane = 100f;

    public float AimSpeed = 20;

    
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private Camera cam;

    public bool NoseAiming = true;

    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = GameplayControllerScript.instance.playerInputActions;
        playerInput = GameplayControllerScript.instance.playerInput;
        //playerInputActions.Player.Enable();
        //CrossHair.transform.parent = Camera.main.transform;
        cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Plane != null)
        //NoseAim();

        independentAim();

    }

    private void NoseAim(){

        //Vector3 pos = cam.WorldToScreenPoint(Plane.position + Plane.forward * DisFromPlane);
        //Vector3 InnerPos = pos - Plane.forward * 25f;


        //if(transform.position != pos){

            //transform.position = pos;
        //}

        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();

        float Xpos = SmallAimCrossHair.rectTransform.localPosition.x + inputVector.x * Screen.width;
        float Ypos = SmallAimCrossHair.rectTransform.localPosition.y + inputVector.y * Screen.height;

        Xpos = Mathf.Clamp(Xpos, -Screen.width / 2, Screen.width / 2);
        Ypos = Mathf.Clamp(Ypos, -Screen.height / 2, Screen.height / 2);

        Vector3 pos = new Vector3(Xpos , -Ypos, SmallAimCrossHair.rectTransform.localPosition.z);


                //if(SmallAimCrossHair.rectTransform.localPosition != pos){

                    //SmallAimCrossHair.rectTransform.position = pos;
                    SmallAimCrossHair.rectTransform.position = Vector3.MoveTowards(SmallAimCrossHair.rectTransform.localPosition, pos, AimSpeed * Time.deltaTime);
                //}

  

    }


    private void independentAim(){


        Vector2 inputVector;
        float Xpos, Ypos;
        if(NoseAiming){
            inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();//aim with movement
            inputVector.y *= -1;
            

            Xpos = SmallAimCrossHair.rectTransform.localPosition.x + inputVector.x * Screen.width;
            Ypos = SmallAimCrossHair.rectTransform.localPosition.y + inputVector.y * Screen.height;

            if(inputVector.magnitude == 0){
                Xpos = 0;
                Ypos = 0;
            }

        }else{
            inputVector = playerInputActions.Player.Aim.ReadValue<Vector2>();// aim with aim

             Xpos = SmallAimCrossHair.rectTransform.localPosition.x + inputVector.x * Screen.width;
             Ypos = SmallAimCrossHair.rectTransform.localPosition.y + inputVector.y * Screen.height;
        }
        

         //Xpos = SmallAimCrossHair.rectTransform.localPosition.x + inputVector.x * Screen.width;
         //Ypos = SmallAimCrossHair.rectTransform.localPosition.y + inputVector.y * Screen.height;


        //Xpos = Mathf.Clamp(Xpos, -AimXDis, AimXDis);
        //Ypos = Mathf.Clamp(Ypos, -AimYDis, AimYDis);

        Xpos = Mathf.Clamp(Xpos, -Screen.width / 2, Screen.width / 2);
        Ypos = Mathf.Clamp(Ypos, -Screen.height / 2, Screen.height / 2);


         SmallAimCrossHair.rectTransform.localPosition = Vector3.MoveTowards(SmallAimCrossHair.rectTransform.localPosition,new Vector3(
             Xpos,
             Ypos, 
             -500f), 
             AimSpeed * Time.deltaTime);

             BigAimCrossHair.rectTransform.localPosition = SmallAimCrossHair.rectTransform.localPosition - new Vector3(50f * inputVector.x, 50f * inputVector.y, 0);
            
             //Debug.Log(inputVector);


    }

}
