using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class TempBaseCard2 : MonoBehaviour
{

    protected int _Attack; //bas attack
    protected int _Hp; // bas hp
    public int _Cost; //bas kostnad
    protected bool isPlayer1; // if this card is for player 1
    protected AudioClip _SpawnSound; // när du lägger ut detta kort gör det detta ljud
    protected AudioClip _AttackSound;//  när du attackerar med detta kort gör det detta ljud
    protected AudioClip _DieSound; // när kortet dör så spelar det detta ljud
    AudioSource audioS = new AudioSource(); // så att dem kan spela ljudet

    bool growing = true; //så att den växer och minskar när man har tryckt på ett kort
    bool hasAttackt; // om kortet har redan attackerat(så det bara kan attackera en gng)

    bool attacking; // ifall kortet är i attack läge
    bool playOnce = true; //ifall du har lagt ut kortet ska den inte kunna anfalla och ska spela spawn ljudet
    static int _amountOfDmg; // hur mycket skada fienden ska ta(och att bara en ska kunna anfalla åt gången)
    static GameObject cardThatTakeDmg; // så att det kortet som anfaller också tar skada

    public virtual void Start()
    {
        audioS = gameObject.AddComponent<AudioSource>(); // så kortet får en egen audiosource
        
        if (!TurnBased.Player1Turn) // ifall det inte är spelare1 runda ska kortet inte tillhöra spelare 1
        {
            isPlayer1 = false;
        }
        else// ifall det inte är spelare2 runda ska kortet inte tillhöra spelare 2
        {
            isPlayer1 = true;
        }
    }

    public void respawn(Vector3 NewPos)
    {
        transform.position = NewPos;
    }

    public virtual void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            // så man kan se alla stats utom kostnaden eftersom du redan lagt ut kortet
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
                if (i == 2)
                {
                    transform.GetChild(2).gameObject.SetActive(false);
                }
            }
            else // så man se vad kortet har för slags stats
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

        if (_amountOfDmg > 0 && attacking) // om du ska anfalla så händer detta
        {
            grow(); // så spelaren änklare ser vilket kort som är aktiverat
            attackHero(); // så att man kan anfalla motståndaren "Hero"
        }

        else // om du har anfallt så ska kortet sluta växa och man ska inte kunna anfalla mer
        {
            transform.localScale = new Vector3(1, 1, 1);
            attacking = false;
        }
       
        if (GetComponent<CardFuntion>().isDown)
        {
            RestartCard();
        }
        else // ifall kortet är i handen
        {
            if (TurnBased.Player1Turn)  //om det är spelares 1 runda ska han inte se motståndarens kort i handen men jan ska se sina egna
            {
                if (!isPlayer1)
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
                if (isPlayer1)
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

    /// <summary>
    ///  så att korten kan attackera igen nästa runda samt att man ser sina egna kort men ej fiendens
    /// </summary>
    void RestartCard()
    {
        if (hasAttackt) // så att kortet ändrar färg ifall du la ner kortet eller har anfallt ett kort
        {
            GetComponent<SpriteRenderer>().material.color = Color.grey;
            if (playOnce) // ifall du just la ut kortet så ska den göra ljudet
            {
                if(_SpawnSound != null)
                    audioS.PlayOneShot(_SpawnSound);

                playOnce = false;
            }


        }
        else if (!hasAttackt) // sätt tillbaka färgen
        {
            GetComponent<SpriteRenderer>().material.color = Color.white;
        }

        if (!TurnBased.Player1Turn && isPlayer1) // så att spelares 1 kort kan slå spelares 2 kort men inte sina egna
        {
            transform.GetChild(3).gameObject.SetActive(false);
            attacking = false;
            hasAttackt = false;
        }

        else if (TurnBased.Player1Turn && !isPlayer1)// så att spelares 2 kort kan slå spelares 1 kort men inte sina egna
        {
            transform.GetChild(3).gameObject.SetActive(false);
            attacking = false;
            hasAttackt = false;
        }

    }

    /// <summary>
    /// så att man kan anfalla spelarna
    /// </summary>
    void attackHero()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition; // få musens position
            Ray castPoint;
            RaycastHit hit; // om du träffar ett kort du ska välja
            if (TurnBased.Player1Turn && isPlayer1) // så att bara spelare 1 kan anfalla spelares 2 "hero"
            {
                castPoint = Camera.main.ScreenPointToRay(mouse); // musens pos

                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {
                    if(hit.collider.name == "Player2") // om du träffade spelares 2 hero ska spelare 1 ta skada och kortet ska inte kunna anfalla igen
                    {
                        HeroScript.Hero2Health -= _Attack;
                        hasAttackt = true;
                        _amountOfDmg = 0; 
                    }
                }
            }
            if (!TurnBased.Player1Turn && !isPlayer1)// så att bara spelare 2 kan anfalla spelares 1 "hero"
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

    /// <summary>
    /// så att kortet "stuttsar" ifall du har valt det till att anfalla
    /// </summary>
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

    /// <summary>
    /// om du trycker på obj som har detta script
    /// </summary>
    void OnMouseDown()
    {
        if (GetComponent<CardFuntion>().isDown)
        {
            if (TurnBased.Player1Turn) // om det är spelare 1 runda ska hans kort kunna anfalla och inte skada sina egna
            {
                if (isPlayer1 && !hasAttackt) // kortet tillhör spelare1 och inte har anfallt ska den kunna skada finenden
                {
                    _amountOfDmg = 0; // så att man inte råkar skada sig själv(vet ej varför men blev ett bugg med det och detta löste det)
                    cardThatTakeDmg = gameObject; // så att det kort som anfaller ska ta skada
                    _amountOfDmg = _Attack;
                    hasAttackt = true;
                    attacking = true;
                }

                else if (!isPlayer1 && cardThatTakeDmg.GetComponent<TempBaseCard2>().attacking) // så att spelare 2 kort ska kunna ta skada. samt att dem inte kan skada sig själva
                {
                    TakeDamage(_amountOfDmg); // så att detta kort tar skada
                    _amountOfDmg = _Attack; // så att finde kortet tar lika mycket skada som detta kort ska ge
                    cardThatTakeDmg.GetComponent<TempBaseCard2>().PlayAttackSound();
                    _amountOfDmg = 0; // så nästa kort ej tar skada 

                }

            }
            else
            {
                if (!isPlayer1 && !hasAttackt)
                {
                    _amountOfDmg = 0;
                    cardThatTakeDmg = gameObject;
                    _amountOfDmg = _Attack;
                    hasAttackt = true;
                    attacking = true;
                }
                else if (isPlayer1 && cardThatTakeDmg.GetComponent<TempBaseCard2>().attacking)
                {
                    TakeDamage(_amountOfDmg);
                    _amountOfDmg = _Attack;
                    cardThatTakeDmg.GetComponent<TempBaseCard2>().TakeDamage(_amountOfDmg);
                    _amountOfDmg = 0;
                }
            }

        }
        else
        {
            hasAttackt = true;
        }
    }

    void OnMouseOver()
    {
        if (!GetComponent<CardFuntion>().isDown)
        {
            GetComponent<SpriteRenderer>().material.color = Color.red;
        }
    }
    public virtual void PlayAttackSound()
    {
        if(_AttackSound != null)
            audioS.PlayOneShot(_AttackSound);
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

    public virtual void TakeDamage(int Dmg)
    {
        _Hp -= Dmg;
        _amountOfDmg = 0;
        if (_Hp <= 0)
        {
            if(TurnBased.Player1Turn)
            {
                SpawnCards.CardsP1.Remove(gameObject);
            }
            else
            {
                SpawnCards.CardsP2.Remove(gameObject);
            }
            Destroy(gameObject);
        }

    }

    public virtual void StartingStats(int Ad, int Hp, int Co)
    {
        _Attack = Ad;
        _Hp = Hp;
        _Cost = Co;
    }
}
