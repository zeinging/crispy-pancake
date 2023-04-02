using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leanTweenFader : MonoBehaviour {

    public GameObject TopSlider, BotSlider;

    public float StartDuration = 0.5f, EndDuration = 1f;

    private Vector3 TopDefaultPos, BotDefaultPos;


    private void OnEnable()
    {
        TopSlider.transform.localPosition = new Vector3(0, Screen.height / 2, 0);

        BotSlider.transform.localPosition = TopSlider.transform.localPosition * -1;


        TopDefaultPos = TopSlider.transform.localPosition;
        BotDefaultPos = BotSlider.transform.localPosition;
    }

    public void FadingLeanTween()
    {

        LeanTween.moveLocal(TopSlider, new Vector3(0, 0, 0) , StartDuration);
        LeanTween.moveLocal(BotSlider, new Vector3(0, 0, 0), StartDuration);

    }

    public void ResetSliders()
    {
        LeanTween.moveLocal(TopSlider, TopDefaultPos, EndDuration);
        LeanTween.moveLocal(BotSlider, BotDefaultPos, EndDuration);
    }

    


}
