using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{

    [SerializeField]
    int damagePoints = 10;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.DealDamage(damagePoints);
        }
        else if (collision.gameObject.CompareTag("Archer"))
        {
            Archer archer = collision.gameObject.GetComponent<Archer>();
            archer.DealDamage(damagePoints);
        }
        else if (collision.gameObject.CompareTag("player_castle"))
        {
            PlayerCastle castle = collision.gameObject.GetComponent<PlayerCastle>();
            castle.DealDamage(damagePoints);
        }
    }
}
