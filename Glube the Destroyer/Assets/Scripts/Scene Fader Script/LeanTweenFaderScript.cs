using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeanTweenFaderScript : MonoBehaviour {

	public static LeanTweenFaderScript instance;

	[SerializeField]
	private GameObject Panel;

    [SerializeField]
    private Slider loadbar;

    [SerializeField]
    private GameObject TopSlide, BottomSlide;
    //private Animator anim;

    public float Duration = 0.3f, staySeconds = 3f;

    public string sceneLevel;

    //[HideInInspector]
    //public Player Player;

    public Image LoadingImage;

    public AudioSource TransitionSund;

    public bool AutoLoad = false;


	// Use this for initialization
	void Awake () {
		MakeSigleton ();
        //Player = GetComponentInChildren<Player>();
        if(AutoLoad)
        StartCoroutine(WaitTest(staySeconds));
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

    public IEnumerator WaitTest(float t){

        yield return new WaitForSeconds(t);
        LoadLevel(sceneLevel);

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
        //anim.Play("FadeIn");
        //Vector3 topDefault = TopSlide.transform.position;
        //Vector3 botDefault = BottomSlide.transform.position;
        //LeanTween.moveLocal(TopSlide, new Vector3(0, 0, 0), Duration);
        //LeanTween.moveLocal(BottomSlide, new Vector3(0, 0, 0), Duration);

        Color temp = Panel.GetComponent<Image>().color;
        //Mathf.Clamp01(temp.a);
        while(temp.a != 1){

        temp.a = Mathf.MoveTowards(temp.a, 1, Duration * 0.02f);
        
        //temp.a += Time.deltaTime;
        //Debug.Log(temp);
        Panel.GetComponent<Image>().color = temp;
        yield return null;
        }
        LoadingImage.gameObject.SetActive(true);
        
        Time.timeScale = 0f;
        //temp.a = 1;
        //Panel.GetComponent<Image>().color = temp;

        StartCoroutine(PlaySoundEffect());


        yield return StartCoroutine(MyCoroutineScript.WaitForRealSeconds(1f));
        //SceneManager.LoadScene (level);
         AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        // loadbar.gameObject.SetActive(true);
        // while (!operation.isDone) {
        //     float progress = Mathf.Clamp01(operation.progress / .9f);
        //     loadbar.value = progress;
        //     yield return null;
        // }


        //string temp = SceneManager.GetActiveScene().name;
        //GetComponentInChildren<Player>().SceneName = temp;//store the scene name is the player script
        


        //loadbar.gameObject.SetActive(false);
        //GameplayController.instance.DialogueBool = true;// freezes the player durring fader animation
        //LoadingImage.gameObject.SetActive(false);
        yield return StartCoroutine (MyCoroutineScript.WaitForRealSeconds(1f));//this is used to give more time to load a scene.
        //anim.Play("FadeOut");
        //LeanTween.moveLocal(TopSlide, new Vector3(0, 320, 0), Duration);
        //LeanTween.moveLocal(BottomSlide, new Vector3(0, -320, 0), Duration);
        
        //Time.timeScale = 1f;
        

        while(temp.a != 0){

        temp.a = Mathf.MoveTowards(temp.a, 0, Duration * 0.02f);
        //temp.a -= Time.deltaTime;
        //Debug.Log(temp);
        Panel.GetComponent<Image>().color = temp;

        if(temp.a < 0.5){
            Time.timeScale = 1f;
            LoadingImage.gameObject.SetActive(false);
        }
        //Mathf.Clamp01(temp.a);
        yield return null;
        }
        //LoadingImage.gameObject.SetActive(false);
        //temp.a = 0;
        //Panel.GetComponent<Image>().color = temp;

        StartCoroutine(PlaySoundEffect());
        //GameplayController.instance.TouchedGUIButn = true;

        //Time.timeScale = 1f;
        
        yield return StartCoroutine(MyCoroutineScript.WaitForRealSeconds(1.2f));
        
        // GameplayController.instance.TouchedGUIButn = false; used to freeze player until scenefader can be used again
        Panel.SetActive(false);
        //GameplayController.instance.DialogueBool = false;// this unfreezes the player
        //print("moving again");





    }

   





}//SceneFaderScript
