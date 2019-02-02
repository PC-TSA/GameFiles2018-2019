using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for some reason unity and visual studio are being weird, im getting constant bugs, so I'm just gonna write the pseudo code and you can fill it in
public class Interactable : MonoBehaviour {
   /* public string popUpText;//message to display when this object is being interacted with by the player

    public GameObject teleportLocation;//set in inspector to teleport to if this item is a door

    public List<string> dialogueList;//list of dialogue lines to be iterated through
    public void interact()
    {
        if(gameObject.GetTag() == "item")
        {
            addToInventory();
        }
        else if (gameObject.GetTag() == "door")
        {
            //fadetoblack()
            //teleport player to  teleportLocation
        }
        else if (gameObject.GetTag() == "NPC")
        {
            startDialogue();
        }
    }

    void addToInventory()
    {

    }

    IEnumerator dialogue()
    {
        int index = 0;//for iteration through the list of dialogue strings
        if(Input.GetKeyDown(KeyCode.E))
        {
            /*if(index < dialogueList.length)
            {
                UI textObject.text = dialogueList.get(index)
                UI dialogueBackgroundObject.setActive(true);
                index++;
            }
            else
            {
                 UI textObject.text = null;
                 UI dialogueBackgroundObject.setActive(false);
                 yield return null;
            }
            
        }

    }
        */
}
