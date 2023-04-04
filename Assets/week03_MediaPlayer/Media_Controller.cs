using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Media_Controller : MonoBehaviour
{
    //AudioClip Clip;
    AudioSource Player;
    public string PlayKey, StopKey;

    void Start()
    {
        Player = GetComponent<AudioSource>();
        Player.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if(Input.inputString.ToLower() == PlayKey.ToLower())
            {
                PlayMedia();
            }

            if (Input.inputString.ToLower() == StopKey.ToLower())
            {
                StopMedia();
            }
        }
    }

    public void PlayMedia()
    {
        Player.Play();
        print("play");
    }

    public void StopMedia()
    {
        Player.Stop();
        print("stop");
    }

}
