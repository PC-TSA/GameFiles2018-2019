using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    public List<string> dialogue;
    public int currentDialogue;

    public bool isTalkingToPlayer;
    public bool playerInRange;

    public GameObject playerObj;
    public GameObject playerBody;
    public GameObject cameraObj;

    public GameController gc;

    //Smooth rotate to face player vars
    public int rotationSpeed;

    public Quaternion startRotation;
    public Quaternion targetRotation;

	// Use this for initialization
	void Start () {
        startRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if(playerInRange && !isTalkingToPlayer)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                talkToNpc();
            }
        }

        if (isTalkingToPlayer)
        {
            if(transform.rotation != targetRotation)
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); //Smooth rotate to player

            if(Input.GetKeyDown(KeyCode.Space))
                nextDialogue();
            if (Input.GetKeyDown(KeyCode.Escape))
                stopTalkingToNpc();
        }
        else if (transform.rotation != startRotation)
            transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, rotationSpeed * Time.deltaTime); //Smooth rotate to initial rotation
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Character")
        {
            gc.displayBox.SetActive(true);
            gc.displayBoxText.text = "E";
            gc.displayBoxInteractionObjects.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Character")
        {
            gc.displayBox.SetActive(false);
            playerInRange = false;
        }
    }

    public void talkToNpc()
    {
        if(!isTalkingToPlayer)
        {
            targetRotation = Quaternion.LookRotation(playerBody.transform.position - transform.position);
            isTalkingToPlayer = true;
            gc.displayBoxText.text = dialogue[0];
            gc.displayBoxDialogueObjects.SetActive(true);
            gc.displayBoxInteractionObjects.SetActive(false);
            playerObj.GetComponent<MovementScript>().enabled = false;
            cameraObj.GetComponent<CameraController>().enabled = false;

            if (GetComponent<SpecialNPCScript>() != null)
                GetComponent<SpecialNPCScript>().canDoSpecialThing = true; //Allows special npcs to do their special thing again
        }
    }

    public void stopTalkingToNpc()
    {
        gc.displayBoxDialogueObjects.SetActive(false);
        gc.displayBox.SetActive(false);
        playerObj.GetComponent<MovementScript>().enabled = true;
        cameraObj.GetComponent<CameraController>().enabled = true;
        currentDialogue = 0;
        StartCoroutine(talkToNpcCooldown());
    }

    public void nextDialogue()
    {
        if(currentDialogue + 1 >= dialogue.Count)
        {
            //If the current dialogue is the last one, fade box out and give back control
            stopTalkingToNpc();
        }
        else
        {
            currentDialogue++;
            gc.displayBoxText.text = dialogue[currentDialogue];
        }
    }

    IEnumerator talkToNpcCooldown()
    {
        yield return new WaitForSeconds(1f);
        isTalkingToPlayer = false;
    }
}
