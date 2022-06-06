using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickScript : MonoBehaviour
{
    public float speed, rotateSpeed;
    void Update()
    {
        transform.position += Vector3.up * speed;
        transform.Rotate(0, rotateSpeed, 0);
    }
}
