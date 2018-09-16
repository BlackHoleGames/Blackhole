using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour {

    public AudioClip bossMusic, blackHoleEnter;

    private bool  stopMusic, modifyThePitch, switchMusic;
    private AudioSource audioSource;
    private AudioClip clipToPlay;
    private float startingPitch, targetPitch, lerpTime;
    
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        stopMusic = false;
        modifyThePitch = false;
        switchMusic = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (stopMusic)
        {
            if (audioSource.volume > 0.0f) audioSource.volume -= Time.deltaTime;
            if (audioSource.volume <= 0.0f) {
                audioSource.Stop();
                stopMusic = false;
                if (switchMusic) {
                    switchMusic = false;         
                    audioSource.clip = clipToPlay;
                    audioSource.volume = 1.0f;
                    audioSource.Play();
                }
            }
        }
        if (modifyThePitch) {
            lerpTime += Time.deltaTime;
            audioSource.pitch = Mathf.Lerp(startingPitch,targetPitch,lerpTime);
            if (Mathf.Abs(audioSource.pitch-targetPitch) <= 0.1f ) {
                modifyThePitch = false;              
            }
        }
	}

    public void ChangeToBossMusic() {
        stopMusic = true;
        switchMusic = true;
        clipToPlay = bossMusic;
        audioSource.loop = true;
    }

    public void PlayBlackHoleEnterSound() {
        stopMusic = true;
        switchMusic = true;
        clipToPlay = blackHoleEnter;
        audioSource.loop = false;
    }
    public void StartMusic() {
        if (stopMusic) stopMusic = false;
        audioSource.Play();
    }

    public void StopMusic() {
        stopMusic = true;
    }

    public void LowerThePitch() {
        startingPitch = 1.0f;
        targetPitch = 0.5f;
        lerpTime = 0.0f;
    }

    public void RestoreThePitch() {
        targetPitch = 1.0f;
        startingPitch = 0.5f;
        lerpTime = 0.0f;
    }
}
