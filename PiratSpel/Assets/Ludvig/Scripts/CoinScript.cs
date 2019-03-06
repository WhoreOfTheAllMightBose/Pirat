using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CoinScript : NetworkBehaviour
{
    public static bool TurnChange;
    public static int WhoTurn;
    public static int CoinAmountP1;
    public static int CoinAmountP2;
    public static int RoundCounter;
    public static int CoinGain;
    public Text DebugingText;
    // Start is called before the first frame update
    void Start()
    {
        CoinAmountP1 = 5;
        CoinAmountP2 = 5;
        WhoTurn = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        DebugingText.text = "" + CoinAmountP1;

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
        }
        if (TurnChange == true && WhoTurn == 1)
        {
            CoinAmountP2 += CoinGain;
        }




    }
}
