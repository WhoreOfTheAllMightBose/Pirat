using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBased : MonoBehaviour
{

    public static TurnBased Instance { set; get; }
    public GameObject[] Players;
    bool[] allowedMoves { set; get; }

    Vector3 v3out = Vector3.zero;
 

    public static bool Player1Turn = true;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnMouseDown()
    {
        if(Players.Length == 2)
        {
            if (Player1Turn)
            {
                Players[0].GetComponent<MeshRenderer>().material.color = Color.red;
                Players[1].GetComponent<MeshRenderer>().material.color = Color.white;
                Player1Turn = false;
            }
            else
            {
                Players[1].GetComponent<MeshRenderer>().material.color = Color.red;
                Players[0].GetComponent<MeshRenderer>().material.color = Color.white;
                Player1Turn = true;
            }

            Debug.Log("Mouse is over GameObject. " + gameObject.name);
        }
    }
}
