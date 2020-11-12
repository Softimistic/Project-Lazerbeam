using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsHUD : MonoBehaviour
{
    private Health health;
    private Text[] _text;

    public GameObject Ship;

    void Start()
    {
        _text = this.GetComponentsInChildren<Text>();
        health = GameObject.Find("Health").GetComponent<Health>();
    }

    void FixedUpdate()
    {
        _text[0].text = "0";//Target_Player.SP.ToString(); //Update score text
        _text[1].text = health.getHealth(); //Update health (structural integrity) text
    }
}