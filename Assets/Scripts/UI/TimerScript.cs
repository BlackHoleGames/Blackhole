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

            timeLine.value = (t % 60) / 59;   

        }
        if (gameover) {
            FinishGame();

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
