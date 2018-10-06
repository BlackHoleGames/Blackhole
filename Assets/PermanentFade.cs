using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermanentFade : MonoBehaviour {

    // Use this for initialization
    private Text TutorialText;
    private Image ImageText;
    void Start () {
        if (GetComponent<Text>() != null)
        {
            TutorialText = GetComponent<Text>();
            StartCoroutine(TextSequence());
        }
        else
        {
            ImageText = GetComponent<Image>();
        }
    }

    private IEnumerator TextSequence()
    {
        while (true)
        {
            TutorialText.canvasRenderer.SetAlpha(0.0f);
            yield return new WaitForSeconds(0.5f);
            TutorialText.CrossFadeAlpha(0.0f, 0.5f, false);
            yield return new WaitForSeconds(0.5f);
        }
    }
    private IEnumerator ImageSequence()
    {
        while (true)
        {
            ImageText.canvasRenderer.SetAlpha(0.0f);
            yield return new WaitForSeconds(0.5f);
            ImageText.CrossFadeAlpha(0.0f, 0.5f, false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Update is called once per frame
    void Update () {

    }
}
