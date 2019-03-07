﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject LookAtThisThing;
    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    public int Where;
    public static bool camchange;
    public static bool camchange2;
    public Camera cam1;
    public static int blackouttimer;
    public static int blackouttimer2;
    void Start()
    {
        LookAtThisThing = GameObject.FindGameObjectWithTag("LookAtThis");
        Where = 0;
        camchange = false;
    }
    
    
    void Update()
    {
        transform.LookAt(LookAtThisThing.transform.position);   

        if (CoinScript.TurnChange == true)
        {
            for (int i = 0; i < 1; i++)
            {
                Where++;
            }
        }

        if (camchange == true)
        {
            blackouttimer++;
        }
        if (blackouttimer >= 1)
        {
            cam1.enabled = false;
            camchange2 = true;
        }
        if (camchange2 == true)
        {
            blackouttimer2++;
        }
        if (blackouttimer2 >= 200)
        {
            camchange = false;
        }
        if (camchange == false && blackouttimer >= 1)
        {
            blackouttimer2 = 0;
            blackouttimer = 0;
            cam1.enabled = true;
        }

        if (Where == 0)
        {
            transform.position = SpawnPoint1.transform.position;
        }
        if (Where == 1)
        {
            transform.position = SpawnPoint2.transform.position;
        }
        if (Where == 2)
        {
            Where = 0;
        }



    }
}
