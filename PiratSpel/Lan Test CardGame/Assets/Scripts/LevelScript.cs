using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public static Vector3 MapSizeMax;
    public static Vector3 MapSizeMin;

    void Start()
    {
        MapSizeMax = transform.position + GetComponent<Renderer>().bounds.extents;
        MapSizeMin = transform.position - GetComponent<Renderer>().bounds.extents;


    }

    void Update()
    {



    }
}
