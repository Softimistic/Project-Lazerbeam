using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class MessageEvent : MonoBehaviour
{
    [Tooltip("Drag messager from UI here")] public Messager Messager; //Assign in editor

    [Header("Message that will be played when triggered")]
    [Tooltip("Portrait that will be shown during message")] public Sprite Portrait; //Assign in editor
    [Tooltip("Message audio")] public AudioClip Audio; //Assign in editor

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Messager.PlayMessage(Portrait, Audio);
            this.enabled = false;
        }
    }
}
