using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultDoorScript : MonoBehaviour {

    public bool playerInRange;

    private void OnTriggerEnter(Collider other) //Enables the interact box showing that the player should press 'Space' to enter the vault
    {
        if (other.gameObject.tag == "Character" && other.transform.parent.GetComponent<PlayerController>().hasCube)
            other.transform.parent.GetComponent<PlayerController>().EnableInteractUI("E");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character" && other.transform.parent.GetComponent<PlayerController>().hasCube)
            other.transform.parent.GetComponent<PlayerController>().DisableInteractUI();
    }

    private void OnTriggerStay(Collider other) //If the player is in the range and presses space, take them to the vault & change some settings (post processing)
    {
        if(other.gameObject.tag == "Character" && Input.GetKeyDown(KeyCode.E) && other.transform.parent.GetComponent<PlayerController>().hasCube)
            other.transform.parent.GetComponent<PlayerController>().EnterVault();
    }
}
