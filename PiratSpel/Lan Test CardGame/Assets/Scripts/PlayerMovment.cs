using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    Vector3 direction;
    public float Speed;

    Vector3 size;
    void Start()
    {
        size = GetComponent<Renderer>().bounds.extents;


    }


    void FixedUpdate()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        if (direction != Vector3.zero)
            transform.forward = direction;

        transform.position += direction * Speed * Time.deltaTime;

        float maxX = Mathf.Clamp(transform.position.x, LevelScript.MapSizeMin.x + size.x, LevelScript.MapSizeMax.x - size.x);
        float maxZ = Mathf.Clamp(transform.position.z, LevelScript.MapSizeMin.z + size.z, LevelScript.MapSizeMax.z - size.z);

        transform.position = new Vector3(maxX, transform.position.y, maxZ);
    }
}
