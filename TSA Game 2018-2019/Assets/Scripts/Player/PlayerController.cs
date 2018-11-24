using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject gameController;
    public GameObject playerCamera;

    //Movement & Rotation
    public float mouseSensitivity = 100f;
    public float xClamp; //How far up/down you can look
    public float rotY; // rotation around the up/y axis
    public float rotX; // rotation around the right/x axis

    void Start () {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }
	
	// Update is called once per frame
	void Update () {
        movementAndRotation();
    }

    public void movementAndRotation()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        transform.Translate(x, 0, 0);
        transform.Translate(0, 0, z);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -xClamp, xClamp);

        Quaternion headRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        Quaternion bodyRotation = Quaternion.Euler(transform.rotation.x, rotY, 0.0f);
        playerCamera.transform.rotation = headRotation;
        transform.rotation = bodyRotation;
    }
}
