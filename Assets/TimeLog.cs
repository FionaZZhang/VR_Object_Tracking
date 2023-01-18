using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class TimeLog : MonoBehaviour
{
    public List<double> timeLog;
    public List<double> inActiveLog;
    private float startTime;
    private float inActiveTime;
    private float stillTime;
    private Logic logic;
    private bool isStart;
    private bool isActive;
    private Transform player;
    private Vector3 lastPos;

    // Start is called before the first frame update
    void OnEnable()
    {
        logic = GameObject.Find("ExperimentLogic").GetComponent<Logic>();
        player = GameObject.Find("PlayerObj").GetComponent<Transform>();
        timeLog = new List<double>(8);
        inActiveLog = new List<double>(8);
        startTime = 0;
        inActiveTime = 0;
        isStart = false;
        isActive = false;
        lastPos = player.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.O) && (!isStart) && (logic.currLvlNumb != 0))
        {
            startTime = Time.time;
            isStart = true;
            isActive = true;
        }
        else if (!logic.isLogging && (isStart))
        {
            timeLog.Add((Math.Round(Time.time - startTime, 2)));
            inActiveLog.Add((Math.Round(stillTime, 2)));
            stillTime = 0;
            isStart = false;
        
        }

        if (isStart)
        {
            if ((player.position[2] == lastPos[2]) && isActive)
            {
                inActiveTime = Time.time;
                isActive = false;
                print("stop");
            }
            else if ((player.position[2] != lastPos[2]) && !isActive)
            {
                stillTime += Time.time - inActiveTime;
                print("active");
                isActive = true;
            }

        }

        if (logic.isExperFinished)
        {
            print(string.Join(", ", timeLog));
        }
        lastPos = player.position;
    }
}
