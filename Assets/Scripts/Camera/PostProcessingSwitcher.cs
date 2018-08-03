using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.Rendering;

public class PostProcessingSwitcher : MonoBehaviour {

    public enum Profiles {NORMAL, MAGNETIC_STORM, TIMEWARP};

    public PostProcessingProfile[] profiles;
    private PostProcessingBehaviour ppb;
    private float intentsityEffect;
    private bool StartMagneticEffect;

    
	// Use this for initialization
	void Start () {
        ppb = GetComponent<PostProcessingBehaviour>();
        intentsityEffect = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchPostProcess(Profiles.MAGNETIC_STORM);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartMagneticEffect = false;
        }
        if (StartMagneticEffect && profiles[1].grain.settings.intensity < 1.0f)
        {
            intentsityEffect += Time.deltaTime;
            if (intentsityEffect > 1.0f) intentsityEffect = 1.0f;
            GrainModel.Settings grainSettings = profiles[1].grain.settings;
            grainSettings.intensity = intentsityEffect;
            profiles[1].grain.settings = grainSettings;
        }
        if (!StartMagneticEffect && profiles[1].grain.settings.intensity > 0.0f)
        {
            intentsityEffect -= Time.deltaTime;
            GrainModel.Settings grainSettings = profiles[1].grain.settings;

            if (intentsityEffect < 0.0f)
            {
                grainSettings.intensity = 0.0f;
                profiles[1].grain.settings = grainSettings;
                SwitchPostProcess(Profiles.NORMAL);
            }
            grainSettings.intensity = intentsityEffect;
            profiles[1].grain.settings = grainSettings;
        }


    }

    public void SwitchPostProcess(Profiles newProfile) {
        ppb.profile = profiles[(int)newProfile];
        if (newProfile == Profiles.MAGNETIC_STORM) StartMagneticEffect = true;
 
    }
}
