using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class glubeAnimationUI : MonoBehaviour
{

    public float RotateSpeed = 5;
    public Button AnimationButn;

    public Animator anim;
    public GameObject CharacterModel;

    //public AnimatorClipInfo[] testinganim;

    public List<string> AnimationNames;

    public List<float> AnimationTimes;

    public Button tempButn;
    //public string[] AnimationNames;

    public string ArmatureGlubeLetters = "ArmatureGlube|";

    // Start is called before the first frame update
    void Start()
    {
        GetAnimationClips();
        SpawnButns();
        //AnimationButn.gameObject.GetComponentInChildren<TMP_Text>().text = "test";
    }

    // Update is called once per frame
    void Update()
    {
        RotateCharacter();
    }


    void RotateCharacter(){

        Quaternion temp = CharacterModel.transform.rotation;
        temp = Quaternion.Euler(temp.eulerAngles.x, temp.eulerAngles.y + RotateSpeed * 10 * Time.deltaTime, temp.eulerAngles.z);
        CharacterModel.transform.rotation = temp;

    }

    public AnimationClip GetAnimationClips(){
        if(!anim)return null;

        string tempName;
        float tempTime;

        foreach(AnimationClip clip in anim.runtimeAnimatorController.animationClips){

                //if(clip.name.Contains("ArmatureGlube|")){
                    tempName = clip.name;
                   tempName = tempName.Replace(ArmatureGlubeLetters, "");

                   tempTime = clip.length;
                    
                //}


                AnimationNames.Add(tempName);
                AnimationTimes.Add(tempTime);
        }
        return null;
    }


    public void SpawnButns(){

       //AnimatorClipInfo[] animtemp = anim.GetCurrentAnimatorClipInfo(0);
       //Button butntemp = AnimationButn;
       int indes = 0;

       foreach(string Aname in AnimationNames){
           tempButn = Instantiate(AnimationButn, AnimationButn.transform.position, AnimationButn.transform.rotation, AnimationButn.transform.parent);
           tempButn.name = tempButn.name + indes.ToString();
           float anTime = AnimationTimes[indes];
           indes++;
            tempButn.gameObject.GetComponentInChildren<TMP_Text>().text = Aname;
            tempButn.onClick.AddListener(() => ChangeAnimation(Aname, anTime));
            

       }

       AnimationButn.gameObject.SetActive(false);

       

    }


    public void ChangeAnimation(string AnimName, float animTime){

        //anim.CrossFade(ArmatureGlubeLetters + AnimName,1f);

        //float tempAnimTime = AnimationTimes[]

        anim.CrossFade(AnimName, 0f);
        
        //anim.Play(AnimName);
    }

}
