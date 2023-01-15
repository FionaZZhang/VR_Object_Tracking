using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessFeedback : MonoBehaviour
{
    //private static float initialDistance;
    public Light directionalLight;
    public float IntensityInterval;
    public Transform lightPos;

    // Start is called before the first frame update
    void Start()
    {
        directionalLight.intensity = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float d = Logic.distanceToTarget;
        directionalLight.intensity = 1.5f * IntensityInterval - d / 130 * IntensityInterval;

    }

    private void OnEnable()
    {
        lightPos.position = new Vector3(-65.028f, 4.41f, 69.42f);
    }

    private void OnDisable()
    {
        directionalLight.intensity = 1;
        lightPos.position = new Vector3(0, 3, 0);
    }
}


