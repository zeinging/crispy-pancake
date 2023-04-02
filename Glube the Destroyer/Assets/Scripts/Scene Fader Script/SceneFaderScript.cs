using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFaderScript : MonoBehaviour {

	public static SceneFaderScript instance;

	[SerializeField]
	private GameObject Panel;

    [SerializeField]
    private Slider loadbar;

    [SerializeField]
    private Animator anim;

    //[HideInInspector]
    //public Player Player;

    public AudioSource TransitionSund;


	// Use this for initialization
	void Awake () {
		MakeSigleton ();
        //Player = GetComponentInChildren<Player>();
        //LoadLevel("SpacePlaneTestScene");
    }
	

	void MakeSigleton(){
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	public void LoadLevel(string level){
		StartCoroutine (FadeInOut (level));
	}

    //public void SaveGame() {

       //SaveGameSystem.NewMarySue(Player);

    //}

    
    IEnumerator PlaySoundEffect()
    {
        if (TransitionSund != null)
        {
            TransitionSund.Play();
            // yield return StartCoroutine(MyCoroutineScript.WaitForRealSeconds(.3f));
            yield return new WaitForSeconds(0.6f);
            TransitionSund.Play();
        }
    }


    IEnumerator FadeInOut(string level) {
        Panel.SetActive(true);

        if (anim != null)
            anim.Play("FadeIn");

        if (GetComponentInChildren<leanTweenFader>() != null)
            GetComponentInChildren<leanTweenFader>().FadingLeanTween();

        StartCoroutine(PlaySoundEffect());


        yield return StartCoroutine(MyCoroutineScript.WaitForRealSeconds(1f));
        //SceneManager.LoadScene (level);
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        loadbar.gameObject.SetActive(true);
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadbar.value = progress;
            yield return null;
        }


        string temp = SceneManager.GetActiveScene().name;
        //GetComponentInChildren<Player>().SceneName = temp;//store the scene name is the player script
        


        loadbar.gameObject.SetActive(false);
        //GameplayController.instance.DialogueBool = true;// freezes the player durring fader animation
        yield return StartCoroutine (MyCoroutineScript.WaitForRealSeconds(1f));//this is used to give more time to load a scene.

        if(anim != null)
        anim.Play("FadeOut");

        if(GetComponentInChildren<leanTweenFader>() != null)
        GetComponentInChildren<leanTweenFader>().ResetSliders();

        StartCoroutine(PlaySoundEffect());
        //GameplayController.instance.TouchedGUIButn = true;

        
        
        yield return StartCoroutine(MyCoroutineScript.WaitForRealSeconds(1.2f));
        
        // GameplayController.instance.TouchedGUIButn = false; used to freeze player until scenefader can be used again
        Panel.SetActive(false);
        //GameplayController.instance.DialogueBool = false;// this unfreezes the player
        //print("moving again");





    }

   





}//SceneFaderScript
