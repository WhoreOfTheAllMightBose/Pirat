using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBaseCard : BaseCard
{
 //   protected AudioClip _SpawnSound; // när du lägger ut detta kort gör det detta ljud
    protected AudioClip _AttackSound;//  när du attackerar med detta kort gör det detta ljud
    protected AudioClip _DieSound; // när kortet dör så spelar det detta ljud
  //  AudioSource audioS = new AudioSource(); // så att dem kan spela ljudet

    bool growing = true; //så att den växer och minskar när man har tryckt på ett kort
    bool hasAttackt; // om kortet har redan attackerat(så det bara kan attackera en gng)

    bool attacking; // ifall kortet är i attack läge
    bool playOnce = true; //ifall du har lagt ut kortet ska den inte kunna anfalla och ska spela spawn ljudet
    // Start is called before the first frame update

    // Update is called once per frame
    public override void  Update()
    {
        seeStats();
      
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
            RestartCard();

        base.Update();
    }

    void RestartCard()
    {
        if (hasAttackt) // så att kortet ändrar färg ifall du la ner kortet eller har anfallt ett kort
        {
            GetComponent<SpriteRenderer>().material.color = Color.grey;
            if (playOnce) // ifall du just la ut kortet så ska den göra ljudet
            {
                if (_SpawnSound != null)
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
            print("kom in hit");
            transform.GetChild(3).gameObject.SetActive(false);
            attacking = false;
            hasAttackt = false;
        }

        else if (TurnBased.Player1Turn && !isPlayer1)// så att spelares 2 kort kan slå spelares 1 kort men inte sina egna
        {
            print("kom in hit");
            transform.GetChild(3).gameObject.SetActive(false);
            attacking = false;
            hasAttackt = false;
        }

    }

    void seeStats()
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

    private void OnMouseDown()
    {
         fight();
    }

    void fight()
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
                else if (!isPlayer1 && cardThatTakeDmg.GetComponent<MinionBaseCard>().attacking) // så att spelare 2 kort ska kunna ta skada. samt att dem inte kan skada sig själva
                {
                    TakeDamage(_amountOfDmg); // så att detta kort tar skada
                    _amountOfDmg = _Attack; // så att finde kortet tar lika mycket skada som detta kort ska ge
                    cardThatTakeDmg.GetComponent<MinionBaseCard>().TakeDamage(_amountOfDmg);
                    cardThatTakeDmg.GetComponent<MinionBaseCard>().PlayAttackSound();
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
                else if (isPlayer1 && cardThatTakeDmg.GetComponent<MinionBaseCard>().attacking)
                {
                    TakeDamage(_amountOfDmg);
                    _amountOfDmg = _Attack;
                    cardThatTakeDmg.GetComponent<MinionBaseCard>().TakeDamage(_amountOfDmg);
                    cardThatTakeDmg.GetComponent<MinionBaseCard>().PlayAttackSound();
                    _amountOfDmg = 0;
                }
            }

        }
        else
        {
            hasAttackt = true;
        }
    }

    public virtual void PlayAttackSound()
    {
        if (_AttackSound != null)
            audioS.PlayOneShot(_AttackSound);
    }

    public void TakeDamage(int Dmg)
    {
        _Hp -= Dmg;
        _amountOfDmg = 0;
        if (_Hp <= 0)
        {
            if (TurnBased.Player1Turn)
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
