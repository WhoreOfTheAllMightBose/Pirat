using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroScript : MonoBehaviour
{
    public static int Hero1Health;
    public static int Hero2Health;
    public Camera PlayerCamera;
    public Camera TurnWaitCamera;
    public Camera EndingCamera;
    public Text text;
    public Text text2;
    public TextMesh P1Text;
    public TextMesh P2Text;
    public int RestartingGame;
    public int countdown;
    
    void Start()
    {
        Hero1Health = 20;
        Hero2Health = 15;
        EndingCamera.enabled = false;
    }

    private void Death()
    {
        PlayerCamera.enabled = false;
        TurnWaitCamera.enabled = false;
        EndingCamera.enabled = true;
        if (Hero1Health <= 0)
        {
            text.text = "Player 2 Wins!";
            text2.text = "Restarting in  " + 1 * Time.deltaTime;
            RestartingGame++;
        }
        if (Hero2Health <= 0)
        {
            text.text = "Player 1 Wins!";
            text2.text = "Restarting in  " + 1 * Time.deltaTime;
            RestartingGame++;
        }
        if (RestartingGame >= 1200)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //byt namn med scenen vi kommer spela i så retartar man spelet om vis tid
            Hero1Health = 20;
            Hero2Health = 20;
            RestartingGame = 0;
        }
    }

    private void OnMouseDown()
    {
    }

    void Update()
    {
        P1Text.text = "" + Hero1Health;
        P2Text.text = "" + Hero2Health;
        if (Hero1Health <= 0 || Hero2Health <= 0)
        {
            Death();
        }
    }
}
