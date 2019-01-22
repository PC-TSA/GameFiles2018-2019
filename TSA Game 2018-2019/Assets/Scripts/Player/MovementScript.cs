using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public int speed;
    public int jumpSpeed;

    public void Start()
    {
        
    }

    public void FixedUpdate()
    {
        transform.Translate(speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, speed * Input.GetAxis("Vertical") * Time.deltaTime);
    }
}
