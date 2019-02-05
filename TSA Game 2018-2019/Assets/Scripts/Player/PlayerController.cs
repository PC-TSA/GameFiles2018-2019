using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameController gc;

    //The character object that is a child of this object. Includes the camera as a child and the model itself
    public GameObject character;

    public GameObject characterBody;

    public GameObject playerCamera;

    public GameObject playerCanvasObj;
    public GameObject interactDisplay; //The box on the bottom of the screen that shows which key to press to interact with something
    public GameObject fadeToBlackObj; //The object used to fade to and from a black screen

    PostProcessVolume postProcessVolume;
    Bloom bloomLayer = null;
    AmbientOcclusion ambientOcclusionLayer = null;
    ColorGrading colorGradingLayer = null;

    public GameObject dustParticleSystem; //The particle system that spawns 'dust' particles

    public int switchingAnims;

    public bool isWeaponEquipped;
    public GameObject weapon;

    public bool lockIntoAnim;

    public bool hasCube;

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
            if (switchingAnims > 3)
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

        if (!lockIntoAnim && !characterBody.GetComponent<Animator>().GetBool("hasHandOut"))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isWeaponEquipped = !isWeaponEquipped;
                weapon.SetActive(isWeaponEquipped);
            }

            if (Input.GetMouseButton(0) && !characterBody.GetComponent<Animator>().GetBool("isSlashing"))
            {
                characterBody.GetComponent<Animator>().SetBool("isSlashing", true);
                characterBody.GetComponent<Animator>().SetBool("isIdle", false);
                characterBody.GetComponent<Animator>().SetBool("isRunning", false);
                AnimCanSwitch();
            }
            /*if(Input.GetKeyDown(KeyCode.E))
            {
                characterBody.GetComponent<Animator>().SetBool("hasHandOut", true);
                characterBody.GetComponent<Animator>().SetBool("isIdle", false);
                AnimCanSwitch();
            }*/

            if (characterBody.GetComponent<Animator>().GetBool("isSlashing") && characterBody.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f && characterBody.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Slash")) //If the slashing anim is done playing
            {
                characterBody.GetComponent<Animator>().SetBool("isIdle", true);
                characterBody.GetComponent<Animator>().SetBool("isSlashing", false);
                AnimCanSwitch();
            }

            if (characterBody.GetComponent<Animator>().GetBool("isRunning") && !characterBody.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Run"))
                AnimCanSwitch();
            if (characterBody.GetComponent<Animator>().GetBool("isSlashing") && !characterBody.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Slash"))
                AnimCanSwitch();
        }
    }

    public void EnterVault()
    {
        StartCoroutine(FadeToBlackTP(gc.vaultObj.GetComponent<VaultController>().teleportPoint.transform.position, gc.vaultObj, true));
        //Sets bloom when entering the vault to be higher
        bloomLayer.intensity.value = 2.5f;
        bloomLayer.threshold.value = 0.5f;
        ambientOcclusionLayer.intensity.value = 0.7f;

        gc.GetComponent<AudioSource>().clip = gc.spookyTheme;
        gc.GetComponent<AudioSource>().Play();
    }

    public void EnterSanctuary()
    {
        StartCoroutine(FadeToBlackTP(gc.sanctuaryObj.GetComponent<SanctuaryController>().teleportPoint.transform.position, gc.sanctuaryObj, gc.vaultObj)); //Fades to black, TPs player to sanctuary, enables sanctuary, disables vault
    }

    public void LeaveSanctuary()
    {
        //Sets bloom when leaving the sanctuary to be lower
        bloomLayer.intensity.value = 2f;
        bloomLayer.threshold.value = 1.1f;

        ambientOcclusionLayer.intensity.value = 0.6f;

        //If they haven't 'beaten the game' (gotten through the sanctuary) before, show credits
        if (gc.haveBeatenGame)
            StartCoroutine(FadeToBlackTP(gc.townObj.GetComponent<TownController>().teleportPoint.transform.position, gc.vaultObj, false));
        else
        {
            //Show credits code here
            gc.haveBeatenGame = true;
            StartCoroutine(FadeToBlackTP(gc.townObj.GetComponent<TownController>().teleportPoint.transform.position, gc.vaultObj, false));
        }
    }

    public void AnimCanSwitch() //When called allows the animator to switch states once. This prevents repeted spamming of an animation's transition
    {
        switchingAnims = 1;
        characterBody.GetComponent<Animator>().SetBool("canSwitch", true);
    }

    public void EnableInteractUI(string text) //Enables the box on the bottom of the screen showing which key to press to interact with something
    {
        interactDisplay.SetActive(true);
        interactDisplay.transform.GetChild(0).GetComponent<Text>().text = text;
    }

    public void DisableInteractUI() //Disables the box on the bottom of the screen showing which key to press to interact with something
    {
        interactDisplay.SetActive(false);
    }

    IEnumerator FadeToBlackTP(Vector3 coords) //Teleports the player to these coordinates once the fade to black has finished
    {
        fadeToBlackObj.GetComponent<Animator>().Play("FadeToBlack");
        yield return new WaitForSeconds(1);
        transform.position = coords;
    }

    IEnumerator FadeToBlackTP(Vector3 coords, GameObject obj, bool status) //Teleports the player to these coordinates once the fade to black has finished; will also disable/enable a gameobject when on the black screen
    {
        fadeToBlackObj.GetComponent<Animator>().Play("FadeToBlack");
        yield return new WaitForSeconds(1);
        obj.SetActive(status);
        DisableInteractUI();
        transform.GetChild(0).position = coords;
    }

    IEnumerator FadeToBlackTP(Vector3 coords, GameObject objTrue, GameObject objFalse) //Teleports the player to these coordinates once the fade to black has finished; will also enable/disable objTrue/objFalse respectively
    {
        fadeToBlackObj.GetComponent<Animator>().Play("FadeToBlack");
        yield return new WaitForSeconds(1);
        objTrue.SetActive(true);
        objFalse.SetActive(false);
        DisableInteractUI();
        transform.GetChild(0).position = coords;
    }
}