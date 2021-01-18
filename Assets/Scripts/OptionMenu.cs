using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetVolume(float volume)
    {
        FindObjectOfType<AudioManager>().SetVolume(volume);
    }
}
