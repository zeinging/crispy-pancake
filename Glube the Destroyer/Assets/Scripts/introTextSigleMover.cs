using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class introTextSigleMover : MonoBehaviour
{

    RectTransform myText;

    float speed, MaskY;

    private Camera cam;

    private Canvas can;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<RectTransform>();
        speed = FindAnyObjectByType<ScrollingTestManager>().TextSpeed;
        gameObject.SetActive(false);
        MaskY = FindObjectOfType<Mask>().rectTransform.sizeDelta.y;
        cam = FindObjectOfType<Camera>();
        can = GetComponentInParent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        myText.anchoredPosition = Vector2.MoveTowards(myText.anchoredPosition, new Vector2(0, (cam.scaledPixelHeight / can.scaleFactor) - MaskY), speed * Time.deltaTime);
        //Debug.Log(cam.scaledPixelHeight / can.scaleFactor);
        if(myText.anchoredPosition.y >= (cam.scaledPixelHeight / can.scaleFactor) -MaskY){
        gameObject.SetActive(false);
        //this.enabled = false;
        }
    }
}
