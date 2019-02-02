using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultDoorScript : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "PlayerCharacter" && Input.GetKeyDown(KeyCode.Space))
            other.transform.parent.GetComponent<PlayerController>().enterVault();
    }
}
