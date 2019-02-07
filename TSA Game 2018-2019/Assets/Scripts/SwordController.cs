using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {

    public int damage;
    public bool canDamage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && other.GetType() == typeof(SphereCollider) && canDamage)
        {
            other.GetComponent<EnemyController>().TakeDamage(damage);
            StartCoroutine(DamageCooldown());
        }
        else if(other.tag == "Boss" && other.GetType() == typeof(BoxCollider) && canDamage)
        {
            other.GetComponent<BossController>().TakeDamage(damage);
            StartCoroutine(DamageCooldown());
        }
    }

    IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(1.5f);
        canDamage = true;
    }
}
