using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTriggerAera : MonoBehaviour
{

    public Portraits PlaneTriggeredPortrait;

    [TextArea(3,5)]
    public string PlaneDialogue;

    public Portraits GlubeTriggeredPortrait;

    [TextArea(3,5)]
    public string GlubeDialogue;

    public Portraits DestroyedPortrait;

    [TextArea(3,5)]
    public string DestroyedDialogue;

    public float DialogueDuration = 5f, DestroyedDuration = 5f;

    public bool TriggeredPlane = false, TriggeredGlube = false;

    private float tempTime;

    //public GameObject Panel;

    //public TMPro.TMP_Text DialogueTex;

    public enum Portraits{
        Hart,Gerber,BlondeMan,KidBoy,RedHead,KidGirl,Johna,Pinky
    }
    //public Portraits TriggerPortrait, DestroyedPortrait;

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

            if(gameObject.scene.isLoaded)//checks if was destroyed because of scene change
            DialogueManager.instance.OpenManager(DestroyedDuration, DestroyedPortrait.ToString(), DestroyedDialogue);
        }
    }


    // private IEnumerator PanelTime(){//probably not being used at the moment

    //     GetComponent<SphereCollider>().enabled = false;//disable so this only works once.
    //     GetComponent<MeshRenderer>().enabled = false;
    //     Panel.SetActive(true);
    //     GameObject TempParent = Panel.transform.GetChild(1).gameObject;
    //     GameObject tempPort = TempParent.transform.Find(TriggerPortrait.ToString()).gameObject;
    //     //Debug.Log(SelectedPortrait.ToString());
    //     //if(Panel.transform.Find(SelectedPortrait.ToString()))
    //     //Debug.Log(Panel.transform.GetChild(1).name);
    //     //GameObject tempPort = Panel.transform.Find(SelectedPortrait.ToString()).gameObject;
    //     tempPort.SetActive(true);
    //     yield return new WaitForSeconds(DialogueDuration);
    //     Panel.SetActive(false);
    //     tempPort.SetActive(false);
    //     //if(DestroyedDialogue == "" )
    //     //Destroy(gameObject);

    // }

     void OnTriggerEnter(Collider other){

        //Debug.Log(barkDialogue);
        //DialogueManager.instance.OpenManager(DialogueDuration, GlubeTriggeredPortrait.ToString(), GlubeDialogue);
        
        if(other.GetComponent<PlayerPlane>() && !TriggeredPlane){//plane entered trigger zone

            //DialogueTex.text = barkDialogue;
            //StartCoroutine(PanelTime());
            //StartCoroutine(DialogueManager.instance.OpenDialogue(DialogueDuration, SelectedPortrait.ToString(), barkDialogue));
            //this.gameObject.SetActive(false);
            //if(DestroyedDialogue == "")
            //Destroy(gameObject);//destroy to prevent triggering the same dialogue
            if(PlaneDialogue != ""){// only run this if there is dailogue for the plane entering the trigger zone
            DialogueManager.instance.OpenManager(DialogueDuration, PlaneTriggeredPortrait.ToString(), PlaneDialogue);
            TriggeredPlane = true;
            //GetComponent<MeshRenderer>().enabled = false;// tempoary might get rid of later
            //GetComponent<SphereCollider>().enabled = false;//disabe trigger to prevent same dialogue showing up again.
            }


            //if(DestroyedDialogue == "" )
            //Destroy(gameObject);
        }
        
        
        if(other.GetComponentInParent<DestoryNearestBuildingDirector>()){//glube entered trigger zone.
            //Debug.Log("Glube entered Trigger zone");
            if(!TriggeredGlube && GlubeDialogue != ""){
            DialogueManager.instance.OpenManager(DialogueDuration, GlubeTriggeredPortrait.ToString(), GlubeDialogue);
            TriggeredGlube = true;
            }

        }

    }

}
