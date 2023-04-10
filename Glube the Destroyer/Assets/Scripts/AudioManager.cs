using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private GameObject AudioSFX, AudioMusic;
    public static AudioManager instance;

    FMOD.Studio.Bus MusicBus, SFXBus;

    [SerializeField][Range(-80f, 10f)]
    private float MusicVolume, SFXVolume;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this){
            Destroy(this);
        }else{

        instance = this;

        }
        if(transform.childCount > 0){

        AudioSFX = transform.GetChild(0).gameObject;
        AudioMusic = transform.GetChild(1).gameObject;

        }

        MusicBus = FMODUnity.RuntimeManager.GetBus("bus:/MUSIC");
        SFXBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX");

    }

    // Update is called once per frame
    void Update()
    {
        MusicBus.setVolume(DecibleToLinear(MusicVolume));
        SFXBus.setVolume(DecibleToLinear(SFXVolume));
    }

    private float DecibleToLinear(float dB){

        float linear = Mathf.Pow(10.0f, dB / 20);
        return linear;

    }

    private void DisableChildren(){//disable to prevent them from playing a SFX right after unpausing
        for(int i = 0; i < AudioSFX.transform.childCount; i++){
            if(AudioSFX.transform.GetChild(i).gameObject.activeInHierarchy)
                AudioSFX.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void PauseMusic(bool isPaused){
        if(!isPaused){
            //DisableChildren();
        }
        //AudioSFX.SetActive(!isPaused);

        //AudioMusic.SetActive(!isPaused);
        SFXBus.setPaused(isPaused);
        MusicBus.setPaused(isPaused);//should pause music
    }
}
