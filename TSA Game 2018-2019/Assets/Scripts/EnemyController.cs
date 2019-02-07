using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public GameObject playerBody;

    public GameObject healthBar;

    public int damage = 3;
    public int health = 15;

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
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
            Destroy(transform.parent);
        healthBar.GetComponent<Slider>().value = health;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Character")
        {
            collision.transform.parent.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
