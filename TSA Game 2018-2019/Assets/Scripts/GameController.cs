using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /*private void OnApplicationFocus(bool focus)
    {
        if(focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }*/
}
