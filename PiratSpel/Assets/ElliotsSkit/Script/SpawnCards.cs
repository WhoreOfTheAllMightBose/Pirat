using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCards : MonoBehaviour
{
    public GameObject[] P1Playingcards;
    public GameObject[] P2Playingcards;
    public static List<GameObject> CardsP1;
    public static List<GameObject> CardsP2;
    GameObject g;
    Vector3 spawnpos;
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

    private void OnMouseDown()
    {
        //print(CardsP1.Count);
        //print(CardsP2.Count);
        if (TurnBased.Player1Turn)
        {
            GameObject g;
                spawnpos = new Vector3(-10 + P1Playingcards[0].transform.localScale.x  *3* CardsP1.Count, 0, -0.8f);
                g = P1Playingcards[0];
                CardsP1.Add(g);
                Instantiate(g, spawnpos, Quaternion.identity);
           
         //   GetComponent<TemporaryCard>().CardsP1.Add(g);
        }

        if (!TurnBased.Player1Turn)
        {
            GameObject g;
                spawnpos = new Vector3(12 - P2Playingcards[0].transform.localScale.x * 3 * CardsP2.Count, 0, 22);
                g = P2Playingcards[0];
                CardsP2.Add(g);
                Instantiate(g,spawnpos,Quaternion.identity);
        }
    }
}
