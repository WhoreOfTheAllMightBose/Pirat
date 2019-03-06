using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryCard : MonoBehaviour
{
    public bool IsSelected;
    public GameObject tempCard;
    public Vector3[] PossibleMoves;
    Vector3 restartpos;


    public int CurrentX { set; get; }
    public int CurrentY { set; get; }
    public bool IsPlayer1;
    bool isDown = false;
    bool isOver;

    void Start()
    {
    }
    //int CurrentXMax;
    //int CurrentXMin;
    //int CurrentYMax;
    //int CurrentYMin;

    private void Update()
    {
        if (IsSelected)
        {
            //  Vector3 mousepos = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.z);
            //transform.position = Camera.main.WorldToScreenPoint(mousepos) * Time.deltaTime;
            // transform.position = Vector3.Lerp(transform.position, mousepos, 1 * Time.deltaTime);
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            RaycastHit hitD;
            if (!isDown)
            {
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {
                    // print(Input.mousePosition);
                    print(hit.transform.name);
                    Vector3 pos = new Vector3(hit.point.x, 1, hit.point.z);
                    transform.position = pos;
                }
            }
            if (Physics.Raycast(transform.position, Vector3.down, out hitD))
            {
                isOver = true;

                if (Input.GetMouseButtonDown(0))
                {
                    if (hitD.transform.name == "slot1" && IsPlayer1)
                    {

                        isDown = true;
                        print(hitD.transform.name);
                        transform.position = hitD.transform.position;
                        //     turnOfHighLigt();
                    }
                    else if (hitD.transform.name == "slot2" && !IsPlayer1)
                    {
                        isDown = true;
                        print(hitD.transform.name);
                        transform.position = hitD.transform.position;
                    }
                    else
                        isOver = false;
                }
            }
            else
                isOver = false;
        }
    }

    void turnOfHighLigt()
    {
        if (TurnBased.Player1Turn && IsPlayer1)
        {

            GameObject g = GameObject.FindGameObjectWithTag("Player1slots");


                for (int i = 0; i < 4; i++)
                {
                //  g.GetComponentsInChildren<MeshRenderer>().
                }
        }
        else if (!TurnBased.Player1Turn && !IsPlayer1)
        {

            IsSelected = !IsSelected;

            GameObject g = GameObject.FindGameObjectWithTag("Player2slots");

            if (IsSelected)
            {
                restartpos = transform.position;
                for (int i = 0; i < 4; i++)
                {
                    g.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else
            {
                print(restartpos);
                transform.position = restartpos;
                for (int i = 0; i < 4; i++)
                {
                    g.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "slot")
    //    {
    //        transform.position = other.transform.position;
    //    }
    //}

    private void OnMouseDown()
    {
        CurrentX = (int)Input.mousePosition.x;
        CurrentY = (int)Input.mousePosition.y;
        if (!isOver)
        {
            if (TurnBased.Player1Turn && IsPlayer1)
            {
                IsSelected = !IsSelected;

                GameObject g = GameObject.FindGameObjectWithTag("Player1slots");

                if (IsSelected)
                {
                    restartpos = transform.position;
                    for (int i = 0; i < 4; i++)
                    {
                        g.transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
                else
                {
                    transform.position = restartpos;
                    for (int i = 0; i < 4; i++)
                    {
                        g.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
            else if (!TurnBased.Player1Turn && !IsPlayer1)
            {

                IsSelected = !IsSelected;

                GameObject g = GameObject.FindGameObjectWithTag("Player2slots");

                if (IsSelected)
                {
                    restartpos = transform.position;
                    for (int i = 0; i < 4; i++)
                    {
                        g.transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
                else
                {
                    print(restartpos);
                    transform.position = restartpos;
                    for (int i = 0; i < 4; i++)
                    {
                        g.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }

        }
    }

    public void SetPosition(int x, int y)
    {
            CurrentY = y;
            CurrentX = x;
    }

    public virtual bool[,] PossibleMove()
    {
        return new bool[4,2]; // 4 = xles 2 = yled
    }
}
