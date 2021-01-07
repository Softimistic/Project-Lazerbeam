using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildDeathChecker : SceneTransitionEvent
{
    [Tooltip("Disable GameObject instead of loading scene")] [SerializeField] public bool DisableMode;
    private int _counter = 0;

    void FixedUpdate()
    {
        if (GetComponentsInChildren<Transform>().Length == 1)
        {
            if (DisableMode)
            {
                this.gameObject.SetActive(false);
            }

            if (_counter > 100)
            { 
                FindObjectOfType<AudioManager>().PauseAll();
                LoadNextScene();
            }
            _counter++;
        }
    }
}
