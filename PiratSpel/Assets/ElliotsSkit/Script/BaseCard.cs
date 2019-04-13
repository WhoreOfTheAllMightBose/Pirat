using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard : MonoBehaviour
{
    protected AudioClip _SpawnSound; // när du lägger ut detta kort gör det detta ljud
    protected AudioSource audioS = new AudioSource(); // så att dem kan spela ljudet

    protected int _Attack; //bas attack
    protected int _Hp; // bas hp
    public int _Cost; //bas kostnad
    protected bool _IsPlayer1; // if this card is for player 1
    protected static bool _P1HasTaunt;
    protected static bool _P2HasTaunt;

    bool hasAttackt; // om kortet har redan attackerat(så det bara kan attackera en gng)
    bool playOnce = true; //ifall du har lagt ut kortet ska den inte kunna anfalla och ska spela spawn ljudet
    bool attacking; // ifall kortet är i attack läge

    protected static int _amountOfDmg; // hur mycket skada fienden ska ta(och att bara en ska kunna anfalla åt gången)
    protected static GameObject cardThatTakeDmg; // så att det kortet som anfaller också tar skada

    // Start is called before the first frame update
    public virtual  void Start()
    {
        audioS = gameObject.AddComponent<AudioSource>(); // så kortet får en egen audiosource
        if (!TurnBased.Player1Turn) // ifall det inte är spelare1 runda ska kortet inte tillhöra spelare 1
        {
            _IsPlayer1 = false;
        }
        else// ifall det inte är spelare2 runda ska kortet inte tillhöra spelare 2
        {
            _IsPlayer1 = true;
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (GetComponent<CardFuntion>().IsSelected)
        {

        }

        if (!GetComponent<CardFuntion>().isDown) // ifall kortet är i handen
        {
            if (TurnBased.Player1Turn)  //om det är spelares 1 runda ska han inte se motståndarens kort i handen men jan ska se sina egna
            {
                if (!_IsPlayer1)
                {
                    transform.GetChild(3).gameObject.SetActive(true);
                    transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {
                    transform.GetChild(3).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(true);
                }
            }
            else // om det är spelares 2 runda ska han inte se motståndarens kort i handen men jan ska se sina egna
            {
                if (_IsPlayer1)
                {
                    transform.GetChild(3).gameObject.SetActive(true);
                    transform.GetChild(2).gameObject.SetActive(false);
                }

                else
                {
                    transform.GetChild(3).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(true);
                }
            }

        }

    }



    public void respawn(Vector3 newpos)
    {
        transform.position = newpos;
    }
    /// <summary>
    ///  så att korten kan attackera igen nästa runda samt att man ser sina egna kort men ej fiendens
    /// </summary>

    /// <summary>
    /// så att man kan anfalla spelarna
    /// </summary>
    public virtual void attackHero()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition; // få musens position
            Ray castPoint;
            RaycastHit hit; // om du träffar ett kort du ska välja
            if (TurnBased.Player1Turn && _IsPlayer1 && !_P2HasTaunt) // så att bara spelare 1 kan anfalla spelares 2 "hero"  
            {
                castPoint = Camera.main.ScreenPointToRay(mouse); // musens pos

                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {
                    if (hit.collider.name == "Player2") // om du träffade spelares 2 hero ska spelare 1 ta skada och kortet ska inte kunna anfalla igen
                    {
                        HeroScript.Hero2Health -= _Attack;
                        hasAttackt = true;
                        _amountOfDmg = 0;
                    }
                }
            }
            else if (!TurnBased.Player1Turn && !_IsPlayer1 && !_P1HasTaunt)// så att bara spelare 2 kan anfalla spelares 1 "hero"  
            {
                castPoint = Camera.main.ScreenPointToRay(mouse);// musens pos

                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {
                    if (hit.collider.name == "Player1")// om du träffade spelares 1 hero ska spelare 2 ta skada och kortet ska inte kunna anfalla igen
                    {
                        HeroScript.Hero1Health -= _Attack;
                        hasAttackt = true;
                        _amountOfDmg = 0;
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (!GetComponent<CardFuntion>().isDown)
            hasAttackt = true;
    }

    void OnMouseOver()
    {
        if (!GetComponent<CardFuntion>().isDown)
        {
            GetComponent<SpriteRenderer>().material.color = Color.red;
        }
    }

    void OnMouseExit()
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Amount"></param>
    /// <param name="type">Hp, Ad eller Co</param>
    public void Buff(int Amount, string type)
    {
        if (type == "Ad")
        {
            _Attack += Amount;
        }
        if (type == "Hp")
        {
            _Hp += Amount;
        }
        if (type == "Co")
        {
            _Cost += Amount;
        }
    }


}
