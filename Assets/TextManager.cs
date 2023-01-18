using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    private Image background;
    private GameObject tutorialTxt;
    private bool isTutorialActive;
    private float lastSwitched;

    private Text content;

    private Logic logic;
    private TimeLog timeRecord;

    // Start is called before the first frame update
    void Start()
    {

        background = GetComponent<Image>();
        tutorialTxt = this.transform.Find("Tutorial").gameObject;
        isTutorialActive = true;
        tutorialTxt.SetActive(isTutorialActive);

        content = tutorialTxt.GetComponent<Text>();

        logic = GameObject.Find("ExperimentLogic").GetComponent<Logic>();
        timeRecord = GameObject.Find("ExperimentLogic").GetComponent<TimeLog>();
        
    }

    // Update is called once per frame
    void Update()
    {
        string newContent = "";

        if (logic.currLvlNumb == 0)
        {
            newContent = "Choosing lvl order fase " + "\n";
            newContent += "Press B button to choose another preset" + "\n";
            newContent += "Press Y button to confirm preset choice and start" + "\n";
            newContent += "Current Preset Number: " + logic.presetActive.ToString() + "\n";
        }
        else if (logic.isExperFinished)
        {
            newContent += "Active Time" + "\n";
            newContent += string.Join(" // ", timeRecord.timeLog) + "\n";
            newContent += "Inactive Time" + "\n";
            newContent += string.Join(" // ", timeRecord.inActiveLog);
        }
        else if (!logic.isLogging)
        {
            isTutorialActive = true;
            background.enabled = true;
            tutorialTxt.SetActive(true);
            newContent += "Welcome to Finding Nemo!" + "\n";
            newContent += "Press A to start !";
        }
        else if (logic.isLogging)
        {
            newContent += "Go! Follow the different types of hints and find nemo (((:";

        }
        content.text = newContent;

        // DEBUG CODE - not importnat
        //content.text += "Amplitude: " + HapticFeedback.vibAmplitude.ToString()+ "\n";
        //content.text += "Frequency: " + HapticFeedback.vibFrquency.ToString()+ "\n";

        // bool isCooldownOver = Time.time - lastSwitched > 0.5;

        if(OVRInput.GetUp((OVRInput.Button)CustomControls.TutorialSwitch)) //&& isCooldownOver)
        {
            isTutorialActive = !isTutorialActive;
            background.enabled = isTutorialActive;
            tutorialTxt.SetActive(isTutorialActive);
            //lastSwitched = Time.time;
        }
    }

}
