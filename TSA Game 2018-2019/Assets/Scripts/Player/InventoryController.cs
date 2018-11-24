using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    public GameObject bow; //The bow object attached to the left hand
    public GameObject playerLeftHand;
    public GameObject playerRightHand;
    public bool bowActive;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
            toggleBow();
	}

    public void toggleBow()
    {
        bowActive = !bowActive;
        bow.SetActive(!bow.activeSelf);
    }
}
