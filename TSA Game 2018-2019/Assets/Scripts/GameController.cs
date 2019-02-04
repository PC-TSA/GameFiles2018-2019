using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public bool haveBeatenGame;

    public AudioClip shortMainTheme; //Used in the second cutscene, where the player reaches for the cube

    public GameObject vaultObj; //The entire first vault object; is disabled by default, enabled when enterred from PlayerController's EnterVault()
    public GameObject sanctuaryObj; //^
    public GameObject townObj;

	// Use this for initialization
	void Start () {
        QualitySettings.vSyncCount = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        if(focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
