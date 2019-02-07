using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerController : MonoBehaviour {

    public GameObject playerObj;
    public GameObject playerBody;

    public bool playerInRange; //If the player is in the aggro range

    public Quaternion targetRotation;
    public float rotationToFaceSpeed = 3.5f;

    public bool canTarget;
    public bool canRotate;

    public bool isRolling;

    public Vector3 velocity;
	
	// Update is called once per frame
	void Update () {
		if(canRotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationToFaceSpeed * Time.deltaTime); //Smooth rotate to player
        }

        velocity = GetComponent<Rigidbody>().velocity;

        if (isRolling)
        {
            if (Mathf.Abs(GetComponent<Rigidbody>().velocity.x) < 1.2f && Mathf.Abs(GetComponent<Rigidbody>().velocity.y) < 1.2f && Mathf.Abs(GetComponent<Rigidbody>().velocity.z) < 1.2f && canTarget)
                StartCoroutine(canRotateTimer());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Character")
        {
            canTarget = true;
            StartCoroutine(canRotateTimer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Character")
            canTarget = false;
    }

    IEnumerator canRotateTimer()
    {
        isRolling = false;
        canRotate = true;
        targetRotation = Quaternion.LookRotation(playerBody.transform.position - transform.position);
        yield return new WaitForSeconds(1.5f);
        canRotate = false;
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 450);
        isRolling = true;
    }
}
