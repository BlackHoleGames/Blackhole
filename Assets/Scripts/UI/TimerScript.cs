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
    public bool gameover = false;
    public Text GameOverText;
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

            timerText.text = minutes + ":" + seconds;
            timeLine.value = (t % 60) / 30;//timeLine.value + (t % 60) * 0.00001f;       
            gameover = false;     
        }
        if (timeLine.value == 1.0f) {
            GameOver();

            float t2 = Time.time - endTime;
            float seconds2 = (t2 % 60);
            if (seconds2 > 3)
            {
                restartMap();
                startTime = Time.time;
                //SceneManager.UnloadScene(1);
                SceneManager.LoadScene(0);
                //                Application.LoadLevel(Application.loadedLevel);
            }

        }

    }
    public void GameOver()
    {
        if (!gameover)
        {
            endTime = Time.time;
            gameover = true;
        }
        GameOverText.gameObject.SetActive(true);
    }
        //StartCoroutine(LoadAsynchron(sceneIndex));
        //AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
//        operation.progress
//    }
//    IEnumerator LoadAsynchron(int sceneIndex)
//    {
//        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
//        while (!operation.isDone)
//        {
//            yield return null;
//        }
//    }
}
