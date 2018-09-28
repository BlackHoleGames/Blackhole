using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TitleScreen : MonoBehaviour {

    public Text TitleText;
    public Text PressStartText;
    public Image Curtain;
    public string loadLevel;
    public bool PStart;
    public bool isLeaderBoardTime = true;
    // Use this for initialization


    IEnumerator Start() {

        TitleText.canvasRenderer.SetAlpha(0.0f);
        PressStartText.canvasRenderer.SetAlpha(0.0f);
        Curtain.canvasRenderer.SetAlpha(1.0f);
        Curtain.CrossFadeAlpha(0.0f, 3.0f, false);
        yield return new WaitForSeconds(3.0f);
        
        FadeInImage();
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < 5; i++) {
            FadeInText();
            yield return new WaitForSeconds(0.75f);
            FadeOut();
            yield return new WaitForSeconds(0.75f);
        }
        SaveGameStatsScript.GameStats.isGameOver = false;
        //Score
        //SceneManager.LoadScene(7);
        if (SaveGameStatsScript.GameStats.StatusUISequence) { 
            SceneManager.LoadScene(7);
            SaveGameStatsScript.GameStats.StatusUISequence = false;
            SaveGameStatsScript.GameStats.SetStats();
        }
        else//Video Tutorial
        {
            SceneManager.LoadScene(1);
            SaveGameStatsScript.GameStats.StatusUISequence = true;
            SaveGameStatsScript.GameStats.SetStats();
        }


    }

    void FadeInText()
    {
        PressStartText.CrossFadeAlpha(1.0f, 0.75f, false);
    }
    void FadeInImage()
    {
        TitleText.CrossFadeAlpha(1.0f, 1.0f, false);      
    }
    void FadeOut()
    {
        PressStartText.CrossFadeAlpha(0.0f, 0.75f, false);
    }
}
