using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    private bool changeScene = false;
    private IEnumerator SceneCloseTimerSequence;
    private IEnumerator SceneOpenTimerSequence;
    public Image BlackFade;
    private bool activate =false;
    
    void Start()
    {
        BlackFade.canvasRenderer.SetAlpha(1.0f);
        SceneOpenTimerSequence = FadeToInitLevel(2.0f);
        StartCoroutine(SceneOpenTimerSequence);
        changeScene = false;
        activate = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDeath;
    }
    // Update is called once per frame
    void Update () {

        if (GameObject.FindGameObjectWithTag("Player") != null &&
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDeath && !changeScene)
        {
            changeScene = true;
            SceneCloseTimerSequence = FadeToEndingLevel(2.0f);
            StartCoroutine(SceneCloseTimerSequence);
        }
        if (GameObject.FindGameObjectWithTag("Player")!= null &&
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isFinished && !changeScene)
        {
            changeScene = true;
            SceneCloseTimerSequence = FadeToEndingLevel(2.0f);
            StartCoroutine(SceneCloseTimerSequence);
        }
    }
    IEnumerator FadeToEndingLevel (float levelTimer)
    {
        BlackFade.CrossFadeAlpha(1.0f, 2.0f, false);
        //BlackFade.canvasRenderer.SetAlpha(1.0f);
        yield return new WaitForSeconds(levelTimer);
        SceneManager.LoadScene(6);
    }
    IEnumerator FadeToInitLevel(float levelTimer)
    {
        BlackFade.CrossFadeAlpha(0.0f, 2.0f, false);
        //BlackFade.canvasRenderer.SetAlpha(1.0f);
        yield return new WaitForSeconds(levelTimer);
    }
}
