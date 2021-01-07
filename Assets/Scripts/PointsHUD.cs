using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsHUD : MonoBehaviour
{
    private Health _health;
    private Shield _shield;
    private Text[] _text;

    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _text = this.GetComponentsInChildren<Text>();
        _health = player.GetComponent<Health>();
        _shield = player.GetComponent<Shield>();
    }

    void FixedUpdate()
    {
        _text[0].text = "0";//Target_Player.SP.ToString(); //Update score text
        _text[1].text = _health.GetHealth(); //Update health (structural integrity lower) text
        _text[2].text = _shield.getShield(); //Update shield (structural integrity upper) text
    }
}