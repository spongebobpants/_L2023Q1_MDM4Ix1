using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Media_Controller_Adv : MonoBehaviour
{
    AudioClip Clip;
    AudioSource Player;
    public List<AudioClip> AudioClips;
    int currentMediaIndex = 0;
    public string PlayKey, StopKey;
    public GameObject MediaInfo;
    public GameObject MediaPrefab;
    public GameObject MediaPrefabParent;

    void Start()
    {
        Player = GetComponent<AudioSource>();
        currentMediaIndex = Random.Range(0, AudioClips.Count);
        Player.clip = AudioClips[currentMediaIndex];
        //print(currentMediaIndex + ":" + Player.clip.name);
        Player.Stop();
        DisplayMediaInfo();
        ListMedia();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.inputString.ToLower() == PlayKey.ToLower())
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
        print("Play");
    }

    public void StopMedia()
    {
        Player.Stop();
        print("Stop");
    }

    public void NextMedia()
    {
        print("NextMedia");
        currentMediaIndex++;
        if(currentMediaIndex > AudioClips.Count - 1)
        {
            currentMediaIndex = 0;            
        }
        SetMedia();
    }

    public void PrevMedia()
    {
        print("PrevMedia");
        currentMediaIndex--;
        if (currentMediaIndex < 0)
        {
            currentMediaIndex = AudioClips.Count - 1;            
        }
        SetMedia();
    }

    void SetMedia()
    {
        Player.clip = AudioClips[currentMediaIndex];
        //print(currentMediaIndex + ":" + Player.clip.name);
        DisplayMediaInfo();
    }

    public void SetMedia(int mediaIndex)
    {
        currentMediaIndex = mediaIndex;
        Player.clip = AudioClips[currentMediaIndex];
        print(currentMediaIndex + ":" + Player.clip.name);
        DisplayMediaInfo();
    }

    void DisplayMediaInfo()
    {
        Player.clip = AudioClips[currentMediaIndex];
        print(currentMediaIndex + ":" + Player.clip.name);
        MediaInfo.GetComponent<TMP_Text>().text = Player.clip.name;
    }

    void ListMedia()
    {
        for(int i = 0; i < AudioClips.Count; i++)
        {
            GameObject Clone = Instantiate(MediaPrefab);
            Clone.SendMessage("SetIndex", i);
            Clone.GetComponent<TMP_Text>().text = AudioClips[i].name;
            Clone.transform.SetParent(MediaPrefabParent.transform);
        }
    }
}
