using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TitleScreen : MonoBehaviour {

    public Image splashImage;
    public Text splashText;
    public string loadLevel;
    public bool PStart;
    // Use this for initialization
    IEnumerator Start () {
        splashImage.canvasRenderer.SetAlpha(0.0f);
        splashText.canvasRenderer.SetAlpha(0.0f);
        FadeInImage();
//        PStart = false;
        for(int i = 0; i < 5; i++) {
//            if (PStart) break;
            FadeInText();
            yield return new WaitForSeconds(1.5f);
//            if (PStart) break;
            FadeOut();
            yield return new WaitForSeconds(1.5f);
        }
        //if (PStart)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        //}else
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //}
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    void Update () {
        //if (Input.GetKeyDown("enter"))
        //{
        //    PStart = true;
        //}
    }

    void FadeInText()
    {
        splashText.CrossFadeAlpha(1.0f, 1.5f, false);
    }
    void FadeInImage()
    {
        splashImage.CrossFadeAlpha(1.0f, 1.5f, false);      
    }
    void FadeOut()
    {
        splashText.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
