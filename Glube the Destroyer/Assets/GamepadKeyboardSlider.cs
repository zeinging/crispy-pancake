using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamepadKeyboardSlider : MonoBehaviour
{


    public Slider GamepadKeyboard;

    public CanvasGroup RightResButtons, LeftResButton;

    // Start is called before the first frame update
    void Start()
    {
        ChangeActiveButton();
    }

    public void ChangeActiveButton(){
        if(GamepadKeyboard.value == 0){
            LeftResButton.alpha = 0.5f;
            LeftResButton.interactable = false;
        }else{
            LeftResButton.alpha = 1f;
            LeftResButton.interactable = true;
        }

        if(GamepadKeyboard.value == GamepadKeyboard.maxValue){
            RightResButtons.alpha = 0.5f;
            RightResButtons.interactable = false;
        }else{
            RightResButtons.alpha = 1f;
            RightResButtons.interactable = true;
        }
    }

    public void MouseButtonChangeRes(bool isRight){
        if(isRight){
            GamepadKeyboard.value++;
        }else{
            GamepadKeyboard.value--;
        }
    }



}
