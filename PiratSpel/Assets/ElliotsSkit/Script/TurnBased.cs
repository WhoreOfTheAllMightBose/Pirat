﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBased : MonoBehaviour
{
    public GameObject[] Players;
    //public TemporaryCard[,] tempCard { set; get; }

    //TemporaryCard selectedTempCard;

    public static bool Player1Turn = true;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        coloring();
    }

    void coloring()
    {
        if (Players.Length == 2)
        {
            if (Player1Turn)
            {
                Players[0].GetComponent<MeshRenderer>().material.color = Color.red;
                Players[1].GetComponent<MeshRenderer>().material.color = Color.white;
            }
            else
            {
                Players[1].GetComponent<MeshRenderer>().material.color = Color.red;
                Players[0].GetComponent<MeshRenderer>().material.color = Color.white;
            }

           // Debug.Log("Mouse is over GameObject. " + gameObject.name);
        }
    }
    void OnMouseDown()
    {
        Player1Turn = !Player1Turn; // byter runda
        
        if(Player1Turn)
        {
            for (int i = 0; i < TempBaseCard.ID ; i++)
            {
                if(GetComponent<TempBaseCard>().isPlayer1)
                {
                    GetComponent<TempBaseCard>().hasattackt = false;
                }
            }
        }
        if (!Player1Turn)
        {
            for (int i = 0; i < TempBaseCard.ID; i++)
            {
                if (!GetComponent<TempBaseCard>().isPlayer1)
                {
                    GetComponent<TempBaseCard>().hasattackt = false;
                }
            }
        }

        CoinScript.RoundCounter++;
        CoinScript.TurnChange = true;
        CoinScript.WhoTurn++;
    }
}
