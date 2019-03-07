using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class CardFuntion : MonoBehaviour
{
    public bool IsSelected; // ifall du har tryckt på kortet(innan du lägger ut det)

    Vector3 restartpos; // ifall du trycker utanför kortrutorna ska dem komma tillbaka till denna position
    public bool IsPlayer1 = true; // ifall detta kort tillhör spelare 1
    public bool isDown = false; // om du har lagt ner kortet
    bool isOver; // om kortet är över en ruta den kan bli plaserad på

    void Start()
    {
        //om det inte är spelare 1 runda ska kortet inte tillhöra spelare 1 och ska spawna på andra sidan
        if (!TurnBased.Player1Turn)
        {
            IsPlayer1 = false;
            transform.Rotate(90, 0, 180);
        }
        else // om kortet tillhör spelare 1 ska den vara spelare1 kort och spawna på hans sida
        {
            IsPlayer1 = true;
            transform.Rotate(90, 0, 0);
        }

    }

    void Update()
    {
        if (IsSelected) // ifall du har valt vilket kort du ska hålla 
        {
            Vector3 mouse = Input.mousePosition; // få musens position
            Ray castPoint;
            RaycastHit hit; // om du träffar ett kort du ska välja
            if (TurnBased.Player1Turn && IsPlayer1)
            {
                castPoint = Camera.main.ScreenPointToRay(mouse);
                if (!isDown)
                {
                    if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                    {
                        // print(Input.mousePosition);
                        //print(hit.transform.name);
                        Vector3 pos = new Vector3(hit.point.x, 1, hit.point.z);
                        transform.position = pos; // så att kortet du håller har samma position som musen
                    }
                }
                // castPoint = Camera.main.ScreenPointToRay(mouse); // från kamerans vinkel ska musens positions utgå ifrån inte ifråns världen. Annars ger den för stora värden
            }
            else if(!TurnBased.Player1Turn && !IsPlayer1)
            {
                castPoint = Camera.main.ScreenPointToRay(mouse);
                if (!isDown)
                {
                    if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                    {
                        // print(Input.mousePosition);
                        //print(hit.transform.name);
                        Vector3 pos = new Vector3(hit.point.x, 1, hit.point.z);
                        transform.position = pos; // så att kortet du håller har samma position som musen
                    }
                }
                //  castPoint = Camera.main.ScreenPointToRay(mouse); // från kamerans vinkel ska musens positions utgå ifrån inte ifråns världen. Annars ger den för stora värden
            }
            
          
            RaycastHit hitD; // om du träffar en position under kortet
           
            if (Physics.Raycast(transform.position, Vector3.down, out hitD))
            {
                isOver = true; // om du är över en plats du kan lägga kortet

                if (Input.GetMouseButtonDown(0)) // och om du trycker ner vänster musknapp
                {
                    if (hitD.transform.name == "slot1" && IsPlayer1) // kollar ifall det är ett kort som tillhör spelare 1 och en yta som tillhör spelare 1
                    {
                        isDown = true; // så att du sätter ner kortet och inte kan ta upp det igen
                       // print(hitD.transform.name);
                        transform.position = hitD.transform.position; // så du får samma position som den position som är under kortet
                        turnOfHighLigt(); // så att dem inte lyser längre
                    }
                    else if (hitD.transform.name == "slot2" && !IsPlayer1) // kollar så att detta kort tillhör spelare 2 och se till att yta nockdå tillhör spelare 2
                    {
                        isDown = true;// så att du sätter ner kortet och inte kan ta upp det igen
                      //  print(hitD.transform.name);
                        transform.position = hitD.transform.position;// så du får samma position som den position som är under kortet
                        turnOfHighLigt();// så att dem inte lyser längre
                    }
                    else
                        isOver = false; // om du trycker på ett annat kort ska kortet komma tillbaka
                }
            }
            else
                isOver = false; // ifall du trycker utanför en yta där du kan sätta kortet ska det komma till sin start position
        }
    }

    /// <summary>
    /// ständer av ljusen där du kan sätta korten
    /// </summary>
    void turnOfHighLigt()
    {
        if (TurnBased.Player1Turn && IsPlayer1) // se till att du ständer av spelares 1 ljus på spelares 1 runda
        {

            GameObject g = GameObject.FindGameObjectWithTag("Player1slots"); // temporär obj för att komma åt ljusen som deras parent


            for (int i = 0; i < 4; i++) // antal ytor = 4st
            {

                g.transform.GetChild(i).gameObject.SetActive(false); //ständer av dem så du inte kan interagera av misstag med ytorna där du ska sätta korten

            }
        }
        else if (!TurnBased.Player1Turn && !IsPlayer1) // se till att du ständer av spelares 2 ljus på spelares 2 runda
        {


            GameObject g = GameObject.FindGameObjectWithTag("Player2slots");// temporär obj för att komma åt ljusen som deras parent
            {
                for (int i = 0; i < 4; i++)// antal ytor = 4st
                {
                    g.transform.GetChild(i).gameObject.SetActive(false);//ständer av dem så du inte kan interagera av misstag med ytorna där du ska sätta korten
                }
            }
        }
    }

    private void OnMouseDown() // ifall du trykcer på de obj som har detta script
    {
        if (!isOver && !isDown)  // !IsOver = ifall du inte har obj över en yta den ska sättas ut på  
        {                        // IsDown = så att du inte kommer åt obj om du har satt ner det på en tom spel yta

            RaycastHit cardOnTop; // en sak som inte funkar just nu. men jobbar på det

            if (TurnBased.Player1Turn && IsPlayer1) // ifall det är spelares 1 runda och spelares 1 kort
            {
                IsSelected = !IsSelected; // gör så att obj blir vald

                GameObject g = GameObject.FindGameObjectWithTag("Player1slots"); //en temporär obj för att komma åt ljuskällorna

                if (IsSelected) // ifall du har tagit upp ett obj
                {
                    restartpos = transform.position; // sparar obj start position

                    for (int i = 0; i < 4; i++) // en sak som inte funkar just nu. men jobbar på det
                    {
                        Debug.DrawRay(transform.position, Vector3.down * 1, Color.green, 5);// en sak som inte funkar just nu. men jobbar på det

                        if (!Physics.Raycast(transform.position, Vector3.down, out cardOnTop))// en sak som inte funkar just nu. men jobbar på det
                        {
                            g.transform.GetChild(i).gameObject.SetActive(true); // så att alla tomma ytor ska ljus upp
                        }
                        // g.transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
                else // ifall du skulle trycka utanför alla tomma ytor ska du komma tillbaka till samma position som innan och man ska släcka alla ljus
                {
                    transform.position = restartpos; // få tillbaka den gamla positionen
                    for (int i = 0; i < 4; i++) // 4 ljuskällor
                    {
                        g.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
            else if (!TurnBased.Player1Turn && !IsPlayer1) // samma sak som ovanför fast för spelare 2
            {

                IsSelected = !IsSelected; 

                GameObject g = GameObject.FindGameObjectWithTag("Player2slots");

                if (IsSelected)
                {
                    restartpos = transform.position;
                    for (int i = 0; i < 4; i++)
                    {
                        Debug.DrawRay(transform.position, Vector3.down * 1, Color.green, 5);

                        if (!Physics.Raycast(transform.position, Vector3.down, out cardOnTop))
                        {
                            g.transform.GetChild(i).gameObject.SetActive(true);
                        }
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
}
