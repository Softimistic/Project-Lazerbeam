using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsHUD : MonoBehaviour
{
    public HenkController Target_Player;
    private Text[] _text;

    // Start is ca lled before the first frame update
    void Start()
    {
        _text = GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _text[0].text = Target_Player.SP.ToString();
        _text[1].text = Target_Player.HP.ToString();
    }
}