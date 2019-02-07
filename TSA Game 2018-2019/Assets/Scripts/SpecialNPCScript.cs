using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialNPCScript : MonoBehaviour {

    public bool canDoSpecialThing; //If this npc can do it's special thing

    public bool isTownOldMan; //If this npc is the old man that points to the vault door in the town in chapter 2
	
	// Update is called once per frame
	void Update () {
        if(isTownOldMan && GetComponent<NPCController>().currentDialogue == 3 && canDoSpecialThing)
        {
            canDoSpecialThing = false;
            GetComponent<Animator>().Play("Npc3Pointing");
        }
	}
}
