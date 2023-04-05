using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public float DialogueDuration = 5f;

    //private float tempTime;

    public GameObject Panel, imagesParent;

    public TMPro.TMP_Text DialogueTex;

    private GameObject currentPortrait;

    public static DialogueManager instance;

    private IEnumerator coroutine;

    //public bool isTalking = false;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null && instance != this){
            Destroy(this);
        }else{

        instance = this;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

        public void OpenManager(float Duration, string charName, string Dia){

            if(currentPortrait != null)
            currentPortrait.SetActive(false);//disable current portrait

            if(coroutine != null)//check if coroutine was assigned
            StopCoroutine(coroutine);
            //isTalking = true;
            coroutine = OpenDialogue(Duration, charName, Dia);
            StartCoroutine(coroutine);

        }


        public IEnumerator OpenDialogue(float Duration, string charName, string Dia){

        //ResetPortraits();

        Panel.SetActive(true);
        imagesParent = Panel.transform.GetChild(1).gameObject;
        
        //GameObject tempPort = TempParent.transform.Find(charName).gameObject;
        currentPortrait = imagesParent.transform.Find(charName).gameObject;
        DialogueTex.text = Dia;
        currentPortrait.SetActive(true);
        //isTalking = false;

        float tempTime = Duration;
        while(tempTime > 0){

            //if(isTalking){

                //Debug.Log("should of reset");
                //tempTime = Duration;//reset if another dialogue opens
                
            //}

            tempTime -= Time.deltaTime;
            //Debug.Log(tempTime);
            yield return null;
        }
        //isTalking = false;
        Panel.SetActive(false);
        currentPortrait.SetActive(false);
        //Destroy(DialogueTrigger);

    }

}
