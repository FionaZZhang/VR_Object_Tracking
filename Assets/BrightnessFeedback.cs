using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessFeedback : MonoBehaviour
{
    //private static float initialDistance;
    public Light directionalLight;
    public float IntensityInterval;
    public Transform lightPos;
    public float distance;
    public Logic logic;

    // Start is called before the first frame update
    void Start()
    {
        directionalLight.intensity = 1;
        logic = GameObject.Find("ExperimentLogic").GetComponent<Logic>();
    }

    // Update is called once per frame
    void Update()
    {
        float d = Logic.distanceToTarget;
        lightPos.position = logic.targetTrans.position;
        distance = Vector3.Distance(logic.targetTrans.position, logic.playerStartPos);
        directionalLight.intensity = 1.5f * IntensityInterval - d / distance * IntensityInterval;

    }

    private void OnDisable()
    {
        directionalLight.intensity = 1;
        lightPos.position = new Vector3(0, 3, 0);
    }
}


