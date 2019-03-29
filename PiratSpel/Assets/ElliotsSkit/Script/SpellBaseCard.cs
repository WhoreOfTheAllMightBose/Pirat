using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBaseCard : MonoBehaviour
{
    protected int _dmg;
    protected int _heal;
    protected int _cost;
    bool selected;

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
                        if (FindObjectOfType<CardFuntion>().isDown || FindObjectOfType<CardFuntion>() == null)
                        {
                            hit.collider.GetComponent<TempBaseCard2>().TakeDamage(_dmg);
                            hit.collider.GetComponent<TempBaseCard2>().Buff(_heal, "Hp");
                            Destroy(gameObject);
                        }
                    }
                    else
                        selected = false;
                }
            }
        }

    }

    private void OnMouseDown()
    {
        selected = true;
        if(FindObjectOfType<CardFuntion>().IsPlayer1)
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
    }

}
