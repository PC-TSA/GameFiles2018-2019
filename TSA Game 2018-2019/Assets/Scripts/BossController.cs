using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour {

    public GameObject playerBody;

    public GameObject healthBar;

    public int damage;
    public int health;
    public float speed;

    public bool canRotate;

    public float rotationToFaceSpeed;

    public Quaternion targetRotation;

    public bool playerInRange;

    public float attackRange; //How close the player needs to be for the boss to stop chasing and attempt a swing
    public bool isSlashing;

    private void Start()
    {
        healthBar.GetComponent<Slider>().maxValue = health;
        healthBar.GetComponent<Slider>().value = health;
    }

    public void Update()
    {
        //Health Bar
        Vector3 v = playerBody.transform.position - healthBar.transform.position;
        v.x = v.z = 0.0f;
        healthBar.transform.LookAt(playerBody.transform.position - v);
        healthBar.transform.Rotate(0, 180, 0);
        healthBar.transform.position = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);

        if(playerInRange && !isSlashing)
        {
            targetRotation = Quaternion.LookRotation(playerBody.transform.position - transform.position);
            if (canRotate)
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationToFaceSpeed * Time.deltaTime); //Smooth rotate to player

            GetComponent<Rigidbody>().velocity = transform.forward * speed;

            if (Vector3.Distance(playerBody.transform.position, transform.position) < attackRange) //Attack
            {
                GetComponent<Animator>().Play("Slash");
                StartCoroutine(SlashingTimer());
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
            Destroy(gameObject);
        healthBar.GetComponent<Slider>().value = health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            canRotate = true;
            playerInRange = true;
            GetComponent<Animator>().Play("Run");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Character")
        {
            playerInRange = false;
        }
    }

    IEnumerator SlashingTimer()
    {
        isSlashing = true;
        yield return new WaitForSeconds(2);
        isSlashing = false;
        GetComponent<Animator>().Play("Run");
    }
}
