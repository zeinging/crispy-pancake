using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class IntroScrollingText : MonoBehaviour
{
    public float speed = 40f, skipTextDuration = 2f;
    public string levelName;

    public GameObject NotificationText;

    private bool isFinished = false, Skiping = false;

    private RectTransform TextForImageOne, TextForImageTwo, TextForImageThree, TextForImageFour;

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
        TextForImageOne = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        

        Vector3 temp = TextForImageOne.anchoredPosition;
        //temp.y += 1;
        temp.y = 0;
        TextForImageOne.anchoredPosition = Vector3.MoveTowards(TextForImageOne.anchoredPosition, temp, speed * Time.deltaTime);
        if(TextForImageOne.anchoredPosition.y == 0){
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
