
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float slomoTime = 0.05f;

    private bool slowDown = false;
	// Update is called once per frame
	void Update () {
        if (Time.timeScale > slomoTime && slowDown) {
            Time.timeScale -= Time.unscaledDeltaTime;
        }
        if (Time.timeScale < 1.0 && !slowDown) {
            Time.timeScale += Time.unscaledDeltaTime;
        }
    }

    public void StartSloMo() {
        slowDown = true; ;
    }

    public void RestoreTime() {
        slowDown = false;
    }
}
