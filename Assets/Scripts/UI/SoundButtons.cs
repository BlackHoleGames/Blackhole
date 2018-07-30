using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButtons : MonoBehaviour {

    public AudioClip soundPlay;
    //public AudioClip soundScroll;
    //public AudioClip soundBack;
    private Button buttonPlay { get { return GetComponent<Button>(); } }
    //public Button buttonScroll { get { return GetComponent<Button>(); } }
    //public Button buttonBack { get { return GetComponent<Button>(); } }
    private AudioSource sourcePlay  { get { return GetComponent<AudioSource>(); } }
    //private AudioSource sourceScroll { get { return GetComponent<AudioSource>(); } }
    //private AudioSource sourceBack  { get { return GetComponent<AudioSource>(); } }
    // Use this for initialization
    void Start () {
        gameObject.AddComponent<AudioSource>();
        sourcePlay.clip = soundPlay;
        sourcePlay.playOnAwake = false;
        buttonPlay.onClick.AddListener(()=> playFileStart());
        //sourceScroll.clip = soundScroll;
        //sourceScroll.playOnAwake = false;
        //buttonScroll.onClick.AddListener(() => playFileScroll());
        //sourceBack.clip = soundBack;
        //sourceBack.playOnAwake = false;
        //buttonBack.onClick.AddListener(() => playFileBack());
    }
	void playFileStart()
    {
        sourcePlay.PlayOneShot(soundPlay);
    }
    //void playFileScroll()
    //{
    //    sourceScroll.PlayOneShot(soundScroll);
    //}
    //void playFileBack()
    //{
    //    sourceBack.PlayOneShot(soundBack);
    //}
    // Update is called once per frame
    //void Update () {
    //	
    //}
}
