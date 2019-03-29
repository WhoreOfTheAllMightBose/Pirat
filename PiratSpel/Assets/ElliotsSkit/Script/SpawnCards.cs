using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class SpawnCards : MonoBehaviour
{
    public GameObject[] P1Playingcards;
    public GameObject[] P2Playingcards;
    public static List<GameObject> CardsP1;
    public static List<GameObject> CardsP2;
    GameObject g;
    Vector3 spawnpos;
    int rand;
    // Start is called before the first frame update
    void Start()
    {
        CardsP1 = new List<GameObject>();
        CardsP2 = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        RefreshCardPos();

    }

    GameObject randCard()
    {
        if(!TurnBased.Player1Turn)
        {
            rand = Random.Range(0, P1Playingcards.Length);
            return P1Playingcards[rand];
        }
        else
        {
            rand = Random.Range(0, P2Playingcards.Length);
            return P2Playingcards[rand];
        }
    }

    public void RefreshCardPos()
    {
        if (TurnBased.Player1Turn)
        {
            for (int i = 0; i < CardsP1.Count; i++)
            {
                //if (CardsP1[i].GetComponent<CardFuntion>().isDown)
                //    CardsP1.Remove(CardsP1[i]);
                if(CardsP1.Count > 7 && i >= 7)
                    CardsP1[i].GetComponent<TempBaseCard2>().respawn(new Vector3(-10 + P1Playingcards[0].transform.localScale.x * 3.5f * i, 0, -5f));

                CardsP1[i].GetComponent<TempBaseCard2>().respawn(new Vector3(-10 + P1Playingcards[0].transform.localScale.x * 3.5f * i, 0, -0.8f));
            }


        }

        if (!TurnBased.Player1Turn)
        {
            for (int i = 0; i < CardsP2.Count; i++)
            {
                //if (CardsP2[i].GetComponent<CardFuntion>().isDown)
                //    CardsP2.Remove(CardsP1[i]);

                if(CardsP2.Count > 7 && i >= 7)
                    CardsP2[i].GetComponent<TempBaseCard2>().respawn(new Vector3(12 - P2Playingcards[0].transform.localScale.x * 3.5f * i, 0, 27));
                CardsP2[i].GetComponent<TempBaseCard2>().respawn(new Vector3(12 - P2Playingcards[0].transform.localScale.x * 3.5f * i, 0, 22));
            }
        }
    }


    private void OnMouseDown()
    {
        if (TurnBased.Player1Turn && CoinScript.CoinAmountP1 > 0)
        {
            CoinScript.CoinAmountP1--;
            if(CardsP1.Count < 7)
                 spawnpos = new Vector3(-10 + P1Playingcards[0].transform.localScale.x * 3.5f * CardsP1.Count, 0, -0.8f);
            else
                spawnpos = new Vector3(-10 + P1Playingcards[0].transform.localScale.x * 3.5f * CardsP1.Count, 0, -5f);

            g = randCard();
            CardsP1.Add(Instantiate(g, spawnpos, Quaternion.identity));

        }

        if (!TurnBased.Player1Turn && CoinScript.CoinAmountP2 > 0)
        {
            CoinScript.CoinAmountP2--;
            if(CardsP2.Count < 7)
                spawnpos = new Vector3(12 - P2Playingcards[0].transform.localScale.x * 3.5f * CardsP2.Count, 0, 22);
            else
                spawnpos = new Vector3(12 - P2Playingcards[0].transform.localScale.x * 3.5f * CardsP2.Count, 0, 27);

            g = randCard();
            CardsP2.Add(Instantiate(g, spawnpos, Quaternion.identity));
        }
    }
}
