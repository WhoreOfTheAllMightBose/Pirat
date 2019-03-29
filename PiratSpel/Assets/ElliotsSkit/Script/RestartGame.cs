using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public GameObject[] Deck1;
    public GameObject[] Deck2;

    public void changeMenueScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void Player1Deck(string deckname)
    {
        if (deckname == "Monkey")
        {
            GetComponent<SpawnCards>().P1Playingcards = Deck1;
            print("deck 1");
        }

        else if (deckname == "IsLand")
        {
            GetComponent<SpawnCards>().P1Playingcards = Deck2;
            print("deck 2");
        }

      
    }
    public void Player2Deck(string deckname)
    {
        if (deckname == "Monkey")
            GetComponent<SpawnCards>().P2Playingcards = Deck1;

        else if (deckname == "IsLand")
            GetComponent<SpawnCards>().P2Playingcards = Deck2;

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
