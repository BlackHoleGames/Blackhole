using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class Briefing : MonoBehaviour {
    public Text briefingText;
    public Image curtainImage;
    public Image TutorialImage;
    public Text PressStartText;
    private bool activatePressStart, blockCoroutineText , blockCoroutineFadeOut, Confirmed = false;
    private IEnumerator ConfirmSequence, InitSequence, ShutDownSequence;

    // Use this for initialization
    void Start () {
        TutorialImage.canvasRenderer.SetAlpha(0.0f);
        curtainImage.canvasRenderer.SetAlpha(1.0f);
        curtainImage.CrossFadeAlpha(0.0f, 1.0f, false);
        PressStartText.canvasRenderer.SetAlpha(0.0f);
        InitSequence = InitScreen();
        StartCoroutine(InitSequence);
    }
    void Update()
    {
        if (activatePressStart && !blockCoroutineText)
        {
            blockCoroutineText = true;
            ConfirmSequence = PressStartTextUI();
            StartCoroutine(ConfirmSequence);
        }else if (activatePressStart && Input.GetKeyDown("enter") ||
            Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Start") ||
            Input.GetKeyDown(KeyCode.Escape) && !blockCoroutineFadeOut)
        {
            blockCoroutineFadeOut = true;
            Confirmed = true;
            ShutDownSequence = ShutDownScreen();
            StartCoroutine(ShutDownSequence);
        }
    }
    void FadeIn()
    {
        TutorialImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }
    void FadeOut()
    {
        TutorialImage.CrossFadeAlpha(0.0f, 1.5f, false);
        
    }
    IEnumerator InitScreen()
    {
        yield return new WaitForSeconds(1.0f);
        FadeIn();
        //yield return new WaitForSeconds(17.5f);
        yield return new WaitForSeconds(2.0f);
        activatePressStart = true;

    }
    IEnumerator ShutDownScreen()
    {
        FadeOut();
        curtainImage.CrossFadeAlpha(0.0f, 1.0f, false);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(4);
    }
    IEnumerator PressStartTextUI()
    {
        while (!Confirmed) { 
            PressStartText.CrossFadeAlpha(1.0f, 0.75f, false);
            yield return new WaitForSeconds(0.75f);
            PressStartText.CrossFadeAlpha(0.0f, 0.75f, false);
            yield return new WaitForSeconds(0.75f);
        }
    }
}
