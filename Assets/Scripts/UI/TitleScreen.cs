using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TitleScreen : MonoBehaviour {

    public Image splashImage;
    public Text splashText;
    public string loadLevel;
    public bool PStart;
    public bool isLeaderBoardTime = true;
    // Use this for initialization


    IEnumerator Start() {
        //        if(GameObject.FindGameObjectWithTag("UIScore").GetComponent<SwitchablePlayerController>().speedOn==null)
        //if (GameObject.FindGameObjectWithTag("UIScore").GetComponent<LeaderBoardScript>() != null)
        //{
        //    isLeaderBoardTime = GameObject.FindGameObjectWithTag("UIScore").GetComponent<LeaderBoardScript>().isLeaderBoardTime;
        //}else
        //{
        //    isLeaderBoardTime = true;
        //}
        splashImage.canvasRenderer.SetAlpha(0.0f);
        splashText.canvasRenderer.SetAlpha(0.0f);
        FadeInImage();
        for (int i = 0; i < 5; i++) {
            FadeInText();
            yield return new WaitForSeconds(1.5f);
            FadeOut();
            yield return new WaitForSeconds(1.5f);
        }
        //Score
        if (SaveGameStatsScript.GameStats.StatusUISequence) { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
            SaveGameStatsScript.GameStats.StatusUISequence = false;
            SaveGameStatsScript.GameStats.SetStats();
        }
        else//Video Tutorial
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SaveGameStatsScript.GameStats.StatusUISequence = true;
            SaveGameStatsScript.GameStats.SetStats();
        }
        
        
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
