using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject NetworkEverything;

    // Use this for initialization
    void Start()
    {
        NetworkEverything = GameObject.FindGameObjectWithTag("LookAtThis");
    }
    
    

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(NetworkEverything.transform.position);
    }
}
