using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScrollingTextScript : MonoBehaviour
{
    public float speed = 20f, skipTextDuration = 1f;
    public string levelName;

    public GameObject NotificationText;

    private bool isFinished = false, Skiping = false;

    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions.UI.Enable();

        playerInputActions.UI.Submit.performed += SkipText;
        //LeanTweenFaderScript.instance.LoadLevel(levelName);
    }

    // Update is called once per frame
    void Update()
    {

        

        Vector3 temp = transform.position;
        temp.y += 1;
        transform.position = Vector3.MoveTowards(transform.position, temp, speed * Time.deltaTime);
        if(transform.position.y > 190f){
            if(!isFinished){
            LeanTweenFaderScript.instance.LoadLevel(levelName);
            isFinished = true;
            }
        }
        //Debug.Log(temp.y);
    }

    private void SkipText(InputAction.CallbackContext context){
        if(context.performed){
            if(Skiping){
            LeanTweenFaderScript.instance.LoadLevel(levelName);
            }
            NotificationText.SetActive(true);
            Skiping = true;
            StartCoroutine(TextTimer(skipTextDuration));
        }
    }

    private IEnumerator TextTimer(float t){

        yield return new WaitForSeconds(t);
        NotificationText.SetActive(false);
        Skiping = false;

    }
}
