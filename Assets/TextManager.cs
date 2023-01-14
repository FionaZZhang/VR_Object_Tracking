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

    // Start is called before the first frame update
    void Start()
    {

        background = GetComponent<Image>();
        tutorialTxt = this.transform.Find("Tutorial").gameObject;
        isTutorialActive = true;
        tutorialTxt.SetActive(isTutorialActive);

        content = tutorialTxt.GetComponent<Text>();

        logic = GameObject.Find("ExperimentLogic").GetComponent<Logic>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //content.text = Logic.distanceToTarget.ToString();

        content.text = "Current Lvl: "+logic.currLvlNumb.ToString()+ "\n";

        
        // DEBUG CODE - not importnat
        //content.text += "Amplitude: " + HapticFeedback.vibAmplitude.ToString()+ "\n";
        //content.text += "Frequency: " + HapticFeedback.vibFrquency.ToString()+ "\n";

        bool isCooldownOver = Time.time - lastSwitched > 0.5;

        if(OVRInput.Get((OVRInput.Button)CustomControls.TutorialSwitch) && isCooldownOver)
        {
            isTutorialActive = !isTutorialActive;
            background.enabled = isTutorialActive;
            tutorialTxt.SetActive(isTutorialActive);
            lastSwitched = Time.time;
        }
    }
}
