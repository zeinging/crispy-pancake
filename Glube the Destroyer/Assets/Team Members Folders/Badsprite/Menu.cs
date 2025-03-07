using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public GameObject PauseMenu, retryMenu;

    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    public GameObject pauseFirstButton, retryFirstButton;
    // Start is called before the first frame update
    //if ((Keyboard.current.tKey.wasPressedThisFrame) || (Gamepad.current.startButton.wasPressedThisFrame))
    void Start()
    {
        PauseMenu.SetActive(false);
        playerInputActions = GameplayControllerScript.instance.playerInputActions;
        playerInput = GameplayControllerScript.instance.playerInput;
        playerInputActions.UI.Pause.Enable();//should only make the pause action active

        playerInputActions.UI.Pause.performed += PauseUnpause;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy(){
        playerInputActions.UI.Disable();
    }

    public void PauseUnpause(InputAction.CallbackContext context) {
        if (context.performed)
        {
            if (!PauseMenu.activeInHierarchy)
            {
                AudioManager.instance.PauseMusic(true);
                PauseMenu.SetActive(true);
                playerInputActions.Player.Disable();
                playerInputActions.UI.Enable();
                Time.timeScale = 0f;

                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);
            }
            else
            {
                AudioManager.instance.PauseMusic(false);
                PauseMenu.SetActive(false);
                playerInputActions.Player.Enable();
                playerInputActions.UI.Disable();
                playerInputActions.UI.Pause.Enable();//reEnable pause action
                Time.timeScale = 1f;
            }
        }
    }

    public void Resume() {
        AudioManager.instance.PauseMusic(false);
        PauseMenu.SetActive(false);
        playerInputActions.Player.Enable();
        playerInputActions.UI.Disable();
        playerInputActions.UI.Pause.Enable();//reEnable pause action
        Time.timeScale = 1f;
    }

    public void MainMenu() {
        //Time.timeScale = 1f;//scene fader should reset time scale to 1
        //SceneManager.LoadScene("MainMenu");
        LeanTweenFaderScript.instance.LoadLevel("MainMenuWithSceneFader");
    }

    public void Retry(){
        LeanTweenFaderScript.instance.ReloadCurrentScene();
    }

    public void openRetryMenu(float t){
        playerInputActions.UI.Pause.Disable();//prevent pausing in the retry menu.
        StartCoroutine(RetryMenuDelay(t));

    }

    private IEnumerator RetryMenuDelay(float t){

        playerInputActions.UI.Pause.Disable();//disable pause button when retry menu appears
        yield return new WaitForSeconds(t);
        retryMenu.SetActive(true);
        playerInputActions.UI.Enable();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(retryFirstButton);

    }
}
