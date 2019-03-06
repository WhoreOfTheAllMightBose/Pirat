using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBased : MonoBehaviour
{
    public static TurnBased Instance { set; get; }
    public GameObject[] Players;
    public bool[,] allowedMoves { set; get; }
    public TemporaryCard[,] tempCard { set; get; }

    TemporaryCard selectedTempCard;

    int selectx = -1;
    int selecty = -1;

    public static bool Player1Turn = true;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        coloring();
        updateselection();
        move();
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

    #region Skit
    public void Selection(GameObject clone)
    {

        if (selectedTempCard == null)
        {
           // ChoseCard(selectx, selecty,clone);
        }

    }

    void move()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (selectedTempCard == null)
            {
                print("träffade inget kort");
                return;
            }
            else
            {
                moveCard(selectx, selecty);
            }
        }
    }


    void updateselection()
    {

        //if (!FindObjectOfType<GameObject>().tag("EndTurnButton") == null)
        //   return;
        //print("selectx = " + selectx);
        //print("selecty = " + selectx);
        selectx = (int)Input.mousePosition.x;
        selecty = (int)Input.mousePosition.y;
        //RaycastHit hit;
        //if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25f))
        //{
        //    Debug.Log(hit.point);
        //    selectx = (int)hit.point.x;
        //    selecty = (int)hit.point.z;
        //    print(new Vector3(selectx, 0, selecty));
        //}
        //else
        //{
        //    selectx = -1;
        //    selecty = -1;
        //}
    }

    //void ChoseCard(int x, int y,GameObject clone)
    //{
    //    if (tempCard[x, y] == null)
    //    {
    //      //  tempCard = clone;
    //    }

    //    if (tempCard[x, y].IsPlayer1 != Player1Turn)
    //        return;

    //    allowedMoves = tempCard[x, y].PossibleMove();

    //    selectedTempCard = tempCard[x, y];
    //    HighLight.Instance.HighLightAllowedMoves(allowedMoves);

    //}

    void moveCard(int x, int y)
    {
        if (allowedMoves[x, y])
        {
            TemporaryCard c = tempCard[x, y];

            if (c != null && c.IsPlayer1 != Player1Turn)
            {
                if (c.GetType() == typeof(TemporaryCard))
                {
                    print("najs");
                    return;
                }

                //activechessman.Remove(c.gameObject);
                //Destroy(c.gameObject);
            }

            tempCard[selectedTempCard.CurrentX, selectedTempCard.CurrentY] = null;
            selectedTempCard.transform.position = transform.position;
            selectedTempCard.SetPosition(x, y);
            tempCard[x, y] = selectedTempCard;
        }

        //HighLight.Instance.HidehighLigst();

        selectedTempCard = null;
    }
    #endregion

    void OnMouseDown()
    {
        Player1Turn = !Player1Turn;
        CoinScript.RoundCounter++;
        CoinScript.TurnChange = true;
        CoinScript.WhoTurn++;
    }
}
