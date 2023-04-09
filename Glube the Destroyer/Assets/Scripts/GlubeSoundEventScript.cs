using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlubeSoundEventScript : MonoBehaviour
{

    //public FMOD.Studio.EventInstance instance;

    //public FMODUnity.EventReference[] fmodEvent;

    //public List<string> FModIndex;

    public GameObject EmittersParent, GlubeSoundSoruceLocation;

    public List<GameObject> Emitters;

    private Animator anim;



    void Start(){

        EmittersParent.transform.parent = GlubeSoundSoruceLocation.transform;//parent emitters to glube
        EmittersParent.transform.localPosition = Vector3.zero;//reset transform or else will be in the wrong spot
        EmittersParent.transform.localRotation = Quaternion.Euler(Vector3.zero);
        EmittersParent.transform.localScale = new Vector3(1, 1, 1);

        anim = GetComponent<Animator>();
        

          for(int i = 0; i < EmittersParent.transform.childCount; i++){

            Emitters.Add(EmittersParent.transform.GetChild(i).gameObject);

          }
        //instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent[0]);
        //instance.start();
    }

    // Update is called once per frame
    void Update()
    {
        //instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        //instance.start();

        if(anim.GetFloat("AgentSpeed") == 0){//have to disable sound with the animator float
            WalkEnd();
        }else{
            IdleEnd();
            if(!Emitters[6].activeInHierarchy){
                WalkStart();
            }
        }

        if(!anim.GetBool("IsAttackingBuilding")){//putting this here so combo sound can be interupted
            Combo1End();
            Combo2End();
        }

    }


    public void Combo1Start(){
    Emitters[0].gameObject.SetActive(true);
    }
    public void Combo1End(){
    Emitters[0].gameObject.SetActive(false);
    }

    public void Combo2Start(){
    Emitters[1].gameObject.SetActive(true);
    }
    public void Combo2End(){
    Emitters[1].gameObject.SetActive(false);
    }

    public void Combo3Start(){
    Emitters[2].gameObject.SetActive(true);
    }
    public void Combo3End(){
    Emitters[2].gameObject.SetActive(false);
    }


     public void HurtStart(){
    Emitters[3].gameObject.SetActive(true);
    }
    public void HurtEnd(){
    Emitters[3].gameObject.SetActive(false);
    }

    public void IdleStart(){
    Emitters[4].gameObject.SetActive(true);
    }
    public void IdleEnd(){
    Emitters[4].gameObject.SetActive(false);
    }

    public void JumpStart(){
    Emitters[5].gameObject.SetActive(true);
    }
    public void JumpEnd(){
    Emitters[5].gameObject.SetActive(false);
    }

    public void WalkStart(){
    Emitters[6].gameObject.SetActive(true);
    }
    public void WalkEnd(){
    Emitters[6].gameObject.SetActive(false);
    }

    // public void PlaySoundNow(){
    //     Debug.Log("Play sound now.");
    //     FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Glube/Combo/Glube_Combo-1");
    // }
}
