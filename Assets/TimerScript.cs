using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour {

    public void TimeLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchron(sceneIndex));
        //AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
//        operation.progress
    }
    IEnumerator LoadAsynchron(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
