using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour {

    public Text timerText;
    public Slider timeLine;
    public float startTime;
    public void TimeLevel(int sceneIndex)
    {
        startTime = Time.time;
    }

    void Update()
    {
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString();

        timerText.text = minutes + ":" + seconds;
        timeLine.value = timeLine.value + (t % 60) * 0.00001f;
        if (timeLine.value == 1) {
            SceneManager.LoadScene(0);
            startTime = Time.time;
        }
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
