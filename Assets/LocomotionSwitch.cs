//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LocomotionSwitch : MonoBehaviour
//{
//    private GameObject locoCtrl;
//    private SimpleCapsuleWithStickMovement stickMovement;
//    private bool stickMovEnabled;
//    private float lastChanged;

//    // Start is called before the first frame update
//    void Start()
//    {
//        locoCtrl = this.transform.Find("LocomotionController").gameObject;
//        stickMovement = this.GetComponent<SimpleCapsuleWithStickMovement>();
//        locoCtrl.SetActive(false);
//        stickMovement.enabled = true;
//        stickMovEnabled = true;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        bool isCooldownOver = (Time.time - lastChanged) > 1.5;

//        if  ( OVRInput.Get((OVRInput.Button)CustomControls.LocoSwitch) && isCooldownOver )
//        {
//            lastChanged = Time.time;

//            if (stickMovEnabled)
//            {
//                stickMovement.enabled = false;
//                locoCtrl.SetActive(true);
//            }
//            else
//            {
//                locoCtrl.SetActive(false);
//                stickMovement.enabled = true;
//            }

//            stickMovEnabled = !stickMovEnabled;
//        }

//    }
//}
