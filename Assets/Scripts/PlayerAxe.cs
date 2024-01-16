using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxe : MonoBehaviour
{
    public int damagePoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("resource"))
        {
            GameResource chopped_resource = collision.GetComponent<GameResource>();
            chopped_resource.DealDamage(damagePoints, chopped_resource.transform.position.x < transform.parent.position.x ? DamageDirection.LEFT : DamageDirection.RIGHT);
        }
    }
}
