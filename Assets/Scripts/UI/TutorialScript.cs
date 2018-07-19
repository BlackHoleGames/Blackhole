using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TutorialScript : MonoBehaviour {

    public Text splashText;
    public string loadLevel;
    public bool PStart;
    // Use this for initialization
    IEnumerator Start()
    {
        splashText.canvasRenderer.SetAlpha(0.0f);
        PStart = false;
        for (int i = 0; i < 5; i++)
        {
            FadeInText();
            yield return new WaitForSeconds(1.5f);
            FadeOut();
            yield return new WaitForSeconds(1.5f);
        }
        if (PStart)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        //splashImage.canvasRenderer.SetAlpha(0.0f);
        //splashImage.CrossFadeAlpha(1.0f, 1.5f, false);
        //waitSeconds();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FadeInText()
    {
        splashText.CrossFadeAlpha(1.0f, 1.5f, false);
    }
    void FadeOut()
    {
        splashText.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
