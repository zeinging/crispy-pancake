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
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions.UI.Enable();

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
                Time.timeScale = 0f;

                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);
            }
            else
            {
                AudioManager.instance.PauseMusic(false);
                PauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void Resume() {
        AudioManager.instance.PauseMusic(false);
        PauseMenu.SetActive(false);
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
        
        StartCoroutine(RetryMenuDelay(t));

    }

    private IEnumerator RetryMenuDelay(float t){

        playerInputActions.UI.Pause.Disable();//disable pause button when retry menu appears
        yield return new WaitForSeconds(t);
        retryMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(retryFirstButton);

    }
}
