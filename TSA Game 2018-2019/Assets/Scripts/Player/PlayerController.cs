using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject character;
    public GameObject dustParticleSystem; //The particle system that spawns 'dust' particles

    private void Start()
    {
        
    }

    void Update()
    {
        var shape = dustParticleSystem.GetComponent<ParticleSystem>().shape;
        shape.position = new Vector3(character.transform.localPosition.x, character.transform.localPosition.y + 2, character.transform.localPosition.z);
    }
}