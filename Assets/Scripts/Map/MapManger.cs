using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManger : MonoBehaviour {

    public enum Stages {INTRO, METEORS_TIMEWARP, METEORS_ENEMIES, MINIBOSS, STRUCT_TIMEWARP,
        STRUCT_ENEMIES, BOSS, ESCAPE}

    public Stages actualStage = Stages.INTRO ;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        switch (actualStage)
        {
            case Stages.INTRO:
                break;
            case Stages.METEORS_TIMEWARP:
                break;
            case Stages.METEORS_ENEMIES:
                break;
            case Stages.MINIBOSS:
                break;
            case Stages.STRUCT_TIMEWARP:
                break;
            case Stages.STRUCT_ENEMIES:
                break;
            case Stages.BOSS:
                break;
            case Stages.ESCAPE:
                break;
        }
    }

    public void GoToNextStage() {
        ++actualStage;
    }

}
