using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlubeSoundEventScript : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlaySoundNow(){
        Debug.Log("Play sound now.");
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Test SFX");
    }
}
