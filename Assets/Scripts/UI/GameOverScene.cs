using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour {

    public Text splashText;
    public Image splashImage;
    public string loadLevel;
    public int sceneSteps;
    // Use this for initialization
    IEnumerator Start()
    {
        Time.timeScale = 1.0f;
        splashText.canvasRenderer.SetAlpha(0.0f);
        splashImage.canvasRenderer.SetAlpha(1.0f);
        FadeIn();
        yield return new WaitForSeconds(3.0f);
        FadeOut();
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneSteps);


    }
    void FadeIn()
    {
        splashImage.CrossFadeAlpha(0.0f, 1.5f, false);
        splashText.CrossFadeAlpha(1.0f, 1.5f, false);
    }
    void FadeOut()
    {
        splashImage.color = Color.white; //CrossFadeAlpha(0.0f, 1.5f, false);
        splashImage.CrossFadeAlpha(1.0f, 2.5f, false);
        splashText.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
