using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour {

    public Text timerText;

    public Slider timeLine;
    public float startTime;
    public float endTime;
    public bool restart = false;
    public static bool gameover = false;
    public Text GameOverText;
    public static bool isGameOver;
    public void TimeLevel(int sceneIndex)
    {
        startTime = Time.time;
        timeLine.value = 0.0f;
        GameOverText.gameObject.SetActive(false);
    }
    void restartMap()
    {
        startTime = Time.time;
        timeLine.value = 0.0f;
        GameOverText.gameObject.SetActive(false);
    }
    void Update()
    {
        if (timeLine.value < 1.0f)
        {
            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString();

            //timerText.text = minutes + ":" + seconds;
            timeLine.value = (t % 60) / 59;//timeLine.value + (t % 60) * 0.00001f;       
            //gameover = false;     
        }
        if (gameover) {
            //GameOverText.gameObject.SetActive(true);
            FinishGame();
            //startTime = Time.time;
            //Comented to Test GameOver();

            //float t2 = Time.time - endTime;
            //float seconds2 = (t2 % 60);
            //Comented for testing if (seconds2 > 3)
            //Comented for testing {
            //Comented for testing     restartMap();
            //Comented for testing     timeLine.value = 0.00001f;
            //Comented for testing     startTime = Time.time;
            //Comented for testing     SceneManager.LoadScene(0);
            //Comented for testing     //SceneManager.LoadScene(1);    
            //Comented for testing 
            //Comented for testing }

        }

    }
    public void FinishGame()
    {
        gameover = false;
        isGameOver = false;
        //System.Threading.Thread.Sleep(2000);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
