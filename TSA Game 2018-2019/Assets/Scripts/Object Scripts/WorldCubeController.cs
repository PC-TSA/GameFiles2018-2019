using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WorldCubeController : MonoBehaviour { //Used to control the 'world cube', essentially the cube that is floating in the crator before you get it

    public GameController gc;
    public PlayableAsset cubeCutscene;
    public bool cutsceneBeingPlayed;
    public GameObject characterObj;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Character") //Child of main player object, has the collider
        {
            characterObj = other.gameObject;
            cutsceneBeingPlayed = true;

            other.transform.parent.GetComponent<PlayableDirector>().playableAsset = cubeCutscene;
            other.transform.parent.GetComponent<PlayableDirector>().Play();
            other.transform.parent.GetComponent<PlayerController>().hasCube = true;

            gc.GetComponent<AudioSource>().clip = gc.shortMainTheme;
            gc.GetComponent<AudioSource>().Play();
        }
    }

    public void Update()
    {
        if(cutsceneBeingPlayed && characterObj.transform.parent.GetComponent<PlayableDirector>().time > 12)
        {
            Destroy(gameObject);
        }
    }
}
