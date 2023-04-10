using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayControllerScript : MonoBehaviour
{

    public int PlayerHealth, GlubeHealth, RemainingBuildings, lostPlanes;

    public GameObject BuildingParent, retryMenu, PlayerPlane;

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
        if(GlubeHealth < 1)
        PlayerWins();

        if(RemainingBuildings == 0 && GlubeHealth > 0){//this should make player win out weigh glube's win if they happen around the same time
        GlubeWins();
        }

        if(PlayerHealth < 1){

        //PlayerDeath();
        }
    }

    public void PlayerTakeDamage(int damage){
        PlayerHealth -= damage;
    }

    public void GlubeTakeDamage(int damage){
        GlubeHealth -= damage;
    }

    public void ABuildingDestroyed(){
        RemainingBuildings--;
    }

    public void PlayerWins(){
            Debug.Log("Player Won!");
            playerWon = true;
            //AudioManager.instance.StopMusic();
            AudioManager.instance.GlubeDefeatIntro();
            PlayerPlane.GetComponent<PlayerPlane>().DisableControls();
            PlayerPlane.GetComponent<PlayerPlane>().enabled = false;
            //AudioManager.instance.GlubeDefeatLoop();
            //ToTitleScreen();
            if(!leaving){
                //LeanTweenFaderScript.instance.LoadLevel("CreditsScene");
                StartCoroutine(WinCutscene(15f));
                leaving = true;
            }
    }
    private IEnumerator WinCutscene(float t){
        yield return new WaitForSeconds(t);
        LeanTweenFaderScript.instance.LoadLevel("CreditsScene");
    }

    public void PlayerDeath(){
            Debug.Log("Player Crashed");
            //AudioManager.instance.StopMusic();

            if(!menuOpened){//should only open once.
            PlayerPlane.GetComponent<PlayerPlane>().DisableControls();
            retryMenu.GetComponent<Menu>().openRetryMenu();
            AudioManager.instance.PlayerDown();
            menuOpened = true;
            }
            //RespawnPlayer();
            //ToTitleScreen();
    }

    public void PlayerCrashedIntoGlube(){
            Debug.Log("Hart: why did I do that?");
            //AudioManager.instance.ResetMusic();
            retryMenu.GetComponent<Menu>().openRetryMenu();
            AudioManager.instance.PlayerDown();
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

                PlayerPlane.GetComponent<PlayerPlane>().DisableControls();
                retryMenu.GetComponent<Menu>().openRetryMenu();
                AudioManager.instance.PlayerDown();
                menuOpened = true;
                }
            }
            //ToTitleScreen();
    }

    public void ToTitleScreen(){
        if(!leaving){//only load once, not on every frame
        //AudioManager.instance.ResetMusic();
        LeanTweenFaderScript.instance.LoadLevel("MainMenuWithSceneFader");


        leaving = true;
        }
    }

}
