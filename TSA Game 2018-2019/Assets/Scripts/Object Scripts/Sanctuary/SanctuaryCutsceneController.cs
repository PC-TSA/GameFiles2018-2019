using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SanctuaryCutsceneController : MonoBehaviour {

    public GameController gc;
    public PlayableAsset sanctuaryCutscene;
    public bool cutsceneBeingPlayed;
    public GameObject characterObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character") //Child of main player object, has the collider
        {
            characterObj = other.gameObject;
            cutsceneBeingPlayed = true;

            other.transform.parent.GetComponent<PlayableDirector>().playableAsset = sanctuaryCutscene;
            other.transform.parent.GetComponent<PlayableDirector>().Play();

            gc.GetComponent<AudioSource>().clip = gc.shortMainTheme;
            gc.GetComponent<AudioSource>().Play();
        }
    }
}
