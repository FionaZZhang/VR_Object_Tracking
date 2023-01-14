using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualFeedback : MonoBehaviour
{
    private Transform arrowTrans;
    private bool isEnabled;

    // Start is called before the first frame update
    void Start()
    {
        arrowTrans = GameObject.Find("DirArrow").transform;
    }

    // Rotate the arrow to look at the target
    void Update()
    {
        arrowTrans.transform.LookAt(Logic.targetPos);
        arrowTrans.Rotate(90,0,180);
    }

    // Show the Directional Arrow
    private void OnEnable()
    {
        arrowTrans.gameObject.SetActive(true) ;
    }

    // Hide the Directional Arrow
    private void OnDisable()
    {
        arrowTrans.gameObject.SetActive(false) ;
    }
}
