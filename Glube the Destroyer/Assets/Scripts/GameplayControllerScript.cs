using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayControllerScript : MonoBehaviour
{

    public int PlayerHealth, GlubeHealth, RemainingBuildings, lostPlanes;

    public GameObject BuildingParent, retryMenu, GlubesCinemachine, Glube, VictoryCam, GameplayUI, crossHairUI, PlayerPlane;

    private bool leaving = false, menuOpened = false, playerWon = false;

    public static GameplayControllerScript instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this){
            Destroy(this);
        }else{

        instance = this;

        }
        //BuildingParent = GameObject.Find("BuildingParent");
        RemainingBuildings = BuildingParent.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerTakeDamage(int damage){
        PlayerHealth -= damage;
    }

    public void GlubeTakeDamage(int damage){
        GlubeHealth -= damage;
        if(GlubeHealth <= 0){
            PlayerWins();
        }
    }

    public void ABuildingDestroyed(){
        RemainingBuildings--;
        if(RemainingBuildings == 0 && GlubeHealth > 0){
            GlubeWins();
        }
    }

    public void PlayerWins(){
            Debug.Log("Player Won!");
            playerWon = true;
            //AudioManager.instance.StopMusic();
            //GlubesCinemachine.SetActive(true);
            //AudioManager.instance.GlubeDefeatIntro();
            PlayerPlane.GetComponent<PlayerPlane>().DisableControls();
            PlayerPlane.GetComponent<PlayerPlane>().enabled = false;
            PlayerPlane.GetComponent<BoxCollider>().enabled = false;
            PlayerPlane.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //AudioManager.instance.GlubeDefeatLoop();
            //ToTitleScreen();
            //if(!leaving){
                //LeanTweenFaderScript.instance.LoadLevel("CreditsScene");
                StartCoroutine(WinCutscene());
                //leaving = true;
            //}
    }
    private IEnumerator WinCutscene(){
        //AudioManager.instance.StopMusic();
        //PlayerPlane.SetActive(false);
        Glube.GetComponent<DestoryNearestBuildingDirector>().GlubeDefeated();//use this to stop all courtines on glube
        //Glube.GetComponent<DestoryNearestBuildingDirector>().enabled = false;// put this in a function inside the glube script
        //Glube.GetComponent<Assets.Team_Members_Folders.CloakingPotion.GlubeAnimationController>().enabled = false;
        //yield return null;
        //Glube.GetComponentInChildren<Animator>().SetBool("IsAttackingBuilding", false);
        //yield return null;
        //Glube.GetComponentInChildren<Animator>().CrossFade("GLube Idle And Walk", 0.1f);
        //GameplayUI.SetActive(false);
        GameplayUI.transform.GetChild(1).gameObject.SetActive(false);//glube health bar
        GameplayUI.transform.GetChild(2).gameObject.SetActive(false);//player health bar
        crossHairUI.SetActive(false);
        AudioManager.instance.GlubeDefeatLoop();
        yield return new WaitForSeconds(1f);
        GlubesCinemachine.SetActive(true);
        yield return new WaitForSeconds(3f);
        Glube.GetComponentInChildren<Animator>().SetBool("GlubeDefeated", true);
        yield return new WaitForSeconds(6f);
        VictoryCam.SetActive(true);
        yield return new WaitForSeconds(10f);
        GameplayUI.transform.GetChild(4).gameObject.SetActive(true);// credits text
        if(!leaving){

        //LeanTweenFaderScript.instance.LoadLevel("CreditsScene");
        leaving = true;
        }
        //yield return new WaitForSeconds(6f);
        //yield return null;
        //AudioManager.instance.PlayMusic();
        //yield return new WaitForSeconds(t);
        //LeanTweenFaderScript.instance.LoadLevel("CreditsScene");
    }

    public void PlayerDeath(){
            Debug.Log("Player Crashed");
            //AudioManager.instance.StopMusic();

            if(!menuOpened){//should only open once.
            GameplayUI.SetActive(false);
            crossHairUI.SetActive(false);
            PlayerPlane.GetComponent<PlayerPlane>().DisableControls();
            retryMenu.GetComponent<Menu>().openRetryMenu(2f);
            //StartCoroutine(AudioSequence());
            AudioManager.instance.PlayerDown();
            menuOpened = true;
            }
            //RespawnPlayer();
            //ToTitleScreen();
    }

    public void PlayerCrashedIntoGlube(){
            Debug.Log("Hart: why did I do that?");
            //AudioManager.instance.ResetMusic();
            GameplayUI.SetActive(false);
            crossHairUI.SetActive(false);
            GlubesCinemachine.SetActive(true);
            retryMenu.GetComponent<Menu>().openRetryMenu(2f);
            menuOpened = true;
            AudioManager.instance.IntoGlube();
            //AudioManager.instance.PlayerDown();
            //ToTitleScreen();
    }

    public void RespawnPlayer(){
            Debug.Log("Player Respawned");
            AudioManager.instance.PauseMusic(false);
    }

    public void GlubeWins(){
            Debug.Log("Glube Won!");
            //AudioManager.instance.ResetMusic();
            if(!playerWon){//don't run if player won

                if(!menuOpened){//should only open once.
                //PlayerPlane.GetComponent<PlayerPlane>().enabled = false;
                //PlayerPlane.GetComponent<BoxCollider>().enabled = false;
                GameplayUI.SetActive(false);
                crossHairUI.SetActive(false);
                GlubesCinemachine.SetActive(true);//activate CM Vcam to focus on glube.
                PlayerPlane.GetComponent<PlayerPlane>().DisableControls();
                AudioManager.instance.GlubeWins();
                retryMenu.GetComponent<Menu>().openRetryMenu(6.9f);
                StartCoroutine(CutsceneDelay(7f));
                //AudioManager.instance.PlayerDown();
                menuOpened = true;
                }
            }
            //ToTitleScreen();
    }

    private IEnumerator CutsceneDelay(float t){
        yield return new WaitForSeconds(t);
        VictoryCam.SetActive(true);
        AudioManager.instance.GameOVer(2f);
        //AudioManager.instance.GlubeWins();
    }

    public void ToTitleScreen(){
        if(!leaving){//only load once, not on every frame
        //AudioManager.instance.ResetMusic();
        LeanTweenFaderScript.instance.LoadLevel("MainMenuWithSceneFader");


        leaving = true;
        }
    }

}
