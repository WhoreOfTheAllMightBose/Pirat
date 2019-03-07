using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class TempBaseCard : MonoBehaviour
{
    protected int _Attack; //bas attack
    protected int _Hp; // bas hp
    public int _Cost; //bas kostnad
    static bool attacking = true; // om den ska kunna attackera
    static int _amountOfDmg; // hur mycket skada fienden ska ta(och att bara en ska kunna nfalla åt gången)

    public static int ID; // vart id ligger på just nu
    int thisID; // vad detta obj har för id
    static int attackersID; //ser vem som attackerar
    bool growing = true; //så att den växer och minskar när man har tryckt på ett kort
    bool startGrowing; // när den ska börja växa
    public bool hasattackt = false; // så att ett kort bara kan attakera 1gng
    public bool isPlayer1; // om kortet tillhör spelare 1
    // Start is called before the first frame update
    public virtual void Start()
    {
        ID++; // så att max id ökar
        thisID = ID; // ger detta kort sitt id
        if (!TurnBased.Player1Turn) // ifall det inte är spelare1 runda ska kortet inte tillhöra spelare 1
        {
            isPlayer1 = false;
        }
        else// ifall det inte är spelare2 runda ska kortet inte tillhöra spelare 2
        {
            isPlayer1 = true;
        }
        print("this card is nr: " + ID);
    }
    public virtual void Update()
    {

        if (startGrowing && !hasattackt) // så att kortet börjar växa
        {
            grow();
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

    void OnMouseDown()
    {
        if (TurnBased.Player1Turn && GetComponent<CardFuntion>().IsPlayer1)
        {
            if (attacking && GetComponent<CardFuntion>().isDown && !hasattackt)
            {
                attacking = !attacking;
               // hasattackt = false;
                _amountOfDmg = _Attack;
                attackersID = ID;
                startGrowing = true;
            }
        }
        else if (!TurnBased.Player1Turn && !GetComponent<CardFuntion>().IsPlayer1)
        {
            if (attacking && GetComponent<CardFuntion>().isDown && !hasattackt)
            {
                attacking = !attacking;
              //  hasattackt = false;
                _amountOfDmg = _Attack;
                attackersID = ID;
                startGrowing = true;
            }

        }
        else
        {
            if (!attacking && GetComponent<CardFuntion>().isDown)
            {
                if (TurnBased.Player1Turn)
                {
                    for (int i = 0; i < ID; i++)
                    {
                        if (isPlayer1)
                        {
                            hasattackt = false;
                        }
                    }
                }
                if (!TurnBased.Player1Turn)
                {
                    for (int i = 0; i < ID; i++)
                    {
                        if (!isPlayer1)
                        {
                            hasattackt = false;
                        }
                    }
                }

                attacking = !attacking;
                startGrowing = false;
                hasattackt = true;
                transform.localScale = new Vector3(1, 1, 1);
                attackersID = -1;
                TakeDamage(_amountOfDmg);
            }
            //for (int i = 0; i < ID; i++)
            //{
                
            //}
        }
    }


    /// <summary>
    /// Om det är en debuff så skriv -x
    /// </summary>
    /// <param name="Ad"></param>
    /// <param name="Hp"></param>
    /// <param name="Co"></param>
    public void Buff(int Ad,int Hp, int Co)
    {
        _Attack += Ad;
        _Hp += Hp;
        _Cost += Co;
    }

    public virtual void TakeDamage(int Dmg)
    {
        _Hp -= Dmg;
        print(ID + " My Hp is: "+_Hp);
        _amountOfDmg = 0;
        if (_Hp <= 0)
            Destroy(gameObject);
    }

    public virtual void StartingStats(int Ad,int Hp,int Co)
    {
        _Attack = Ad;
        _Hp = Hp;
        _Cost = Co;
    }
}
