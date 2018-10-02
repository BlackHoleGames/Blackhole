using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class SwitchablePlayerController : MonoBehaviour
{


    public float fireCooldown;
    public float speedFactor = 1.0f;
    public float rotationSpeed = 4.0f;
    public float XLimit = 10.0f;
    public float ZLimit = 5.0f;
    public int rotSpeed = 20;
    public float TimeWarpXLimit;
    public float TimeWarpYLimit;
    public float RollLimit = 30.0f;
    public float PitchLimit = 30.0f;
    //public float invul = 1.0f;
    public float sloMo = 2.0f;
    public float alertModeDuration = 3.0f;
    public float invulnerabilityDuration = 2.0f;
    public static float shield = 10.0f;
    public float shieldRegenPerSec = 1.0f;
    public float timeBombRegenPerSec = 1.0f;
    public AudioClip[] timeBombClips;
    public Slider life;
    public Image fillLife, fillTimeBomb;
    public Text liveValue;
    public GameObject projectile, sphere, ghost, parentAxis, pShoot, quitMenu;
    public static bool camMovementEnemies;
    public Vector3 readjustInitialPos, initialRot, rotX, rotZ;
    public float actualLife;
    private AudioSource slomo, timebomb, gunshot, timewarp;
    private float firingCounter, t, rtimeZ, rtimeX, alertModeTime, rotationTargetZ, rotationTargetX;
    private bool readjustPosition, startRotatingRoll, startRotatingPitch, restorePitch, godMode;
    private TimeManager tm;
    private AudioManagerScript ams;
    private List<GameObject> ghostList;
    private bool isSavingData, is_vertical, is_firing, play;
    public int lifePoints, lives = 3;
    //public Transform cameraTrs;
    //public bool camRotate = false;
    private PostProcessingSwitcher pps;
    public bool speedOnProp, StandByVertProp = false;
    private IEnumerator FireRutine;
    public bool isUpdatingLife, isDestroying, isDeath, emptyStockLives = false;
    public bool activateBomb , emptyStockBombs, isFinished, isShotingbyPad, playerHit, impactforshake, isAlert = false;
    private IEnumerator DisableAction;
    private float disableTimer = 2.0f;
    public bool isEnding, isRestoring, isDeathDoor, ghostEnabled, invulAfterSlow, disableSecure = false;
    public bool gamePaused;
    private float lifeDeath = -0.1f;
    private float lifeLimit = 0.1f;
    public bool onTutorial,secureBomb = false;
    public enum TutorialStages { SHOOTTUTORIAL, TIMEBOMBTUTORIAL, TIMEWARPTUTORIAL };
    private TutorialStages actualTutorialStage = TutorialStages.SHOOTTUTORIAL;
    private bool antiDoubleImpact;

    //public CameraBehaviour cameraShaking;
    // Use this for initialization
    void Start()
    {
        godMode = false;
        play = true;
        gamePaused = false;
        actualLife = shield;
        life.value = actualLife;
        lifePoints = (int)actualLife;
        rotationTargetZ = 0.0f;
        rotationTargetX = 0.0f;
        firingCounter = 0.0f;
        rtimeZ = 0.0f;
        rtimeX = 0.0f;
        initialRot = transform.rotation.eulerAngles;
        startRotatingRoll = false;
        startRotatingPitch = false;
        restorePitch = false;
        is_firing = false;
        is_vertical = true;
        tm = GetComponent<TimeManager>();
        ams = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        pps = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingSwitcher>();
        ghostList = new List<GameObject>();
        camMovementEnemies = false;
        playerHit = false;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        slomo = audioSources[0];
        timebomb = audioSources[1];
        gunshot = audioSources[2];
        timewarp = audioSources[3];
        liveValue.text = "X3";
        isDeathDoor = false;
        onTutorial = false;
        //parentAxis = gameObject;
    }

    /*
        is_firing = false;
        rotation = 0;
        alertModeTime = 0
        isPlaying -> stop
        playerHit = false;
        regenTimeBomb = 1.0f
    */

    // Update is called once per frame
    void Update()
    {

        if (!isEnding) {
            if (!isDeath && !isDestroying)
            {
                if ((Input.GetKeyDown(KeyCode.X) 
                    || Input.GetKeyDown(KeyCode.Escape) 
                    || Input.GetButtonDown("Pause")) && (lives > 0))
                {
                    if (!gamePaused)
                    {
                        quitMenu.SetActive(true);
                        tm.PauseGame();
                        gamePaused = true;
                        secureBomb = true;
                    }
                    else
                    {
                        quitMenu.SetActive(false);
                        tm.UnPauseGame();
                        gamePaused = false;
                    }
                }
                if (!gamePaused)
                {
                    if (play)
                    {
                        float axisX = Input.GetAxis("Horizontal");
                        float axisY = Input.GetAxis("Vertical");
                        double RT = Input.GetAxis("RT");

                        Move(axisX, axisY);
                        //ManagePitchRotation(axisY);
                        ManageRollRotation(axisX);
                        ManageInput(RT);
                    }
                    if (startRotatingRoll || startRotatingPitch) Rotate();
                    else
                    {
                        playerHit = false;
                    }
                    if (readjustPosition) ReadjustPlayer();
                    if (is_firing)
                    {
                        Fire();
                        firingCounter -= Time.unscaledDeltaTime;
                    }
                    if (isDeathDoor)
                    {
                        pps.GetComponent<PostProcessingSwitcher>().ActivateDamageEffect();
                    }
                    else pps.GetComponent<PostProcessingSwitcher>().StopDamageEffect();
                    if (actualLife == 0.0) fillLife.enabled = false;
                    else fillLife.enabled = true;
                }
                else {
                    if (onTutorial) {
                        switch (actualTutorialStage) {
                            case TutorialStages.SHOOTTUTORIAL:
                                double RT = Input.GetAxis("RT");
                                if (Input.GetButtonDown("Fire1") || (RT > 0)) {
                                    ManageInput(RT);
                                    tm.UnPauseGame();
                                    ++actualTutorialStage;
                                    onTutorial = false;
                                }
                                break;
                            case TutorialStages.TIMEBOMBTUTORIAL:
                                if (Input.GetKeyDown(KeyCode.Space) || (Input.GetButtonDown("AButton")))
                                {
                                    ManageTimeBomb();
                                    tm.UnPauseGame();
                                    ++actualTutorialStage;
                                    onTutorial = false;
                                }
                                break;
                            case TutorialStages.TIMEWARPTUTORIAL:
                                if (Input.GetKeyDown(KeyCode.Space) || (Input.GetButtonDown("AButton")))
                                {
                                    ManageTimeBomb();
                                    tm.UnPauseGame();
                                    ++actualTutorialStage;
                                    onTutorial = false;
                                }
                                break;
                        }
                    }
                }
            }else if (!isSavingData)
            {
                isSavingData = true;
                SaveGameStatsScript.GameStats.isGameOver = true;
                SaveGameStatsScript.GameStats.playerScore = ScoreScript.score;
            }
            //if (actualLife < shield) Regen();
            //if (fillTimeBomb.fillAmount < 1.0f) RegenTimeBomb();
        }else
        {
            SaveGameStatsScript.GameStats.isGameOver = true;
            SaveGameStatsScript.GameStats.playerScore = ScoreScript.score;
        }
    }

    public void ActivateOnTutorial() {
        onTutorial = true;
    }

    public void ManageInput(double RT)
    {

        if (!gamePaused)
        {
            isShotingbyPad = false;
            RT = System.Math.Round(RT, 2);
            if (RT > 0) isShotingbyPad = true;
            if ((Input.GetButtonDown("Fire1") || (RT > 0)) && !is_firing)
            {
                is_firing = true;
                if (ghostEnabled) foreach (GameObject g in ghostList) g.GetComponent<TimeGhost>().StartFiring();

            }
            if ((Input.GetButtonUp("Fire1")) && is_firing)
            {
                is_firing = false;
                firingCounter = 0.0f;
                if (ghostEnabled) foreach (GameObject g in ghostList) g.GetComponent<TimeGhost>().StopFiring();

            }
            if (isShotingbyPad)
            {
                FireRutine = StoppingShoot(0.5f);
                StartCoroutine(FireRutine);
            }

            if (Input.GetKeyDown(KeyCode.Space) || (Input.GetButtonDown("AButton")))
            {
                if (!emptyStockBombs && !activateBomb && TimeBombManager.bombs>0 && !secureBomb)
                {
                    ManageTimeBomb();
                }else
                {
                    secureBomb = false;
                }
            }
        }
    }

    public void ManageTimeBomb() {
        RumblePad.RumbleState = 3;
        activateBomb = true;
        int clipIndex = (int)UnityEngine.Random.Range(0, 3);
        timebomb.clip = timeBombClips[clipIndex];
        timebomb.Play();
        //tm.StartSloMo();
        GameObject Bubble = Instantiate(sphere, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        if (!is_vertical) Bubble.GetComponent<TimeBubble>().inTimeWarp = true;
    }

    public void UnPauseGame()
    {
        tm.UnPauseGame();
        tm.RestoreTime();
    }

    IEnumerator StoppingShoot(float waitshoot)
    {
        yield return new WaitForSeconds(waitshoot);
        is_firing = false;
        //firingCounter = 0.0f;
        foreach (GameObject g in ghostList) g.GetComponent<TimeGhost>().StopFiring();       
    }

    public void RegenLife()
    {
        fillLife.enabled = true;
        actualLife += shieldRegenPerSec * Time.unscaledDeltaTime;
        fillLife.fillAmount += shieldRegenPerSec * Time.unscaledDeltaTime;
        if (isDeathDoor) isDeathDoor = false;
        life.value = actualLife;
        lifePoints = (int)actualLife;
        isUpdatingLife = true;
    }

    public void RegenTimeBomb()
    {
        fillTimeBomb.fillAmount += timeBombRegenPerSec * Time.unscaledDeltaTime;
    }

    public void Fire()
    {
        if (firingCounter <= 0.0f)
        {
            if (pShoot != null) pShoot.GetComponent<ParticleSystem>().Play();
            Transform t = transform;
            //Instantiate(projectile, t.position, t.rotation);
            Instantiate(projectile, transform.position, transform.rotation);
            gunshot.Play();
            firingCounter = fireCooldown;
        }
    }

    public bool IsFiring() {
        return is_firing;
    }

    public bool VerticalAxisOn() {
        return is_vertical;
    }

    public void SwitchAxis()
    {
        readjustInitialPos = transform.position;
        readjustPosition = true;
        t = 0;
        if (is_vertical) is_vertical = false;
        else is_vertical = true;
        camMovementEnemies = true;
    }

    public void Move(float Xinput, float YZinput)
    {
        float nextPosX = ((Xinput * speedFactor) * (Time.unscaledDeltaTime)); // / Time.timeScale));
        float nextPosYZ = ((YZinput * speedFactor) * (Time.unscaledDeltaTime));  // / Time.timeScale));
        //cameraTrs.Rotate(0, rotSpeed * Time.deltaTime ,0);
        //        cameraTrs.Rotate(0, 0, -ZLimit);
        float coordAD, coordWS;
        float LimitHorizontal, LimitVertical;
        if (is_vertical)
        {
            coordAD = parentAxis.transform.position.x;
            coordWS = parentAxis.transform.position.z;
            LimitHorizontal = XLimit;
            LimitVertical = ZLimit;
        }
        else
        {
            coordAD = parentAxis.transform.position.x;
            coordWS = parentAxis.transform.position.y;
            LimitHorizontal = TimeWarpXLimit;
            LimitVertical = TimeWarpYLimit;
        }
        if ((coordAD + nextPosX > -LimitHorizontal) && (coordAD + nextPosX < LimitHorizontal))
        {
            if (is_vertical)
            {
                parentAxis.transform.position += new Vector3(nextPosX, 0.0f, 0.0f);
            }
            else parentAxis.transform.position += new Vector3(nextPosX, 0.0f, 0.0f);
        }
        if ((coordWS + nextPosYZ > -LimitVertical) && (coordWS + nextPosYZ < LimitVertical))
        {
            if (is_vertical) parentAxis.transform.position += new Vector3(0.0f, 0.0f, nextPosYZ);
            else parentAxis.transform.position += new Vector3(0.0f, nextPosYZ, 0.0f);
        }
        if (YZinput > 0.0f) speedOnProp = true;
        else speedOnProp = false;
        if (VerticalAxisOn()) StandByVertProp = true;
        else StandByVertProp = false;
    }

    //parentAxis
    public void ManageRollRotation(float Xinput)
    {
        
        if (Xinput == 0 && (transform.rotation.eulerAngles.z != 0))
        {
            rotationTargetZ = 0.0f;
            rtimeZ = 0;
            initialRot = transform.rotation.eulerAngles;
            startRotatingRoll = true;
        }
        else if (Xinput != 0)
        {
            if (transform.rotation.eulerAngles.z < RollLimit && transform.rotation.eulerAngles.z > -RollLimit)
            {
                float actualRot = rotationTargetZ;
                rotationTargetZ = RollLimit;
                if (Xinput > 0.0f) rotationTargetZ = -RollLimit;
                if (startRotatingRoll && rotationTargetZ != actualRot)
                {
                    initialRot = transform.rotation.eulerAngles;
                    rtimeZ = 0;
                }
                startRotatingRoll = true;
            }
        }
        if (startRotatingRoll)
        {
            rtimeZ += Time.unscaledDeltaTime / 1.0f;
            rotZ = AngleLerp(initialRot, new Vector3(0.0f, 0.0f, rotationTargetZ), rtimeZ * rotationSpeed);

            //transform.eulerAngles = targetRotationZ;
            if (Mathf.Abs(transform.rotation.eulerAngles.z - rotationTargetZ) < 0.01)
            {
                startRotatingRoll = false;
            }

        }
        
    }

    public void ManagePitchRotation(float YZinput)
    {
        if (!is_vertical && !readjustPosition)
        {
            if (YZinput == 0 && (transform.rotation.eulerAngles.x != 0))
            {
                rotationTargetX = 0.0f;
                rtimeX = 0;
                initialRot = transform.rotation.eulerAngles;
                startRotatingPitch = true;
            }
            if (YZinput != 0)
            {
                if (transform.rotation.eulerAngles.x < RollLimit && transform.rotation.eulerAngles.x > -RollLimit)
                {
                    float actualRot = rotationTargetX;
                    rotationTargetX = RollLimit;
                    if (YZinput < 0.0f) rotationTargetX = -RollLimit;
                    if (startRotatingPitch && rotationTargetX != actualRot)
                    {
                        initialRot = transform.rotation.eulerAngles;
                        rtimeX = 0;
                    }
                    startRotatingPitch = true;
                }
            }
            if (startRotatingPitch)
            {
                rtimeX += Time.unscaledDeltaTime / 1.0f;
                rotX = AngleLerp(initialRot, new Vector3(rotationTargetX, 0.0f, 0.0f), rtimeX * rotationSpeed);
                //parentAxis.transform.eulerAngles = targetRotationX;
                if (Mathf.Abs(parentAxis.transform.rotation.eulerAngles.x - rotationTargetX) < 0.01)
                {
                    startRotatingPitch = false;
                }
            }
        }
    }

    public void Rotate()
    {
        parentAxis.transform.eulerAngles = new Vector3(rotX.x, 0.0f, rotZ.z);
        foreach (GameObject g in ghostList)
        {
            g.GetComponent<TimeGhost>().RotateGhosts();
        }
    }

    public void ReadjustPlayer()
    {
        if (is_vertical)
        {
            if (parentAxis.transform.rotation.eulerAngles.x != 0)
            {
                if (!restorePitch)
                {
                    rotationTargetX = 0.0f;
                    rtimeX = 0;
                    initialRot = transform.rotation.eulerAngles;
                    restorePitch = true;
                }
                else
                {
                    rtimeX += Time.unscaledDeltaTime / 1.0f;
                    rotX = AngleLerp(initialRot, new Vector3(rotationTargetX, 0.0f, 0.0f), rtimeX * rotationSpeed);
                    //parentAxis.transform.eulerAngles = targetRotationX;
                    if (Mathf.Abs(parentAxis.transform.rotation.eulerAngles.x - rotationTargetX) < 0.01)
                    {
                        parentAxis.transform.eulerAngles = new Vector3(0.0f, 0.0f, parentAxis.transform.eulerAngles.z);
                        restorePitch = false;
                        rtimeX = 0;
                    }
                }
            }
            t += Time.unscaledDeltaTime / 1.0f;
            parentAxis.transform.position = Vector3.Lerp(readjustInitialPos, new Vector3(parentAxis.transform.position.x, 0.0f, parentAxis.transform.position.z), t);
            if ((Mathf.Abs(parentAxis.transform.position.y) < 0.01)) readjustPosition = false;
        }
        else
        {
            t += Time.unscaledDeltaTime / 1.0f;
            float targetX = parentAxis.transform.position.x;
            if (targetX > TimeWarpXLimit) targetX = TimeWarpXLimit - 0.1f;
            else if (targetX < -TimeWarpXLimit) targetX = -TimeWarpXLimit + 0.1f;
            parentAxis.transform.position = Vector3.Lerp(readjustInitialPos, new Vector3(targetX, parentAxis.transform.position.y, -7.5f), t);
            if ((Mathf.Abs(Mathf.Abs(parentAxis.transform.position.z) - 7.5f) < 0.01) &&
                (Mathf.Abs(targetX) < TimeWarpXLimit)) readjustPosition = false;
        }
    }

    public void SpawnGhost()
    {
        if (ghostList.Count < 2)
        {
            GameObject obj = Instantiate(ghost, transform.position, transform.rotation);
            if (ghostList.Count > 0) obj.GetComponent<TimeGhost>().leader = ghostList[(ghostList.Count - 1)].transform;
            else obj.GetComponent<TimeGhost>().leader = transform;
            obj.transform.rotation = Quaternion.identity;
            obj.GetComponent<TimeGhost>().SetFiringCounter(firingCounter);
            ghostList.Add(obj);
            ghostEnabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!antiDoubleImpact)
        {
            antiDoubleImpact = true;
            if (!isRestoring)
            {
                if (!godMode)
                {
                    if (other.tag == "Enemy" || other.tag == "EnemyProjectile" || other.tag == "DeathLaser" || other.tag == "Meteorites")
                    {
                        if (other.tag == "Meteorites") other.tag = "Untagged";
                        RumblePad.RumbleState = 1;//Normal Impact                        
                        isUpdatingLife = true;
                        if (actualLife > lifeLimit)
                        {
                            if (other.tag == "DeathLaser") actualLife = lifeDeath;
                            else actualLife = lifeLimit;
                        }
                        else actualLife = lifeDeath;
                        UpdateStatusLifeIcons();
                        //life.value = actualLife;
                        //lifePoints = (int)actualLife;
                        if (actualLife < 0.0f)
                        {
                            Instantiate(Resources.Load("BlueExplosion"), transform.position, transform.rotation);
                            is_firing = false;
                            firingCounter = 0.0f;
                            DestroyGhosts();
                            actualLife = 0.0f;
                            lifePoints = (int)actualLife;
                            tm.RestoreTime();
                            fillLife.enabled = false;
                            isDestroying = true;
                            lives--;
                            isDeathDoor = false;
                            if (lives <= 0)
                            {
                                ams.StopMusic();
                                tm.RestoreTime();
                                RumblePad.RumbleState = 6;
                                GameObject.FindGameObjectWithTag("L1").SetActive(false);
                                isDeath = true;
                                isFinished = true;
                                liveValue.text = "";
                                SaveGameStatsScript.GameStats.isGameOver = true;
                                SaveGameStatsScript.GameStats.playerScore = ScoreScript.score;
                            }
                            else
                            {
                                switch (lives)
                                {
                                    case 2:
                                        GameObject.FindGameObjectWithTag("L3").SetActive(false);
                                        break;
                                    case 1:
                                        GameObject.FindGameObjectWithTag("L2").SetActive(false);
                                        break;
                                }
                                liveValue.text = "X" + lives.ToString();
                                RumblePad.RumbleState = 5;
                            }

                            //SceneManager.LoadScene(6);
                            //TimerScript.gameover = true;
                            //Remaining deaht animation before this bool.                        
                        } // Death  
                        else
                        {
                            if (actualLife < 0.2f) actualLife = 0.0f;
                            if (actualLife == 0.0f) isDeathDoor = true;
                            if (!isDeathDoor) pps.DamageEffect1Round();
                            impactforshake = true;
                            invulAfterSlow = true;
                            tm.RestoreTime();
                            //RumblePad.RumbleState = 1; //Alarm
                        }
                        if (!isDestroying || actualLife < 2.0f)
                        {
                            activateBomb = true;
                            int clipIndex = (int)UnityEngine.Random.Range(0, 3);
                            timebomb.clip = timeBombClips[clipIndex];
                            timebomb.Play();
                            GameObject Bubble = Instantiate(sphere, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
                            if (!is_vertical) Bubble.GetComponent<TimeBubble>().inTimeWarp = true;

                            DestroyGhosts();
                        }
                    }
                }
            }
            else
            {
                impactforshake = false;
            }
        }
        antiDoubleImpact = false;
    }

    private void UpdateStatusLifeIcons()
    {
        switch (lives)
        {
            case 3:
                foreach (Transform child in GameObject.FindGameObjectWithTag("L3").transform)
                {
                    child.gameObject.SetActive(false);
                }
            break;
            case 2:
                foreach (Transform child in GameObject.FindGameObjectWithTag("L2").transform)
                {
                    child.gameObject.SetActive(false);
                }
                break;
            case 1:
                foreach (Transform child in GameObject.FindGameObjectWithTag("L1").transform)
                {
                    child.gameObject.SetActive(false);
                }
                break;
        }
    }

    public void AddPoints() {
        ScoreScript.score = ScoreScript.score + (int)(100 * ScoreScript.multiplierScore);

    }

   
    public void InitiateWormHole()
    {
        tm.StartWorhmHole();
    }

    Vector3 AngleLerp(Vector3 StartAngle, Vector3 FinishAngle, float t)
    {
        float xLerp = Mathf.LerpAngle(StartAngle.x, FinishAngle.x, t);
        float yLerp = Mathf.LerpAngle(StartAngle.y, FinishAngle.y, t);
        float zLerp = Mathf.LerpAngle(StartAngle.z, FinishAngle.z, t);
        Vector3 Lerped = new Vector3(xLerp, yLerp, zLerp);
        return Lerped;
    }

    public void SetNewLimits(float newX, float newY, bool verticalLimits) {
        if (verticalLimits)
        {
            XLimit = newX;
            ZLimit = newY;
        }
        else {
            TimeWarpXLimit = newX;
            TimeWarpYLimit = newY;
        }
    }

    public void ActivatePlayer() {
        play = true;
    }

    public void SetPlayerGodMode(bool enabled) {
        godMode = enabled;
    }
    public void DestroyGhosts()
    {
        ghostEnabled = false;
        foreach (GameObject g in ghostList)
        {
            Instantiate(Resources.Load("PS_TimeGhost_D"), g.transform.position, g.transform.rotation);
            Destroy(g);
        }
        ghostList.Clear();
        /*foreach (GameObject g in ghostList)
        {
            g.GetComponent<TimeGhost>().StopFiring();
            g.GetComponent<TimeGhost>().DisableGhosts();
        }
        //2 Seconds to disapear
        if (!disableSecure)
        {
            ghostEnabled = false;
            disableSecure = true;
            DisableAction = DisableGhotTimer(disableTimer);
            StartCoroutine(DisableAction);
        }*/
    }

    IEnumerator DisableGhotTimer(float disableDuration)
    {
        yield return new WaitForSeconds(disableDuration);
        while (ghostList.Count > 0)
        {
            Destroy(ghostList[0]);
            ghostList.Remove(ghostList[0]);
        }
        ghostList.Clear();
        disableSecure = false;
    }

    public void DebugInstantiateGhosts(int numbghosts) {
        if (numbghosts != ghostList.Count) {
            if (numbghosts < ghostList.Count)
            {
                for (int i= ghostList.Count; i > numbghosts; i--)
                {
                    GameObject toRemove = ghostList[i-1];
                    ghostList.Remove(toRemove);
                    Destroy(toRemove);
                }
            }
            else {
                for (int j = 0; j < numbghosts; ++j) {
                    SpawnGhost();
                }
            }
        }
    }
}