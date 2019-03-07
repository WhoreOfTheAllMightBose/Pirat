using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class TempBaseCard2 : MonoBehaviour
{

    protected int _Attack; //bas attack
    protected int _Hp; // bas hp
    public int _Cost; //bas kostnad
    protected bool isPlayer1;

    bool growing = true; //så att den växer och minskar när man har tryckt på ett kort
    bool hasAttackt;

    public static int _ID; // vart id ligger på just nu
    int thisID; // vad detta obj har för id
    bool attacking;
    static int _amountOfDmg; // hur mycket skada fienden ska ta(och att bara en ska kunna anfalla åt gången)
    static int _amountOfDmgReseve;
    static GameObject cardThatTakeDmg;

    public virtual void Start()
    {
        _ID++; // så att max id ökar
        thisID = _ID; // ger detta kort sitt id
        
        if (!TurnBased.Player1Turn) // ifall det inte är spelare1 runda ska kortet inte tillhöra spelare 1
        {
            isPlayer1 = false;
        }
        else// ifall det inte är spelare2 runda ska kortet inte tillhöra spelare 2
        {
            isPlayer1 = true;
        }
        print("this card is nr: " + _ID);
    }

    public virtual void Update()
    {

        for (int i = 0; i < 3; i++)
        {
            if (GetComponent<CardFuntion>().isDown)
            {
                if (i == 0)
                {

                    transform.GetChild(0).GetComponentInChildren<TextMesh>().text = _Hp.ToString();
                }
                if (i == 1)
                {
                    transform.GetChild(1).GetComponentInChildren<TextMesh>().text = _Attack.ToString();
                }
                if(i == 2)
                {
                    transform.GetChild(2).gameObject.SetActive(false);
                }
            }
            else
            {
                if (i == 0)
                {

                    transform.GetChild(0).GetComponentInChildren<TextMesh>().text = _Hp.ToString();
                }
                if (i == 1)
                {
                    transform.GetChild(1).GetComponentInChildren<TextMesh>().text = _Attack.ToString();
                }
                if (i == 2)
                {
                    transform.GetChild(2).GetComponentInChildren<TextMesh>().text = _Cost.ToString();
                }

            }
        }

        if (_amountOfDmg > 0 && attacking)
            grow();
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            attacking = false;
        }
        print("attacking = " + attacking);

        if (hasAttackt && GetComponent<CardFuntion>().isDown)
        {
            GetComponent<SpriteRenderer>().material.color = Color.grey;
        }
        else if(!hasAttackt && GetComponent<CardFuntion>().isDown)
        {
            GetComponent<SpriteRenderer>().material.color = Color.white;
        }

        if (!TurnBased.Player1Turn && isPlayer1)
        {
            attacking = false;
            hasAttackt = false;
        }

        else if (TurnBased.Player1Turn && !isPlayer1)
        {
            attacking = false;
            hasAttackt = false;
        }

    }

    void grow()
    {
        if (growing) // börjar på true
        {
            transform.localScale += transform.localScale * Time.deltaTime / 2; // hur snabbt den ska öka 
            if (transform.localScale.x > 1.5f) // max gräns
            {
                growing = !growing; // gör så kortet krymper igen
            }
        }
        else
        {
            transform.localScale -= transform.localScale * Time.deltaTime / 2; // hur snabbt kortet ska krympa
            if (transform.localScale.x < 1f)
            {
                growing = !growing; // gör så kortet växer igen
            }
        }
    }

    private void OnMouseDown()
    {
        if(GetComponent<CardFuntion>().isDown)
        {
          

            if (TurnBased.Player1Turn && isPlayer1 && !hasAttackt)
            {
                cardThatTakeDmg = this.gameObject;
                _amountOfDmg = _Attack;
                hasAttackt = true;
                attacking = true;
            }

            else if (!TurnBased.Player1Turn && !isPlayer1 && !hasAttackt)
            {
                cardThatTakeDmg = this.gameObject;
                _amountOfDmg = _Attack;
                hasAttackt = true;
                attacking = true;
            }
            else if(!TurnBased.Player1Turn && isPlayer1)
            {
                TakeDamage(_amountOfDmg);
                _amountOfDmg = _Attack;
                cardThatTakeDmg.GetComponent<TempBaseCard2>().TakeDamage(_amountOfDmg);
                _amountOfDmg = 0;
            }
        
            else if (TurnBased.Player1Turn && !isPlayer1)
            {
                TakeDamage(_amountOfDmg);
                _amountOfDmg = _Attack;
                cardThatTakeDmg.GetComponent<TempBaseCard2>().TakeDamage(_amountOfDmg);
                _amountOfDmg = 0;
            }
        }
        else
        {
            hasAttackt = true;
        }
    }

    private void OnMouseOver()
    {
        if (!GetComponent<CardFuntion>().isDown)
        {
            GetComponent<SpriteRenderer>().material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().material.color = Color.white;
    }

    /// <summary>
    /// Om det är en debuff så skriv -x
    /// </summary>
    /// <param name="Ad"></param>
    /// <param name="Hp"></param>
    /// <param name="Co"></param>
    public void Buff(int Ad, int Hp, int Co)
    {
        _Attack += Ad;
        _Hp += Hp;
        _Cost += Co;
    }

    public virtual void TakeDamage(int Dmg)
    {
        _Hp -= Dmg;
        print(_ID + " My Hp is: " + _Hp);
        _amountOfDmg = 0;
        if (_Hp <= 0)
            Destroy(gameObject);
    }

    public virtual void StartingStats(int Ad, int Hp, int Co)
    {
        _Attack = Ad;
        _Hp = Hp;
        _Cost = Co;
    }
}
