﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{

    private float scrW;
    private float scrH;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 10;
        GUI.BeginGroup(new Rect(scrW * 12, 0, scrW * 4.18f, scrH * 10.05f));
        
        GUI.Box(new Rect(0, 0, scrW * 4.18f, scrH * 10.05f), "");

        GUI.Box(new Rect(0, 0, scrW * 4.18f, scrH), "Buy Towers");

        if (GUI.Button(new Rect(scrW, scrH * 2, scrW, scrH), "1"))
        {

        }

        if (GUI.Button(new Rect(scrW, scrH * 3.5f, scrW, scrH), "2"))
        {

        }

        if (GUI.Button(new Rect(scrW, scrH * 5, scrW, scrH), "3"))
        {

        }

        GUI.EndGroup();
    }
}
