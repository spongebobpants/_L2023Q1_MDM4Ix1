using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Media_Prefab_Controller : MonoBehaviour
{
    public GameObject Media_Controller_Adv;
    int ThisIndex;

    private void Start()
    {
        Media_Controller_Adv = GameObject.Find("Media_Controller");
    }
    public void SetIndex(int index)
    {
        ThisIndex = index;
        print(gameObject.name + ", index=" + ThisIndex);
        gameObject.name = "Media" + ThisIndex;
    }

    public void PlayMedia()
    {
        print(gameObject.name + ", PlayMedia=" + ThisIndex);
        Media_Controller_Adv.GetComponent<Media_Controller_Adv>().SetMedia(ThisIndex);
        Media_Controller_Adv.GetComponent<Media_Controller_Adv>().PlayMedia();
    }
}
