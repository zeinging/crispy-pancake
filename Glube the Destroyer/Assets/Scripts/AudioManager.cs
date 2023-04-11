using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private GameObject GlubeSFX, AudioMusic, AudioPlayerSFX, EnviromentSFX, currentTrack;

    public List<GameObject> MusicTracks, PlayerSFX;
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

        GlubeSFX = transform.GetChild(0).gameObject;
        AudioMusic = transform.GetChild(1).gameObject;
        AudioPlayerSFX = transform.GetChild(2).gameObject;
        EnviromentSFX = transform.GetChild(3).gameObject;

        }

        MusicBus = FMODUnity.RuntimeManager.GetBus("bus:/MUSIC");
        SFXBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX");


        for(int i = 0; i < AudioMusic.transform.childCount; i++){//get music tracks
            MusicTracks.Add(AudioMusic.transform.GetChild(i).gameObject);
            Debug.Log("" + MusicTracks[0]);
            if(AudioMusic.transform.GetChild(i).gameObject.activeInHierarchy){
                currentTrack = AudioMusic.transform.GetChild(i).gameObject;
            }
            
        }

        for(int i = 0; i < AudioPlayerSFX.transform.childCount; i++){
            PlayerSFX.Add(AudioPlayerSFX.transform.GetChild(i).gameObject);
        }


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

    // private void DisableChildren(){//disable to prevent them from playing a SFX right after unpausing
    //     for(int i = 0; i < AudioSFX.transform.childCount; i++){
    //         if(AudioSFX.transform.GetChild(i).gameObject.activeInHierarchy)
    //             AudioSFX.transform.GetChild(i).gameObject.SetActive(false);
    //     }
    // }

    public void StopRuble(){
        PlayerSFX[0].SetActive(false);
    }
    public void PlayerCrashed(){
        PlayerSFX[1].SetActive(true);
    }
    public void PlayerBoost(){
        PlayerSFX[2].SetActive(true);
    }

    public void CrashedIntoGlube(){
        PlayerSFX[3].SetActive(true);
    }

    public void BuildingExplodeStart(){
        BuildingExplodeEnd();
        EnviromentSFX.SetActive(true);
        //StartCoroutine(ExplodeSFXDelay(t));
    }

    private IEnumerator ExplodeSFXDelay(float t){
        yield return new WaitForSeconds(t);
        EnviromentSFX.SetActive(true);
    }

    public void BuildingExplodeEnd(){
        EnviromentSFX.SetActive(false);
    }

    public void PauseMusic(bool isPaused){
        //if(!isPaused){
            //DisableChildren();
        //}
        //AudioSFX.SetActive(!isPaused);

        //AudioMusic.SetActive(!isPaused);
        SFXBus.setPaused(isPaused);
        MusicBus.setPaused(isPaused);//should pause music
    }

    public void StopMusic(){//not sure if .setactive to false is better or not.
        //SFXBus.setPaused(false);
        MusicBus.setPaused(true);
    }

    public void PlayMusic(){
        MusicBus.setPaused(false);
    }

    public void ResetMusic(){
        MusicBus.setPaused(false);// just in case music is paused for some reason
        AudioMusic.SetActive(false);
    }

    

    public void MainThemeLoop(){
        if(MusicTracks[0] != currentTrack){
        currentTrack.SetActive(false);
        MusicTracks[0].SetActive(true);
        currentTrack = MusicTracks[0];
        }
    }

    public void GlubeDefeatIntro(){
        StopMusic();
    if(MusicTracks[1] != currentTrack){
    currentTrack.SetActive(false);
    MusicTracks[1].SetActive(true);
    currentTrack = MusicTracks[1];
    StartCoroutine(PlayMusicDelay(10f));
    }
    }

    public void GlubeDefeatLoop(){
        StopMusic();
    if(MusicTracks[2] != currentTrack){
    currentTrack.SetActive(false);
    MusicTracks[2].SetActive(true);
    currentTrack = MusicTracks[2];
    StartCoroutine(PlayMusicDelay(10f));
    }
    }

    public void PlayerDown(){
        StopMusic();
        StopRuble();
        PlayerCrashed();
    if(MusicTracks[3] != currentTrack){
    currentTrack.SetActive(false);
    MusicTracks[3].SetActive(true);
    currentTrack = MusicTracks[3];
    StartCoroutine(PlayMusicDelay(2));
    }
    }

    public void IntoGlube(){
        StopMusic();
        StopRuble();
        CrashedIntoGlube();
    if(MusicTracks[3] != currentTrack){
    currentTrack.SetActive(false);
    MusicTracks[3].SetActive(true);
    currentTrack = MusicTracks[3];
    StartCoroutine(PlayMusicDelay(2));
    }
    }

    public void GlubeWins(){
        StopMusic();
        //StopRuble();
        //CrashedIntoGlube();
    if(MusicTracks[3] != currentTrack){
    currentTrack.SetActive(false);
    MusicTracks[3].SetActive(true);
    currentTrack = MusicTracks[3];
    StartCoroutine(PlayMusicDelay(3.9f));
    }
    }

    private IEnumerator PlayMusicDelay(float t){
                yield return new WaitForSeconds(t);
                PlayMusic();
                //PlayerDown();
    }

}
