using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHammer : MonoBehaviour
{
    public int healingPoints = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player_castle"))
        {
            PlayerCastle playerCastle = collision.gameObject.GetComponent<PlayerCastle>();
            if (playerCastle)
            {
                playerCastle.Heal(healingPoints);
            }
        }
    }
}
