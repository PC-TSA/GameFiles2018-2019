using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour {

    public ParticleSystem particles;
	
	// Update is called once per frame
	void Update () {
        var shape = particles.shape;
        shape.position = transform.position;
    }
}
