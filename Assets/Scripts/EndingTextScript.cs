using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTextScript : SceneTransitionEvent
{
    AudioSource myAudio;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.time = 1.8f;
        Invoke("AudioPlayTimer", 9.0f);
        Invoke("LoadMenuScene", 19.0f);
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void AudioPlayTimer()
    {
        myAudio.Play();
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(TargetScene);
    }
}
