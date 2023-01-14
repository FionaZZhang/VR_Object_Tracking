//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CheckPoint : MonoBehaviour
//{
//    private int checkpointNumb = 0;

//    bool isChecked = false;

//    Rigidbody myBody;

//    BoxCollider myCollider;

//    Quaternion originalRotation;

//    List<Vector3> checkPointsPos;

//    bool hasCheckedAll = false;

//    private float lastSwitched;


//    private void OnEnable()
//    {
//        this.GetComponent<Renderer>().material.color = new Color(255, 215, 0);
//    }


//    // Start is called before the first frame update
//    void Start()
//    {
//        // for easier use of components 
//        myBody = this.GetComponent<Rigidbody>();
//        myCollider = this.GetComponent<BoxCollider>();

//        // initial settings
//        myCollider.isTrigger = true;
//        myBody.mass = 100;
//        myBody.useGravity = false;
//        myCollider.enabled = true;

//        // save the original "upright" rotation
//        originalRotation = this.transform.rotation;

//        //set color to gold
//        this.GetComponent<Renderer>().material.color = new Color(255, 215, 0);

//        // list of coordinates to set checkpoints at
//        checkPointsPos = new List<Vector3>()  {
//                                new Vector3(60f,6.5f,76f),
//                                new Vector3(60,6.5f,35f),
//                                new Vector3(60f,6.5f,-20f),
//                                //new Vector3(-60f,6.5f,85f)

//        };

//        // Teleport to the first position
//        this.transform.position = checkPointsPos[checkpointNumb];
//    }

    

//    // Update is called once per frame
//    void Update()
//    {
//        bool isCooldownOver = Time.time - lastSwitched > 1;

//        if (OVRInput.Get((OVRInput.Button)CustomControls.CheckPointReset))
//        {
//            lastSwitched = Time.time;
//            Reset();
//        }


//        if (hasCheckedAll) { return; }

//        if (!isChecked)
//        {
//            // rotate arround Y-axis for a cool effect
//            this.transform.Rotate(transform.up, 0.2f);
//        }
//        // if not checked and lies still
//        else if (myBody.velocity.x == 0f &&
//                 myBody.velocity.y == 0f &&
//                 myBody.velocity.z == 0f)
//        {
//            //back to gold color
//            this.GetComponent<Renderer>().material.color = new Color(255, 215, 0);
//            myBody.useGravity = false;

//            // teleport and reset rotation
//            this.transform.position = checkPointsPos[checkpointNumb];
//            this.transform.rotation = originalRotation;

//            // shout down collision physics for the object
//            myCollider.isTrigger = true;

//            isChecked = false;
//        }

//    }

    
//    private void OnTriggerExit(Collider other)
//    {

//        if (hasCheckedAll) { return; }

//        else if (!isChecked && other.gameObject.tag == "Player")
//        {
//            Debug.Log("CheckPoint " + (checkpointNumb + 1) + " Reached !");


//            if (checkpointNumb == checkPointsPos.Count - 1)
//            {
//                hasCheckedAll = true;
//                print("All Checkpoints reached !");
//            }
//            checkpointNumb++;
//            isChecked = true;


//            //change color to green
//            this.GetComponent<Renderer>().material.color = new Color(0, 255, 0);

//            //use physics for collision with ground
//            myCollider.isTrigger = false;

//            // Lift up a bit (cool visual effect)
//            myBody.velocity = Vector3.up * 13;
//            myBody.AddForce(Vector3.right*50,ForceMode.Impulse);

//            //use gravity so it falls down on the ground
//            myBody.useGravity = true;

            
//        }
//    }

//    private void Reset()
//    {
//        hasCheckedAll = false;
//        checkpointNumb = 0;

//        //back to gold color
//        this.GetComponent<Renderer>().material.color = new Color(255, 215, 0);
//        myBody.useGravity = false;

//        // teleport and reset rotation
//        this.transform.position = checkPointsPos[checkpointNumb];
//        this.transform.rotation = originalRotation;

//        // shout down collision physics for the object
//        myCollider.isTrigger = true;

//        isChecked = false;

//    }


//}
