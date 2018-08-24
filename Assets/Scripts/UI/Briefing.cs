using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class Briefing : MonoBehaviour {
    public Text briefingText;
    public Image curtainImage;
    // Use this for initialization
    IEnumerator Start () {
        briefingText.canvasRenderer.SetAlpha(0.0f);
        curtainImage.canvasRenderer.SetAlpha(0.0f);
        FadeIn();
        yield return new WaitForSeconds(3.0f);
        FadeOut();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void FadeIn()
    {
        briefingText.CrossFadeAlpha(1.0f, 1.5f, false);
    }
    void FadeOut()
    {
        briefingText.CrossFadeAlpha(0.0f, 1.5f, false);
        curtainImage.CrossFadeAlpha(1.0f, 2.5f, false);
    }
}
