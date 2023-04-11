using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Slider GlubeSlider, PlayerSlider;

    public GameObject GlubeFill, PlayerFill;

    public TMPro.TMP_Text BuildingCounter;

    // Start is called before the first frame update
    void Start()
    {
        GlubeSlider.maxValue = GameplayControllerScript.instance.GlubeHealth;
        PlayerSlider.maxValue = GameplayControllerScript.instance.PlayerHealth;
        BuildingCounter.text = GameplayControllerScript.instance.RemainingBuildings.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(GlubeSlider.value != GameplayControllerScript.instance.GlubeHealth)
        GlubeSlider.value = GameplayControllerScript.instance.GlubeHealth;

        if(PlayerSlider.value != GameplayControllerScript.instance.PlayerHealth)
        PlayerSlider.value = GameplayControllerScript.instance.PlayerHealth;

        if(GlubeSlider.value == 0)
        GlubeFill.SetActive(false);

        if(PlayerSlider.value == 0)
        PlayerFill.SetActive(false);

        if(int.Parse(BuildingCounter.text) != GameplayControllerScript.instance.RemainingBuildings){
            BuildingCounter.text = GameplayControllerScript.instance.RemainingBuildings.ToString();
        }


    }
}
