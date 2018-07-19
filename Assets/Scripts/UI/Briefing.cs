using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class Briefing : MonoBehaviour {
    public Text splashText;
    public bool PStart;
    // Use this for initialization
    IEnumerator Start () {
        splashText.canvasRenderer.SetAlpha(0.0f);
        FadeIn();
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void FadeIn()
    {
        splashText.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("enter"))
        {
            PStart = true;
        }
    }
}
