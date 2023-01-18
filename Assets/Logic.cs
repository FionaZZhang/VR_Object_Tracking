using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{

    // basic lvl info
    
    private bool isLvlCompleted;

    private Transform playerTrans;
    public Transform targetTrans;

    public Vector3 playerStartPos;

    private float keyLastPressed;
    // private float startTime;

    public int currLvlNumb { private set; get; } = 0;
    public int presetActive { private set; get; } = 1;
    public List<int> lvlOrder { private set; get; }

    public bool isExperFinished { private set; get; }
    public bool isLogging { private set; get; }




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
        // targetTrans = GameObject.Find("Target").transform;
        playerTrans = GameObject.Find("PlayerObj").transform;
        // experiment target position
        targetPos = targetTrans.position;
        // players start position
        playerStartPos = playerTrans.position;
        // time log
        isLogging = false;
        // Save pointers to feedback objects
        visualFeedback = GameObject.Find("VisualFeedbackObj").GetComponent<VisualFeedback>();
        audioFeedback = GameObject.Find("AudioFeedbackObj").GetComponent<AudioFeedback>();
        hapticFeedback = GameObject.Find("HapticFeedbackObj").GetComponent<HapticFeedback>();
        brightnessFeedback = GameObject.Find("BrightnessFeedbackObj").GetComponent<BrightnessFeedback>();
    }

    void Start()
    {   
        UpdateFeedbackForm(-1);
        LoadLvlPreset();
    }

    // Update is called once per frame
    void Update()
    {
        //Experiment is finished
        if (isExperFinished) { return; }

        bool isKeyCoolDownOver = (Time.time - keyLastPressed) > 0.6f;

        // Start logging time
        // if (Input.GetKeyUp(KeyCode.O) && (currLvlNumb != 0)) { isLogging = true; }
        if (OVRInput.GetUp((OVRInput.Button)CustomControls.StartLog) && currLvlNumb != 0) { isLogging = true; }

        // lvl 0 - choosing preset 
        if (currLvlNumb == 0 && isKeyCoolDownOver)
        {
            if ( OVRInput.Get((OVRInput.Button)CustomControls.NextPreset) )
            {
                keyLastPressed = Time.time;
                presetActive += 1;
                if (presetActive > 2) { presetActive = 1; }
                LoadLvlPreset();
            }
            //else if ( Input.GetKey(KeyCode.P) )
            else if ( OVRInput.Get((OVRInput.Button)CustomControls.ChoosePreset) )
            {
                isLvlCompleted = true;
            }

        }

        // Update distance and angle to rotate to look at the target 
        distanceToTarget = Vector3.Distance(playerTrans.position, targetTrans.position);
        Vector3 targetOrientation = Vector3.Normalize(targetTrans.position - playerTrans.position);
        Vector3 currentOrientation = playerTrans.forward;
        angleToRotTarget = Vector3.Angle(targetOrientation, currentOrientation);

        //Close enough to the target - lvl completed
        if (currLvlNumb > 0 &&  distanceToTarget < 3f)
        {
            isLvlCompleted = true;
            isLogging = false;
        }

        //Current level is finished
        if(isLvlCompleted)
        {
            isLvlCompleted = false;
            //No more levels
            if (lvlOrder.Count == 0) 
            { 
                isExperFinished = true;
                UpdateFeedbackForm(-1);
                return;
            }
            // Some levels left to complete
            else
            {
                currLvlNumb = lvlOrder[0];
                // pop first elem in list
                lvlOrder.RemoveAt(0);
                // teleport player to start position
                playerTrans.position = playerStartPos;
                // restart timer
                UpdateFeedbackForm(currLvlNumb);
            }
        }

        // Skip this level
        if (currLvlNumb > 0 && isLogging)
        {
            if (OVRInput.GetUp((OVRInput.Button)CustomControls.SkipLvl))
            {
                playerTrans.position = targetTrans.position;
            }
        }

              
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
            // Knife
            case 1:
                targetTrans.position = new Vector3(-60.4f, 4.2f, -71.4f);
                targetPos = targetTrans.position;
                visualFeedback.gameObject.SetActive(true);
                break;

            // Audio
            case 2:
                targetTrans.position = new Vector3(-66.5f, 4.2f, -37.5f);
                targetPos = targetTrans.position;
                audioFeedback.gameObject.SetActive(true);
                break;

            // Haptic
            case 3:
                targetTrans.position = new Vector3(-4.48f, 5.64f, -61.31f);
                targetPos = targetTrans.position;
                hapticFeedback.gameObject.SetActive(true);
                break;

            // Brightness
            case 4:
                targetTrans.position = new Vector3(54.864f, 3.61f, 19.76f);
                targetPos = targetTrans.position;
                brightnessFeedback.gameObject.SetActive(true);
                break;

            // Audio + Haptic
            case 5:
                targetTrans.position = new Vector3(-67.53f, 2.97f, 35.75f);
                targetPos = targetTrans.position;
                audioFeedback.gameObject.SetActive(true);
                hapticFeedback.gameObject.SetActive(true);
                break;

            // Audio + Brightness
            case 6:
                targetTrans.position = new Vector3(-64.04f, 2.97f, 64.691f);
                targetPos = targetTrans.position;
                audioFeedback.gameObject.SetActive(true);
                brightnessFeedback.gameObject.SetActive(true);
                break;

            // Haptic + Brightness
            case 7:
                targetTrans.position = new Vector3(85.188f, 3.446f, -57.6f);
                targetPos = targetTrans.position;
                hapticFeedback.gameObject.SetActive(true);
                brightnessFeedback.gameObject.SetActive(true);
                break;

            // Audio + Haptic + Brightness
            case 8:
                targetTrans.position = new Vector3(-62.99f, 3.446f, 20.69f);
                targetPos = targetTrans.position;
                audioFeedback.gameObject.SetActive(true);
                hapticFeedback.gameObject.SetActive(true);
                brightnessFeedback.gameObject.SetActive(true);
                break;

            default:
                break;
        }
    }

    private void LoadLvlPreset()
    {
        switch (presetActive)
        {
            // Increasing disturbtion: K B H A BH BA HA AHB
            case 1:
                lvlOrder = new List<int> { 1, 4, 3, 2, 7, 6, 5, 8};
                break;

            // Decreasing disturbtion: K AHB HA BA BH A H B      
            case 2:
                lvlOrder = new List<int> { 1, 8, 5, 6, 7, 2, 3, 4};
                break;
        }
    }



}
