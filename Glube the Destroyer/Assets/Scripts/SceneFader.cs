using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveToScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator MoveToScene(){


        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);

    }


}
