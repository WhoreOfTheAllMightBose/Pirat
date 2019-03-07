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

        spawnpos = new Vector3(-10, 0, -0.8f);
        g = P1Playingcards[0];
        CardsP1.Add(g);

        spawnpos = new Vector3(-10, 0, 22);
        g = P2Playingcards[0];
        CardsP2.Add(g);
       // Instantiate(g);
    }

    // Update is called once per frame
    void Update()
    {
        print(CardsP1.Count);
        print(CardsP2.Count);
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

    private void OnMouseDown()
    {
        //print(CardsP1.Count);
        //print(CardsP2.Count);
        if (TurnBased.Player1Turn && CoinScript.CoinAmountP1 > 0)
        {
            CoinScript.CoinAmountP1--;
            spawnpos = new Vector3(-10 + P1Playingcards[0].transform.localScale.x * 3.5f * CardsP1.Count, 0, -0.8f);
            //for (int i = 0; i < CardsP1.Count; i++)
            //{
            //    if (i + 2 <= CardsP1.Count)
            //    {
            //        float temp = Vector3.Distance(CardsP1[i].transform.position, CardsP1[i + 1].transform.position);
            //        if (temp < 74)
            //        {
            //            spawnpos = new Vector3(-10 + P1Playingcards[i].transform.localScale.x * 3.5f * i, 0, -0.8f);
            //        }
            //    }
            //}
            g = randCard();
            CardsP1.Add(g);
            Instantiate(g, spawnpos, Quaternion.identity);

            //   GetComponent<TemporaryCard>().CardsP1.Add(g);
        }

        if (!TurnBased.Player1Turn && CoinScript.CoinAmountP2 > 0)
        {
            CoinScript.CoinAmountP2--;
            spawnpos = new Vector3(12 - P2Playingcards[0].transform.localScale.x * 3.5f * CardsP2.Count, 0, 22);
            g = randCard();
            CardsP2.Add(g);
            Instantiate(g, spawnpos, Quaternion.identity);
        }
    }
}
