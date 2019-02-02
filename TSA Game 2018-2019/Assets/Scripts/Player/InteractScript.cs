using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for some reason unity and visual studio are being weird, im getting constant bugs, so I'm just gonna write the pseudo code and you can fill it in
public class InteractScript : MonoBehaviour {
    /*
    public GameObject playerCamera;
    public GameObject previousItemLookedAt = null;
    public GameObject itemLookedAt = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        checkForInteraction();
        if(itemLookedAt != null)
        {
            //UI Textobject.text = itemLookedAt.GetComponent<Interactable>().popUpText;
            //UI imageObject.setActive(true); //this would be like an image of an E key
            if(Input.GetKeyDown(KeyCode.E))
            {
                interact();
                itemLookedAt = null;
            }
        }
    }

    void checkForInteraction()
    {
        Ray ray = playerCamera.GetComponent<Camera>().ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo,2f))//if somethign collides with a ray sent out 2 units
        {
            var objectHit = hitInfo.collider.GetComponent<Interactable>();//set a variable to the hit objects ineractable script
            if(objectHit == null)//if the object didnt have the interactable script
            {
                itemLookedAt = null;
            }
            else if(objectHit != null && itemLookedAt != objectHit && itemLookedAt != previousItemLookedAt)//if the object has the interactable script, and is different from what was previously looked at
            {
                itemLookedAt = objectHit;
            }
        }
    }

    void interact()
    {
        itemLookedAt.GetComponent<Interactable>().interact();
    }
    */
}
