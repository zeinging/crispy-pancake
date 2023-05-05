using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LogoWaitScript : MonoBehaviour
{
    public float duration = 2f, inputWait = 0;
    public string levelName;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions.UI.Submit.performed += SkipText;

        if(duration > 0)
        StartCoroutine(TextTimer());

        StartCoroutine(inputTimer());

    }

    private void SkipText(InputAction.CallbackContext context){
        if(context.performed){
            StopAllCoroutines();
            LeanTweenFaderScript.instance.LoadLevel(levelName);
            playerInputActions.UI.Disable();
        }
    }

    private IEnumerator inputTimer(){

        while(inputWait > 0){
            inputWait -= Time.deltaTime;
            yield return null;
        }
        inputWait = 0;
        playerInputActions.UI.Submit.Enable();
    }

    private IEnumerator TextTimer(){

        while(duration > 0){
            duration -= Time.deltaTime;
            yield return null;
        }
        duration = 0;
        playerInputActions.UI.Disable();
        LeanTweenFaderScript.instance.LoadLevel(levelName);

    }
}
