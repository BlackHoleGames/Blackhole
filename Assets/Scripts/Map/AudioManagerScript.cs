using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour {

    public float volumeRecoverDuration = 1.0f;
    private float secondsToStopMusic = 1.0f;
    public AudioClip bossMusic, blackHoleEnter;
    private bool  stopMusic,  switchMusic, lowerMusic;
    private AudioSource audioSource;
    private AudioClip clipToPlay;
    private GameObject reverbNormal, reverbBlackHole, reverbTimeBomb, actualReverb;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        stopMusic = false;
        switchMusic = false;
        GameObject ReverbZones = GameObject.Find("ReverbZones");
        reverbNormal = ReverbZones.transform.GetChild(0).gameObject;
        reverbTimeBomb = ReverbZones.transform.GetChild(1).gameObject;
        reverbBlackHole = ReverbZones.transform.GetChild(2).gameObject;
        actualReverb = reverbNormal;
    }
	
	// Update is called once per frame
	void Update () {
        if (stopMusic)
        {
            if (audioSource.volume > 0.0f) audioSource.volume -= Time.deltaTime/secondsToStopMusic;
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

        if (lowerMusic) {
                if (audioSource.volume < 1.0f) audioSource.volume += Time.deltaTime / volumeRecoverDuration;
                else {
                    audioSource.volume = 1.0f;
                    lowerMusic = false;
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
        secondsToStopMusic = 1.0f;
    }

    public void LowerMusic(float  duration) {
        audioSource.volume = 0;
        volumeRecoverDuration = duration;
        lowerMusic = true;
    }

    public void StopMusicInXSeconds(float duration) {
        stopMusic = true;
        secondsToStopMusic = duration;
    }

    public GameObject GetActualReverb() {
        return actualReverb;
    }

    public GameObject GetReverbTimeBomb() {
        return reverbTimeBomb;
    }

    public void SetBlackHoleReverb() {
        reverbNormal.SetActive(false);
        reverbBlackHole.SetActive(true);
        actualReverb = reverbBlackHole;
    }
}
