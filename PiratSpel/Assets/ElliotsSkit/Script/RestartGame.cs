using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public GameObject[] Deck1;
    public GameObject[] Deck2;
    bool p1nodeck = true;
    bool p2nodeck = true;

    public void changeMenueScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
