using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{

    // basic lvl info
    private int numbOfLvls = 5;
    public int currLvlNumb { private set; get; }
    public int startLvl;
    private bool isLvlCompleted;
    private bool isExperFinished;

    private Transform playerTrans;
    private Transform targetTrans;

    private Vector3 playerStartPos;

    // static public variables can be accesed from anywhere
    public static float distanceToTarget { private set; get; }
    public static float angleToRotTarget { private set; get; }
    public static Vector3 targetPos { private set; get; }

    // Instances of Feedback Classes
    public VisualFeedback visualFeedback { private set; get; }
    public AudioFeedback audioFeedback { private set; get; }
    public HapticFeedback hapticFeedback { private set; get; }
    public BrightnessFeedback brightnessFeedback { private set; get; }


    private void OnEnable()
    {
        targetTrans = GameObject.Find("Target").transform;
        playerTrans = GameObject.Find("PlayerObj").transform;
        // experiment target position
        targetPos = targetTrans.position;
        // players start position
        playerStartPos = playerTrans.position;

        // Save pointers to feedback objects
        visualFeedback = GameObject.Find("VisualFeedbackObj").GetComponent<VisualFeedback>();
        audioFeedback = GameObject.Find("AudioFeedbackObj").GetComponent<AudioFeedback>();
        hapticFeedback = GameObject.Find("HapticFeedbackObj").GetComponent<HapticFeedback>();
        brightnessFeedback = GameObject.Find("BrightnessFeedbackObj").GetComponent<BrightnessFeedback>();
    }

    void Start()
    {   
        currLvlNumb = startLvl;
        UpdateFeedbackForm(currLvlNumb);
    }

    // Update is called once per frame
    void Update()
    {
        //Experiment is finished
        if (isExperFinished) { return; }

        //Current level is finished
        if(isLvlCompleted)
        {
            isLvlCompleted = false;
            //No more levels
            if (currLvlNumb  > numbOfLvls) 
            { 
                isExperFinished = true;
                UpdateFeedbackForm(-1);
                return;
            }
            // Some levels left to complete
            else
            {
                currLvlNumb += 1;
                // teleport player to start position
                playerTrans.position = playerStartPos;
                UpdateFeedbackForm(currLvlNumb);
            }
        }

        // Update distance and angle to rotate to look at the target 
        distanceToTarget = Vector3.Distance(playerTrans.position, targetTrans.position);
        Vector3 targetOrientation = Vector3.Normalize(targetTrans.position - playerTrans.position);
        Vector3 currentOrientation = playerTrans.forward;
        angleToRotTarget = Vector3.Angle(targetOrientation, currentOrientation);

        //Close enough to the target - lvl completed
        if(distanceToTarget < 3f) { isLvlCompleted = true; }
    }

    
    // Enables feedback forms ccording to lvl number
    void UpdateFeedbackForm( int lvlNumb)
    {
        //Disable all feedback forms 
        visualFeedback.gameObject.SetActive(false);
        audioFeedback.gameObject.SetActive(false);
        hapticFeedback.gameObject.SetActive(false);
        brightnessFeedback.gameObject.SetActive(false);
        // enable proper feedback forms
        switch (lvlNumb)
        {
            case 1:
                visualFeedback.gameObject.SetActive(true);
                break;
            case 2:
                visualFeedback.gameObject.SetActive(true);
                audioFeedback.gameObject.SetActive(true);
                break;
            case 3:
                visualFeedback.gameObject.SetActive(true);
                hapticFeedback.gameObject.SetActive(true);
                break;
            case 4:
                audioFeedback.gameObject.SetActive(true);
                hapticFeedback.gameObject.SetActive(true);
                visualFeedback.gameObject.SetActive(true);
                break;
            case 5:
                brightnessFeedback.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }



}
