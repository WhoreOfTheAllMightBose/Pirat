using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroScript : MonoBehaviour
{
    public int Hero1Health;
    public int Hero2Health;
    public Camera PlayerCamera;
    public Camera TurnWaitCamera;
    public Camera EndingCamera;
    public Text text;
    public Text text2;
    public int RestartingGame;
    
    void Start()
    {
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
        }
    }
    
    void Update()
    {
        if (Hero1Health <= 0 || Hero2Health <= 0)
        {
            Death();
        }
    }
}
