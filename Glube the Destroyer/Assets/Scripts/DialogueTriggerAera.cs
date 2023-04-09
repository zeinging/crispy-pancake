using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTriggerAera : MonoBehaviour
{

    public string barkDialogue, DestroyedDialogue;

    public float DialogueDuration = 5f, DestroyedDuration = 5f;

    private float tempTime;

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

    void OnDestroy(){//open dialogue after trigger destroyed

        if(DestroyedDialogue != "" ){

            if(!gameObject.scene.isLoaded)//checks if was destroyed because of scene change
            DialogueManager.instance.OpenManager(DestroyedDuration, SelectedPortrait.ToString(), DestroyedDialogue);
        }
    }


    private IEnumerator PanelTime(){

        GetComponent<SphereCollider>().enabled = false;//disable so this only works once.
        GetComponent<MeshRenderer>().enabled = false;
        Panel.SetActive(true);
        GameObject TempParent = Panel.transform.GetChild(1).gameObject;
        GameObject tempPort = TempParent.transform.Find(SelectedPortrait.ToString()).gameObject;
        //Debug.Log(SelectedPortrait.ToString());
        //if(Panel.transform.Find(SelectedPortrait.ToString()))
        //Debug.Log(Panel.transform.GetChild(1).name);
        //GameObject tempPort = Panel.transform.Find(SelectedPortrait.ToString()).gameObject;
        tempPort.SetActive(true);
        yield return new WaitForSeconds(DialogueDuration);
        Panel.SetActive(false);
        tempPort.SetActive(false);
        //Destroy(gameObject);

    }

     void OnTriggerEnter(Collider other){

        //Debug.Log(barkDialogue);
        
        
        if(other.GetComponent<PlayerPlane>()){

            //DialogueTex.text = barkDialogue;
            //StartCoroutine(PanelTime());
            //StartCoroutine(DialogueManager.instance.OpenDialogue(DialogueDuration, SelectedPortrait.ToString(), barkDialogue));
            DialogueManager.instance.OpenManager(DialogueDuration, SelectedPortrait.ToString(), barkDialogue);
            //this.gameObject.SetActive(false);
            //if(DestroyedDialogue == "")
            //Destroy(gameObject);//destroy to prevent triggering the same dialogue
            GetComponent<MeshRenderer>().enabled = false;// tempoary might get rid of later
            GetComponent<SphereCollider>().enabled = false;//disabe trigger to prevent same dialogue showing up again.
        }

    }

}
