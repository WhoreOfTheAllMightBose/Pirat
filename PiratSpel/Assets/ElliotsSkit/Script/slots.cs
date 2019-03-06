using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slots : MonoBehaviour
{
    public bool isFree = true;
    private void Update()
    {
        if(!isFree)
        {
            transform.GetComponent<Collider>().enabled = !transform.GetComponent<Collider>().enabled;
        }
    }
}
