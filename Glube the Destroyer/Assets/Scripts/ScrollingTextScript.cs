using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScrollingTextScript : MonoBehaviour
{
    public float speed = 20f, skipTextDuration = 1f;
    public string levelName;

    public GameObject NotificationText;

    private bool isFinished = false, Skiping = false;

    private RectTransform MovingText;

    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions.UI.Enable();

        if(NotificationText != null)
        playerInputActions.UI.Submit.performed += SkipText;
        //LeanTweenFaderScript.instance.LoadLevel(levelName);
        MovingText = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        

        Vector3 temp = MovingText.anchoredPosition;
        //temp.y += 1;
        temp.y = 0;
        MovingText.anchoredPosition = Vector3.MoveTowards(MovingText.anchoredPosition, temp, speed * Time.deltaTime);
        if(MovingText.anchoredPosition.y == 0){
            if(!isFinished){
            playerInputActions.UI.Disable();
            if(levelName != ""){
            LeanTweenFaderScript.instance.LoadLevel(levelName);
            }
                
            isFinished = true;
            }
        }
        //Debug.Log(temp.y);
    }

    private void SkipText(InputAction.CallbackContext context){
        if(context.performed){
            if(Skiping){
                playerInputActions.UI.Disable();
            LeanTweenFaderScript.instance.LoadLevel(levelName);
            }
            NotificationText.SetActive(true);
            Skiping = true;
            StartCoroutine(TextTimer(skipTextDuration));
        }
    }

    private IEnumerator TextTimer(float t){

        yield return new WaitForSeconds(t);
        NotificationText.SetActive(false);
        Skiping = false;

    }
}
