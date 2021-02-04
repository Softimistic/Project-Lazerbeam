using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldChecker : MonoBehaviour
{
    public GameObject Boostbar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.childCount == 0)
        {
            //button destroyed
            gameObject.SetActive(false);
            FindObjectOfType<PlayerController>().BossMode = true;
            Boostbar.SetActive(false);
        }
    }
}
