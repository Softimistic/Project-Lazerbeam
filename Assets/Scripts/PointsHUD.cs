using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsHUD : MonoBehaviour
{
    [SerializeField] private GameObject Health;
    [SerializeField] private GameObject Shield;
    private Health _health;
    private Shield _shield;
    private Text[] _text;

    public GameObject Ship;

    void Start()
    {
        _text = this.GetComponentsInChildren<Text>();
        _health = Health.GetComponent<Health>();
        _shield = Shield.GetComponent<Shield>();
    }

    void FixedUpdate()
    {
        _text[0].text = "0";//Target_Player.SP.ToString(); //Update score text
        _text[1].text = _health.getHealth(); //Update health (structural integrity lower) text
        _text[2].text = _shield.getShield(); //Update shield (structural integrity upper) text
    }
}