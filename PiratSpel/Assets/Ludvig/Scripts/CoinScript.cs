using System.Collections;
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
<<<<<<< HEAD
    public static int debugg;
    public Text DebugingText;
=======
    public Text coins;
    //public Text DebugingText;
>>>>>>> 68076b2ec47b51ae20d3e2666c25fc02a60eebe7
    // Start is called before the first frame update
    void Start()
    {
        CoinAmountP1 = 5;
        CoinAmountP2 = 5;
        WhoTurn = 0;
        CoinGain = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
<<<<<<< HEAD
        DebugingText.text = "" + CoinAmountP1;
        DebugingText.text = "" + CoinAmountP2;
        if (RoundCounter == 6)
=======

        coins.text = "" + CoinAmountP1;

        if (RoundCounter >= 6)
>>>>>>> 68076b2ec47b51ae20d3e2666c25fc02a60eebe7
        {
            CoinGain++;
            RoundCounter = 0;
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
<<<<<<< HEAD
            WhoTurn = 0;
        }
        if (WhoTurn == 2)
        {
            WhoTurn = 0;
        }

        
        


        Debug.Log("" + debugg);
=======
        }

>>>>>>> 68076b2ec47b51ae20d3e2666c25fc02a60eebe7
    }
}
