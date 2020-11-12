using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetect : MonoBehaviour
{
    public int position;
    //      1      
    //2            3
    //      4       

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("ParryObject"))
        {
           // Debug.Log("TriggerEnter");
            transform.parent.GetComponent<EnemyAI>().setDetectArray(position, true);
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("ParryObject"))
        {
         //   Debug.Log("TriggerExit");
            transform.parent.GetComponent<EnemyAI>().setDetectArray(position, false);
        }
    }

}
