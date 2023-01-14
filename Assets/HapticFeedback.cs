using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticFeedback : MonoBehaviour
{
    private float lastPlayed;
    private bool timeToPlay;

    private float lastVibrationStarted;

    private float lastKeyInput;
    public static float vibAmplitude;
    public static float vibFrquency;


    // every vibration has fixed duration
    private float vibDuration = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float d = Logic.distanceToTarget;
        
        // Strength of vibration depends on distance to the target(min 0.1)
        vibAmplitude = (0.0001f * Mathf.Pow(d, 2)) - (0.023f * d) + 1.692f;
        if(vibAmplitude < 0.1f) { vibAmplitude = 0.1f; }

        // Gap between vibrations depends on distance to the target(max 4 sec)
        float vibGap = (0.001f * Mathf.Pow(d, 2)) + (0.0202f * d) + 0.0718f;
        if (vibGap > 4) { vibGap = 4; }
        bool isNewVibNeeded = (Time.time - lastVibrationStarted) > vibGap;

        // DEBUG CODE - not important
        //bool isKeyCooldownOver = (Time.time - lastKeyInput) > 1.5;
        //if (OVRInput.Get((OVRInput.Button)CustomControls.frequency) 
        //    && isKeyCooldownOver)
        //{
        //    vibFrquency += 0.1f;
        //    lastKeyInput = Time.time;
        //}
        //else if (OVRInput.Get((OVRInput.Button)CustomControls.amplitude) 
        //    && isKeyCooldownOver)
        //{
        //    vibAmplitude += 0.1f;
        //    lastKeyInput = Time.time;
        //}

        // if true time to turn the current vibration off
        bool isVibFinished = (Time.time - lastVibrationStarted)>vibDuration;

        OVRInput.Controller controller = OVRInput.Controller.LTouch;
        if (isNewVibNeeded)
        {
            // Start new vibration
            OVRInput.SetControllerVibration(vibFrquency, vibAmplitude, controller);
            lastVibrationStarted = Time.time;
        }
        else if(isVibFinished)
        {
            // finish previous vibration
            OVRInput.SetControllerVibration(0, 0, controller);
        }


    }
}
