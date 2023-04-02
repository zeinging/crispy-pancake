using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTriggerAera : MonoBehaviour
{

    public string barkDialogue;

    public float DilogueDuration = 5f;

    public GameObject Panel;

    public TMPro.TMP_Text DialogueTex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator PanelTime(){

        Panel.SetActive(true);
        yield return new WaitForSeconds(DilogueDuration);
        Panel.SetActive(false);

    }

     void OnTriggerEnter(){

        Debug.Log(barkDialogue);
        
        DialogueTex.text = barkDialogue;
        StartCoroutine(PanelTime());

    }

}
