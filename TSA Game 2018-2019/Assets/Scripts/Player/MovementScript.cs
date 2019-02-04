using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public int speed;
    public GameObject character;
    public GameObject characterBody;

    public PlayerController pc;

    public void FixedUpdate()
    {
        if(!characterBody.GetComponent<Animator>().GetBool("hasHandOut"))
        {
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                character.transform.Translate(-speed * Input.GetAxis("Vertical") * Time.deltaTime, 0f, speed * Input.GetAxis("Horizontal") * Time.deltaTime);
                if (!characterBody.GetComponent<Animator>().GetBool("isRunning") && !characterBody.GetComponent<Animator>().GetBool("isSlashing"))
                {
                    characterBody.GetComponent<Animator>().SetBool("isRunning", true);
                    characterBody.GetComponent<Animator>().SetBool("isIdle", false);
                    pc.AnimCanSwitch();
                }
                else if(characterBody.GetComponent<Animator>().GetBool("isRunning") && !characterBody.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Run"))
                    pc.AnimCanSwitch();
            }
            else if (characterBody.GetComponent<Animator>().GetBool("isRunning"))
            {
                characterBody.GetComponent<Animator>().SetBool("isRunning", false);
                characterBody.GetComponent<Animator>().SetBool("isIdle", true);
                pc.AnimCanSwitch();
            }
        }
    }
}
