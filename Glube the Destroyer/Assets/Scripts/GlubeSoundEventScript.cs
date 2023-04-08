using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlubeSoundEventScript : MonoBehaviour
{

    //public FMOD.Studio.EventInstance instance;

    //public FMODUnity.EventReference[] fmodEvent;

    //public List<string> FModIndex;

    public GameObject EmittersParent;

    public List<string> Emitters;

    private Animator anim;



    void Start(){

        anim = GetComponent<Animator>();
        

          for(int i = 0; i < EmittersParent.transform.childCount; i++){

            Emitters.Add(EmittersParent.transform.GetChild(i).name);

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
        }

        if(!anim.GetBool("IsAttackingBuilding")){//putting this here so combo sound can be interupted
            Combo1End();
            Combo2End();
        }

    }


    public void Combo1Start(){
    EmittersParent.transform.Find(Emitters[0]).gameObject.SetActive(true);
    }
    public void Combo1End(){
    EmittersParent.transform.Find(Emitters[0]).gameObject.SetActive(false);
    }

    public void Combo2Start(){
    EmittersParent.transform.Find(Emitters[1]).gameObject.SetActive(true);
    }
    public void Combo2End(){
    EmittersParent.transform.Find(Emitters[1]).gameObject.SetActive(false);
    }

    public void Combo3Start(){
    EmittersParent.transform.Find(Emitters[2]).gameObject.SetActive(true);
    }
    public void Combo3End(){
    EmittersParent.transform.Find(Emitters[2]).gameObject.SetActive(false);
    }


     public void HurtStart(){
    EmittersParent.transform.Find(Emitters[3]).gameObject.SetActive(true);
    }
    public void HurtEnd(){
    EmittersParent.transform.Find(Emitters[3]).gameObject.SetActive(false);
    }

    public void IdleStart(){
    EmittersParent.transform.Find(Emitters[4]).gameObject.SetActive(true);
    }
    public void IdleEnd(){
    EmittersParent.transform.Find(Emitters[4]).gameObject.SetActive(false);
    }

    public void JumpStart(){
    EmittersParent.transform.Find(Emitters[5]).gameObject.SetActive(true);
    }
    public void JumpEnd(){
    EmittersParent.transform.Find(Emitters[5]).gameObject.SetActive(false);
    }

    public void WalkStart(){
    EmittersParent.transform.Find(Emitters[6]).gameObject.SetActive(true);
    }
    public void WalkEnd(){
    EmittersParent.transform.Find(Emitters[6]).gameObject.SetActive(false);
    }

    public void PlaySoundNow(){
        Debug.Log("Play sound now.");
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Glube/Combo/Glube_Combo-1");
    }
}
