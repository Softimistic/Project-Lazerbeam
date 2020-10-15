using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)]  float lerpTime;
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
            angled += (angularSpeed * Time.deltaTime) % 360;
            newPosition.x = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);//x position
            newPosition.y = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);//y position
            newPosition.z = plane.transform.position.z + targetDistance;
            transform.position = newPosition ;
             //transform.Rotate(rotAxis, degPerSec * Time.deltaTime);
        }
        else
        {
            transform.position += transform.forward * enemySpeed * Time.deltaTime;
        }
    }
}