using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

public class PlayerController : MonoBehaviour
{
    //The character object that is a child of this object. Includes the camera as a child and the model itself
    public GameObject character;

    public GameObject characterBody;

    public GameObject playerCamera;
    PostProcessVolume postProcessVolume;
    Bloom bloomLayer = null;
    AmbientOcclusion ambientOcclusionLayer = null;
    ColorGrading colorGradingLayer = null;

    public GameObject dustParticleSystem; //The particle system that spawns 'dust' particles

    public int switchingAnims;

    private void Start()
    {
        //Gets post processing profile attached to camera so values can be changed at runtime
        postProcessVolume = playerCamera.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out bloomLayer);
        postProcessVolume.profile.TryGetSettings(out ambientOcclusionLayer);
        postProcessVolume.profile.TryGetSettings(out colorGradingLayer);
    }

    void Update()
    {
        if (switchingAnims > 0)
        {
            if (switchingAnims > 1)
            {
                characterBody.GetComponent<Animator>().SetBool("canSwitch", false);
                switchingAnims = 0;
            }
            else
                switchingAnims++;
        }
        //Makes sure dust particle system spawns new particles around the player
        var shape = dustParticleSystem.GetComponent<ParticleSystem>().shape;
        shape.position = new Vector3(character.transform.localPosition.x, character.transform.localPosition.y + 2, character.transform.localPosition.z);
    }

    public void enterVault()
    {
        //Sets bloom when entering the vault to be higher
        bloomLayer.intensity.value = 2.5f;
        bloomLayer.threshold.value = 0.5f;

        ambientOcclusionLayer.intensity.value = 0.7f;
    }

    public void leaveVault()
    {
        //Sets bloom when leaving the vault to be lower
        bloomLayer.intensity.value = 2f;
        bloomLayer.threshold.value = 1.1f;

        ambientOcclusionLayer.intensity.value = 0.6f;
    }

    public void animCanSwitch() //When called allows the animator to switch states once. This prevents repeted spamming of an animation's transition
    {
        switchingAnims = 1;
        characterBody.GetComponent<Animator>().SetBool("canSwitch", true);
    }
}