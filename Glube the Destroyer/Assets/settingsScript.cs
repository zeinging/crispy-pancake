using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class settingsScript : MonoBehaviour
{

    //public AudioMixer audiomixer;

    ////public AudioSource MusicSource, SFXSource;

    public TMPro.TMP_Dropdown resolutionDropdown;

    public Toggle FullScreenTogle;

    public TMPro.TMP_Dropdown GraphicDropdown;

    public Slider MusicSlider, SFXSlider;

    Resolution[] resolutuions;

    public int currentResolutionIndex = 0;

    //public RectTransform resTemp;
    //public float MoveAmount;
    public Slider ResolutionSlider;

    public Button RightResButtons, LeftResButton;

    public CanvasGroup ApplyButton;

    public TMPro.TMP_Text testText;

    [SerializeField]
    private List<string> options = new List<string>();

    private void Start()
    {
        FullScreenTogle.isOn = Screen.fullScreen;//set toggle display

        //GraphicDropdown.value = QualitySettings.GetQualityLevel();//set dropdown display
        


        //MoveAmount = resTemp.rect.height - 2;
        //float voltemp;
        //bool temp = audiomixer.GetFloat("Volume", out voltemp);
        //VolumeSlider.value = voltemp;
        //volume currently doesn't save
        
        

        resolutuions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        //options = new List<string>();

        //int currentResolutionIndex = 0;

        for (int i = 0; i < resolutuions.Length; i++)
        {
            //options = new List<string>(resolutuions.Length);

            string option = (resolutuions[i].width + " x " + resolutuions[i].height + " " + resolutuions[i].refreshRate) + "Hz";

            if(resolutuions[i].refreshRate == 60){//should only add 60 Hrtz resolutions after unity update
                options.Add(option);//add the first one
                Debug.Log(option);
            }

            //if(i > 0 ){//if statement skips the first one

                //if(resolutuions[i - 1].ToString() != option){//if statement should prevent duplicates.

                    //options.Add(option);
                //}
                //if(options[i] == option)

            //}

            //if (resolutuions[i].width == Screen.currentResolution.width &&
                //resolutuions[i].height == Screen.currentResolution.height) {

                //currentResolutionIndex = i;

            //}

            if (resolutuions[i].width == Screen.width &&
                resolutuions[i].height == Screen.height &&
                resolutuions[i].refreshRate == Screen.currentResolution.refreshRate)//get window resolution
            {

                currentResolutionIndex = i;
                
                //Debug.Log("should only happen once");
                

            }

            //if(Screen.currentResolution.ToString() == resolutuions[i].ToString()){

                //currentResolutionIndex = i;
                //Debug.Log("should only happen once");
            //}
            


        }

        //resolutionDropdown.AddOptions(options);
        //resolutionDropdown.value = currentResolutionIndex;        
        //resolutionDropdown.RefreshShownValue();

        ResolutionSlider.minValue = 0;
        if(options.Count > 0)
        ResolutionSlider.maxValue = options.Count - 1;
        Debug.Log("should only happen once");
        ResolutionSlider.GetComponentInChildren<TMPro.TMP_Text>().text = resolutuions[currentResolutionIndex].ToString();
        ResolutionSlider.value = currentResolutionIndex;

        //ChangeActiveButton();
    }

    void OnEnable(){
        FullScreenTogle.isOn = Screen.fullScreen;
        ResolutionSlider.GetComponentInChildren<TMPro.TMP_Text>().text = resolutuions[currentResolutionIndex].ToString();
        ResolutionSlider.value = currentResolutionIndex;
    }

    // public void OnClose(){
    //     LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.5f).setLoopPingPong(1);
    // }    

    // public void ChangeActiveButton(){
    //     if(ResolutionSlider.value == 0){
    //         LeftResButton.interactable = false;
    //     }else{
    //         LeftResButton.interactable = true;
    //     }

    //     if(ResolutionSlider.value == 15){
    //         RightResButtons.interactable = false;
    //     }else{
    //         RightResButtons.interactable = true;
    //     }
    // }

    // public void MouseButtonChangeRes(bool isRight){
    //     if(isRight){
    //         ResolutionSlider.value++;
    //     }else{
    //         ResolutionSlider.value--;
    //     }
    // }
    public void ChangeResolution(){
        //currentResolutionIndex = (int)ResolutionSlider.value;
        ResolutionSlider.GetComponentInChildren<TMPro.TMP_Text>().text = resolutuions[(int)ResolutionSlider.value].ToString();

        testText.text = "index = " + currentResolutionIndex + " slider = " + ResolutionSlider.value + " options: " + options.Capacity;

        if(currentResolutionIndex != (int)ResolutionSlider.value){
            ApplyButton.interactable = true;
            ApplyButton.alpha = 1;
        }else{
            ApplyButton.interactable = false;
            ApplyButton.alpha = 0.5f;
        }
    }

    public void NewSetResolution(){
        Resolution tempres = resolutuions[(int)ResolutionSlider.value];
        Screen.SetResolution(tempres.width, tempres.height, Screen.fullScreen, tempres.refreshRate);
        ApplyButton.interactable = false;
        ApplyButton.alpha = 0.5f;
        ResolutionSlider.Select();
        currentResolutionIndex = (int)ResolutionSlider.value;
        testText.text = "index = " + currentResolutionIndex + " slider = " + ResolutionSlider.value + " options: " + options.Capacity;
    }

    public void SetResolution(int ResolutionIndex) {

        Resolution resolutions = resolutuions[ResolutionIndex];
        Screen.SetResolution(resolutions.width, resolutions.height, Screen.fullScreen);
    
    }

    public void ToggleFullScreen(bool isFullScreen) {

        Screen.fullScreen = isFullScreen;//only works in build version

    }

    public void SetGraphicQuality(int QualityIndex) {

        QualitySettings.SetQualityLevel(QualityIndex);//qualtiy window updates only if the mouse hovers over the project quality settings

    }

    public void SetMusicVolume() {

        //audiomixer.SetFloat("Volume", volume);
        //MusicSource.volume = MusicSlider.value / 20;

    }

    public void SetSFXVolume(){
        //SFXSource.volume = SFXSlider.value / 20;
    }

    

    public void ExitButton() {

        //resolutionDropdown.ClearOptions();

        Application.Quit();
    

    }

}
