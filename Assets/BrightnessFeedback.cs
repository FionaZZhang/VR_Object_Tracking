using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessFeedback : MonoBehaviour
{
    //private static float initialDistance;
    public Light directionalLight;
    public float IntensityInterval;

    // Start is called before the first frame update
    void Start()
    {
        // initialDistance = Logic.distanceToTarget;
        directionalLight.intensity = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float d = Logic.distanceToTarget;
        directionalLight.intensity = 1.5f * IntensityInterval - d / 130 * IntensityInterval;

    }

    private void OnDisable()
    {
        directionalLight.intensity = 1;
    }
}


