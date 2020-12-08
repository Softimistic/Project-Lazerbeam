using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messager : MonoBehaviour
{
    private AudioSource _audioSource;
    private Image _frame;
    private Image _portrait;
    private bool _active; //Wether or not a message is currently playing

    void Start()
    {
        _frame = GetComponent<Image>();
        _portrait = GetComponentsInChildren<Image>()[1];
        _audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if ((_active && !_audioSource.isPlaying) || _active && Input.GetKeyDown("x"))
        {
            StopMessage();
        }
    }
 
    /// <summary>
    /// Show portrait and play message audio
    /// </summary>
    public void StopMessage()
    {
        _active = false;
        _frame.enabled = false;
        _portrait.enabled = false;
        _audioSource.Stop();
    }
    public void PlayMessage(Sprite portrait, AudioClip audio)
    {
        _active = true;
        _frame.enabled = true;
        _portrait.enabled = true;
        _portrait.sprite = portrait;//Resources.Load<Sprite>(portraitFileName);
        _audioSource.clip = audio;//Resources.Load<AudioClip>(audioFileName);
        _audioSource.Play();
    }
}