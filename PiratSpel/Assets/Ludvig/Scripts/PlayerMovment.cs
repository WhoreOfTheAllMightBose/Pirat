using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovment : NetworkBehaviour
{
    Vector3 direction;
    public float Speed;

    
    

    void FixedUpdate()
    {
        if (this.isLocalPlayer)
        {
            
            float movement = Input.GetAxis("Horizontal");
            GetComponent<Rigidbody>().velocity = new Vector3(movement * Speed, 0.0f);
        //    direction.x = Input.GetAxis("Horizontal");
        //    direction.z = Input.GetAxis("Vertical");
            

        //    transform.position += direction * Speed * Time.deltaTime;
            
        }

        
    }
}
