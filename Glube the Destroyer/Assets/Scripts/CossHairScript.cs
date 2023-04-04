using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CossHairScript : MonoBehaviour
{


    public Image independentAimCrossHair, NoseAimCrossHair;

    public Transform Plane;

    //public GameObject CrossHair;

    public float AimXDis = 5f, AimYDis = 5f, DisFromCamera = 70f, DisFromPlane = 100f;

    public float AimSpeed = 20;

    
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions.Player.Enable();
        //CrossHair.transform.parent = Camera.main.transform;
        cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Plane != null)
        NoseAim();

        //independentAim();

    }

    private void NoseAim(){

        Vector3 pos = cam.WorldToScreenPoint(Plane.position + Plane.forward * DisFromPlane);
        Vector3 InnerPos = pos - Plane.forward * 25f;
        //Debug.Log(pos);
        //pos.x *= Screen.width;
        //pos.y *= Screen.height;
        //pos.z = 0;


        //if(NoseAimCrossHair.rectTransform.localPosition != pos){
            //NoseAimCrossHair.rectTransform.localPosition = pos;
        //}

        if(transform.position != pos){

            transform.position = pos;
        }

        //if(NoseAimCrossHair.transform.position != InnerPos){
            //NoseAimCrossHair.transform.position = InnerPos;
        //}

    }


    private void independentAim(){


        Vector2 inputVector = playerInputActions.Player.Aim.ReadValue<Vector2>();

        float Xpos = independentAimCrossHair.rectTransform.localPosition.x + inputVector.x * Screen.width;
        float Ypos = independentAimCrossHair.rectTransform.localPosition.y + inputVector.y * Screen.height;


        Xpos = Mathf.Clamp(Xpos, -AimXDis, AimXDis);
        Ypos = Mathf.Clamp(Ypos, -AimYDis, AimYDis);


         independentAimCrossHair.rectTransform.localPosition = Vector3.MoveTowards(independentAimCrossHair.rectTransform.localPosition,new Vector3(
             Xpos,
             Ypos, 
             -500f), 
             AimSpeed * Time.deltaTime);


    }

}
