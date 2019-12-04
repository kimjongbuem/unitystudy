using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRain : MonoBehaviour
{
    private WeatherManager _WM; public bool isRain;
    void Start()
    {
        _WM = FindObjectOfType<WeatherManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isRain) _WM.RainDrop();
        else _WM.RainStop();
    }
}
