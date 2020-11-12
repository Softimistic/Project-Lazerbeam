using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsHUD : MonoBehaviour
{
    public Health health;
    private Text[] _text;

    void Start()
    {
        _text = GetComponentsInChildren<Text>();
    }

    void FixedUpdate()
    {
        _text[0].text = "0";//Target_Player.SP.ToString(); //Update score text
        _text[1].text = health.getHealth().ToString(); //Update health (structural integrity) text
    }
}