using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour //Joke of a class I know, we can expand on it later
{
    private Messager _messager;
    private int _counter;
    
    void Start()
    {
        _messager = GameObject.Find("Message").GetComponent<Messager>();
        _counter = 0;
    }

    void FixedUpdate()
    {
        _counter++;
        if (_counter == 100)
        {
            _messager.PlayMessage("Joe Portrait", "Joe Biden Speech");
        }
    }
}
