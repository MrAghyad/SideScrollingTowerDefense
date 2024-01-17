using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCastle : MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    GameObject archerPrefab;

    [SerializeField]
    EnemyCastle enemyCastle;

    [SerializeField]
    int meatCount;

    [SerializeField]
    int woodCount;

    [SerializeField]
    Damageable damageable;

    public int Health { get => damageable.GetHealth(); }

    private void Awake()
    {
        damageable = GetComponent<Damageable>();
    }

    public void Heal(int healingPoints)
    {
        damageable.Heal(healingPoints);
    }

    public void DealDamage(int damagePoints)
    {
        damageable.DealDamage(damagePoints);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newArcherObj = Instantiate(archerPrefab, spawnPoint.position, Quaternion.identity);
            Archer archer = newArcherObj.GetComponentInChildren<Archer>();
            archer.SetEnemyCastle(enemyCastle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player)
            {
                GameResourceUnit resource = player.GetCarriedResource();
                if(resource)
                {
                    if (resource.type == GameResourceType.MEAT)
                        meatCount += resource.value;
                    else if (resource.type == GameResourceType.WOOD)
                        woodCount += resource.value;

                    Destroy(resource.gameObject);
                }
            }    
        }
        
    }
}
