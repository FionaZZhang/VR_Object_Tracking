using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFeedback : MonoBehaviour
{
    private AudioSource sound;
    private float lastPlayed;
    private bool timeToPlay;

    // Start is called before the first frame update
    
    void Start()
    {
        sound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float d = Logic.distanceToTarget;
        // time gap between sounds depends on distance to the target
        float timeBetweenSounds =  (0.0001f * Mathf.Pow(d, 2)) + (0.0025f * d) + 0.1763f;
        timeToPlay = (Time.time - lastPlayed) > timeBetweenSounds;

        if (timeToPlay)
        {
            sound.Play();
            lastPlayed = Time.time;
        }

    }
}
