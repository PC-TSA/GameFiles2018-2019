using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public int speed;
    public int jumpSpeed;
    public GameObject character;

    public void FixedUpdate()
    {
        character.transform.Translate(-speed * Input.GetAxis("Vertical") * Time.deltaTime, 0f, speed * Input.GetAxis("Horizontal") * Time.deltaTime);
    }
}
