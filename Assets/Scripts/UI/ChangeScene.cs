using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    private bool changeScene = false;
    private IEnumerator SceneTimerSequence;
    public Image BlackFade;
    private bool activate =false;
    
    void Start()
    {
        BlackFade.canvasRenderer.SetAlpha(0.0f);
        changeScene = false;
        activate = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDeath;
    }
    // Update is called once per frame
    void Update () {

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>() != null &&
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDeath && !changeScene)
        {
            changeScene = true;
            SceneTimerSequence = FadeToLevel(2.0f);
            StartCoroutine(SceneTimerSequence);
        }
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>()!= null &&
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isFinished && !changeScene)
        {
            changeScene = true;
            SceneTimerSequence = FadeToLevel(2.0f);
            StartCoroutine(SceneTimerSequence);
        }
    }
    IEnumerator FadeToLevel (float levelTimer)
    {
        BlackFade.CrossFadeAlpha(1.0f, 2.0f, false);
        //BlackFade.canvasRenderer.SetAlpha(1.0f);
        yield return new WaitForSeconds(levelTimer);
        SceneManager.LoadScene(6);
    }
}
