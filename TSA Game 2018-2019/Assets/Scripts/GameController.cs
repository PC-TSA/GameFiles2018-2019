using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public bool haveBeatenGame;

    public GameObject playerObj;

    public AudioClip shortMainTheme; //Used in the second cutscene, where the player reaches for the cube
    public AudioClip vaultTheme;
    public AudioClip spookyTheme;

    public GameObject vaultObj; //The entire first vault object; is disabled by default, enabled when enterred from PlayerController's EnterVault()
    public GameObject sanctuaryObj; //^
    public GameObject townObj;

    public GameObject startButton;
    public GameObject mainMenuText;

    public GameObject displayBox;
    public Text displayBoxText;
    public GameObject displayBoxDialogueObjects;
    public GameObject displayBoxInteractionObjects;

    public ParticleSystem meteorParticle;
    public ParticleSystem meteorExplosionParticle;

    public GameObject npcs;

	// Use this for initialization
	void Start () {
        QualitySettings.vSyncCount = 1;
        Cursor.visible = true;
    }

    public void startGame()
    {
        playerObj.GetComponent<PlayableDirector>().Play();

        Color temp = mainMenuText.GetComponent<Image>().color;
        temp.a = 0;
        mainMenuText.GetComponent<Image>().color = temp;

        startButton.SetActive(false);
        meteorParticle.Play();
        meteorExplosionParticle.Play();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
