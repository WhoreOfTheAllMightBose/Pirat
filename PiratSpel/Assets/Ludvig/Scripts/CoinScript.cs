﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    public static bool TurnChange;
    public static int WhoTurn;
    public static int CoinAmountP1;
    public static int CoinAmountP2;
    public static int RoundCounter;
    public static int CoinGain;
    public Text coinsp1;
    public Text coinsp2;
    //public Text DebugingText;
    // Start is called before the first frame update
    void Start()
    {
        CoinGain = 1;
        CoinAmountP1 = 5;
        CoinAmountP2 = 5;
        WhoTurn = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        coinsp1.text = "" + CoinAmountP1;
        coinsp2.text = "" + CoinAmountP2;

        if (RoundCounter >= 6)
        {
            CoinGain++;
            RoundCounter = 0;
        }
        if (WhoTurn >= 2)
        {
            WhoTurn = 0;
        }

        if (TurnChange == true && WhoTurn == 0)
        {
            CoinAmountP1 += CoinGain;
            TurnChange = false;
        }
        if (TurnChange == true && WhoTurn == 1)
        {
            CoinAmountP2 += CoinGain;
            TurnChange = false;
        }

    }
}
