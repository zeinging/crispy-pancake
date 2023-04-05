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

    public enum Portraits{
        Hart,Gerber,Civilian1,Civilian2,Civilian3,Civilian4,Civilian5,Civilian6
    }
    public Portraits SelectedPortrait;

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
        GameObject TempParent = Panel.transform.GetChild(1).gameObject;
        GameObject tempPort = TempParent.transform.Find(SelectedPortrait.ToString()).gameObject;
        //Debug.Log(SelectedPortrait.ToString());
        //if(Panel.transform.Find(SelectedPortrait.ToString()))
        //Debug.Log(Panel.transform.GetChild(1).name);
        //GameObject tempPort = Panel.transform.Find(SelectedPortrait.ToString()).gameObject;
        tempPort.SetActive(true);
        yield return new WaitForSeconds(DilogueDuration);
        Panel.SetActive(false);
        tempPort.SetActive(false);

    }

     void OnTriggerEnter(){

        //Debug.Log(barkDialogue);
        
        DialogueTex.text = barkDialogue;
        StartCoroutine(PanelTime());

    }

}
