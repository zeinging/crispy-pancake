using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, optionsMenu;

    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    public GameObject mainFirstButton, CreditsBack, optionsBack, creditsButton, CreditsText, optionsButton;
    public string levelName;

    // Start is called before the first frame update
    //if ((Keyboard.current.tKey.wasPressedThisFrame) || (Gamepad.current.startButton.wasPressedThisFrame))
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        //playerInputActions.UI.Enable();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainFirstButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelStart() {
        //SceneManager.LoadScene(levelName);
        playerInputActions.UI.Disable();
        EventSystem.current.gameObject.SetActive(false);//deactivate event system to prevent inputs while scene fading
        LeanTweenFaderScript.instance.LoadLevel(levelName);
    }

    public void Credits(){
        if(!CreditsText.activeInHierarchy){
            CreditsText.SetActive(true);
            mainMenu.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(CreditsBack);
        }else{
            CreditsText.SetActive(false);
            mainMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(creditsButton);
        }
    }

    public void Options(){
        if(!optionsMenu.activeInHierarchy){
            optionsMenu.SetActive(true);
            mainMenu.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionsBack);
        }else{
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionsButton);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
