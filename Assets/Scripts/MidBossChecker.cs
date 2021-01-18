using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBossChecker : MonoBehaviour
{
    public GameObject eyeball;
 
    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 1)
        {
            eyeball.GetComponent<MeshCollider>().enabled = true;
        }
    }
}
