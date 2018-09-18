using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class Briefing : MonoBehaviour {
    public Text briefingText;
    public Image curtainImage;
    public Image TutorialImage;
    // Use this for initialization
    IEnumerator Start () {
        TutorialImage.canvasRenderer.SetAlpha(0.0f);
        curtainImage.canvasRenderer.SetAlpha(1.0f);
        curtainImage.CrossFadeAlpha(0.0f, 1.0f, false);
        yield return new WaitForSeconds(1.0f);
        FadeIn();
        yield return new WaitForSeconds(17.5f);
        FadeOut();
        curtainImage.CrossFadeAlpha(0.0f, 1.0f, false);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void FadeIn()
    {
        TutorialImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }
    void FadeOut()
    {
        TutorialImage.CrossFadeAlpha(0.0f, 1.5f, false);
        
    }
}
