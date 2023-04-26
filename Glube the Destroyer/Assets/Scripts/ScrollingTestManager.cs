using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScrollingTestManager : MonoBehaviour
{
    public float TextSpeed = 20f, skipTextDuration = 1f, imageDelay = 2f;

    public float ImageFadeSpeed = 0.1f;
    public string levelName;

    public GameObject NotificationText;

    private bool isFinished = false, Skiping = false;

    public int PastProgress = 0;

    public Image[] pastEvents;

    public RectTransform[] textForImages;

    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    public bool easteregg = false;

    public GameObject easterEggTitle;
    // Start is called before the first frame update
    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions.UI.Enable();

        playerInputActions.UI.Submit.performed += SkipText;
        //LeanTweenFaderScript.instance.LoadLevel(levelName);
        //StartCoroutine(PlayPastEvents(pastEvents, imageDuration, imageDelay));
        ResizeAndRePositionTextBox();

        if(!easteregg){
            pastEventsPlayer(PastProgress, imageDelay);
        }else{
            StartCoroutine(easterEgg());
        }
    }

    // Update is called once per frame
    void Update()
    {

      //if(textForImages[0].GetComponent<TMPro.TMP_Text>().isTextOverflowing){
        //ScrollingText();
      //}
        //Debug.Log(textForImages[0].GetComponent<TMPro.TMP_Text>().preferredHeight);

        //ScrollingText();
    }

    private void ResizeAndRePositionTextBox(){

        //textForImages[0].transform.parent.GetComponent<Mask>().enabled = true;

        for(int i = 0; i < textForImages.Length; i++){
        Vector2 temp = textForImages[i].sizeDelta;
        temp.y = textForImages[i].GetComponent<TMPro.TMP_Text>().preferredHeight;
        textForImages[i].sizeDelta = temp;//resize text box
        textForImages[i].pivot = new Vector2(0.5f, 0);// change pivot point to the bottom
        Vector2 MaskBottom = textForImages[i].transform.parent.GetComponent<RectTransform>().sizeDelta;
        MaskBottom.y *= -1;
        MaskBottom.y -= textForImages[i].sizeDelta.y;
        textForImages[i].anchoredPosition = MaskBottom;// reposition text box to be just bellow the mask
        //textForImages[i].anchoredPosition -= new Vector2(0, textForImages[i].sizeDelta.y);// reposition text box to be just bellow the mask
        //if(i > 0)
        //textForImages[i].gameObject.SetActive(false);//disable text probably isn't needed
        }

    }

    private void ScrollingText(){

        if(PastProgress < textForImages.Length){

            Vector2 temp = textForImages[PastProgress].anchoredPosition;
            temp.y = 0;
            textForImages[PastProgress].anchoredPosition = Vector2.MoveTowards(textForImages[PastProgress].anchoredPosition, temp, TextSpeed * Time.deltaTime);
            if(textForImages[PastProgress].anchoredPosition.y == 0){
                PastProgress++;

            }
        }else{
                if(!isFinished && PastProgress == textForImages.Length){
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

    IEnumerator easterEgg(){

        yield return new WaitForSeconds(2.5f);
        easterEggTitle.SetActive(true);
        while(easterEggTitle.transform.localScale != Vector3.zero){
            easterEggTitle.transform.localScale = Vector3.MoveTowards(easterEggTitle.transform.localScale, Vector3.zero, 0.8f * Time.deltaTime);
            yield return null;
        }
        pastEventsPlayer(PastProgress, imageDelay);

    }

    private void pastEventsPlayer(int past, float delay){
        if(PastProgress < pastEvents.Length){
        StartCoroutine(FadeInPastEvent(past, delay));
        }

    }

    private IEnumerator FadeInPastEvent(int past, float delay){

        float MaskY = FindObjectOfType<Mask>().rectTransform.sizeDelta.y;

        if(PastProgress == 0){
        yield return new WaitForSeconds(2f);
        textForImages[0].gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(delay);

        Color temp = pastEvents[past].color;

        Vector2 TextTemp = textForImages[PastProgress].anchoredPosition;
        TextTemp.y = 0;

        while(temp.a != 1f){//fade in image
            temp.a = Mathf.MoveTowards(temp.a, 1f, ImageFadeSpeed * Time.deltaTime);
            pastEvents[past].color = temp;
            //Debug.Log(temp.a);
            if(temp.a > 0.5f){
            //textForImages[past].gameObject.SetActive(true);
            //textForImages[past].anchoredPosition = Vector2.MoveTowards(textForImages[PastProgress].anchoredPosition, TextTemp, TextSpeed * Time.deltaTime);
            }
            yield return null;
        }

        //Vector2 TextTemp = textForImages[PastProgress].anchoredPosition;
        //TextTemp.y = 0;
        while(textForImages[past].anchoredPosition.y < 0){//scroll text up the screen
            //textForImages[past].anchoredPosition = Vector2.MoveTowards(textForImages[PastProgress].anchoredPosition, TextTemp, TextSpeed * Time.deltaTime);

            // if(textForImages[past].anchoredPosition.y > -100f){
            //     if(past < textForImages.Length - 1){
            //     textForImages[past + 1].gameObject.SetActive(true);
            //     }   
            // }

            yield return null;
        }

        if(past < textForImages.Length - 1){
        textForImages[past + 1].gameObject.SetActive(true);
        }

        //Debug.Log(textForImages.Length);
        if(past == textForImages.Length - 1){
            //Debug.Log("Last image");

            while(textForImages[past].gameObject.activeInHierarchy){
                yield return null;
            }

        }

        while(temp.a != 0){//fade out image
            temp.a = Mathf.MoveTowards(temp.a, 0, ImageFadeSpeed * Time.deltaTime);
            pastEvents[past].color = temp;
            yield return null;
        }
        //temp.a = 1;
        //past.color = temp;
        

        yield return new WaitForSeconds(delay * 2);


        if(PastProgress < pastEvents.Length - 1){
            PastProgress++;
            StartCoroutine(FadeInPastEvent(PastProgress, delay));
        }else{
            LeanTweenFaderScript.instance.LoadLevel(levelName);
        }


        // while(temp.a != 0){
        //     temp.a = Mathf.MoveTowards(temp.a, 0, ImageFadeSpeed * Time.deltaTime);
        //     past.color = temp;
        //     yield return null;
        // }
        //PastProgress++;
        //pastEventsPlayer(imageDuration, delay);
        //temp.a = 0;
        //past.color = temp;

    }

    private IEnumerator FadeOutPastEvent(int past, float delay){

        yield return new WaitForSeconds(delay);

        Color temp = textForImages[past].GetComponent<Image>().color;

        while(temp.a != 0){
            temp.a = Mathf.MoveTowards(temp.a, 0, ImageFadeSpeed * Time.deltaTime);
            textForImages[past].GetComponent<Image>().color = temp;
            yield return null;
        }
        //PastProgress++;

    }

}
