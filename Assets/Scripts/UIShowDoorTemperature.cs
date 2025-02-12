﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class UIShowDoorTemperature : MonoBehaviour
{
    [SerializeField] Transform pivotTranfromDoor;
    [SerializeField] GameObject txtGameObj;
    [SerializeField] Text txtTemperature;
    [SerializeField] float targetAngle = 10.0f;

    private float startAngle;
    private bool wasGameover = false;

    [Header("Temperature Settings")]
    [SerializeField] float minTemperature =85.00f;
    [SerializeField] float maxTemperature = 92.00f;

    int temperatureValue;
    // Start is called before the first frame update
    void Start()
    {
        startAngle = pivotTranfromDoor.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAngle();
        
    }

    protected virtual void OnHandHoverBegin(Hand hand)
    {
        RandomCelcius();

    }

    protected virtual void HandHoverUpdate(Hand hand)
    {
        txtGameObj.SetActive(true);
    }

    protected virtual void OnHandHoverEnd(Hand hand)
    {
        txtGameObj.SetActive(false);
    }

    void RandomCelcius()
    {
        temperatureValue = Mathf.RoundToInt( Random.Range(minTemperature, maxTemperature) );
        txtTemperature.text = temperatureValue.ToString() + " ºC";
        Invoke("RandomCelcius", 4.0f);
    }

    void CheckAngle()
    {
        if (targetAngle >0)
        {
            if (pivotTranfromDoor.rotation.eulerAngles.y >= startAngle + targetAngle)
            {
                if (!wasGameover)
                {
                    GameController.instanceGame.OnGameOver();
                    wasGameover = true;
                }
            }
        }
        else if (targetAngle < 0)
        {
            if (pivotTranfromDoor.rotation.eulerAngles.y <= startAngle + targetAngle)
            {
                if (!wasGameover)
                {
                    GameController.instanceGame.OnGameOver();
                    wasGameover = true;
                }
            }
        }
        
    }
}
