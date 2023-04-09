using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayControllerScript : MonoBehaviour
{

    public int PlayerHealth, GlubeHealth, RemainingBuildings;

    public GameObject BuildingParent;

    private bool leaving = false;

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
        if(GlubeHealth == 0)
        PlayerWins();

        if(RemainingBuildings == 0 && GlubeHealth > 0){//this should make player win out weigh glube's win if they happen around the same time
        GlubeWins();
        }

        if(PlayerHealth == 0){

        PlayerDeath();
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
            ToTitleScreen();
    }

    public void PlayerDeath(){
            Debug.Log("Player Crashed");
            //RespawnPlayer();
            ToTitleScreen();
    }

    public void PlayerCrashedIntoGlube(){
            Debug.Log("Hart: why did I do that?");
            ToTitleScreen();
    }

    public void RespawnPlayer(){
            Debug.Log("Player Respawned");
    }

    public void GlubeWins(){
            Debug.Log("Glube Won!");
            ToTitleScreen();
    }

    public void ToTitleScreen(){
        if(!leaving){//only load once, not on every frame
        LeanTweenFaderScript.instance.LoadLevel("MainMenuWithSceneFader");
        leaving = true;
        }
    }

}
