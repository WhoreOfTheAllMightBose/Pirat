using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    GameObject[] Deck1;
    GameObject[] Deck2;
    public void changeMenueScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void PlayerDeck(int player,string deckname)
    {
        if(player == 1)
        {
            if (deckname == "Monkey")
                GetComponent<SpawnCards>().P1Playingcards = Deck1;

            else if (deckname == "IsLand")
                GetComponent<SpawnCards>().P1Playingcards = Deck2;
        }
        if (player == 2)
        {
            if (deckname == "Monkey")
                GetComponent<SpawnCards>().P2Playingcards = Deck1;

            else if (deckname == "IsLand")
                GetComponent<SpawnCards>().P2Playingcards = Deck2;
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
