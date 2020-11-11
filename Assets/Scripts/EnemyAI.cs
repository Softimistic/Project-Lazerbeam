using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] float lerpTime;
    [SerializeField] Color[] finalColors;
    public float targetDistance = 10.0f;
    public float enemySpeed = 0.0f;
    public GameObject plane; //The object that AI around 
    public float angularSpeed;//sp
    public float aroundRadius;//Radius
    [Tooltip("Angular velocity in degrees per seconds")]
    public float degPerSec = 60.0f;
    [Tooltip("Rotation axis")]
    public Vector3 rotAxis = Vector3.up;
    private float angled;

    private int colotIndex;

    private float t = 0f;

    private int len;

    private MeshRenderer _meshRenderer;

    private bool[] detect_array = {false, false, false, false, false};
    //      0      
    //1     2      3
    //      4       
    //DEZE MOETEN ANDERS WANT HET SCHIP BEWEEGT TOCH ZEIWAARDS HOND
   

    // Start is called before the first frame update
     void Start()
     {
         _meshRenderer = gameObject.GetComponent<MeshRenderer>();
         len = finalColors.Length;
     }

    void Update()
    {
        _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color, finalColors[colotIndex],
            lerpTime * Time.deltaTime);
        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            colotIndex++;
            colotIndex = (colotIndex >= len) ? 0 : colotIndex;
        }
    }

    void LateUpdate()
    {
        if (transform.position.z - plane.transform.position.z <= targetDistance)
        {
            Vector3 newPosition = transform.position;
            Vector3 checkedPosition = transform.position;

            angled += (angularSpeed * Time.deltaTime) % 360;
            newPosition.x = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);//x position, Als deze hoger word gaat hij naar Rechts, lager = naar links                      
            newPosition.y = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);//y position, Als deze hoger word gaat hij naar Omhoog, lager = omlaag    





            //check aids 

            //aids = stilstaan

            if (!detect_array[2] && (
               (newPosition.y > transform.position.y && detect_array[0]) ||//als omhoog gaat (dus naar 0)
               (newPosition.y < transform.position.y && detect_array[4]) ||//als omglaag gaat && Naar links(dus naar 4)

               (newPosition.x > transform.position.x && detect_array[3])||//Naar rechts(dus naar 3)
               (newPosition.x < transform.position.x && detect_array[4])//Naar links(dus naar 1)
           ))
            {
                angled -= (angularSpeed * Time.deltaTime) % 360;
            }


            //Geen aids
            else
            {
                checkedPosition = newPosition;
                
            }
            checkedPosition.z = plane.transform.position.z + targetDistance;
            transform.position = checkedPosition;
        }
        else
        {
            transform.position += transform.forward * enemySpeed * Time.deltaTime;
        }


       
    }
    public void setDetectArray(int detectPosition, bool isEnter)
     {
      

     
        detect_array[detectPosition] = isEnter;

  //      UnityEngine.Debug.Log(detect_array[0] + " " + detect_array[1] + " " + detect_array[2] + " " + detect_array[3] + " " + detect_array[4]);


        //detect_array[0]+" "+ detect_array[1] + " " + detect_array[2] + " " + 
        //Makes sure that (late)Update doesnt get called for this period

    }

}