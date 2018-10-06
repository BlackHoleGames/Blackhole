using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.Rendering;

public class PostProcessingSwitcher : MonoBehaviour {

    public enum Profiles {NORMAL, MAGNETIC_STORM, TIMEWARP};

    public PostProcessingProfile[] profiles;
    private PostProcessingBehaviour ppb;
    private float intentsityEffect, smoothingEffect, lerpTime, damageTimer, damageEffectDuration;
    private bool StartMagneticEffect, IncreasingSmooth, endlessDamageEffect, stopDamageEffect;
    public bool StartDamageEffect = false;

    // Use this for initialization
    void Start () {
        ppb = GetComponent<PostProcessingBehaviour>();
        intentsityEffect = 0.0f;
        smoothingEffect = 0.0f;
        IncreasingSmooth = true;
        damageTimer = 0.0f;
        damageEffectDuration = 1.5f;
        endlessDamageEffect = false;
        stopDamageEffect = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P) && !StartMagneticEffect) SwitchPostProcess(Profiles.MAGNETIC_STORM);        
        if (Input.GetKeyDown(KeyCode.O) && StartMagneticEffect) StartMagneticEffect = false;
        if (Input.GetKeyDown(KeyCode.K) && !StartDamageEffect) StartDamageEffect = true;
        if (StartMagneticEffect && profiles[1].grain.settings.intensity < 1.0f) MagneticIntensityUp();
        if (!StartMagneticEffect && profiles[1].grain.settings.intensity > 0.0f) MagneticIntensityDown();
        if (StartDamageEffect) {
            damageTimer += Time.unscaledDeltaTime;
            if (damageTimer < damageEffectDuration) ManageDamageEffect();
            else {
                StartDamageEffect = false;
                damageTimer = 0.0f;
                SetSmoothSettings(0.0f);
            }
        }
        if (endlessDamageEffect) {
            if (stopDamageEffect) {
                if (smoothingEffect == 0) {
                    endlessDamageEffect = false;
                    stopDamageEffect = false;
                }
                else ManageDamageEffect();
            }
            else ManageDamageEffect();
        }
    }

    public void MagneticIntensityUp() {
        intentsityEffect += Time.deltaTime;
        if (intentsityEffect > 1.0f) intentsityEffect = 1.0f;
        GrainModel.Settings grainSettings = profiles[1].grain.settings;
        grainSettings.intensity = intentsityEffect;
        profiles[1].grain.settings = grainSettings;
    }

    public void MagneticIntensityDown()
    {
        intentsityEffect -= Time.deltaTime;
        if (intentsityEffect < 0.0f) {
            SetGrainSettings(0.0f);
            SwitchPostProcess(Profiles.NORMAL);
        }
        else SetGrainSettings(intentsityEffect);                
    }

    public void ManageDamageEffect() {
        lerpTime += Time.unscaledDeltaTime / 0.75f;
        if (IncreasingSmooth) {
            smoothingEffect = Mathf.Lerp(0, 1, lerpTime);
            if (smoothingEffect >= 1.0f) {
                IncreasingSmooth = false;
                lerpTime = 0.0f;
                smoothingEffect = 1.0f;
            }
        }
        else {
            smoothingEffect = Mathf.Lerp(1, 0, lerpTime);
            if (smoothingEffect <= 0.0f) {
                IncreasingSmooth = true;
                lerpTime = 0.0f;
                smoothingEffect = 0.0f;
            }
        }
        SetSmoothSettings(smoothingEffect);
    }

    public void SetSmoothSettings(float smoothEffectValue) {
        VignetteModel.Settings vignetteSettings = profiles[0].vignette.settings;
        vignetteSettings.smoothness = smoothEffectValue;
        profiles[0].vignette.settings = vignetteSettings;
    }

    public void SetGrainSettings(float smoothEffectValue)
    {
        GrainModel.Settings grainSettings = profiles[1].grain.settings;
        grainSettings.intensity = intentsityEffect;
        profiles[1].grain.settings = grainSettings;
    }

    public void SwitchPostProcess(Profiles newProfile) {
        ppb.profile = profiles[(int)newProfile];
        if (newProfile == Profiles.MAGNETIC_STORM) StartMagneticEffect = true;
 
    }

    public void ActivateDamageEffect() {
        if (StartDamageEffect) {
            StartDamageEffect = false;
            lerpTime = 0.0f;
            smoothingEffect = 1.0f;
            damageTimer = 0.0f;
            IncreasingSmooth = true;
        }
    }

    public void StopDamageEffect() {
        stopDamageEffect = true;
    }

    public void DamageEffect1Round() {
        if (StartDamageEffect) {
            IncreasingSmooth = false;
            lerpTime = 0.0f;
            smoothingEffect = 1.0f;
        }
        else {
            StartDamageEffect = true;
            damageEffectDuration = 1.5f;            
        }
    }
}
