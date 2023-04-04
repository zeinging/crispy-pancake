using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.UI;

public class AimRectacleScript : MonoBehaviour
{


    //public Image OutterRectacle;

    public GameObject CrossHair;

    public float AimXDis = 5f, AimYDis = 5f, DisFromCamera = 70f;

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
        CrossHair.transform.parent = Camera.main.transform;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Vector3 RectPos = OutterRectacle.rectTransform.position;

        //Vector3 temp = new Vector3(inputVector.x + RectPos.x, inputVector.y + RectPos.y, RectPos.z);// might need to change up to z axis
        //Vector3 temp = Vector3.zero;

        if(Mouse.current.leftButton.wasPressedThisFrame){
            playerInput.SwitchCurrentControlScheme();
            //playerInput.
        }

        Vector2 inputVector = playerInputActions.Player.Aim.ReadValue<Vector2>();

        float Xpos = CrossHair.transform.localPosition.x + inputVector.x;
        float Ypos = CrossHair.transform.localPosition.y + inputVector.y;

        //float Xpos = inputVector.x;
        //float Ypos = inputVector.y;

        Xpos = Mathf.Clamp(Xpos, -AimXDis, AimXDis);
        Ypos = Mathf.Clamp(Ypos, -AimYDis, AimYDis);

        //Vector3 MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //CrossHair.transform.localPosition = new Vector3(Xpos, Ypos, DisFromCamera);

         CrossHair.transform.localPosition = Vector3.MoveTowards(CrossHair.transform.localPosition,new Vector3(
             Xpos,
             Ypos, 
             DisFromCamera), 
             AimSpeed * Time.deltaTime);

        Debug.Log("New Input Mouse:" + inputVector);
        //Debug.Log("Screen To World Mouse:" + MousePos);

        //temp.z = 0;
        //OutterRectacle.rectTransform.position = Vector3.MoveTowards(OutterRectacle.rectTransform.position, temp, AimSpeed * Time.deltaTime);
    }
}
