using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour {
    public AudioClip soundPlay;
    public Text TitleText_1;
    public Text TitleText_2;
    public Text TitleText_3;
    public Text TitleText_4;
    public Text Credits;
    public Text DedicatedTo;
    public Text TheEnd;
    public Text ThanksForPlaying;
    public Image Curtain;
    public GameObject Cam1BH;
    public GameObject Cam1GO;
    public string loadLevel;
    public bool startCredits;
    public bool isLeaderBoardTime = true;
    // Use this for initialization
    private bool shutdown = false;
    private int countTimer;

    private AudioSource sourcePlay { get { return GetComponent<AudioSource>(); } }
    IEnumerator Start()
    {
        Time.timeScale = 1.0f;
        countTimer = 0;
        StartCoroutine(DedicatedGame());
        TitleText_1.canvasRenderer.SetAlpha(0.0f);
        TitleText_2.canvasRenderer.SetAlpha(0.0f);
        TitleText_3.canvasRenderer.SetAlpha(0.0f);
        TitleText_4.canvasRenderer.SetAlpha(0.0f);
        DedicatedTo.canvasRenderer.SetAlpha(0.0f);
        TheEnd.canvasRenderer.SetAlpha(0.0f);
        ThanksForPlaying.canvasRenderer.SetAlpha(0.0f);
        Curtain.canvasRenderer.SetAlpha(1.0f);
        Curtain.CrossFadeAlpha(0.0f, 3.0f, false);
        yield return new WaitForSeconds(1.0f);
        sourcePlay.PlayOneShot(soundPlay);
        yield return new WaitForSeconds(2.0f);
        TitleText_1.CrossFadeAlpha(1.0f, 2.0f, false);
        yield return new WaitForSeconds(6.0f);
        TitleText_2.CrossFadeAlpha(1.0f, 2.0f, false);
        yield return new WaitForSeconds(6.0f);
        TitleText_3.CrossFadeAlpha(1.0f, 2.0f, false);
        yield return new WaitForSeconds(2.0f);
        TitleText_4.CrossFadeAlpha(1.0f, 2.0f, false);
        yield return new WaitForSeconds(6.0f);
        TitleText_1.CrossFadeAlpha(0.0f, 1.0f, false);
        TitleText_2.CrossFadeAlpha(0.0f, 1.0f, false);
        TitleText_3.CrossFadeAlpha(0.0f, 1.0f, false);
        TitleText_4.CrossFadeAlpha(0.0f, 1.0f, false);
        startCredits = true;

    }
    void Update()
    {
        //Acabar en 4400
        if (startCredits) Credits.transform.position += new Vector3(0.0f, Time.deltaTime * 60, 0.0f);
        if (countTimer == 150)
        {
            StartCoroutine(DedicatedToInit());
        }
        if (countTimer == 156)
        {
            StartCoroutine(DedicatedToEnd());
        }
        if (countTimer == 162)
        {
            StartCoroutine(EndInit1());
        }
        if (countTimer == 166)
        {
            StartCoroutine(EndInit2());
            shutdown = true;
            FadeInCurtain();
            StartCoroutine(ShutdownGame());
        }

        //if (Credits.transform.position.y > 5000.0f && !shutdown)
        //{

        //}
    }

    private IEnumerator EndInit1()
    {
        TheEnd.CrossFadeAlpha(1.0f, 2.0f, false);
        yield return new WaitForSeconds(0.1f);
    }
    private IEnumerator EndInit2()
    {
        ThanksForPlaying.CrossFadeAlpha(1.0f, 2.0f, false);
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator DedicatedToInit()
    {
        DedicatedTo.CrossFadeAlpha(1.0f, 1.5f, false);
        yield return new WaitForSeconds(0.1f);
    }
    private IEnumerator DedicatedToEnd()
    {
        DedicatedTo.CrossFadeAlpha(0.0f, 3.0f, false);
        yield return new WaitForSeconds(0.1f);
    }

    void FadeInCurtain()
    {
        Curtain.CrossFadeAlpha(1.0f, 3.0f, false);
    }

    IEnumerator DedicatedGame()
    {
        while (true) { 
            yield return new WaitForSeconds(1.0f);
            countTimer++;
        }
    }
    IEnumerator ShutdownGame()
    {
        Curtain.CrossFadeAlpha(1.0f, 3.0f, false);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(6);
    }
}
