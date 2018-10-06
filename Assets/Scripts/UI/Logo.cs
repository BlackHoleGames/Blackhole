using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Logo : MonoBehaviour {

    public Image LogoImage;
    public Image CurtainImage;
    public string loadLevel;

	// Use this for initialization
	IEnumerator Start () {
        Cursor.visible = false;
        LogoImage.canvasRenderer.SetAlpha(0.0f);
        CurtainImage.canvasRenderer.SetAlpha(0.0f);
        yield return new WaitForSeconds(1.0f);
        FadeIn();
        yield return new WaitForSeconds(3.0f);
        FadeOut();
        yield return new WaitForSeconds(2.0f);
        FadeInCurtain();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
    void FadeIn()
    {
        LogoImage.CrossFadeAlpha(1.0f, 1.0f, false);
    }
    void FadeOut()
    {
        LogoImage.CrossFadeAlpha(0.0f, 2.0f, false);
    }
    void FadeInCurtain()
    {
        CurtainImage.CrossFadeAlpha(1.0f, 1.0f, false);
    }
}
