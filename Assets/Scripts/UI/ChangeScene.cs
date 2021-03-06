﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    private IEnumerator SceneQuitGameSequence;
    private IEnumerator SceneCloseTimerSequence;
    private IEnumerator SceneOpenTimerSequence;
    private IEnumerator SceneBHSequence;
    private IEnumerator TextBHSequence;
    public Image BlackFade;
    public Text BHText;
    public AudioClip soundPlay;
    public bool shutdown = false;
    public bool isBlackHoleTime = false;
    public bool quitGame = false;
    private bool activateTextBH = false;
    public float SecondsInBH = 20.0f;
    public float SecondsOutBH = 3.0f;
    public float SecondsToWaitDestroy = 2.0f;
    public float SecondsToWaitEnd = 2.0f;
    public float SecondsTextFlick = 1.0f;
    private GameObject player;
    private AudioSource alarmSound;
    private AudioSource sourceAlarm { get { return GetComponent<AudioSource>(); } }
    void Start()
    {
        Cursor.visible = false;
        gameObject.AddComponent<AudioSource>();
        sourceAlarm.clip = soundPlay;
        sourceAlarm.playOnAwake = false;
        BlackFade.canvasRenderer.SetAlpha(1.0f);
        BHText.canvasRenderer.SetAlpha(0.0f);
        SceneOpenTimerSequence = FadeToInitLevel(2.0f);
        StartCoroutine(SceneOpenTimerSequence);
        player = GameObject.FindGameObjectWithTag("Player");
        alarmSound = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update () {
        if(shutdown)
        {
            SceneCloseTimerSequence = FadeToEndingLevel(SecondsToWaitEnd);
            StartCoroutine(SceneCloseTimerSequence);
        }
        if (isBlackHoleTime && !activateTextBH)
        {
            activateTextBH = true;
            TextBHSequence = FadeToTextBH(SecondsTextFlick);
            StartCoroutine(TextBHSequence);

        }
    }


    public void StartBlackHoleSequence() {
        isBlackHoleTime = true;
        SceneBHSequence = FadeToInitBH();
        StartCoroutine(SceneBHSequence);
    }
    IEnumerator FadeToQuitLevel(float levelTimer)
    {
        yield return new WaitForSeconds(levelTimer);
    }
    IEnumerator FadeToEndingLevel (float levelTimer)
    {
        yield return new WaitForSeconds(SecondsToWaitDestroy);
        BlackFade.CrossFadeAlpha(1.0f, SecondsToWaitEnd, false);
        //BlackFade.canvasRenderer.SetAlpha(1.0f);
        yield return new WaitForSeconds(levelTimer);
        if (quitGame)
        {
            SaveGameStatsScript.GameStats.isGameOver = false;
            SaveGameStatsScript.GameStats.playerScore = 0;
            SaveGameStatsScript.GameStats.SetStats();
            SceneManager.LoadScene(1);
        }
        else
        {
            if (player != null &&
                !player.GetComponent<SwitchablePlayerController>().isEnding)
            {
                
                SaveGameStatsScript.GameStats.isGameOver = true;
                SaveGameStatsScript.GameStats.playerScore = ScoreScript.score;
                SaveGameStatsScript.GameStats.SetStats();
                SceneManager.LoadScene(7);
            }
            else
            {
                
                SaveGameStatsScript.GameStats.isGameOver = true;
                SaveGameStatsScript.GameStats.playerScore = ScoreScript.score;
                SaveGameStatsScript.GameStats.SetStats();
                SceneManager.LoadScene(5);
            }
        }
    }
    IEnumerator FadeToInitLevel(float levelTimer)
    {
        BlackFade.CrossFadeAlpha(0.0f, 2.0f, false);
        //BlackFade.canvasRenderer.SetAlpha(1.0f);
        yield return new WaitForSeconds(levelTimer);
    }
    IEnumerator FadeToInitBH()
    {
        yield return new WaitForSeconds(5.0f);
        RumblePad.RumbleState = 7;
        isBlackHoleTime = false;
        BlackFade.CrossFadeAlpha(1.0f, 3.0f, false);
        yield return new WaitForSeconds(SecondsInBH);
        BlackFade.CrossFadeAlpha(0.0f, 5.0f, false);
        yield return new WaitForSeconds(SecondsOutBH);
    }
    IEnumerator FadeToTextBH(float flickText)
    {
        for (int i=0; i < 3; i++) {
            alarmSound.Play();
            BHText.CrossFadeAlpha(1.0f, flickText, false);
            sourceAlarm.PlayOneShot(soundPlay);
            yield return new WaitForSeconds(flickText);
            BHText.CrossFadeAlpha(0.0f, flickText, false);
            sourceAlarm.PlayOneShot(soundPlay);
            yield return new WaitForSeconds(flickText);
        }
        activateTextBH = true;
    }
}
