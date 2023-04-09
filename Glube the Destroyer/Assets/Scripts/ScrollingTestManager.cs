using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScrollingTestManager : MonoBehaviour
{
    public float TextSpeed = 20f, skipTextDuration = 1f, imageDuration = 2f, imageDelay = 2f;

    public float ImageFadeSpeed = 0.1f;
    public string levelName;

    public GameObject NotificationText;

    private bool isFinished = false, Skiping = false;

    public int PastProgress = 0;

    public Image[] pastEvents;

    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        //playerInputActions.UI.Enable();

        playerInputActions.UI.Submit.performed += SkipText;
        //LeanTweenFaderScript.instance.LoadLevel(levelName);
        //StartCoroutine(PlayPastEvents(pastEvents, imageDuration, imageDelay));
        pastEventsPlayer(imageDuration, imageDelay);
    }

    // Update is called once per frame
    void Update()
    {

        

        //ScrollingText();
    }

    private void ScrollingText(){

                Vector3 temp = transform.position;
        temp.y += 1;
        transform.position = Vector3.MoveTowards(transform.position, temp, TextSpeed * Time.deltaTime);
        if(transform.position.y > 190f){
            if(!isFinished){
            LeanTweenFaderScript.instance.LoadLevel(levelName);
            isFinished = true;
            }
        }

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

    private void pastEventsPlayer(float imageDuration, float delay){
        if(PastProgress < pastEvents.Length){
        StartCoroutine(PlayPastEvents(pastEvents[PastProgress], imageDuration, delay));
        }

    }

    private IEnumerator PlayPastEvents(Image past, float imageDuration, float delay){

        yield return new WaitForSeconds(delay);

        Color temp = past.color;

        while(temp.a != 1f){
            temp.a = Mathf.MoveTowards(temp.a, 1f, ImageFadeSpeed * Time.deltaTime);
            past.color = temp;
            //Debug.Log(temp.a);
            yield return null;
        }
        //temp.a = 1;
        //past.color = temp;
        

        yield return new WaitForSeconds(imageDuration);


        while(temp.a != 0){
            temp.a = Mathf.MoveTowards(temp.a, 0, ImageFadeSpeed * Time.deltaTime);
            past.color = temp;
            yield return null;
        }
        PastProgress++;
        pastEventsPlayer(imageDuration, delay);
        //temp.a = 0;
        //past.color = temp;

    }

}
