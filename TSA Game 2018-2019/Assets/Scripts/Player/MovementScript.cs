using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public int speed;
    public int jumpSpeed;
    public GameObject character;
    public GameObject characterBody;

    public PlayerController pc;

    public void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            character.transform.Translate(-speed * Input.GetAxis("Vertical") * Time.deltaTime, 0f, speed * Input.GetAxis("Horizontal") * Time.deltaTime);
            if (!characterBody.GetComponent<Animator>().GetBool("isRunning"))
            {
                characterBody.GetComponent<Animator>().SetBool("isRunning", true);
                pc.animCanSwitch();
            }
        }
        else if (characterBody.GetComponent<Animator>().GetBool("isRunning"))
        {
            characterBody.GetComponent<Animator>().SetBool("isRunning", false);
            pc.animCanSwitch();
        }
    }
}
