using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwordController : MonoBehaviour {

    public int damage;
    public bool canDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character" && canDamage)
        {
            other.transform.parent.GetComponent<PlayerController>().TakeDamage(damage);
            StartCoroutine(DamageCooldown());
        }
    }

    IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(1);
        canDamage = true;
    }
}
