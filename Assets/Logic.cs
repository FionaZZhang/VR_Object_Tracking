using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{

    // basic lvl info
    
    private bool isLvlCompleted;

    private Transform playerTrans;
    public Transform targetTrans;

    private Vector3 playerStartPos;

    
    private float keyLastPressed;

    public int currLvlNumb { private set; get; } = 0;
    public int presetActive { private set; get; } = 1;
    public List<int> lvlOrder { private set; get; }

    public bool isExperFinished { private set; get; }

    


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
            
        // lvl 0 - choosing preset 
        if(currLvlNumb == 0 && isKeyCoolDownOver)
        {
            if ( OVRInput.Get((OVRInput.Button)CustomControls.NextPreset) )
            {
                keyLastPressed = Time.time;
                presetActive += 1;
                if (presetActive > 6) { presetActive = 1; }
                LoadLvlPreset();
            }
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
        if (currLvlNumb > 0 &&  distanceToTarget < 3f) { isLvlCompleted = true; }

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
                UpdateFeedbackForm(currLvlNumb);
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
            case 1:
                targetTrans.position = new Vector3(-4.93f, 5.42f, -60.33f);
                targetPos = targetTrans.position;
                visualFeedback.gameObject.SetActive(true);
                break;
            case 2:
                targetTrans.position = new Vector3(-67.8f, 3.718f, -29.9f);
                targetPos = targetTrans.position;
                //visualFeedback.gameObject.SetActive(true);
                audioFeedback.gameObject.SetActive(true);
                break;
            case 3:
                targetTrans.position = new Vector3(-84.8f, 3.718f, -84.8f);
                targetPos = targetTrans.position;
                //visualFeedback.gameObject.SetActive(true);
                audioFeedback.gameObject.SetActive(true);
                hapticFeedback.gameObject.SetActive(true);
                break;
            case 4:
                targetTrans.position = new Vector3(-65.028f, 4.41f, 69.42f);
                targetPos = targetTrans.position;
                //visualFeedback.gameObject.SetActive(true);
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
            case 1:
                lvlOrder = new List<int> { 1, 2, 3, 4 };
                break;
            case 2:
                lvlOrder = new List<int> { 2, 3, 1, 4 };
                break;
            case 3:
                lvlOrder = new List<int> { 3, 1, 4, 2 };
                break;
            case 4:
                lvlOrder = new List<int> { 4, 1, 2, 3 };
                break;
            case 5:
                lvlOrder = new List<int> { 1, 3, 4, 2 };
                break;
            case 6:
                lvlOrder = new List<int> { 2, 4, 1, 3 };
                break;
        }
    }



}
