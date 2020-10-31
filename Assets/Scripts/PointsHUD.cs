using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsHUD : MonoBehaviour
{
    public HenkController Target_Player;
    private Text[] _text;

    void Start()
    {
        _text = GetComponentsInChildren<Text>();
    }

    void FixedUpdate()
    {
        _text[0].text = Target_Player.SP.ToString(); //Update score text
        _text[1].text = Target_Player.HP.ToString(); //Update health (structural integrity) text
    }
}