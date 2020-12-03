using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour //Joke of a class I know, we can expand on it later
{
    private Messager _messager;
    private int _counter;

    private string[] currentPortrait_Message = { "", "" };

    void Start()
    {
        _messager = GameObject.Find("Message").GetComponent<Messager>();
        _counter = 0;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("x"))
        {
            _messager.StopMessage(currentPortrait_Message[0], currentPortrait_Message[1]);
        }


        _counter++;
        if (_counter == 100)
        {
            _messager.StopMessage(currentPortrait_Message[0], currentPortrait_Message[1]);
            currentPortrait_Message[0] = "Joe Portrait";
            currentPortrait_Message[1] = "Joe Biden Speech";
            _messager.PlayMessage(currentPortrait_Message[0], currentPortrait_Message[1]);
        }
    }
}
