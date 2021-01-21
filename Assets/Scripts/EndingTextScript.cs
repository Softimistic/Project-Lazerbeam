using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTextScript : MonoBehaviour
{
    AudioSource myAudio;
    public float time = 9f;
    bool audioplaying = false;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.time = 1.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else if(time <= 0 && audioplaying == false)
        {
            Debug.Log("Play Audio Here -- Timer Over!!");
            myAudio.Play();
            audioplaying = true;
        }

    }
}
