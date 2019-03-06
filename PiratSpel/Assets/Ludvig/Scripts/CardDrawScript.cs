using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrawScript : MonoBehaviour
{

    private bool DrawCard;
    
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        if (CoinScript.WhoTurn == 0)
        {
            CoinScript.CoinAmountP1--;
            DrawCard = true;
        }
        if (CoinScript.WhoTurn == 1)
        {
            CoinScript.CoinAmountP2--;
            DrawCard = true;
        }
    }

    private void DrawAcard()
    {

        //DrawCardLogic

    }
    
    void Update()
    {
        if (DrawCard == true)
        {
            DrawAcard();
            DrawCard = false;
        }


    }
}
