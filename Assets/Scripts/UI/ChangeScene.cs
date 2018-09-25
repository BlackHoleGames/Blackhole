using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    private bool changeScene = false;
    private IEnumerator SceneCloseTimerSequence;
    private IEnumerator SceneOpenTimerSequence;
    private IEnumerator SceneBHSequence;
    public Image BlackFade;
    public bool shutdown = false;
    private bool activateDeath =false;
    private bool activateBH = false;
    public float SecondsInBH = 2.0f;
    public float SecondsOutBH = 2.0f;
    void Start()
    {
        Cursor.visible = false;
        BlackFade.canvasRenderer.SetAlpha(1.0f);
        SceneOpenTimerSequence = FadeToInitLevel(2.0f);
        StartCoroutine(SceneOpenTimerSequence);
        changeScene = false;
        activateDeath = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDeath;
    }
    // Update is called once per frame
    void Update () {
        if(shutdown)
        {
            changeScene = true;
            SceneCloseTimerSequence = FadeToEndingLevel(3.0f);
            StartCoroutine(SceneCloseTimerSequence);
        }
        //else if (GameObject.FindGameObjectWithTag("Player").GetComponent<BlackHoleTransition>()!=null
        //    && GameObject.FindGameObjectWithTag("Player").GetComponent<BlackHoleTransition>().startCount
        //    && !activateBH)
        //{
        //    SceneBHSequence = FadeToInitBH();
        //    StartCoroutine(SceneBHSequence);
        //}
    }
    //public void Shutdown()
    //{
    //    changeScene = true;
    //    SceneCloseTimerSequence = FadeToEndingLevel(3.0f);
    //    StartCoroutine(SceneCloseTimerSequence);
    //}

    public void StartBlackHoleSequence() {
        SceneBHSequence = FadeToInitBH();
        StartCoroutine(SceneBHSequence);
    }

    IEnumerator FadeToEndingLevel (float levelTimer)
    {
        BlackFade.CrossFadeAlpha(1.0f, 2.0f, false);
        //BlackFade.canvasRenderer.SetAlpha(1.0f);
        yield return new WaitForSeconds(levelTimer);
        if (GameObject.FindGameObjectWithTag("Player") != null &&
            !GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isEnding)
        {
            SceneManager.LoadScene(6);
        }else
        {
            SceneManager.LoadScene(7);
        }
    }
    IEnumerator FadeToInitLevel(float levelTimer)
    {
        BlackFade.CrossFadeAlpha(0.0f, 2.0f, false);
        //BlackFade.canvasRenderer.SetAlpha(1.0f);
        yield return new WaitForSeconds(levelTimer);
    }
    IEnumerator FadeToInitBH()
    {
        BlackFade.CrossFadeAlpha(1.0f, SecondsInBH, false);
        yield return new WaitForSeconds(SecondsInBH);
        BlackFade.CrossFadeAlpha(0.0f, SecondsOutBH, false);
        yield return new WaitForSeconds(SecondsOutBH);
    }

}
