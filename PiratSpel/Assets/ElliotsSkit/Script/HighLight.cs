using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLight : MonoBehaviour
{
    /* Används inte
    public static HighLight Instance { set; get; }

    public GameObject HighlightPreFab;
    List<GameObject> highlights;

    void Start()
    {
        Instance = this;
        highlights = new List<GameObject>();
    }

    GameObject getHighLightObject()
    {
        GameObject go = highlights.Find(g => !g.activeSelf);

        if (go == null)
        {
            go = Instantiate(HighlightPreFab);
            highlights.Add(go);
        }
        return go;
    }

    public void HighLightAllowedMoves(bool[,] moves)
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (moves[i, j])
                {
                    GameObject go = getHighLightObject();
                    go.SetActive(true);
                    go.transform.position = new Vector3(i + HighlightPreFab.transform.localScale.y, 0, j + HighlightPreFab.transform.localScale.x);
                }
            }
        }

    }

    public void HidehighLigst()
    {
        foreach (GameObject go in highlights)
            go.SetActive(false);
    }
    */
}
