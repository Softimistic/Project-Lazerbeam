using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Briefing : SceneTransitionEvent
{
    [Tooltip("Drag messager from UI here")] public Messager Messager; //Assign in editor 
    private MessageEvent[] _messageEvents; //Assign in editor by adding children gameObjects
    private int _counter = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        _messageEvents = GetComponentsInChildren<MessageEvent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_counter == 0)
        {
            _messageEvents[_counter].Play();
            _counter++;
        }
        else if (!Messager.IsMessageActive())
        {
            if (_messageEvents.Length == _counter)
            {
                LoadNextScene();
            }
            else
            {
                _messageEvents[_counter].Play();
                _counter++;
            }
        }
    }
}
