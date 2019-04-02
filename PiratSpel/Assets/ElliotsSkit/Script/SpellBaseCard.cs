using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBaseCard : MonoBehaviour
{
    protected int _dmg;
    protected int _heal;
    protected int _cost;
    bool selected;
    bool p1turn;

    // Update is called once per frame
    void Update()
    {
        if(selected)
        {
            GetComponent<SpriteRenderer>().material.color = Color.red;

            if (Input.GetMouseButtonDown(0) && selected)
            {
                Ray castPoint;
                RaycastHit hit; // om du träffar ett kort du ska välja
                castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {
                    if (hit.collider.tag == "MinionCard" || hit.collider.tag == "Player")
                    {
                        if (TurnBased.Player1Turn)
                        {
                            if (CoinScript.CoinAmountP1 - _cost >= 0)
                            {
                                CoinScript.CoinAmountP1 -= _cost;
                            }
                        }
                        else
                        {
                            if (CoinScript.CoinAmountP2 - _cost >= 0)
                            {
                                CoinScript.CoinAmountP2 -= _cost;
                            }
                        }
                        if (hit.collider.name == "Player2") // om du träffade spelares 2 hero ska spelare 1 ta skada och kortet ska inte kunna anfalla igen
                        {
                            HeroScript.Hero2Health -= _dmg;
                        }
                        else if (hit.collider.name == "Player1")
                        {
                            HeroScript.Hero1Health -= _dmg;
                        }
                        else if (hit.collider.gameObject.GetComponent<CardFuntion>().isDown)
                        {
                            if (hit.collider.gameObject.GetComponent<CardFuntion>().IsPlayer1)
                                p1turn = true;
                            else
                                p1turn = false;

                            hit.collider.GetComponent<TempBaseCard2>().TakeDamage(_dmg);
                            hit.collider.GetComponent<TempBaseCard2>().Buff(_heal, "Hp");
                        }
                      
                        Destroy(gameObject);
                    }
                  

                }
                else
                    selected = false;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().material.color = Color.white;
        }

    }

    private void OnMouseDown()
    {
        selected = true;
    }

}
